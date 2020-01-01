using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Nikse.SubtitleEdit.Core.ContainerFormats.Matroska;
using NonInvasiveKeyboardHookLibrary;
using YandexLinguistics.NET;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;
using SystemColors = System.Drawing.SystemColors;

namespace BilingualSubtitler
{
    public partial class MainForm : Form
    {
        enum VideoState
        {
            PlayingWithOriginalSubtitles,
            PausedWithBilingualSubtitles
        }

        private Subtitle[] m_originalSubtitles;
        private Subtitle[] m_firstRussianSubtitles;
        private Subtitle[] m_secondRussianSubtitles;
        private Subtitle[] m_thirdRussianSubtitles;

        private Translator m_translator;
        private KeyboardHookManager m_keyboardHookManager = new KeyboardHookManager();
        private InputSimulator m_inputSimulator;
        private VideoState m_videoState = VideoState.PlayingWithOriginalSubtitles;



        private string mkvtoolnixOutput;
        private string formTitle = "Создание двуязычных субтитров";
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        private List<Button> buttons;
        private List<Button> buttonsForSaving;
        private List<Label> labels;
        private int subsLines = 0;
        private bool flagSubsAreTranslated = false;
        private bool flagSubsAreSaved = false;
        private bool flagSubsAreExtractingSuccessfully = false;
        private bool flagItIsAlreadyBilingualSubtitles = false;
        private bool disableCheckboxSubsInOneLine;
        private bool disableCheckboxRemoveStylesFromSecondarySubs;
        private Color tempCurrentPrimarySubtitlesColor;
        private Color tempCurrentSecondarySubtitlesColor;
        private Color previousButtonColor;
        public event SubtitlesAreNotTranslatedHandler SubtitlesAreNotTranslated;
        public event SubtitlesAreTranslatedHandler SubtitlesAreTranslated;

        public delegate void SubtitlesAreNotTranslatedHandler(object sender, EventArgs eventArgs);
        public delegate void SubtitlesAreTranslatedHandler(object sender, EventArgs eventArgs);

        public MainForm()
        {
            openFileDialog.Title = "Выберите файл";
            openFileDialog.Filter = "Файлы субтитров SubRip|*.srt";

            //saveFileDialog.InitialDirectory = GO.AppFolders.Temporary;
            saveFileDialog.Filter = "Файлы субтитров SubRip|*.srt";
            //saveFileDialog.DefaultExt = GO.Constants.NewsExtension;
            saveFileDialog.AddExtension = true;

            InitializeComponent();

            backgroundWorkerTranslation.WorkerReportsProgress = true;
            backgroundWorkerTranslation.WorkerSupportsCancellation = true;

            backgroundWorkerWriteSubsToDataGrid.WorkerReportsProgress = true;
            backgroundWorkerWriteSubsToDataGrid.WorkerSupportsCancellation = true;

            m_keyboardHookManager.Start();
            m_inputSimulator = new InputSimulator();

            m_keyboardHookManager.RegisterHotkey(
                (int)VirtualKeyCode.SPACE,
                () =>
                {
                    switch (m_videoState)
                    {
                        case VideoState.PlayingWithOriginalSubtitles:
                            {
                                m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_S);
                                m_videoState = VideoState.PausedWithBilingualSubtitles;
                                break;
                            }
                        case VideoState.PausedWithBilingualSubtitles:
                            {
                                m_inputSimulator.Keyboard.ModifiedKeyStroke(
                                    VirtualKeyCode.SHIFT,
                                    VirtualKeyCode.VK_S);
                                m_videoState = VideoState.PlayingWithOriginalSubtitles;
                                break;
                            }

                    }
                });

            // Register virtual key code 0x60 = NumPad0
            m_keyboardHookManager.RegisterHotkey(
                 (int)VirtualKeyCode.UP,
                 () =>
                 {
                     m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                 });
            m_keyboardHookManager.RegisterHotkey(
                (int)VirtualKeyCode.DOWN,
                () =>
                {
                    m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                });
            m_keyboardHookManager.RegisterHotkey(
                (int)VirtualKeyCode.LEFT,
                () =>
                {
                    m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                });
            m_keyboardHookManager.RegisterHotkey(
                (int)VirtualKeyCode.RIGHT,
                () =>
                {
                    m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
                });

        }

