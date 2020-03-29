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
    public enum SubtitlesType
    {
        Original,
        FirstRussian,
        SecondRussian,
        ThirdRussian
    }

    public partial class MainForm : Form
    {
        public class SubtitlesInfo
        {
            public Subtitle[] Subtitles;

            public SubtitlesBackgroundWorker BackgroundWorker
            {
                get;
                private set;
            }

            public ProgressBar ProgressBar { get; private set; }
            public Label ProgressLabel { get; private set; }
            public Button ButtonOpen { get; private set; }
            public Button ButtonTranslate { get; private set; }

            public Label ActionLabel { get; private set; }
            public TextBox OutputTextBox { get; private set; }

            public int TrackNumber;
            public string TrackLanguage;
            public string TrackName;


            public SubtitlesInfo(ProgressBar progressBar, Label progressLabel, Button buttonOpen, Button buttonTranslate,
               Label actionLabel, TextBox outputTextBox)
            {
                ProgressBar = progressBar;
                ProgressLabel = progressLabel;
                ButtonOpen = buttonOpen;
                ButtonTranslate = buttonTranslate;
                ActionLabel = actionLabel;
                OutputTextBox = outputTextBox;
            }

            public void SetBackgroundWorker(SubtitlesBackgroundWorker backgroundWorker, SubtitlesType subtitlesType)
            {
                BackgroundWorker = backgroundWorker;
                BackgroundWorker.SubtitlesType = subtitlesType;
            }
        }

        enum VideoState
        {
            Playing,
            Paused
        }

        enum SubtitlesState
        {
            Original,
            Bilingual
        }

        private const string SUBTITLES_ARE_OPENING = "Субтитры считываются...";
        private const string SUBTITLES_ARE_OPENED = "Субтитры считаны!";
        private const string SUBTITLES_ARE_TRANSLATING = "Субтитры переводятся...";
        private const string SUBTITLES_ARE_TRANSLATED = "Субтитры переведены!";

        private Dictionary<SubtitlesType, SubtitlesInfo> m_subtitles;

        private Translator m_translator;

        private KeyboardHookManager m_keyboardHookManager;
        private InputSimulator m_inputSimulator;

        private VideoState m_videoState;
        private SubtitlesState m_subtitlesState;
        private ComboboxItem m_videoPlayingComboBoxItem = new ComboboxItem
        { Text = "воспроизводится", Value = VideoState.Playing };
        private ComboboxItem m_videoPausedComboBoxItem = new ComboboxItem
        { Text = "на паузе", Value = VideoState.Paused };
        private ComboboxItem m_subtitlesOriginalSubtitlesComboBoxItem = new ComboboxItem
        { Text = "оригинальными субтитрами", Value = SubtitlesState.Original };
        private ComboboxItem m_subtitlesBilingualSubtitlesComboBoxItem = new ComboboxItem
        { Text = "двуязычными субтитрами", Value = SubtitlesState.Bilingual };

        private Dictionary<VideoState, ComboboxItem> m_videoStatesAndRelatedComboBoxItems;
        private Dictionary<SubtitlesState, ComboboxItem> m_subtitlesStatesAndRelatedComboBoxItems;

        private MainForm m_mainForm;
        private List<Button> m_buttons;
        private Color m_previousButtonColor;

        private delegate void AddListItem();
        private AddListItem MyDelegate;

        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;

        private byte[] m_shiftState;
        private Process m_videoPlayerProcess;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        static extern bool SetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);


        public MainForm()
        {
            InitializeComponent();

            videoStateComboBox.Items.Add(m_videoPlayingComboBoxItem);
            videoStateComboBox.Items.Add(m_videoPausedComboBoxItem);
            videoStateComboBox.SelectedIndex = 0;

            subtitlesStateComboBox.Items.Add(m_subtitlesOriginalSubtitlesComboBoxItem);
            subtitlesStateComboBox.Items.Add(m_subtitlesBilingualSubtitlesComboBoxItem);
            subtitlesStateComboBox.SelectedIndex = 0;

            m_videoStatesAndRelatedComboBoxItems = new Dictionary<VideoState, ComboboxItem>
            {
                {VideoState.Playing, m_videoPlayingComboBoxItem},
                { VideoState.Paused, m_videoPausedComboBoxItem}
            };

            m_subtitlesStatesAndRelatedComboBoxItems = new Dictionary<SubtitlesState, ComboboxItem>
            {
                {SubtitlesState.Original, m_subtitlesOriginalSubtitlesComboBoxItem},
                { SubtitlesState.Bilingual, m_subtitlesBilingualSubtitlesComboBoxItem}
            };

            m_subtitles = new Dictionary<SubtitlesType, SubtitlesInfo>
            {
                {
                    SubtitlesType.Original, new SubtitlesInfo(
                        primarySubtitlesProgressBar, primarySubtitlesProgressLabel,
                        openPrimarySubtitlesButton, null,
                        primarySubtitlesActionLabel, primarySubtitlesTextBox)
                },
                {
                    SubtitlesType.FirstRussian, new SubtitlesInfo(
                        firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel,
                        openFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton,
                        firstRussianSubtitlesActionLabel, firstRussianSubtitlesTextBox)
                },
                {
                    SubtitlesType.SecondRussian, new SubtitlesInfo(
                        secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel,
                        openSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton,
                        secondRussianSubtitlesActionLabel, secondRussianSubtitlesTextBox)
                },
                {
                    SubtitlesType.ThirdRussian, new SubtitlesInfo(
                        thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel,
                        openThirdRussianSubtitlesButton, translateToThirdRussianSubtitlesButton,
                        thirdRussianSubtitlesActionLabel, thirdRussianSubtitlesTextBox)
                }
            };

            m_buttons = new List<Button>
            {
                openPrimarySubtitlesButton,
                openFirstRussianSubtitlesButton,
                openSecondRussianSubtitlesButton,
                openThirdRussianSubtitlesButton,
                translateToPrimarySubtitlesButton,
                translateToFirstRussianSubtitlesButton,
                translateToSecondRussianSubtitlesButton,
                translateToThirdRussianSubtitlesButton
            };

            foreach (var button in m_buttons)
            {
                button.MouseEnter += button_MouseEnter;
                button.MouseLeave += button_MouseLeave;
            }

            m_inputSimulator = new InputSimulator();

            m_keyboardHookManager = new KeyboardHookManager();
            m_keyboardHookManager.Start();

            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.SPACE, ActionForHotkeyThatArePauseButton);

            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.UP, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.DOWN, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.LEFT, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.RIGHT, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.CONTROL, ActionForHotkeyThatAreNotPauseButton);
            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.NUMPAD0, ActionForHotkeyThatAreNotPauseButton);

            m_mainForm = this;
            MyDelegate = new AddListItem(ChangeVideoAndSubtitlesComboBoxes);

            primarySubtitlesColorButton.BackColor = Properties.Settings.Default.PrimarySubtitlesColor;
            firstRussianSubtitlesColorButton.BackColor = Properties.Settings.Default.FirstRussianSubtitlesColor;
            secondRussianSubtitlesColorButton.BackColor = Properties.Settings.Default.SecondRussianSubtitlesColor;
            thirdRussianSubtitlesColorButton.BackColor = Properties.Settings.Default.ThirdRussianSubtitlesColor;

            //m_shiftState = File.ReadAllBytes("C:\\Users\\jenek\\source\\repos\\0xotHik\\" +
            //                   "BilingualSubtitler\\BilingualSubtitler\\bin\\Debug\\shiftDown.dat");

        }

        private void ActionForHotkeyThatAreNotPauseButton()
        {
            if (GetActiveProcessName() != "mpc-hc64")
                return;

            //m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);

            m_videoPlayerProcess = Process.GetProcessesByName("mpc-hc64")[0];
            PostMessage(m_videoPlayerProcess.MainWindowHandle, WM_KEYDOWN, (int)VirtualKeyCode.SPACE, 0);
            SwitchSubtitles();
        }

        private void ActionForHotkeyThatArePauseButton()
        {
            if (GetActiveProcessName() != "mpc-hc64")
                return;

            SwitchSubtitles();
        }

        private void SwitchSubtitles()
        {
            // Я так понимаю, сюда мы попадаем еще до переключения паузы/воспроизведения
            if (m_videoState == VideoState.Playing)
            {
                if (m_subtitlesState == SubtitlesState.Original)
                {
                    // Переключаемся на двуязычные

                    //m_inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_S);

                    PostMessage(m_videoPlayerProcess.MainWindowHandle, WM_KEYDOWN, (int)VirtualKeyCode.VK_S, 0);

                    m_subtitlesState = SubtitlesState.Bilingual;
                }

                // Ставим Paused в КомбоБоксе
                m_videoState = VideoState.Paused;
            }
            else
            {
                if (m_subtitlesState == SubtitlesState.Bilingual)
                {
                    // Переключаемся на оригинальные

                    m_inputSimulator.Keyboard.ModifiedKeyStroke(
                        VirtualKeyCode.SHIFT,
                        VirtualKeyCode.VK_S);

                    //Process process = Process.GetProcessesByName("mpc-hc64")[0];

                    ////PostMessage(process.MainWindowHandle, WM_KEYDOWN, (int)VirtualKeyCode.VK_D, 0);
                    //SetKeyboardState(m_shiftState); // set stored keyboard state
                    //PostMessage(process.MainWindowHandle, WM_KEYDOWN, (int)VirtualKeyCode.VK_S, 0);

                    m_subtitlesState = SubtitlesState.Original;
                }

                // Ставим Playing в КомбоБоксе
                m_videoState = VideoState.Playing;
            }

            m_mainForm.BeginInvoke(MyDelegate);
        }

        private void ChangeVideoAndSubtitlesComboBoxes()
        {
            videoStateComboBox.SelectedValueChanged -= videoStateComboBox_SelectedValueChanged;
            subtitlesStateComboBox.SelectedValueChanged -= subtitlesStateComboBox_SelectedValueChanged;

            videoStateComboBox.SelectedItem = m_videoStatesAndRelatedComboBoxItems[m_videoState];
            subtitlesStateComboBox.SelectedItem = m_subtitlesStatesAndRelatedComboBoxItems[m_subtitlesState];

            videoStateComboBox.SelectedValueChanged += videoStateComboBox_SelectedValueChanged;
            subtitlesStateComboBox.SelectedValueChanged += subtitlesStateComboBox_SelectedValueChanged;
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

            // [Script Info]
            // ; This is an Advanced Sub Station Alpha v4+script.
            //    Title: 
            // ScriptType: v4.00 +
            //    Collisions: Normal
            // PlayDepth: 0

            //    [V4 + Styles]
            // Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
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

            // Style: Default,Arial,20,&H00FFFFFF,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,10,1
            // Style: Копировать из Default,Arial,20,&H00C26F03,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,55,1
            // Style: Копировать из Копировать из Default,Arial,20,&H000C15DC,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,100,1
            var subtitleStyleNamePostfix = " sub stream";

            for (int i = 0; i < subtitlesColorPairs.Length; i++)
            {
                var color = subtitlesColorPairs[i].Item2;
                var transparency = i == 0 ? "00" : "64";
                var marginV = i == 3 ? 0 
                    : 45 + i * (2 * 20 + 5);

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
                    // Отсуп снизу
                    $"{marginV}," +
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
                if (subtitles != null)
                {
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
            }

            return assSB;
        }

        private void StartYandexTranslateSubtitles(SubtitlesType subtitlesType)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];

            var yandexTranslateSubtitlesBackgroundWorker = new SubtitlesBackgroundWorker();
            yandexTranslateSubtitlesBackgroundWorker.DoWork += YandexTranslateSubtitles;
            yandexTranslateSubtitlesBackgroundWorker.WorkerReportsProgress = true;
            yandexTranslateSubtitlesBackgroundWorker.ProgressChanged += yandexTranslateSubtitlesBackgroundWorker_ProgressChanged;
            yandexTranslateSubtitlesBackgroundWorker.RunWorkerCompleted += yandexTranslateSubtitlesBackgroundWorker_RunWorkerCompleted;

            subtitlesInfo.SetBackgroundWorker(yandexTranslateSubtitlesBackgroundWorker, subtitlesType);

            subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Minimum;
            subtitlesInfo.ProgressLabel.Text = $"0%";
            subtitlesInfo.ButtonOpen.Enabled = false;
            if (subtitlesInfo.ButtonTranslate != null)
                subtitlesInfo.ButtonTranslate.Enabled = false;
            subtitlesInfo.ActionLabel.Text = SUBTITLES_ARE_TRANSLATING;

            yandexTranslateSubtitlesBackgroundWorker.RunWorkerAsync();
        }

        private void YandexTranslateSubtitles(object sender, DoWorkEventArgs eventArgs)
        {
            var originalSubtitles = m_subtitles[SubtitlesType.Original].Subtitles;

            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            subtitlesInfo.Subtitles = new Subtitle[originalSubtitles.Length];

            for (int i = 0; i < originalSubtitles.Length; i++)
            {
                subtitlesInfo.Subtitles[i] = new Subtitle
                (originalSubtitles[i].Start,
                    originalSubtitles[i].End,
                    YandexTranslateAStringWithChecking(originalSubtitles[i].Text, m_translator)
                );

                parentBgW.ReportProgress(100 * i / originalSubtitles.Length);
            }
        }

        private void yandexTranslateSubtitlesBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs eventArgs)
        {
            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            subtitlesInfo.ProgressBar.Value = eventArgs.ProgressPercentage;
            subtitlesInfo.ProgressLabel.Text = $"{eventArgs.ProgressPercentage}%";
        }

        private void yandexTranslateSubtitlesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eventArgs)
        {
            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Maximum;
            subtitlesInfo.ProgressLabel.Text = $"100%";
            subtitlesInfo.ButtonOpen.Enabled = true;
            if (subtitlesInfo.ButtonTranslate != null)
                subtitlesInfo.ButtonTranslate.Enabled = true;

            subtitlesInfo.ActionLabel.Text = SUBTITLES_ARE_TRANSLATED;
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
            firstRussianSubtitlesActionLabel.Visible = firstRussianSubtitlesProgressLabel.Visible = true;
            StartYandexTranslateSubtitles(SubtitlesType.FirstRussian);

            //StartYandexTranslateSubtitles(m_originalSubtitles, m_firstRussianSubtitles,
            //    firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel, firstRussianSubtitlesActionLabel,
            //    firstRussianSubtitlesTextBox, openFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            secondRussianSubtitlesActionLabel.Visible = secondRussianSubtitlesProgressLabel.Visible = true;
            StartYandexTranslateSubtitles(SubtitlesType.SecondRussian);

            //StartYandexTranslateSubtitles(m_originalSubtitles, m_secondRussianSubtitles,
            //    secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel, secondRussianSubtitlesActionLabel,
            //    secondRussianSubtitlesTextBox, openSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            thirdRussianSubtitlesActionLabel.Visible = thirdRussianSubtitlesProgressLabel.Visible = true;
            StartYandexTranslateSubtitles(SubtitlesType.ThirdRussian);

            //StartYandexTranslateSubtitles(m_originalSubtitles, m_thirdRussianSubtitles,
            //    thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel, thirdRussianSubtitlesActionLabel,
            //    thirdRussianSubtitlesTextBox, openThirdRussianSubtitlesButton, translateToThirdRussianSubtitlesButton);
        }

        private void OpenFileAndReadSubtitlesFromFile(SubtitlesType subtitlesType)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];

            var openFileDialog = new OpenFileDialog();
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var readSubtitlesBackgroundWorker = new SubtitlesBackgroundWorker { WorkerReportsProgress = true };
                readSubtitlesBackgroundWorker.DoWork += readSubtitlesBackgroundWorker_DoWork;
                readSubtitlesBackgroundWorker.ProgressChanged += readSubtitlesBackgroundWorker_ProgressChanged;
                readSubtitlesBackgroundWorker.RunWorkerCompleted += readSubtitlesBackgroundWorker_RunWorkerCompleted;

                subtitlesInfo.SetBackgroundWorker(readSubtitlesBackgroundWorker, subtitlesType);

                subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Minimum;
                subtitlesInfo.ProgressLabel.Text = $"0%";
                subtitlesInfo.ButtonOpen.Enabled = false;
                if (subtitlesInfo.ButtonTranslate != null)
                    subtitlesInfo.ButtonTranslate.Enabled = false;
                subtitlesInfo.ActionLabel.Text = SUBTITLES_ARE_OPENING;

                if (subtitlesType == SubtitlesType.Original)
                {
                    var originalSubtitlesFileFI = new FileInfo(openFileDialog.FileName);
                    var extension = originalSubtitlesFileFI.Extension;
                    var originalFilePathPart = originalSubtitlesFileFI.FullName.Substring(0,
                        originalSubtitlesFileFI.FullName.Length - 
                       (extension.Length - 1));

                    finalSubtitlesFilesPathBeginningRichTextBox.Text = originalFilePathPart;
                }

                subtitlesInfo.BackgroundWorker.RunWorkerAsync(openFileDialog.FileName);
            }

        }

        private void readSubtitlesBackgroundWorker_DoWork(object sender, DoWorkEventArgs eventArgs)
        {
            var filePath = (string)eventArgs.Argument;

            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            var sourceFileFI = new FileInfo(filePath);
            var extension = sourceFileFI.Extension;

            m_translator = new Translator(Properties.Settings.Default.YandexTranslatorAPIKey);

            switch (extension)
            {
                case ".srt":
                    {
                        subtitlesInfo.Subtitles = ReadSRT(filePath);

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

                            subtitlesInfo.Subtitles = new Subtitle[mkvSubtitles.Count];
                            for (int i = 0; i < mkvSubtitles.Count; i++)
                            {
                                var currentMkvSubtitle = mkvSubtitles[i];

                                subtitlesInfo.Subtitles[i] = new Subtitle(currentMkvSubtitle.Start,
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
            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            subtitlesInfo.ProgressBar.Value = eventArgs.ProgressPercentage;
            subtitlesInfo.ProgressLabel.Text = $"{eventArgs.ProgressPercentage}%";
        }

        private void readSubtitlesBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eventArgs)
        {
            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Maximum;
            subtitlesInfo.ProgressLabel.Text = $"100%";
            subtitlesInfo.ButtonOpen.Enabled = true;
            if (subtitlesInfo.ButtonTranslate != null)
                subtitlesInfo.ButtonTranslate.Enabled = true;

            subtitlesInfo.ActionLabel.Text = SUBTITLES_ARE_OPENED;

            // TODO Ошибки?
        }

        private void createOriginalAndBilingualSubtitlesFilesButton_Click(object sender, EventArgs e)
        {
            var originalSubtitles = m_subtitles[SubtitlesType.Original].Subtitles;
            var firstRussianSubtitles = m_subtitles[SubtitlesType.FirstRussian].Subtitles;
            var secondRussianSubtitles = m_subtitles[SubtitlesType.SecondRussian].Subtitles;
            var thirdRussianSubtitles = m_subtitles[SubtitlesType.ThirdRussian].Subtitles;


            var ass = GenerateASSMarkedupDocument(new Tuple<Subtitle[], Color>[]
            {
                new Tuple<Subtitle[], Color>(originalSubtitles, Color.White),
            });
            File.WriteAllText(finalSubtitlesFilesPathBeginningRichTextBox.Text + originalSubtitlesPathTextBox.Text, ass.ToString());

            List<Tuple<Subtitle[], Color>> listSubsPairs = new List<Tuple<Subtitle[], Color>>
            {
                new Tuple<Subtitle[], Color>(originalSubtitles, primarySubtitlesColorButton.BackColor)
            };
            // if (firstRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(firstRussianSubtitles, firstRussianSubtitlesColorButton.BackColor));
            // if (secondRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(secondRussianSubtitles, secondRussianSubtitlesColorButton.BackColor));
            // if (thirdRussianSubtitles != null)
                listSubsPairs.Add(new Tuple<Subtitle[], Color>(thirdRussianSubtitles, thirdRussianSubtitlesColorButton.BackColor));

            ass = GenerateASSMarkedupDocument(listSubsPairs.ToArray());
            File.WriteAllText(finalSubtitlesFilesPathBeginningRichTextBox.Text + bilingualSubtitlesPathTextBox.Text, ass.ToString());

            Properties.Settings.Default.PrimarySubtitlesColor = primarySubtitlesColorButton.BackColor;
            Properties.Settings.Default.FirstRussianSubtitlesColor = firstRussianSubtitlesColorButton.BackColor;
            Properties.Settings.Default.SecondRussianSubtitlesColor = secondRussianSubtitlesColorButton.BackColor;
            Properties.Settings.Default.ThirdRussianSubtitlesColor = thirdRussianSubtitlesColorButton.BackColor;
            Properties.Settings.Default.Save();
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
            primarySubtitlesActionLabel.Visible = primarySubtitlesProgressLabel.Visible = true;
            OpenFileAndReadSubtitlesFromFile(SubtitlesType.Original);

            //OpenFileAndReadSubtitlesFromFile(ref m_originalSubtitles,
            //    primarySubtitlesProgressBar, primarySubtitlesProgressLabel, primarySubtitlesActionLabel, primarySubtitlesTextBox,
            //    openPrimarySubtitlesButton);
        }

        private void openFirstRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            firstRussianSubtitlesActionLabel.Visible = firstRussianSubtitlesProgressLabel.Visible = true;
            OpenFileAndReadSubtitlesFromFile(SubtitlesType.FirstRussian);
            //OpenFileAndReadSubtitlesFromFile(ref m_firstRussianSubtitles,
            //    firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel, firstRussianSubtitlesActionLabel,
            //    firstRussianSubtitlesTextBox, openFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton);
        }

        private void openSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            secondRussianSubtitlesActionLabel.Visible = secondRussianSubtitlesProgressLabel.Visible = true;
            OpenFileAndReadSubtitlesFromFile(SubtitlesType.SecondRussian);

            //OpenFileAndReadSubtitlesFromFile(ref m_secondRussianSubtitles,
            //    secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel, secondRussianSubtitlesActionLabel,
            //    secondRussianSubtitlesTextBox, openSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton);
        }

        private void openThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            thirdRussianSubtitlesActionLabel.Visible = thirdRussianSubtitlesProgressLabel.Visible = true;
            OpenFileAndReadSubtitlesFromFile(SubtitlesType.ThirdRussian);

            //OpenFileAndReadSubtitlesFromFile(ref m_thirdRussianSubtitles,
            //    thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel, thirdRussianSubtitlesActionLabel,
            //    thirdRussianSubtitlesTextBox, openThirdRussianSubtitlesButton, translateToThirdRussianSubtitlesButton);
        }

        private void videoStateComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_videoState = (VideoState)((ComboboxItem)((ComboBox)sender).SelectedItem).Value;
        }

        private void subtitlesStateComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_subtitlesState = (SubtitlesState)((ComboboxItem)((ComboBox)sender).SelectedItem).Value;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var keySettingForm = new KeySettingForm();
            keySettingForm.Show();
        }
    }
    public class SubtitlesBackgroundWorker : BackgroundWorker
    {
        public SubtitlesType SubtitlesType;
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
