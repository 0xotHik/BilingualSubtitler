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
        private KeyboardHookManager m_keyboardHookManager = new KeyboardHookManager();
        private InputSimulator m_inputSimulator;



        private string mkvtoolnixOutput;
        private string formTitle = "Создание двуязычных субтитров";
        private OpenFileDialog openFileDialog = new OpenFileDialog();
        private SaveFileDialog saveFileDialog = new SaveFileDialog();

        private Subtitle[] subtitles;
        private Subtitle[] subs;
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

            panelSubtitlesCreation.Location = new Point(0, 0);
            panelSettings.Location = new Point(0, 0);

            labelCurrentProcess.AutoEllipsis = true;

            m_keyboardHookManager.Start();
            m_inputSimulator = new InputSimulator();

            // Register virtual key code 0x60 = NumPad0
            m_keyboardHookManager.RegisterHotkey(
                (int)VirtualKeyCode.SPACE,
                () =>
                {
                    m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_S);
                });

        }

        private Subtitle[] ReadSubtitles(string filePath)
        {
            var sourceFileFI = new FileInfo(filePath);
            var extension = sourceFileFI.Extension;

            switch (extension)
            {
                case ".srt":
                    {
                        ReadSRT(filePath);
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
                            var subtitles = mkvFile.GetSubtitle(trackSelectionForm.SelectedTrackNumber,
                                (position, total) => { });

                            var ggg = 0;
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return null;
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
                        Int32.Parse(readedLines[i - 1]),
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

        private StringBuilder GenerateASS(Tuple<Subtitle[], Color>[] subtitleStreams)
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
            for (int i = 0; i < subtitleStreams.Length; i++)
            {
                var color = subtitleStreams[i].Item2;

                assSB.AppendLine(
                    $"Style: {i} sub stream," +
                    $"Arial," +
                    $"20," +
                    $"&H00{color.B}{color.G}{color.R}," +
                    $"&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,10,1");
            }

            //    [Events]
            //Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text
            assSB.Append("[Events]\r\n" +
                         "Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text\r\n");

            // Dialogue: 0,0:01:25.29,0:01:28.52,Копировать из Копировать из Default,,0,0,0,,Эй! Сюда! Тут человек!
            foreach (var subtitleStream in subtitleStreams)
            {
                assSB.AppendLine($"Dialogue: 0");
            }

            return assSB;
        }



        // Былое

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Size = new Size(583, 530);

            label1.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            richTextBoxLabelEnterYandexAPIKey.Text = "";
            richTextBoxLabelEnterYandexAPIKey.AppendText("Введите ключ для API ", Color.Black);
            richTextBoxLabelEnterYandexAPIKey.AppendText("Я", Color.Red);
            richTextBoxLabelEnterYandexAPIKey.AppendText("ндекс.Переводчика", Color.Black);

            progressBar.Hide();

            buttons = new List<Button>();
            buttonsForSaving = new List<Button>();
            labels = new List<Label>();

            //buttonOpenFile.Text = "Открыть \n исходные субтитры";
            //buttonWriteBilingualSubtitles.Text = "Сохранить \n созданные субтитры";

            buttons.Add(buttonOpenFile);
            buttons.Add(buttonSubtitlesCreationPanel);
            buttons.Add(buttonSettings);
            buttons.Add(buttonHelp);
            buttons.Add(buttonAbout);

            buttons.Add(buttonBackToMainForm);
            buttons.Add(buttonTranslate);

            buttons.Add(buttonCurrentSecondarySubtitlesColor);
            buttons.Add(buttonCurrentPrimarySubtitlesColor);
            buttons.Add(buttonCancel);

            buttons.Add(buttonCurrentPrimarySubtitlesColorOnSettingsPanel);
            buttons.Add(buttonCurrentSecondarySubtitlesColorOnSettingsPanel);

            buttons.Add(buttonResetToDefault);
            buttons.Add(buttonSaveAndBackToTheMainFormOnSettingsPanel);
            buttons.Add(buttonOpenMKV);

            buttons.Add(buttonRemoveHI);
            buttons.Add(buttonHelpOnSettingsPanel);

            buttonsForSaving.Add(buttonWriteBilingualSubtitles);
            buttonsForSaving.Add(buttonWriteBilingualSubtitlesAfterTranlation);

            labels.Add(labelCurrentPrimarySubtitlesColor);
            labels.Add(labelCurrentSecondarySubtitlesColor);


            foreach (var btn in buttons)
            {
                btn.FlatStyle = FlatStyle.Flat;
            }

            foreach (var btn in buttonsForSaving)
            {
                btn.Enabled = false;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
            }

            buttonCancel.Hide();
            panelSubtitlesCreation.Hide();
            panelSettings.Hide();

            buttons.Remove(buttonRemoveHI);
            buttonRemoveHI.Enabled = false;

            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns[0].Width = "999".Length * 12;
            dataGridView1.Columns[1].Width = "00:00:03,000 -->".Length * 6;
            dataGridView1.Columns[2].Width = (dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[1].Width) / 2;
            dataGridView1.Columns[3].Width = dataGridView1.Columns[2].Width;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True; //Включает перенос строки, целиком не помещающейся в ячейку

            labelCurrentProcess.Text = "";
            labelProcessPercenage.Text = "";

            folderBrowserDialogMKVToolnix.SelectedPath = Properties.Settings.Default.PathToMKVToolnixFolder;
            folderBrowserDialogTempSubs.SelectedPath = Properties.Settings.Default.PathTempSubs;

            this.Text = formTitle;

            openFileDialogMKV.Title = "Выберите файл";
            openFileDialogMKV.Filter = "Видеофайлы Matroska Video|*.mkv";

            //Синий фон у кнопок при наведении курсора
            foreach (var btn in buttons)
            {
                btn.MouseEnter += new EventHandler(btn_MouseEnter);
                btn.MouseLeave += new EventHandler(btn_MouseLeave);
            }

            foreach (var btn in buttonsForSaving)
            {
                btn.MouseEnter += new EventHandler(btn_MouseEnter);
                btn.MouseLeave += new EventHandler(btn_MouseLeave);
            }

            this.dataGridView1.DefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;
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

        private void Subtitles_Are_Not_Translated(object sender, EventArgs eventArgs)
        {
            flagSubsAreTranslated = false;

            foreach (var btn in buttonsForSaving)
            {
                btn.Enabled = false;
            }
        }

        private void Subtitles_Are_Translated(object sender, EventArgs eventArgs)
        {
            flagSubsAreTranslated = true;

            foreach (var btn in buttonsForSaving)
            {
                btn.Enabled = true;
            }

            flagSubsAreSaved = false;
        }

        private string YandexTranslateAStringWithChecking(string originalStr, Translator translator)
        {
            string output = "";
            string tempStr = originalStr;
            int countOfTags = originalStr.Split('<').Length - 1;
            int[,] tagsIndexes = new int[2, countOfTags];
            string[] tags = new string[countOfTags];

            for (int i = 0; i != countOfTags; i++) //Если в строке содержатся символы тэгов, записываем в массив индексы начала и конца тегов
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
                //labelCurrentLine.Text = str;

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

            for (int i = 0; i != countOfTags; i++) //Если в строке содержатся символы тэгов, записываем в массив индексы начала и конца тегов
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
                labelCurrentProcess.Text = "Субтитры успешно сохранены в файл " + saveFileDialog.FileName.ToString();
            }
        }

        private void WriteSubtitles(StreamWriter sw, string type)
        {
            subsLines = subtitles.Length + 1;
            Subtitle[] currentSubtitles = null;
            string currentSubtitlesHEXColor = "";

            if (type == "Primary")
            {
                currentSubtitlesHEXColor = String.Format("{0:X2}{1:X2}{2:X2}",
                    Properties.Settings.Default.CurrentPrimarySubtitlesColor.R,
                    Properties.Settings.Default.CurrentPrimarySubtitlesColor.G,
                    Properties.Settings.Default.CurrentPrimarySubtitlesColor.B); //Текущий цвет в HEX представлении

                currentSubtitles = subtitles;
            }
            else if (type == "Secondary")
            {
                currentSubtitlesHEXColor = String.Format("{0:X2}{1:X2}{2:X2}",
    Properties.Settings.Default.CurrentSecondarySubtitlesColor.R,
    Properties.Settings.Default.CurrentSecondarySubtitlesColor.G,
    Properties.Settings.Default.CurrentSecondarySubtitlesColor.B); //Текущий цвет в HEX представлении

                currentSubtitles = subs;
            }

            foreach (var sub in currentSubtitles)
            {
                string str = "";
                if (type == "Primary")
                    sw.WriteLine(subsLines++);
                else if (type == "Secondary")
                    sw.WriteLine(sub.ID.ToString());

                //sw.WriteLine(sub.Timing);

                str = sub.Text.Substring(0, sub.Text.Length - 1);

                if ((type == "Secondary") && (Properties.Settings.Default.SecondarySubsInOneLine == true))
                {
                    str = str.Replace('\n', ' ');
                }

                if ((type == "Secondary") && (Properties.Settings.Default.RemoveStylesFromSecondarySubs == true))
                {
                    int countOfTags = str.Split('<').Length - 1;
                    int[] tagsIndexes = new int[2];

                    for (int i = 0; i != countOfTags; i++)
                    {
                        tagsIndexes[0] = str.IndexOf('<');
                        tagsIndexes[1] = str.IndexOf('>');

                        str = str.Remove(tagsIndexes[0], tagsIndexes[1] - tagsIndexes[0] + 1);
                    }
                }


                str = str.Insert(0, "<font color=" + '"' + '#' + currentSubtitlesHEXColor + '"' + ">"); //Пишем в начало тэг цвета 
                str += "</font>"; //Закрывам тэг цвета

                sw.WriteLine(str);
                sw.WriteLine("");


                #region Старая схема с уровнями
                //if ((type == "Secondary") && (Properties.Settings.Default.SecondarySubsInOneLine))
                //{
                //}
                //else
                //{
                //    int levels = sub.Text.Split('\n').Length - 1; //Т.к. там везде '\n' на конце титра
                //    sw.WriteLine("<font color=" + '"' + '#' + currentSubtitlesHEXColor + '"' + ">" + sub.Text.Substring(0, sub.Text.IndexOf('\n')) + "</font>");
                //    sub.Text = sub.Text.Remove(0, sub.Text.IndexOf('\n') + 1);

                //    while (levels != 1)
                //    {
                //        sw.WriteLine("<font color=" + '"' + '#' + currentSubtitlesHEXColor + '"' + ">" + sub.Text.Substring(0, sub.Text.IndexOf('\n')) + "</font>");
                //        sub.Text = sub.Text.Remove(0, sub.Text.IndexOf('\n') + 1);

                //        levels--;
                //    }
                //}
                #endregion


            }

        }


        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Text = formTitle + " || " + openFileDialog.SafeFileName;
                ReadSubsFromFile(this, e);
            }

        }

        private void ReadSubsFromFile(object sender, EventArgs e)
        {
            disableCheckboxSubsInOneLine = false;
            disableCheckboxRemoveStylesFromSecondarySubs = false;

            SubtitlesAreNotTranslated(this, e);
            labelCurrentProcess.Text = "Идёт считывание субтитров";

            ClearDataGrid(dataGridView1);
            subsLines = 0;
            string[] readedLines = System.IO.File.ReadAllLines(openFileDialog.FileName); //Читаем из файла оригинальные субтитры

            foreach (string line in readedLines)
            {
                if (line.Contains("-->"))
                    subsLines++;
            }

            subtitles = null;
            subtitles = new Subtitle[subsLines];
            int j = 0;

            for (int i = 0; i < readedLines.Length - 1; i++)
            {
                if (readedLines[i].Contains("-->"))
                {
                    subtitles[j] = new Subtitle(Int32.Parse(readedLines[i - 1]), readedLines[i], (readedLines[i + 1] + '\n'));

                    i += 2;

                    while ((i < readedLines.Length) && (readedLines[i] != ""))
                    {
                        subtitles[j].Text += readedLines[i] + '\n';

                        i++;
                    }

                    j++;
                }
            }

            subs = null;
            subs = new Subtitle[subtitles.Length];

            for (int i = 0; i != subtitles.Length; i++)
            {
                //subs[i] = new Subtitle(subtitles[i].ID, subtitles[i].Timing, subtitles[i].Text);
            }

            //Находим ID последнего субтитра
            int lastLine = subtitles[subtitles.Length - 1].ID / 2;
            //Проверяем, не уже ли двуязычные у нас субтитры

            for (int i = 0; i < lastLine; i++)
            {
                //if (subtitles[i].Timing == subtitles[i + lastLine].Timing)
                //    flagItIsAlreadyBilingualSubtitles = true;
                //else
                //{
                //    flagItIsAlreadyBilingualSubtitles = false;
                //    break;
                //}
            }

            if (flagItIsAlreadyBilingualSubtitles)
            {
                Subtitle[] tempSubs = new Subtitle[lastLine];
                subtitles = new Subtitle[lastLine];
                for (int i = 0; i < lastLine; i++)
                {
                    subtitles[i] = subs[lastLine + i];
                    subtitles[i].ID = i + 1;
                }

                for (int i = 0; i < tempSubs.Length; i++)
                {
                    tempSubs[i] = subs[i];
                }

                subs = new Subtitle[lastLine];

                for (int i = 0; i < subs.Length; i++)
                {
                    subs[i] = tempSubs[i];
                }


                //Выделяем цвета
                if (subtitles[0].Text.Contains("<font color="))
                {
                    Properties.Settings.Default.CurrentPrimarySubtitlesColor = System.Drawing.ColorTranslator.FromHtml(subtitles[0].Text.Substring(subtitles[0].Text.IndexOf("<font color=") + "<font color=".Length + 1, 7));
                    foreach (var sub in subtitles)
                    {
                        sub.Text = sub.Text.Remove(sub.Text.IndexOf("<font color="), "<font color=".Length + 10);
                        sub.Text = sub.Text.Remove(sub.Text.IndexOf("</font>"), "</font>".Length);
                    }
                }

                if (subs[0].Text.Contains("<font color="))
                {
                    Properties.Settings.Default.CurrentSecondarySubtitlesColor = System.Drawing.ColorTranslator.FromHtml(subs[0].Text.Substring(subs[0].Text.IndexOf("<font color=") + "<font color=".Length + 1, 7));
                    foreach (var sub in subs)
                    {
                        sub.Text = sub.Text.Remove(sub.Text.IndexOf("<font color="), "<font color=".Length + 10);
                        sub.Text = sub.Text.Remove(sub.Text.IndexOf("</font>"), "</font>".Length);
                    }
                }

                if (flagItIsAlreadyBilingualSubtitles)
                {
                    int counterForSubsInOneLine = 0;
                    int counterForRemoveStyles = 0;
                    foreach (var sub in subs)
                    {
                        if (sub.Text.Contains('<') && sub.Text.Contains('>'))
                            counterForRemoveStyles++;
                        if (sub.Text.Substring(0, sub.Text.Length - 1).Contains('\n'))
                            counterForSubsInOneLine++;
                    }

                    if (counterForRemoveStyles == 0) disableCheckboxRemoveStylesFromSecondarySubs = true;
                    if (counterForSubsInOneLine == 0) disableCheckboxSubsInOneLine = true;
                }

            }

            if (backgroundWorkerWriteSubsToDataGrid.IsBusy != true)
            {
                AllToDisable();
                buttonCancel.Show();
                buttonCancel.Enabled = true;

                progressBar.Value = 0;
                ModifyProgressBarColor.SetState(progressBar, 1);
                progressBar.Show();
                // Start the asynchronous operation.
                backgroundWorkerWriteSubsToDataGrid.RunWorkerAsync();
            }
        }

        private void backgroundWorkerWriteSubsToDataGrid_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                for (int i = 0; i != subtitles.Length; i++)
                {
                    if (worker.CancellationPending == true)
                    {
                        e.Cancel = true;
                        break;
                    }
                    else
                    {

                        this.Invoke(new MethodInvoker(() => dataGridView1.Rows.Add()));

                        this.Invoke(new MethodInvoker(() => dataGridView1.Rows[i].Cells[0].Value = subtitles[i].ID));
                        //this.Invoke(
                        //    new MethodInvoker(() => dataGridView1.Rows[i].Cells[1].Value = subtitles[i].Timing));
                        this.Invoke(new MethodInvoker(() => dataGridView1.Rows[i].Cells[2].Value = subtitles[i].Text));
                        if (flagItIsAlreadyBilingualSubtitles)
                            this.Invoke(new MethodInvoker(() => dataGridView1.Rows[i].Cells[3].Value = subs[i].Text));

                        worker.ReportProgress((int)((float)i / (float)(subtitles.Length - 1) * 100));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorkerWriteSubsToDataGrid_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            labelCurrentProcess.Text = "Идет считывание субтитров из файла" + openFileDialog.SafeFileName +
                                               ": "; ;
            labelProcessPercenage.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void backgroundWorkerWriteSubsToDataGrid_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                ModifyProgressBarColor.SetState(progressBar, 2);
                labelCurrentProcess.Text = "Считывание субтитров было отменено.";
                labelProcessPercenage.Text = "";
                subs = null;
                subtitles = null;
            }
            else if (e.Error != null)
            {
                ModifyProgressBarColor.SetState(progressBar, 3);
                labelCurrentProcess.Text = "Считывание субтитров завершилось с ошибками.";
                labelProcessPercenage.Text = "";
            }
            else
            {
                labelCurrentProcess.Text = "Считывание субтитров завершилось успешно.";
                labelProcessPercenage.Text = "";
            }


            //progressBar.Value = progressBar.Maximum;
            //progressBar.Hide();

            AllToEnable();
            buttonCancel.Enabled = false;
        }

        private void buttonSubtitlesCreationPanel_Click(object sender, EventArgs e)
        {
            if (subs == null || subs[0] == null)
            {
                MessageBox.Show("Вы не открыли субтитры!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                panelMain.Visible = false;
                panelSubtitlesCreation.Parent = this;
                panelSubtitlesCreation.Visible = true;
                labelCurrentProcess.Text = "";
                labelProcessPercenage.Text = "";

                ModifyProgressBarColor.SetState(progressBar, 1);
                progressBar.Value = 0;
                progressBar.Hide();
                buttonCancel.Hide();

                buttonCurrentPrimarySubtitlesColor.BackColor = Properties.Settings.Default.CurrentPrimarySubtitlesColor;
                buttonCurrentSecondarySubtitlesColor.BackColor = Properties.Settings.Default.CurrentSecondarySubtitlesColor;

                checkBoxSecondarySubsInOneLine.Checked = Properties.Settings.Default.SecondarySubsInOneLine;
                checkBoxRemoveStylesFromSecondarySubs.Checked = Properties.Settings.Default.RemoveStylesFromSecondarySubs;
            }
        }

        private void buttonWriteBilingualSubtitles_Click(object sender, EventArgs e)
        {
            WriteBilingualSubtitles();
        }

        private void buttonBackToMainForm_Click(object sender, EventArgs e)
        {
            ModifyProgressBarColor.SetState(progressBar, 1);
            progressBar.Value = 0;
            progressBar.Hide();
            buttonCancel.Hide();
            panelSubtitlesCreation.Visible = false;
            panelMain.Visible = true;
            labelCurrentProcess.Text = "";
            labelProcessPercenage.Text = "";
            //pan
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            bool flagThereWasNoCancellation = true;

            progressBar.Show();
            progressBar.Value = 0;

            AllToDisable();

            buttonCancel.Enabled = true;
            buttonCancel.Show();

            var translator = new Translator(Properties.Settings.Default.YandexTranslatorAPIKey);
            try
            {
                var translation = translator.Translate("Random String", new LangPair(Lang.En, Lang.Ru), null, false);
                flagThereWasNoCancellation = true;
            }
            catch (YandexLinguisticsException ex)
            {
                if (ex.Message == "API key is invalid")
                {
                    YandexTranslatorAPIKeyForm form = null;
                    if (Properties.Settings.Default.YandexTranslatorAPIKey == "") { form = new YandexTranslatorAPIKeyForm(); }
                    else { form = new YandexTranslatorAPIKeyForm(true); }
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        buttonTranslate_Click(this, e);
                    }
                    else
                        flagThereWasNoCancellation = false;
                }
            }

            if ((flagThereWasNoCancellation) && (backgroundWorkerTranslation.IsBusy != true))
            {
                ModifyProgressBarColor.SetState(progressBar, 1);
                // Start the asynchronous operation.
                backgroundWorkerTranslation.RunWorkerAsync(translator);
            }
            else
            {
                AllToEnable();
                buttonCancel.Enabled = false;
                labelCurrentProcess.Text = "Перевод субтитров невозможен без верного ключа API Яндекс.Переводчика";
                labelProcessPercenage.Text = "";
            }



        }




        private void backgroundWorkerTranslation_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            // Perform a time consuming operation and report progress.

            var translator = (Translator)e.Argument;

            string output = "";

            for (int i = 0; i < subs.Length; i++)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    output = YandexTranslateAStringWithChecking(subs[i].Text, translator);

                    int levels = subs[i].Text.Split('\n').Length - 1; //Т.к. там везде '\n' на конце титра
                    subs[i].Text = output.Substring(0, output.IndexOf('\n')) + '\n';
                    output = output.Remove(0, output.IndexOf('\n') + 1);
                    //Интересно, почему "+1". В предыдущем варианте кода было. Без него не работает. Но ведь \n — это 2 символа? На самом деле, получается, 1.

                    while (levels != 1)
                    {
                        subs[i].Text += output.Substring(0, output.IndexOf('\n')) + '\n';
                        output = output.Remove(0, output.IndexOf('\n') + 1);

                        levels--;
                    }

                    this.Invoke(new MethodInvoker(() => dataGridView1.Rows[i].Cells[3].Value = subs[i].Text));
                    worker.ReportProgress((int)((float)i / (float)(subs.Length - 1) * 100));
                }
            }
        }


        private void backgroundWorkerTranslation_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            labelCurrentProcess.Text = "Идет перевод субтитров: ";
            labelProcessPercenage.Text = e.ProgressPercentage + "%";
        }

        private void backgroundWorkerTranslation_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                ModifyProgressBarColor.SetState(progressBar, 2);
                labelCurrentProcess.Text = "Перевод субтитров был отменен.";
                labelProcessPercenage.Text = "";
            }
            else if (e.Error != null)
            {
                ModifyProgressBarColor.SetState(progressBar, 3);
                labelCurrentProcess.Text = "Перевод субтитров завершился с ошибками.";
                labelProcessPercenage.Text = "";
            }
            else
            {
                FlashWindow.Flash(this);
                buttonCancel.Enabled = false;
                SubtitlesAreTranslated(this, e);
                labelCurrentProcess.Text = "Перевод субтитров завершен успешно.";
                labelProcessPercenage.Text = "";
            }

            AllToEnable();
            buttonCancel.Enabled = false;
        }

        private void AllToEnable()
        {
            foreach (var btn in buttons)
            {
                btn.Enabled = true;
            }

            foreach (var lbl in labels)
            {
                lbl.ForeColor = Color.Black;
            }

            if (disableCheckboxSubsInOneLine)
                checkBoxSecondarySubsInOneLine.Enabled = false;
            else
                checkBoxSecondarySubsInOneLine.Enabled = true;

            if (disableCheckboxRemoveStylesFromSecondarySubs)
                checkBoxRemoveStylesFromSecondarySubs.Enabled = false;
            else
                checkBoxRemoveStylesFromSecondarySubs.Enabled = true;


            //Возвращаем цвета кнопкам выбора цвета
            buttonCurrentPrimarySubtitlesColor.BackColor = Properties.Settings.Default.CurrentPrimarySubtitlesColor;
            buttonCurrentSecondarySubtitlesColor.BackColor = Properties.Settings.Default.CurrentSecondarySubtitlesColor;

            //checkBoxSecondarySubsInOneLine.Enabled = true;
            //checkBoxRemoveStylesFromSecondarySubs.Enabled = true;
        }

        private void AllToDisable()
        {
            buttonCurrentPrimarySubtitlesColor.BackColor = Color.SlateGray;
            buttonCurrentSecondarySubtitlesColor.BackColor = Color.SlateGray;

            foreach (var btn in buttons)
            {
                btn.Enabled = false;
            }

            foreach (var lbl in labels)
            {
                lbl.ForeColor = Color.SlateGray;
            }

            checkBoxSecondarySubsInOneLine.Enabled = false;
            checkBoxRemoveStylesFromSecondarySubs.Enabled = false;
        }
        public void ClearDataGrid(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }

        private void buttonCurrentSecondarySubtitlesColor_Click(object sender, EventArgs e)
        {
            ShowColorDialog("Secondary", true);
            buttonWriteBilingualSubtitlesAfterTranlation.Enabled = true;
        }

        private void buttonCurrentPrimarySubtitlesColor_Click(object sender, EventArgs e)
        {
            ShowColorDialog("Primary", true);
            buttonWriteBilingualSubtitlesAfterTranlation.Enabled = true;
        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {
            if (flagSubsAreExtractingSuccessfully)
                KillTheProcess("mkvextract");
            else
            {
                if ((backgroundWorkerTranslation.IsBusy) &&
                    (backgroundWorkerTranslation.WorkerSupportsCancellation == true))
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerTranslation.CancelAsync();
                }
                else if (backgroundWorkerWriteSubsToDataGrid.WorkerSupportsCancellation == true)
                {
                    // Cancel the asynchronous operation.
                    backgroundWorkerWriteSubsToDataGrid.CancelAsync();
                }
            }
        }

        private void buttonWriteBilingualSubtitlesAfterTranlation_Click(object sender, EventArgs e)
        {
            WriteBilingualSubtitles();
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - panelMain.Height - buttonCancel.Height - 20);
            dataGridView1.Columns[0].Width = "999".Length * 12;
            dataGridView1.Columns[1].Width = "00:00:03,000 -->".Length * 6;
            dataGridView1.Columns[2].Width = (dataGridView1.Width - dataGridView1.Columns[0].Width - dataGridView1.Columns[1].Width) / 2;
            dataGridView1.Columns[3].Width = dataGridView1.Columns[2].Width;

            progressBar.Size = new Size(this.ClientSize.Width - buttonCancel.Width - 30, progressBar.Size.Height);
            progressBar.Location = new Point(dataGridView1.Left, dataGridView1.Bottom + 8);

            buttonCancel.Location = new Point(progressBar.Right + 10, dataGridView1.Bottom + 4);

            panelMain.Size = new Size(this.ClientSize.Width - 10, panelMain.Height);
            panelSubtitlesCreation.Size = new Size(this.ClientSize.Width - 10, panelSubtitlesCreation.Height);
            panelSettings.Size = new Size(this.ClientSize.Width - 10, this.ClientSize.Height - 10);

            buttonBackToMainForm.Location = new Point(panelSubtitlesCreation.Width - buttonBackToMainForm.Width, buttonBackToMainForm.Top);
            buttonWriteBilingualSubtitlesAfterTranlation.Location = new Point(buttonBackToMainForm.Left - buttonWriteBilingualSubtitlesAfterTranlation.Width - 0, buttonWriteBilingualSubtitlesAfterTranlation.Top);

            buttonResetToDefault.Location = new Point(panelSettings.Right - buttonResetToDefault.Width, buttonResetToDefault.Top);
            buttonSaveAndBackToTheMainFormOnSettingsPanel.Location = new Point(buttonResetToDefault.Left - buttonSaveAndBackToTheMainFormOnSettingsPanel.Width, buttonSaveAndBackToTheMainFormOnSettingsPanel.Top);
            buttonHelpOnSettingsPanel.Location = new Point(buttonSaveAndBackToTheMainFormOnSettingsPanel.Left - buttonHelpOnSettingsPanel.Width - 0, buttonHelpOnSettingsPanel.Top);


            labelCurrentProcess.Location = new Point(progressBar.Left, progressBar.Top + 13);
            labelCurrentProcess.Width = System.Windows.Forms.TextRenderer.MeasureText(labelCurrentProcess.Text,
                new Font(labelCurrentProcess.Font.FontFamily,
                    labelCurrentProcess.Font.Size, labelCurrentProcess.Font.Style))
                .Width;
            if (labelCurrentProcess.Width > progressBar.Width -
                (System.Windows.Forms.TextRenderer.MeasureText("100%",
                    new Font(labelCurrentProcess.Font.FontFamily,
                        labelCurrentProcess.Font.Size, labelCurrentProcess.Font.Style))
                    .Width))
            {
                labelCurrentProcess.Width = progressBar.Width -
                                            (System.Windows.Forms.TextRenderer.MeasureText("100%",
                                                new Font(labelCurrentProcess.Font.FontFamily,
                                                    labelCurrentProcess.Font.Size, labelCurrentProcess.Font.Style))
                                                .Width);
            }
            labelProcessPercenage.Location = new Point(labelCurrentProcess.Right, progressBar.Top + 13);

            richTextBoxForYandexAPIKey.Width = panelSettings.Width;
            textBoxPathToMKVToolnix.Width = panelSettings.Width - buttonBrowsePathToMKVToolnix.Width - 20;
            textBoxPathToTemp.Width = panelSettings.Width - buttonBrowsePathToTemp.Width - 20;
            buttonBrowsePathToMKVToolnix.Left = textBoxPathToMKVToolnix.Right;
            buttonBrowsePathToTemp.Left = textBoxPathToTemp.Right;
        }

        private void checkBoxSecondarySubsInOneLine_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SecondarySubsInOneLine = checkBoxSecondarySubsInOneLine.Checked;
            Properties.Settings.Default.Save();
            buttonWriteBilingualSubtitlesAfterTranlation.Enabled = true;
        }

        private void checkBoxRemoveStylesFromSecondarySubs_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RemoveStylesFromSecondarySubs = checkBoxRemoveStylesFromSecondarySubs.Checked;
            Properties.Settings.Default.Save();
            buttonWriteBilingualSubtitlesAfterTranlation.Enabled = true;
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            progressBar.Hide();
            buttonCancel.Hide();
            panelMain.Hide();
            dataGridView1.Hide();
            labelCurrentProcess.Text = "";

            panelSettings.Location = new Point(0, 0);
            panelSettings.Parent = this;
            panelSettings.Visible = true;

            buttonCurrentPrimarySubtitlesColorOnSettingsPanel.BackColor = Properties.Settings.Default.CurrentPrimarySubtitlesColor;
            buttonCurrentPrimarySubtitlesColorOnSettingsPanel.Text =
                Properties.Settings.Default.CurrentPrimarySubtitlesColor.Name;

            if (Properties.Settings.Default.CurrentPrimarySubtitlesColor.GetBrightness() < 0.5)
                buttonCurrentPrimarySubtitlesColorOnSettingsPanel.ForeColor = Color.White;
            else
                buttonCurrentPrimarySubtitlesColorOnSettingsPanel.ForeColor = Color.Black;

            buttonCurrentSecondarySubtitlesColorOnSettingsPanel.BackColor = Properties.Settings.Default.CurrentSecondarySubtitlesColor;
            buttonCurrentSecondarySubtitlesColorOnSettingsPanel.Text =
                Properties.Settings.Default.CurrentSecondarySubtitlesColor.Name;
            if (Properties.Settings.Default.CurrentSecondarySubtitlesColor.GetBrightness() < 0.5)
                buttonCurrentSecondarySubtitlesColorOnSettingsPanel.ForeColor = Color.White;
            else
                buttonCurrentSecondarySubtitlesColorOnSettingsPanel.ForeColor = Color.Black;

            richTextBoxForYandexAPIKey.Text = Properties.Settings.Default.YandexTranslatorAPIKey;
            textBoxPathToMKVToolnix.Text = Properties.Settings.Default.PathToMKVToolnixFolder;
            textBoxPathToTemp.Text = Properties.Settings.Default.PathTempSubs;
            checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.Checked =
                Properties.Settings.Default.RemoveStylesFromSecondarySubs;
            checkBoxSecondarySubsInOneLineOnSettingsPanel.Checked = Properties.Settings.Default.SecondarySubsInOneLine;

            tempCurrentPrimarySubtitlesColor = Properties.Settings.Default.CurrentPrimarySubtitlesColor;
            tempCurrentSecondarySubtitlesColor = Properties.Settings.Default.CurrentSecondarySubtitlesColor;

        }

        private void buttonCurrentPrimarySubtitlesColorOnSettingsPanel_Click(object sender, EventArgs e)
        {
            ShowColorDialog("Primary", false);
            buttonCurrentPrimarySubtitlesColorOnSettingsPanel.BackColor = Properties.Settings.Default.CurrentPrimarySubtitlesColor;
            buttonCurrentPrimarySubtitlesColorOnSettingsPanel.Text =
                Properties.Settings.Default.CurrentPrimarySubtitlesColor.Name;

            if (Properties.Settings.Default.CurrentPrimarySubtitlesColor.GetBrightness() < 0.5)
                buttonCurrentPrimarySubtitlesColorOnSettingsPanel.ForeColor = Color.White;
            else
                buttonCurrentPrimarySubtitlesColorOnSettingsPanel.ForeColor = Color.Black;
        }

        private void buttonCurrentSecondarySubtitlesColorOnSettingsPanel_Click(object sender, EventArgs e)
        {
            ShowColorDialog("Secondary", false);
            buttonCurrentSecondarySubtitlesColorOnSettingsPanel.BackColor = Properties.Settings.Default.CurrentSecondarySubtitlesColor;
            buttonCurrentSecondarySubtitlesColorOnSettingsPanel.Text =
                Properties.Settings.Default.CurrentSecondarySubtitlesColor.Name;

            if (Properties.Settings.Default.CurrentSecondarySubtitlesColor.GetBrightness() < 0.5)
                buttonCurrentSecondarySubtitlesColorOnSettingsPanel.ForeColor = Color.White;
            else
                buttonCurrentSecondarySubtitlesColorOnSettingsPanel.ForeColor = Color.Black;
        }

        private void checkBoxSecondarySubsInOneLineOnSettingsPanel_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SecondarySubsInOneLine = checkBoxSecondarySubsInOneLine.Checked;
        }

        private void checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.RemoveStylesFromSecondarySubs = checkBoxRemoveStylesFromSecondarySubs.Checked;
        }

        private void buttonSaveAndBackToTheMainFormOnSettingsPanel_Click(object sender, EventArgs e)
        {
            //try
            //{
            //Вычищаем символы переноса строки и пробелы из ключа
            int carrySymbols = richTextBoxForYandexAPIKey.Text.Split('\n').Length - 1;
            int spaceSymbols = richTextBoxForYandexAPIKey.Text.Split(' ').Length - 1;

            richTextBoxForYandexAPIKey.Text = richTextBoxForYandexAPIKey.Text.Substring(0, richTextBoxForYandexAPIKey.Text.Length - spaceSymbols - carrySymbols);

            //var translator = new Translator(richTextBoxForYandexAPIKey.Text);
            //var translation = translator.Translate("Random String", new LangPair(Lang.En, Lang.Ru), null, false);
            //translator = null;

            //folderBrowseDialog почему-то оставляет папку без обратного слэша в конце. Фиксим.
            if ((textBoxPathToMKVToolnix.Text.Length != 0) && (textBoxPathToMKVToolnix.Text.Substring(textBoxPathToMKVToolnix.Text.Length - 1, 1) != @"\"))
                textBoxPathToMKVToolnix.Text += @"\";
            if ((textBoxPathToTemp.Text.Length != 0) && (textBoxPathToTemp.Text.Substring(textBoxPathToTemp.Text.Length - 1, 1) != @"\"))
                textBoxPathToTemp.Text += @"\";

            Properties.Settings.Default.YandexTranslatorAPIKey = richTextBoxForYandexAPIKey.Text;

            Properties.Settings.Default.PathToMKVToolnixFolder = textBoxPathToMKVToolnix.Text;
            Properties.Settings.Default.PathTempSubs = textBoxPathToTemp.Text;

            Properties.Settings.Default.RemoveStylesFromSecondarySubs =
                checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.Checked;
            Properties.Settings.Default.SecondarySubsInOneLine =
                checkBoxSecondarySubsInOneLineOnSettingsPanel.Checked;
            Properties.Settings.Default.Save();

            panelSettings.Visible = false;
            panelMain.Visible = true;
            dataGridView1.Visible = true;
            //}
            //catch (YandexLinguisticsException ex)
            //{
            //    if (ex.Message == "API key is invalid")
            //    {
            //        MessageBox.Show("Введенный вам ключ для API Яндекс.Переводчика неверен", "Ключ неверен",
            //            MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    }
            //}

        }

        private void buttonResetToDefault_Click(object sender, EventArgs e)
        {
            //DialogResult dialogResult = MessageBox.Show("Вы уверены, что хотите сбросить настройки на значения по умолчанию?",
            //        "Сброс изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (dialogResult == DialogResult.Yes)
            //{
            //    Properties.Settings.Default.CurrentSecondarySubtitlesColor = Color.Yellow;
            //    Properties.Settings.Default.CurrentPrimarySubtitlesColor = Color.White;
            //    Properties.Settings.Default.SecondarySubsInOneLine = false;
            //    Properties.Settings.Default.RemoveStylesFromSecondarySubs = false;
            //    Properties.Settings.Default.PathTempSubs = "";
            //    Properties.Settings.Default.PathToMKVToolnixFolder = "";
            //    Properties.Settings.Default.YandexTranslatorAPIKey = "";
            //    Properties.Settings.Default.Save();

            //    buttonCurrentPrimarySubtitlesColorOnSettingsPanel.BackColor = Color.White;
            //    buttonCurrentSecondarySubtitlesColorOnSettingsPanel.BackColor = Color.Yellow;
            //    buttonCurrentPrimarySubtitlesColorOnSettingsPanel.ForeColor = Color.Black;
            //    buttonCurrentSecondarySubtitlesColorOnSettingsPanel.ForeColor = Color.Black;
            //    checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.Checked = false;
            //    checkBoxSecondarySubsInOneLineOnSettingsPanel.Checked = false;
            //    richTextBoxForYandexAPIKey.Text = "";
            //    textBoxPathToMKVToolnix.Text = "";
            //    textBoxPathToTemp.Text = "";
            //}

            Properties.Settings.Default.CurrentPrimarySubtitlesColor = tempCurrentPrimarySubtitlesColor;
            Properties.Settings.Default.CurrentSecondarySubtitlesColor = tempCurrentSecondarySubtitlesColor;

            panelSettings.Visible = false;
            panelMain.Visible = true;
            dataGridView1.Visible = true;

        }

        private void linkLabelGetYandexAPIKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelGetYandexAPIKey.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tech.yandex.ru/keys/get/?service=trnsl");
        }

        private void buttonOpenMKV_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();
            ReadSubtitles(openFileDialog.FileName);



            //if (openFileDialogMKV.ShowDialog() == DialogResult.OK)
            //{

            //    if (!System.IO.File.Exists(Properties.Settings.Default.PathToMKVToolnixFolder + "mkvmerge.exe") || (!System.IO.File.Exists(Properties.Settings.Default.PathToMKVToolnixFolder + "mkvextract.exe")))
            //    {
            //        PathToMKVToolnixAndTempForm form = new PathToMKVToolnixAndTempForm();
            //        if (form.ShowDialog() == DialogResult.OK)
            //        {
            //            buttonOpenFile_Click(this, e);
            //        }
            //    }
            //    else
            //    {
            //        ClearDataGrid(dataGridView1);
            //        mkvtoolnixOutput = "";
            //        ModifyProgressBarColor.SetState(progressBar, "green");
            //        this.Text = formTitle + " || " + openFileDialogMKV.SafeFileName;
            //        flagSubsAreExtractingSuccessfully = true;
            //        AllToDisable();
            //        buttonCancel.Show();

            //        ReadFromMKV(Properties.Settings.Default.PathToMKVToolnixFolder + "mkvmerge.exe",
            //            "-I " + '"' + openFileDialogMKV.FileName + '"');
            //    }
            //}
        }

        //private void ReadFromMKV(string utilityName, string arguments)
        //{
        //    try
        //    {
        //        //создаем новый процесс, который будет работать с консолью
        //        var pr = new Process();
        //        //задаем имя запускного файла
        //        pr.StartInfo.FileName = utilityName;
        //        //задаем аргументы для этого файла
        //        pr.StartInfo.Arguments = arguments;
        //        //отключаем использование оболочки, чтобы можно было читать данные вывода
        //        pr.StartInfo.UseShellExecute = false;
        //        //перенаправляем данные вовода
        //        pr.StartInfo.RedirectStandardOutput = true;
        //        //задаем кодировку, чтобы читать кириллические символы
        //        pr.StartInfo.StandardOutputEncoding = Encoding.UTF8;
        //        //запрещаем создавать окно для запускаемой программы                
        //        pr.StartInfo.CreateNoWindow = true;
        //        //подписываемся на событие, которые возвращает данные
        //        pr.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandlerReadingFromMKV);
        //        //включаем возможность определять когда происходит выход из программы, которую будем запускать
        //        pr.EnableRaisingEvents = true;
        //        //подписываемся на событие, когда процесс завершит работу
        //        pr.Exited += new EventHandler(WhenExitProcessOfReadingFromMKV);
        //        //запускаем процесс
        //        pr.Start();
        //        //начинаем читать стандартный вывод
        //        pr.BeginOutputReadLine();
        //    }
        //    catch (Exception error)
        //    {
        //        MessageBox.Show("Ошибка при запуске!\n" + error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void SortOutputHandlerReadingFromMKV(object sendingProcess, DataReceivedEventArgs outLine)
        {
            //используем делегат для доступа к элементу формы из другого потока
            BeginInvoke(new MethodInvoker(delegate
            {
                if (!String.IsNullOrEmpty(outLine.Data))
                {
                    //выводим результат в консоль
                    mkvtoolnixOutput += outLine.Data + '\n';
                }
            }));
        }

        //private void WhenExitProcessOfReadingFromMKV(Object sender, EventArgs e)
        //{
        //    TrackToExtractFromMKVForm trackToExtractFrom = new TrackToExtractFromMKVForm(mkvtoolnixOutput);

        //    if ((trackToExtractFrom.ShowDialog() == DialogResult.OK) && (trackToExtractFrom.selectedID != null))
        //    {
        //        this.Invoke(new MethodInvoker(() => buttonCancel.Enabled = true));
        //        string selectedID = trackToExtractFrom.selectedID;
        //        ExtractFromMKV(Properties.Settings.Default.PathToMKVToolnixFolder + "mkvextract.exe",
        //            " tracks " + '"' + openFileDialogMKV.FileName + '"' + " " + selectedID + ":" +
        //            Properties.Settings.Default.PathTempSubs + "temp.srt", selectedID);
        //    }
        //    else
        //    {
        //        this.Invoke(new MethodInvoker(() => AllToEnable()));
        //        this.Invoke(new MethodInvoker(() => buttonCancel.Hide()));
        //    }
        //}

        private void ExtractFromMKV(string utilityName, string arguments, string selectedID)
        {
            try
            {
                //создаем новый процесс, который будет работать с консолью
                var pr = new Process();
                //задаем имя запускного файла
                pr.StartInfo.FileName = utilityName;
                //задаем аргументы для этого файла
                pr.StartInfo.Arguments = arguments;
                //отключаем использование оболочки, чтобы можно было читать данные вывода
                pr.StartInfo.UseShellExecute = false;
                //перенаправляем данные вовода
                pr.StartInfo.RedirectStandardOutput = true;
                //задаем кодировку, чтобы читать кириллические символы
                pr.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                //запрещаем создавать окно для запускаемой программы                
                pr.StartInfo.CreateNoWindow = true;
                //подписываемся на событие, которые возвращает данные
                pr.OutputDataReceived += new DataReceivedEventHandler(SortOutputHandlerExtractFromMKV);
                //включаем возможность определять когда происходит выход из программы, которую будем запускать
                pr.EnableRaisingEvents = true;
                //подписываемся на событие, когда процесс завершит работу
                pr.Exited += new EventHandler(WhenExitProcessOfExtractingFromMKV);
                //запускаем процесс
                pr.Start();
                //начинаем читать стандартный вывод
                pr.BeginOutputReadLine();
            }

            catch (Exception error)
            {
                MessageBox.Show("Ошибка при запуске!\n" + error.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //catch (YandexLinguisticsException ex)
            //{
            //    if (ex.Message == "API key is invalid")
            //    {
            //        YandexTranslatorAPIKeyForm form = null;
            //        if (Properties.Settings.Default.YandexTranslatorAPIKey == "") { form = new YandexTranslatorAPIKeyForm(); }
            //        else { form = new YandexTranslatorAPIKeyForm(true); }
            //        if (form.ShowDialog() == DialogResult.OK)
            //        {
            //            buttonTranslate_Click(this, e);
            //        }
            //        else
            //            flagThereWasNoCancellation = false;
            //    }
            //}

            //if ((flagThereWasNoCancellation) && (backgroundWorkerTranslation.IsBusy != true))
            //{
            //    ModifyProgressBarColor.SetState(progressBar, 1);
            //     Start the asynchronous operation.
            //    backgroundWorkerTranslation.RunWorkerAsync(translator);
            //}
            //else
            //{
            //    AllToEnable();
            //    buttonCancel.Enabled = false;
            //    labelCurrentProcess.Text = "Перевод субтитров невозможен без верного ключа API Яндекс.Переводчика";
            //    labelProcessPercenage.Text = "";
            //}
        }

        private void SortOutputHandlerExtractFromMKV(object sendingProcess, DataReceivedEventArgs outLine)
        {
            this.Invoke(new MethodInvoker(() => progressBar.Show()));
            //используем делегат для доступа к элементу формы из другого потока
            BeginInvoke(new MethodInvoker(delegate
            {
                if ((!String.IsNullOrEmpty(outLine.Data)) && (outLine.Data.Contains("Progress:")))
                {
                    progressBar.Value = Int32.Parse(outLine.Data.Substring("Progress: ".Length,
                        outLine.Data.IndexOf("%") - "Progress: ".Length));
                    labelCurrentProcess.Text = "Идет считывание субтитров из файла " + openFileDialogMKV.SafeFileName +
                                               ": ";

                    labelProcessPercenage.Text = outLine.Data.Substring("Progress: ".Length,
                                                   outLine.Data.IndexOf("%") - "Progress: ".Length) + "%";
                }
            }));
        }

        private void WhenExitProcessOfExtractingFromMKV(Object sender, EventArgs e)
        {
            if (flagSubsAreExtractingSuccessfully)
            {
                this.Invoke(new MethodInvoker(() => labelCurrentProcess.Text = "Считывание субтитров из файла " + openFileDialogMKV.SafeFileName + " завершено."));
                openFileDialog.FileName = Properties.Settings.Default.PathTempSubs + "temp.srt";
                this.Invoke(new MethodInvoker(() => ReadSubsFromFile(this, e)));
                this.Invoke(new MethodInvoker(() => File.Delete(Properties.Settings.Default.PathTempSubs + "temp.srt")));
            }
            else
            {
                this.Invoke(new MethodInvoker(() => AllToEnable()));
                this.Invoke(new MethodInvoker(() => ModifyProgressBarColor.SetState(progressBar, 2)));
                this.Invoke(new MethodInvoker(() => buttonCancel.Enabled = false));
                this.Invoke(new MethodInvoker(() => labelCurrentProcess.Text = "Процесс чтения субтитров из файла " + openFileDialogMKV.FileName +
                                           " был прерван"));
            }

        }

        private void labelCurrentProcess_TextChanged(object sender, EventArgs e)
        {
            labelCurrentProcess.Width = System.Windows.Forms.TextRenderer.MeasureText(labelCurrentProcess.Text,
                new Font(labelCurrentProcess.Font.FontFamily,
                    labelCurrentProcess.Font.Size, labelCurrentProcess.Font.Style))
                .Width;
            if (labelCurrentProcess.Width > progressBar.Width -
                (System.Windows.Forms.TextRenderer.MeasureText("100%",
                    new Font(labelCurrentProcess.Font.FontFamily,
                        labelCurrentProcess.Font.Size, labelCurrentProcess.Font.Style))
                    .Width))
            {
                labelCurrentProcess.Width = progressBar.Width -
                                            (System.Windows.Forms.TextRenderer.MeasureText("100%",
                                                new Font(labelCurrentProcess.Font.FontFamily,
                                                    labelCurrentProcess.Font.Size, labelCurrentProcess.Font.Style))
                                                .Width);
            }
            labelProcessPercenage.Location = new Point(labelCurrentProcess.Right, progressBar.Top + 13);
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

        private void buttonBrowsePathToMKVToolnix_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogMKVToolnix.ShowDialog() == DialogResult.OK)
                textBoxPathToMKVToolnix.Text = folderBrowserDialogMKVToolnix.SelectedPath;
        }

        private void buttonBrowsePathToTemp_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogTempSubs.ShowDialog() == DialogResult.OK)
                textBoxPathToTemp.Text = folderBrowserDialogTempSubs.SelectedPath;
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
                    buttonCurrentSecondarySubtitlesColor.BackColor =
                        Properties.Settings.Default.CurrentSecondarySubtitlesColor;
                }
                else if (typeOfSubs == "Primary")
                {
                    Properties.Settings.Default.CurrentPrimarySubtitlesColor = colorDialog.Color;
                    buttonCurrentPrimarySubtitlesColor.BackColor =
                        Properties.Settings.Default.CurrentPrimarySubtitlesColor;
                }
                if (saveImmidiately)
                    Properties.Settings.Default.Save();
            }
        }

        private void linkLabelDownloadMKVToolnix_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelDownloadMKVToolnix.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.videohelp.com/software/MKVtoolnix");
        }

        private void linkLabelYandexAPIKeysList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelYandexAPIKeysList.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tech.yandex.ru/keys/");
        }

        private void buttonRemoveHI_Click(object sender, EventArgs e)
        {

        }

        private void buttonHelpOnSettingsPanel_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://0xothik.wordpress.com/bilingual-subtitler/");
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://0xothik.wordpress.com/bilingual-subtitler/");
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            FormAbout formAbout = new FormAbout();
            formAbout.ShowDialog();
        }
    }
}