        private Subtitle[] ReadSubtitles(string filePath)
        {
            Subtitle[] subtitles = null;
            var sourceFileFI = new FileInfo(filePath);
            var extension = sourceFileFI.Extension;

            m_translator = new Translator(Properties.Settings.Default.YandexTranslatorAPIKey);

            switch (extension)
            {
                case ".srt":
                    {
                        subtitles = ReadSRT(filePath);

                        break;
                    }
                case ".mkv":
                    {
                        var mkvFile = new MatroskaFile(filePath);
                        var tracks = mkvFile.GetTracks(true);

                        // Вызов формы для выбора трека субтитров
                        using var trackSelectionForm = new TrackToExtractFromMKVForm(tracks);
                        var dialogResult = trackSelectionForm.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            var mkvTrackInfo =
                                tracks.Find(x => x.TrackNumber == trackSelectionForm.SelectedTrackNumber);
                            var mkvSubtitles = mkvFile.GetSubtitle(trackSelectionForm.SelectedTrackNumber,
                                (position, total) => { });

                            subtitles = new Subtitle[mkvSubtitles.Count];
                            for (int i = 0; i < mkvSubtitles.Count; i++)
                            {
                                var currentMkvSubtitle = mkvSubtitles[i];

                                subtitles[i] = new Subtitle(currentMkvSubtitle.Start,
                                    currentMkvSubtitle.End,
                                    currentMkvSubtitle.GetText(mkvTrackInfo));
                            }
                        }

                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }

            return subtitles;
        }

        private Subtitle[] ReadSRT(string pathToSRTFile)
        {
            subsLines = 0;
            string[] readedLines = File.ReadAllLines(pathToSRTFile); //Читаем из файла субтитры

            foreach (string line in readedLines)
            {
                if (line.Contains("-->"))
                    subsLines++;
            }

            var subtitles = new Subtitle[subsLines];
            int currentSubtitleIndex = 0;
            for (int i = 0; i < readedLines.Length - 1; i++)
            {
                if (readedLines[i].Contains("-->"))
                {
                    subtitles[currentSubtitleIndex] = new Subtitle(
                        readedLines[i],
                        (readedLines[i + 1]));

                    i += 2;

                    while ((i < readedLines.Length) && (!string.IsNullOrWhiteSpace(readedLines[i])))
                    {
                        subtitles[currentSubtitleIndex].Text += $"\n{readedLines[i]}";

                        i++;
                    }

                    currentSubtitleIndex++;
                }
            }

            return subtitles;
        }

        private StringBuilder GenerateASSMarkedupDocument(Tuple<Subtitle[], Color>[] subtitlesColorPairs)
        {
            var assSB = new StringBuilder();

            //[Script Info]
            //; This is an Advanced Sub Station Alpha v4+script.
            //    Title: 
            //ScriptType: v4.00 +
            //    Collisions: Normal
            //PlayDepth: 0

            //    [V4 + Styles]
            //Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
            //    [Events]
            assSB.Append(
                "[Script Info]\r\n" +
                "; This is an Advanced Sub Station Alpha v4+ script.\r\n" +
                "Title: \r\n" +
                "ScriptType: v4.00+\r\n" +
                "Collisions: Normal\r\n" +
                "PlayDepth: 0\r\n" +
                "\r\n" +
                "[V4+ Styles]\r\n" +
                "Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, " +
                "Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, " +
                "MarginL, MarginR, MarginV, Encoding\r\n");

            //Style: Default,Arial,20,&H00FFFFFF,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,10,1
            //Style: Копировать из Default,Arial,20,&H00C26F03,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,55,1
            //Style: Копировать из Копировать из Default,Arial,20,&H000C15DC,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,100,1
            var subtitleStyleNamePostfix = " sub stream";

            for (int i = 0; i < subtitlesColorPairs.Length; i++)
            {
                var color = subtitlesColorPairs[i].Item2;

                assSB.AppendLine(
                    $"Style: {i}{subtitleStyleNamePostfix}," +
                    $"Arial," +
                    $"20," +
                    $"&H00" +
                    $"{color.B.ToString("X2")}" +
                    $"{color.G.ToString("X2")}" +
                    $"{color.R.ToString("X2")}," +
                    $"&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10," +
                    $"{20 + i*(2*20 + 5)}," +
                    $"1");
            }

            //    [Events]
            //Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text
            assSB.Append("[Events]\r\n" +
                         "Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text\r\n");

            // Dialogue: 0,0:01:25.29,0:01:28.52,Копировать из Копировать из Default,,0,0,0,,Эй! Сюда! Тут человек!
            var assTimeFormat = @"h\:mm\:ss\.ff";
            for (int i = 0; i < subtitlesColorPairs.Length; i++)
            {
                var subtitles = subtitlesColorPairs[i].Item1;
                foreach (var subtitle in subtitles)
                {
                    // Перенос
                    if (subtitle.Text.Contains("\n"))
                        subtitle.Text = subtitle.Text.Replace("\n", "\\N");


                    assSB.AppendLine($"Dialogue: 0," +
                                     $"{subtitle.Start.ToString(assTimeFormat)}," +
                                     $"{subtitle.End.ToString(assTimeFormat)}," +
                                     $"{i}{subtitleStyleNamePostfix}," +
                                     $",0,0,0,," +
                                     $"{subtitle.Text}");
                }
            }

            return assSB;
        }

