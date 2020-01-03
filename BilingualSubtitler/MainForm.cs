using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Nikse.SubtitleEdit.Core.ContainerFormats.Matroska;
using NonInvasiveKeyboardHookLibrary;
using YandexLinguistics.NET;
using MessageBox = System.Windows.Forms.MessageBox;

namespace BilingualSubtitler
{
    public partial class MainForm : Form
    {
        public class BackgroundWorkerRigging
        {
            public Subtitle[] Target;
            public ProgressBar ProgressBar { get; private set; }
            public Label ProgressLabel { get; private set; }
            public Button ButtonOpen { get; private set; }
            public Button ButtonTranslate { get; private set; }

            public Label ActionLabel { get; private set; }
            public TextBox OutputTextBox { get; private set; }

            public int TrackNumber;
            public string TrackLanguage;
            public string TrackName;


            public BackgroundWorkerRigging(ref Subtitle[] target,
               ProgressBar progressBar, Label progressLabel, Button buttonOpen, Button buttonTranslate,
               Label actionLabel, TextBox outputTextBox)
            {
                Target = target;
                ProgressBar = progressBar;
                ProgressLabel = progressLabel;
                ButtonOpen = buttonOpen;
                ButtonTranslate = buttonTranslate;
                ActionLabel = actionLabel;
                OutputTextBox = outputTextBox;
            }
        }

        enum VideoState
        {
            PlayingWithOriginalSubtitles,
            PausedWithBilingualSubtitles
        }

        private const string SUBTITLES_ARE_OPENING = "Субтитры считываются...";
        private const string SUBTITLES_ARE_OPENED = "Субтитры считаны!";
        private const string SUBTITLES_ARE_TRANSLATING = "Субтитры переводятся...";
        private const string SUBTITLES_ARE_TRANSLATED = "Субтитры переведены!";

        private Subtitle[] m_originalSubtitles;
        private Subtitle[] m_firstRussianSubtitles;
        private Subtitle[] m_secondRussianSubtitles;
        private Subtitle[] m_thirdRussianSubtitles;

        private Dictionary<BackgroundWorker, BackgroundWorkerRigging> m_workers
            = new Dictionary<BackgroundWorker, BackgroundWorkerRigging>(4);


        private Translator m_translator;
        private KeyboardHookManager m_keyboardHookManager = new KeyboardHookManager();
        private InputSimulator m_inputSimulator;
        private VideoState m_videoState = VideoState.PlayingWithOriginalSubtitles;

        private string formTitle = "Bilingual Subtitler";