        private Subtitle[] YandextranslateSubtitles(Subtitle[] originalSubtitles)
        {
            var rusSubtitles = new Subtitle[originalSubtitles.Length];

            for (int i = 0; i < originalSubtitles.Length; i++)
            {
                rusSubtitles[i] = new Subtitle
                    (originalSubtitles[i].Start,
                    originalSubtitles[i].End,
                    YandexTranslateAStringWithChecking(originalSubtitles[i].Text, m_translator)
                    );
            }

            return rusSubtitles;
        }

        private string YandexTranslateAStringWithChecking(string originalStr, Translator translator)
        {
            string output = "";
            string tempStr = originalStr;
            int countOfTags = originalStr.Split('<').Length - 1;
            int[,] tagsIndexes = new int[2, countOfTags];
            string[] tags = new string[countOfTags];

            //Если в строке содержатся символы тэгов, записываем в массив индексы начала и конца тегов
            for (int i = 0; i != countOfTags; i++)
            {
                tagsIndexes[0, i] = tempStr.IndexOf('<');
                tagsIndexes[1, i] = tempStr.IndexOf('>');

                tags[i] = tempStr.Substring(tagsIndexes[0, i], (tagsIndexes[1, i] - tagsIndexes[0, i] + 1));

                //И крайне весело заменяем символы тэга на какую-то фуйню
                tempStr = tempStr.Remove(tagsIndexes[0, i], 1).Insert(tagsIndexes[0, i], '|'.ToString());
                tempStr = tempStr.Remove(tagsIndexes[1, i], 1).Insert(tagsIndexes[1, i], '|'.ToString());
            }

            try
            {
                var translation = translator.Translate(originalStr, new LangPair(Lang.En, Lang.Ru), null, false);
                output += translation.Text + '\n';
            }
            catch (Exception ex)
            {
                MessageBox.Show("Строка " + originalStr +
                                "была обработана неверно. \n Вместо перевода будет записан оригинальный текст. \n " +
                                "Код ошибки: " + ex.Message);
                output += originalStr + '\n';
            }


            tempStr = output;

            //Если в строке содержатся символы тэгов, записываем в массив индексы начала и конца тегов
            for (int i = 0; i != countOfTags; i++)
            {
                tagsIndexes[0, i] = tempStr.IndexOf('<');
                tagsIndexes[1, i] = tempStr.IndexOf('>');

                tempStr = tempStr.Remove(tagsIndexes[0, i], 1).Insert(tagsIndexes[0, i], '|'.ToString());
                tempStr = tempStr.Remove(tagsIndexes[1, i], 1).Insert(tagsIndexes[1, i], '|'.ToString());
            }

            for (int i = 0; i != countOfTags; i++)
            {
                output = output.Remove(tagsIndexes[0, i], (tagsIndexes[1, i] - tagsIndexes[0, i] + 1));
                output = output.Insert(tagsIndexes[0, i], tags[i]);
            }

            //Если первым в строке идет тэг, то переводчиком он обрабаывается как первая буква предложения, и настоящая первая буква
            //переводчиком переводится в нижний регистр. Поэтому надо вернуть как было.
            if (output.IndexOf('<') == 0)
            {
                string charToUpper = output[output.IndexOf('>') + 1].ToString().ToUpper();
                output = output.Remove(output.IndexOf('>') + 1, 1).Insert(output.IndexOf('>') + 1, charToUpper);
            }

            return output;
        }


        // Былое

        private void Form1_Load(object sender, EventArgs e)
        {

            folderBrowserDialogMKVToolnix.SelectedPath = Properties.Settings.Default.PathToMKVToolnixFolder;
            folderBrowserDialogTempSubs.SelectedPath = Properties.Settings.Default.PathTempSubs;

            this.Text = formTitle;

            openFileDialogMKV.Title = "Выберите файл";
            openFileDialogMKV.Filter = "Видеофайлы Matroska Video|*.mkv";

            ////Синий фон у кнопок при наведении курсора
            //foreach (var btn in buttons)
            //{
            //    btn.MouseEnter += new EventHandler(btn_MouseEnter);
            //    btn.MouseLeave += new EventHandler(btn_MouseLeave);
            //}

            //foreach (var btn in buttonsForSaving)
            //{
            //    btn.MouseEnter += new EventHandler(btn_MouseEnter);
            //    btn.MouseLeave += new EventHandler(btn_MouseLeave);
            //}
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            previousButtonColor = ((Button)sender).BackColor;
            ((Button)sender).BackColor = SystemColors.GradientInactiveCaption;

        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = previousButtonColor;
        }


        private void WriteBilingualSubtitles()
        {
            string currentSecondarySubtitlesHEXColor = String.Format("{0:X2}{1:X2}{2:X2}", Properties.Settings.Default.CurrentSecondarySubtitlesColor.R,
                Properties.Settings.Default.CurrentSecondarySubtitlesColor.G,
                Properties.Settings.Default.CurrentSecondarySubtitlesColor.B); //Текущий цвет в HEX представлении

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName);

                //Сначала пишем русские субтитры
                WriteSubtitles(sw, "Secondary");

                subsLines++;

                //Затем - английские
                WriteSubtitles(sw, "Primary");

                sw.Close();