        private List<Button> m_buttons;
        private Color m_previousButtonColor;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public MainForm()
        {
            InitializeComponent();

            //openFileDialog.Filter = "Файлы субтитров SubRip|*.srt";

            m_buttons = new List<Button> { openFirstRussianSubtitlesButton, translatePrimarySubtitlesButton };

            foreach (var button in m_buttons)
            {
                button.MouseEnter += button_MouseEnter;
                button.MouseLeave += button_MouseLeave;
            }

            m_keyboardHookManager.Start();
            m_inputSimulator = new InputSimulator();

            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.SPACE, ActionForHotkeyThatArePauseButton);

            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.UP, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.DOWN, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.LEFT, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.RIGHT, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.CONTROL, ActionForHotkeyThatAreNotPauseButton);

        }

        private void ActionForHotkeyThatAreNotPauseButton()
        {
            if (GetActiveProcessName() != "mpc-hc64")
                return;

            m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
        }

        private void ActionForHotkeyThatArePauseButton()
        {
            if (GetActiveProcessName() != "mpc-hc64")
                return;

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
        }

        string GetActiveProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }


        private Subtitle[] ReadSRT(string pathToSRTFile)
        {
            var subsLines = 0;
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
                var transparency = i == 0 ? "00" : "64";

                assSB.AppendLine(
                    $"Style: {i}{subtitleStyleNamePostfix}," +
                    $"Arial," +
                    $"20," +
                    $"&H" +
                    $"{transparency}" +
                    $"{color.B.ToString("X2")}" +
                    $"{color.G.ToString("X2")}" +
                    $"{color.R.ToString("X2")}," +
                    $"&H{transparency}00FFFF," +
                    $"&H{transparency}000000," +
                    $"&H{transparency}000000," +
                    $"0,0,0,0,100,100,0,0,1,2,1,2,10,10," +
                    $"{20 + i * (2 * 20 + 5)}," +
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

        private void StartYandexTranslateSubtitles(Subtitle[] originalSubtitles, Subtitle[] target,
            ProgressBar progressBar, Label progressLabel, Label actionLabel,
            TextBox outputTextBox, Button buttonOpen, Button buttonTranslate)
        {
            var yandexTranslateSubtitlesBackgroundWorker = new BackgroundWorker();
            yandexTranslateSubtitlesBackgroundWorker.DoWork += StartYandexTranslateSubtitles;
            yandexTranslateSubtitlesBackgroundWorker.WorkerReportsProgress = true;
            yandexTranslateSubtitlesBackgroundWorker.ProgressChanged += yandexTranslateSubtitlesBackgroundWorker_ProgressChanged;
            yandexTranslateSubtitlesBackgroundWorker.RunWorkerCompleted += yandexTranslateSubtitlesBackgroundWorker_RunWorkerCompleted;

            progressBar.Value = progressBar.Minimum;
            progressLabel.Text = $"0%";
            buttonOpen.Enabled = false;
            buttonTranslate.Enabled = false;
            actionLabel.Text = SUBTITLES_ARE_TRANSLATING;


            m_workers.Add(yandexTranslateSubtitlesBackgroundWorker,
                new BackgroundWorkerRigging
                    (ref target, progressBar, progressLabel, buttonOpen, buttonTranslate, actionLabel, outputTextBox));

            yandexTranslateSubtitlesBackgroundWorker.RunWorkerAsync(originalSubtitles);
        }

        private void StartYandexTranslateSubtitles(object sender, DoWorkEventArgs eventArgs)
        {
            var originalSubtitles = (Subtitle[])eventArgs.Argument;

            var parentBgW = (BackgroundWorker)sender;
            var bgWRigger = m_workers[parentBgW];

            bgWRigger.Target = new Subtitle[originalSubtitles.Length];

            for (int i = 0; i < originalSubtitles.Length; i++)
            {
                bgWRigger.Target[i] = new Subtitle
                (originalSubtitles[i].Start,
                    originalSubtitles[i].End,
                    YandexTranslateAStringWithChecking(originalSubtitles[i].Text, m_translator)
                );

                parentBgW.ReportProgress(100 * i / originalSubtitles.Length);
            }
        }

        private void yandexTranslateSubtitlesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs eventArgs)
        {
            var parentBgW = (BackgroundWorker)sender;
            var bgWRigger = m_workers[parentBgW];

            bgWRigger.ProgressBar.Value = eventArgs.ProgressPercentage;
            bgWRigger.ProgressLabel.Text = $"{eventArgs.ProgressPercentage}%";
        }

        private void yandexTranslateSubtitlesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eventArgs)
        {
            var parentBgW = (BackgroundWorker)sender;
            var bgWRigger = m_workers[parentBgW];

            bgWRigger.ProgressBar.Value = bgWRigger.ProgressBar.Maximum;
            bgWRigger.ProgressLabel.Text = $"100%";
            bgWRigger.ButtonOpen.Enabled = true;
            if (bgWRigger.ButtonTranslate != null)
                bgWRigger.ButtonTranslate.Enabled = true;

            bgWRigger.ActionLabel.Text = SUBTITLES_ARE_TRANSLATED;
            // TODO Ошибки?
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


        private void button_MouseEnter(object sender, EventArgs e)
        {
            m_previousButtonColor = ((Button)sender).BackColor;
            ((Button)sender).BackColor = Color.Gold;

        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = m_previousButtonColor;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(m_originalSubtitles, m_firstRussianSubtitles,
                firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel, firstRussianSubtitlesActionLabel,
                firstRussianSubtitlesTextBox, openFirstRussianSubtitlesButton, translateFirstRussianSubtitlesButton);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(m_originalSubtitles, m_secondRussianSubtitles,
                secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel, secondRussianSubtitlesActionLabel,
                secondRussianSubtitlesTextBox, openSecondRussianSubtitlesButton, translateSecondRussianSubtitlesButton);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(m_originalSubtitles, m_thirdRussianSubtitles,
                thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel, thirdRussianSubtitlesActionLabel,
                thirdRussianSubtitlesTextBox, openThirdRussianSubtitlesButton, translateThirdRussianSubtitlesButton);
        }

        private void OpenFileAndReadSubtitlesFromFile(ref Subtitle[] target,
            ProgressBar progressBar, Label progressLabel, Label actionLabel,
            TextBox outputTextBox, Button buttonOpen, Button buttonTranslate = null)
        {
            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var readSubtitlesBackgroundWorker = new BackgroundWorker();
                readSubtitlesBackgroundWorker.DoWork += ReadSubtitles;
                readSubtitlesBackgroundWorker.WorkerReportsProgress = true;
                readSubtitlesBackgroundWorker.ProgressChanged += readSubtitlesBackgroundWorker_ProgressChanged;
                readSubtitlesBackgroundWorker.RunWorkerCompleted += readSubtitlesBackgroundWorker_RunWorkerCompleted;

                progressBar.Value = progressBar.Minimum;
                progressLabel.Text = $"0%";
                buttonOpen.Enabled = false;
                if (buttonTranslate != null)
                    buttonTranslate.Enabled = false;
                actionLabel.Text = SUBTITLES_ARE_OPENING;


                m_workers.Add(readSubtitlesBackgroundWorker,
                    new BackgroundWorkerRigging
                        (ref target, progressBar, progressLabel, buttonOpen, buttonTranslate, actionLabel, outputTextBox));

                readSubtitlesBackgroundWorker.RunWorkerAsync(openFileDialog.FileName);
            }

        }

        private void ReadSubtitles(object sender, DoWorkEventArgs eventArgs)
        {
            var filePath = (string)eventArgs.Argument;

            var parentBgW = (BackgroundWorker)sender;
            var bgWRigger = m_workers[parentBgW];

            Subtitle[] subtitles = null;
            var sourceFileFI = new FileInfo(filePath);
            var extension = sourceFileFI.Extension;

            m_translator = new Translator(Properties.Settings.Default.YandexTranslatorAPIKey);

            switch (extension)
            {
                case ".srt":
                    {
                        bgWRigger.Target = ReadSRT(filePath);

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
                                (position, total) =>
                                {
                                    parentBgW.ReportProgress((int)(100 * position / total));
                                });

                            bgWRigger.Target = new Subtitle[mkvSubtitles.Count];
                            for (int i = 0; i < mkvSubtitles.Count; i++)
                            {
                                var currentMkvSubtitle = mkvSubtitles[i];

                                bgWRigger.Target[i] = new Subtitle(currentMkvSubtitle.Start,
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
        }

        private void readSubtitlesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs eventArgs)
        {
            var parentBgW = (BackgroundWorker)sender;
            var bgWRigger = m_workers[parentBgW];

            bgWRigger.ProgressBar.Value = eventArgs.ProgressPercentage;
            bgWRigger.ProgressLabel.Text = $"{eventArgs.ProgressPercentage}%";
        }

        private void readSubtitlesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eventArgs)
        {
            var parentBgW = (BackgroundWorker)sender;
            var bgWRigger = m_workers[parentBgW];

            bgWRigger.ProgressBar.Value = bgWRigger.ProgressBar.Maximum;
            bgWRigger.ProgressLabel.Text = $"100%";
            bgWRigger.ButtonOpen.Enabled = true;
            if (bgWRigger.ButtonTranslate != null)
                bgWRigger.ButtonTranslate.Enabled = true;

            bgWRigger.ActionLabel.Text = SUBTITLES_ARE_OPENED;
            // TODO Ошибки?
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
                new Tuple<Subtitle[], Color>(m_originalSubtitles, primarySubtitlesColorButton.BackColor)
            };
            if (m_firstRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(m_firstRussianSubtitles, firstRussianSubtitlesColorButton.BackColor));
            if (m_secondRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(m_secondRussianSubtitles, secondRussianSubtitlesColorButton.BackColor));
            if (m_thirdRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(m_thirdRussianSubtitles, thirdRussianSubtitlesColorButton.BackColor));

            ass = GenerateASSMarkedupDocument(listSubsPairs.ToArray());
            File.WriteAllText(@"D:\Movies\!BS\xxxx.ruseng.ass", ass.ToString());
        }


        private void colorPickingButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;

            var colorPickingDialog = new ColorDialog();
            var dialogResult = colorPickingDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                senderButton.BackColor = colorPickingDialog.Color;
            }
        }

        private void openPrimarySubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFile(ref m_originalSubtitles,
                primarySubtitlesProgressBar, primarySubtitlesProgressLabel, primarySubtitlesActionLabel, primarySubtitlesTextBox,
                openPrimarySubtitlesButton);
        }

        private void openFirstRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFile(ref m_firstRussianSubtitles,
                firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel, firstRussianSubtitlesActionLabel,
                firstRussianSubtitlesTextBox, openFirstRussianSubtitlesButton, translateFirstRussianSubtitlesButton);
        }

        private void openSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFile(ref m_secondRussianSubtitles,
                secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel, secondRussianSubtitlesActionLabel,
                secondRussianSubtitlesTextBox, openSecondRussianSubtitlesButton, translateSecondRussianSubtitlesButton);
        }

        private void openThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFile(ref m_thirdRussianSubtitles,
                thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel, thirdRussianSubtitlesActionLabel,
                thirdRussianSubtitlesTextBox, openThirdRussianSubtitlesButton, translateThirdRussianSubtitlesButton);
        }
    }
}