                flagSubsAreSaved = true;
            }
        }

        private void WriteSubtitles(StreamWriter sw, string type)
        {
        }

        private void ReadSubsFromFile(object sender, EventArgs e)
        {
        }
        //    disableCheckboxSubsInOneLine = false;
        //    disableCheckboxRemoveStylesFromSecondarySubs = false;

        //    SubtitlesAreNotTranslated(this, e);
        //    labelCurrentProcess.Text = "Идёт считывание субтитров";

        //    ClearDataGrid(dataGridView1);
        //    subsLines = 0;
        //    string[] readedLines = System.IO.File.ReadAllLines(openFileDialog.FileName); //Читаем из файла оригинальные субтитры

        //    foreach (string line in readedLines)
        //    {
        //        if (line.Contains("-->"))
        //            subsLines++;
        //    }

        //    subtitles = null;
        //    subtitles = new Subtitle[subsLines];
        //    int j = 0;

        //    for (int i = 0; i < readedLines.Length - 1; i++)
        //    {
        //        if (readedLines[i].Contains("-->"))
        //        {
        //            subtitles[j] = new Subtitle(Int32.Parse(readedLines[i - 1]), readedLines[i], (readedLines[i + 1] + '\n'));

        //            i += 2;

        //            while ((i < readedLines.Length) && (readedLines[i] != ""))
        //            {
        //                subtitles[j].Text += readedLines[i] + '\n';

        //                i++;
        //            }

        //            j++;
        //        }
        //    }

        //    subs = null;
        //    subs = new Subtitle[subtitles.Length];

        //    for (int i = 0; i != subtitles.Length; i++)
        //    {
        //        //subs[i] = new Subtitle(subtitles[i].ID, subtitles[i].Timing, subtitles[i].Text);
        //    }

        //    //Находим ID последнего субтитра
        //    //int lastLine = subtitles[subtitles.Length - 1].ID / 2;
        //    //Проверяем, не уже ли двуязычные у нас субтитры

        //    //for (int i = 0; i < lastLine; i++)
        //    //{
        //    //    //if (subtitles[i].Timing == subtitles[i + lastLine].Timing)
        //    //    //    flagItIsAlreadyBilingualSubtitles = true;
        //    //    //else
        //    //    //{
        //    //    //    flagItIsAlreadyBilingualSubtitles = false;
        //    //    //    break;
        //    //    //}
        //    //}

        //    if (flagItIsAlreadyBilingualSubtitles)
        //    {
        //        Subtitle[] tempSubs = new Subtitle[lastLine];
        //        subtitles = new Subtitle[lastLine];
        //        for (int i = 0; i < lastLine; i++)
        //        {
        //            subtitles[i] = subs[lastLine + i];
        //            subtitles[i].ID = i + 1;
        //        }

        //        for (int i = 0; i < tempSubs.Length; i++)
        //        {
        //            tempSubs[i] = subs[i];
        //        }

        //        subs = new Subtitle[lastLine];

        //        for (int i = 0; i < subs.Length; i++)
        //        {
        //            subs[i] = tempSubs[i];
        //        }


        //        //Выделяем цвета
        //        if (subtitles[0].Text.Contains("<font color="))
        //        {
        //            Properties.Settings.Default.CurrentPrimarySubtitlesColor = System.Drawing.ColorTranslator.FromHtml(subtitles[0].Text.Substring(subtitles[0].Text.IndexOf("<font color=") + "<font color=".Length + 1, 7));
        //            foreach (var sub in subtitles)
        //            {
        //                sub.Text = sub.Text.Remove(sub.Text.IndexOf("<font color="), "<font color=".Length + 10);
        //                sub.Text = sub.Text.Remove(sub.Text.IndexOf("</font>"), "</font>".Length);
        //            }
        //        }

        //        if (subs[0].Text.Contains("<font color="))
        //        {
        //            Properties.Settings.Default.CurrentSecondarySubtitlesColor = System.Drawing.ColorTranslator.FromHtml(subs[0].Text.Substring(subs[0].Text.IndexOf("<font color=") + "<font color=".Length + 1, 7));
        //            foreach (var sub in subs)
        //            {
        //                sub.Text = sub.Text.Remove(sub.Text.IndexOf("<font color="), "<font color=".Length + 10);
        //                sub.Text = sub.Text.Remove(sub.Text.IndexOf("</font>"), "</font>".Length);
        //            }
        //        }

        //        if (flagItIsAlreadyBilingualSubtitles)
        //        {
        //            int counterForSubsInOneLine = 0;
        //            int counterForRemoveStyles = 0;
        //            foreach (var sub in subs)
        //            {
        //                if (sub.Text.Contains('<') && sub.Text.Contains('>'))
        //                    counterForRemoveStyles++;
        //                if (sub.Text.Substring(0, sub.Text.Length - 1).Contains('\n'))
        //                    counterForSubsInOneLine++;
        //            }

        //            if (counterForRemoveStyles == 0) disableCheckboxRemoveStylesFromSecondarySubs = true;
        //            if (counterForSubsInOneLine == 0) disableCheckboxSubsInOneLine = true;
        //        }

        //    }

        //    if (backgroundWorkerWriteSubsToDataGrid.IsBusy != true)
        //    {
        //        AllToDisable();
        //        buttonCancel.Show();
        //        buttonCancel.Enabled = true;

        //        progressBar.Value = 0;
        //        ModifyProgressBarColor.SetState(progressBar, 1);
        //        progressBar.Show();
        //        // Start the asynchronous operation.
        //        backgroundWorkerWriteSubsToDataGrid.RunWorkerAsync();
        //    }
        //}

        private void backgroundWorkerWriteSubsToDataGrid_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;

            //try
            //{
            //    for (int i = 0; i != subtitles.Length; i++)
            //    {
            //        if (worker.CancellationPending == true)
            //        {
            //            e.Cancel = true;
            //            break;
            //        }
            //        else
            //        {

            //            worker.ReportProgress((int)((float)i / (float)(subtitles.Length - 1) * 100));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void backgroundWorkerWriteSubsToDataGrid_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorkerWriteSubsToDataGrid_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void backgroundWorkerTranslation_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;

            //// Perform a time consuming operation and report progress.

            //var translator = (Translator)e.Argument;

            //string output = "";

            //for (int i = 0; i < subs.Length; i++)
            //{
            //    if (worker.CancellationPending == true)
            //    {
            //        e.Cancel = true;
            //        break;
            //    }
            //    else
            //    {
            //        output = YandexTranslateAStringWithChecking(subs[i].Text, translator);

            //        int levels = subs[i].Text.Split('\n').Length - 1; //Т.к. там везде '\n' на конце титра
            //        subs[i].Text = output.Substring(0, output.IndexOf('\n')) + '\n';
            //        output = output.Remove(0, output.IndexOf('\n') + 1);
            //        //Интересно, почему "+1". В предыдущем варианте кода было. Без него не работает. Но ведь \n — это 2 символа? На самом деле, получается, 1.

            //        while (levels != 1)
            //        {
            //            subs[i].Text += output.Substring(0, output.IndexOf('\n')) + '\n';
            //            output = output.Remove(0, output.IndexOf('\n') + 1);

            //            levels--;
            //        }

            //        worker.ReportProgress((int)((float)i / (float)(subs.Length - 1) * 100));
            //    }
            //}
        }

        private void backgroundWorkerTranslation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorkerTranslation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void AllToDisable()
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((!flagSubsAreSaved) && (flagSubsAreTranslated))
            {
                DialogResult dialogResult = MessageBox.Show("Переведенные субтитры не сохранены! \nВы хотите сохранить их перед выходом из приложения?",
                    "Выход без сохранения", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    WriteBilingualSubtitles();
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            if (!flagSubsAreExtractingSuccessfully)
                KillTheProcess("mkvextract", true);

        }

        private void KillTheProcess(string target_name)
        {
            System.Diagnostics.Process[] local_procs = System.Diagnostics.Process.GetProcesses();
            try
            {
                System.Diagnostics.Process target_proc = local_procs.First(p => p.ProcessName == target_name);
                target_proc.Kill();
                flagSubsAreExtractingSuccessfully = false;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Процесс извлечения субтитров не найден!");
            }
        }

        //
        /// 
        /// 
        /// РАСКОММЕНТИРОВАТЬ
        /// 
        /// 
        private void KillTheProcess(string target_name, bool doNotDisplayException)
        {
            //System.Diagnostics.Process[] local_procs = System.Diagnostics.Process.GetProcesses();
            //System.Diagnostics.Process target_proc = local_procs.First(p => p.ProcessName == target_name);
            //target_proc.Kill();
            //flagSubsAreExtractingSuccessfully = false;

        }

        private void ShowColorDialog(string typeOfSubs, bool saveImmidiately)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (typeOfSubs == "Secondary")
                colorDialog.Color = Properties.Settings.Default.CurrentSecondarySubtitlesColor;
            else if (typeOfSubs == "Primary")
                colorDialog.Color = Properties.Settings.Default.CurrentPrimarySubtitlesColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (typeOfSubs == "Secondary")
                {
                    Properties.Settings.Default.CurrentSecondarySubtitlesColor = colorDialog.Color;
                }
                else if (typeOfSubs == "Primary")
                {
                    Properties.Settings.Default.CurrentPrimarySubtitlesColor = colorDialog.Color;
                }
                if (saveImmidiately)
                    Properties.Settings.Default.Save();
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_firstRussianSubtitles = YandextranslateSubtitles(m_originalSubtitles);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            m_secondRussianSubtitles = YandextranslateSubtitles(m_originalSubtitles);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            m_thirdRussianSubtitles = YandextranslateSubtitles(m_originalSubtitles);
        }

        private Subtitle[] OpenFileAndReadSubtitlesFromFile()
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
                return ReadSubtitles(openFileDialog.FileName);

            return null;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var ass = GenerateASSMarkedupDocument(new Tuple<Subtitle[], Color>[]
            {
                new Tuple<Subtitle[], Color>(m_originalSubtitles, Color.White),
            });
            File.WriteAllText(@"D:\Movies\!BS\xxxx.eng.ass", ass.ToString());

            List<Tuple<Subtitle[], Color>> listSubsPairs = new List<Tuple<Subtitle[], Color>>
            {
                new Tuple<Subtitle[], Color>(m_originalSubtitles, button9.BackColor)
            };
            if (m_firstRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(m_firstRussianSubtitles, button10.BackColor));
            if (m_secondRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(m_secondRussianSubtitles, button11.BackColor));
            if (m_thirdRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(m_thirdRussianSubtitles, button12.BackColor));

            ass = GenerateASSMarkedupDocument(listSubsPairs.ToArray());
            File.WriteAllText(@"D:\Movies\!BS\xxxx.ruseng.ass", ass.ToString());
        }


        private void colorPickingButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button) sender;

            var colorPickingDialog = new ColorDialog();
            var dialogResult = colorPickingDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                senderButton.BackColor = colorPickingDialog.Color;
            }
        }

        private void openPrimarySubtitlesButton_Click(object sender, EventArgs e)
        {
            m_originalSubtitles = OpenFileAndReadSubtitlesFromFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_firstRussianSubtitles = OpenFileAndReadSubtitlesFromFile();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            m_secondRussianSubtitles = OpenFileAndReadSubtitlesFromFile();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            m_thirdRussianSubtitles = OpenFileAndReadSubtitlesFromFile();
        }
    }
}
