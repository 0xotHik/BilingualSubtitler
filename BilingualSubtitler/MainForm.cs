using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using Settings = BilingualSubtitler.Properties.Settings;
using VirtualKeyCode = WindowsInput.Native.VirtualKeyCode;

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
            public Button ButtonTranslateWordByWord { get; private set; }

            public Label ActionLabel { get; private set; }
            public TextBox OutputTextBox { get; private set; }

            public int TrackNumber;
            public string TrackLanguage;
            public string TrackName;


            public SubtitlesInfo(ProgressBar progressBar, Label progressLabel, Button buttonOpen, Button buttonTranslate, Button buttonTranslateWordByWord,
               Label actionLabel, TextBox outputTextBox)
            {
                ProgressBar = progressBar;
                ProgressLabel = progressLabel;
                ButtonOpen = buttonOpen;
                ButtonTranslate = buttonTranslate;
                ButtonTranslateWordByWord = buttonTranslateWordByWord;
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

        private int m_changeSubtitlesToBilingualHotkeyCode;
        private VirtualKeyCode? m_changeSubtitlesToBilingualHotkeyModifierKeyVirtualKeyCode;
        private VirtualKeyCode? m_changeSubtitlesToBilingualHotkeyVirtualKeyCode;
        private int m_changeSubtitlesToOriginalHotkeyCode;
        private VirtualKeyCode? m_changeSubtitlesToOriginalHotkeyModifierKeyVirtualKeyCode;
        private VirtualKeyCode? m_changeSubtitlesToOriginalHotkeyVirtualKeyCode;

        private int m_videoplayerPauseHotkey;

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

        private List<Button> m_buttons;
        private Color m_previousButtonColor;

        private delegate void ChangeVideoAndSubtitlesComboBoxes();
        private ChangeVideoAndSubtitlesComboBoxes m_changeVideoAndSubtitlesComboBoxesDelegate;

        private delegate void ChangeSubtitlesToBilingual();
        private ChangeSubtitlesToBilingual m_changeSubtitlesToBilingualDelegate;

        private delegate void ChangeSubtitlesToOriginal();
        private ChangeSubtitlesToOriginal m_changeSubtitlesToOriginalDelegate;


        const UInt32 WM_KEYDOWN = 0x0100;

        private string m_videoPlayerProcessName;
        private IntPtr m_videoPlayerProcessMainWindowHandle;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);


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
                        openPrimarySubtitlesButton, null, null,
                        primarySubtitlesActionLabel, primarySubtitlesTextBox)
                },
                {
                    SubtitlesType.FirstRussian, new SubtitlesInfo(
                        firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel,
                        openFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton, translateWordByWordToFirstRussianSubtitlesButton,
                        firstRussianSubtitlesActionLabel, firstRussianSubtitlesTextBox)
                },
                {
                    SubtitlesType.SecondRussian, new SubtitlesInfo(
                        secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel,
                        openSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton, translateWordByWordToSecondRussianSubtitlesButton,
                        secondRussianSubtitlesActionLabel, secondRussianSubtitlesTextBox)
                },
                {
                    SubtitlesType.ThirdRussian, new SubtitlesInfo(
                        thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel,
                        openThirdRussianSubtitlesButton, translateToThirdRussianSubtitlesButton, translateWordByWordToThirdRussianSubtitlesButton,
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
                translateToThirdRussianSubtitlesButton,
                createOriginalAndBilingualSubtitlesFilesButton,
                settingsButton
            };

            //foreach (var button in m_buttons)
            //{
            //    button.MouseEnter += button_MouseEnter;
            //    button.MouseLeave += button_MouseLeave;
            //}

            m_inputSimulator = new InputSimulator();

            m_keyboardHookManager = new KeyboardHookManager();
            m_keyboardHookManager.Start();

            m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.SPACE, ActionForHotkeyThatArePauseButton);

            //Properties.Settings.Default.Hotkeys = new StringCollection
            //{
            //    $"UP@{(int) VirtualKeyCode.UP}",
            //    $"DOWN@{(int) VirtualKeyCode.DOWN}",
            //    $"LEFT@{(int) VirtualKeyCode.LEFT}",
            //    $"RIGHT@{(int) VirtualKeyCode.RIGHT}",
            //    $"CONTROL@{(int) VirtualKeyCode.CONTROL}",
            //    $"NUMPAD0@{(int) VirtualKeyCode.NUMPAD0}",
            //    $"SUBTRACT@{(int) VirtualKeyCode.SUBTRACT}",
            //    $"SUBTRACT@{(int) VirtualKeyCode.ADD}",
            //    $"SUBTRACT@{(int) VirtualKeyCode.RETURN}"
            //};
            //Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString = new Hotkey(VirtualKeyCode.VK_S).ToString();
            //Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString = new Hotkey(VirtualKeyCode.VK_S, VirtualKeyCode.SHIFT).ToString();
            //Settings.Default.VideoPlayerPauseButtonString = new Hotkey(VirtualKeyCode.SPACE).ToString();

            //Properties.Settings.Default.Save();

            SetProgramSettings();

            m_changeVideoAndSubtitlesComboBoxesDelegate = ChangeVideoAndSubtitlesComboBoxesHandler;

            //m_shiftState = File.ReadAllBytes("C:\\Users\\jenek\\source\\repos\\0xotHik\\" +
            //                   "BilingualSubtitler\\BilingualSubtitler\\bin\\Debug\\shiftDown.dat");

        }

        private void SetProgramSettings()
        {
            // Хоткеи программы
            m_keyboardHookManager.Stop();
            m_keyboardHookManager.UnregisterAll();
            //
            var videoPlayerPauseHotkey = new Hotkey(Settings.Default.VideoPlayerPauseButtonString);
            foreach (var hotkeyString in Properties.Settings.Default.Hotkeys)
            {
                var hotkey = new Hotkey(hotkeyString);

                if (hotkey.KeyCode != videoPlayerPauseHotkey.KeyCode)
                    m_keyboardHookManager.RegisterHotkey(hotkey.KeyCode, ActionForHotkeyThatAreNotPauseButton);
                else
                    m_keyboardHookManager.RegisterHotkey(hotkey.KeyCode, ActionForHotkeyThatArePauseButton);
            }
            m_keyboardHookManager.Start();

            // Хоткеи видеоплеера
            var videoPlayerChangeToBilingualSubtitlesHotkey = new Hotkey(Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString);
            var videoPlayerChangeToOriginalSubtitlesHotkey = new Hotkey(Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString);
            //
            switch (videoPlayerChangeToBilingualSubtitlesHotkey.ModifierKey)
            {
                case null:
                    {
                        m_changeSubtitlesToBilingualHotkeyCode = videoPlayerChangeToBilingualSubtitlesHotkey.KeyCode;
                        m_changeSubtitlesToBilingualDelegate = ChangeSubtitlesToBilingualByPostMessage;

                        m_changeSubtitlesToBilingualHotkeyVirtualKeyCode = null;
                        m_changeSubtitlesToBilingualHotkeyModifierKeyVirtualKeyCode = null;
                        break;
                    }
                default:
                    {
                        m_changeSubtitlesToBilingualHotkeyVirtualKeyCode = (VirtualKeyCode)videoPlayerChangeToBilingualSubtitlesHotkey.KeyCode;
                        m_changeSubtitlesToBilingualHotkeyModifierKeyVirtualKeyCode = videoPlayerChangeToBilingualSubtitlesHotkey.ModifierKey;

                        m_changeSubtitlesToBilingualDelegate = ChangeSubtitlesToBilingualByInputSimulator;
                        m_changeSubtitlesToBilingualHotkeyCode = -1;
                        break;
                    }
            }

            switch (videoPlayerChangeToOriginalSubtitlesHotkey.ModifierKey)
            {
                case null:
                    {
                        m_changeSubtitlesToOriginalHotkeyCode = videoPlayerChangeToOriginalSubtitlesHotkey.KeyCode;
                        m_changeSubtitlesToOriginalDelegate = ChangeSubtitlesToOriginalByPostMessage;

                        m_changeSubtitlesToOriginalHotkeyVirtualKeyCode = null;
                        m_changeSubtitlesToOriginalHotkeyModifierKeyVirtualKeyCode = null;
                        break;
                    }
                default:
                    {
                        m_changeSubtitlesToOriginalHotkeyVirtualKeyCode = (VirtualKeyCode)videoPlayerChangeToOriginalSubtitlesHotkey.KeyCode;
                        m_changeSubtitlesToOriginalHotkeyModifierKeyVirtualKeyCode = videoPlayerChangeToOriginalSubtitlesHotkey.ModifierKey;

                        m_changeSubtitlesToOriginalDelegate = ChangeSubtitlesToOriginalByInputSimulator;
                        m_changeSubtitlesToOriginalHotkeyCode = -1;
                        break;
                    }
            }
            //
            m_videoplayerPauseHotkey = new Hotkey(Settings.Default.VideoPlayerPauseButtonString).KeyCode;


            primarySubtitlesColorButton.BackColor = Properties.Settings.Default.PrimarySubtitlesColor;
            firstRussianSubtitlesColorButton.BackColor = Properties.Settings.Default.FirstRussianSubtitlesColor;
            secondRussianSubtitlesColorButton.BackColor = Properties.Settings.Default.SecondRussianSubtitlesColor;
            thirdRussianSubtitlesColorButton.BackColor = Properties.Settings.Default.ThirdRussianSubtitlesColor;

            originalSubtitlesFileNameEnding.Text = Properties.Settings.Default.OriginalSubtitlesFileNameEnding;
            bilingualSubtitlesFileNameEnding.Text = Properties.Settings.Default.BilingualSubtitlesFileNameEnding;

            originalSubtitlesFileNameEnding.Visible =
                originalSubtitlesFileNameEndingLabel.Visible =
                    Settings.Default.CreateOriginalSubtitlesFile;

            m_videoPlayerProcessName = Properties.Settings.Default.VideoPlayerProcessName;

            secondRussianSubtitlesGroupBox.Visible = hideSecondRussianSubtitlesButton.Visible =
                Settings.Default.SecondRussianSubtitlesIsVisible;
            showSecondRussianSubtitlesButton.Visible = !Settings.Default.SecondRussianSubtitlesIsVisible;

            thirdRussianSubtitlesGroupBox.Visible = hideThirdRussianSubtitlesButton.Visible =
                Settings.Default.ThirdRussianSubtitlesIsVisible;
            showThirdRussianSubtitlesButton.Visible = !Settings.Default.ThirdRussianSubtitlesIsVisible;

        }

        private void ChangeVideoAndSubtitlesComboBoxesHandler()
        {
            videoStateComboBox.SelectedValueChanged -= videoStateComboBox_SelectedValueChanged;
            subtitlesStateComboBox.SelectedValueChanged -= subtitlesStateComboBox_SelectedValueChanged;

            videoStateComboBox.SelectedItem = m_videoStatesAndRelatedComboBoxItems[m_videoState];
            subtitlesStateComboBox.SelectedItem = m_subtitlesStatesAndRelatedComboBoxItems[m_subtitlesState];

            videoStateComboBox.SelectedValueChanged += videoStateComboBox_SelectedValueChanged;
            subtitlesStateComboBox.SelectedValueChanged += subtitlesStateComboBox_SelectedValueChanged;
        }

        #region Эмуляция инпута



        private void ActionForHotkeyThatAreNotPauseButton()
        {
            //if (GetActiveProcessName() != m_videoPlayerProcessName)
            //    return;
            //m_videoPlayerProcess = Process.GetProcessesByName("mpc-hc64")[0];

            var activeProcess = GetActiveProcess();
            if (activeProcess.ProcessName != m_videoPlayerProcessName)
                return;
            m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

            PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_videoplayerPauseHotkey, 0);
            SwitchSubtitles();
        }

        private void ActionForHotkeyThatArePauseButton()
        {
            //if (GetActiveProcessName() != m_videoPlayerProcessName)
            //    return;

            var activeProcess = GetActiveProcess();
            if (activeProcess.ProcessName != m_videoPlayerProcessName)
                return;
            m_videoPlayerProcessMainWindowHandle = activeProcess.MainWindowHandle;

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

                    m_changeSubtitlesToBilingualDelegate.Invoke();
                    //m_changeSubtitlesToBilingualDelegate.BeginInvoke(null, null);

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

                    m_changeSubtitlesToOriginalDelegate.Invoke();
                    //m_changeSubtitlesToOriginalDelegate.BeginInvoke(null, null);

                    m_subtitlesState = SubtitlesState.Original;
                }

                // Ставим Playing в КомбоБоксе
                m_videoState = VideoState.Playing;
            }

            BeginInvoke(m_changeVideoAndSubtitlesComboBoxesDelegate);
        }

        private void ChangeSubtitlesToBilingualByPostMessage()
        {
            PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_changeSubtitlesToBilingualHotkeyCode, 0);
        }

        private void ChangeSubtitlesToBilingualByInputSimulator()
        {
            m_inputSimulator.Keyboard.ModifiedKeyStroke(
                m_changeSubtitlesToBilingualHotkeyModifierKeyVirtualKeyCode.Value,
                m_changeSubtitlesToBilingualHotkeyVirtualKeyCode.Value);
        }

        private void ChangeSubtitlesToOriginalByPostMessage()
        {
            PostMessage(m_videoPlayerProcessMainWindowHandle, WM_KEYDOWN, m_changeSubtitlesToOriginalHotkeyCode, 0);
        }

        private void ChangeSubtitlesToOriginalByInputSimulator()
        {
            m_inputSimulator.Keyboard.ModifiedKeyStroke(
                m_changeSubtitlesToOriginalHotkeyModifierKeyVirtualKeyCode.Value,
                m_changeSubtitlesToOriginalHotkeyVirtualKeyCode.Value);
        }

        string GetActiveProcessName()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            Process p = Process.GetProcessById((int)pid);
            return p.ProcessName;
        }

        Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            return Process.GetProcessById((int)pid);
        }

        #endregion


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
            // Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline,
            // StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle,
            // Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
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

            string[] styleComponents = null;
            for (int i = 0; i < subtitlesColorPairs.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            styleComponents = Settings.Default.OriginalSubtitlesStyleString.Split(';');
                            break;
                        }
                    case 1:
                        {
                            styleComponents = Settings.Default.FirstRussianSubtitlesStyleString.Split(';');
                            break;
                        }
                    case 2:
                        {
                            styleComponents = Settings.Default.SecondRussianSubtitlesStyleString.Split(';');
                            break;
                        }
                    case 3:
                        {
                            styleComponents = Settings.Default.ThirdRussianSubtitlesStyleString.Split(';');
                            break;
                        }
                }

                var font = styleComponents[0];
                var marginV = styleComponents[1];
                var size = styleComponents[2];
                var outline = styleComponents[3];
                var shadow = styleComponents[4];

                var transparencyPercentage = styleComponents[5];
                var transparency = (int.Parse(transparencyPercentage) * 255 / 100).ToString("X");

                //var transparency = i == 0 ? "00" : "64";
                //var marginV = i == 3 ? 0
                //    : (2*20 + 5) + i * (2 * 20 + 5);
                //var outline = 2;
                //var shadow = 1;

                var color = subtitlesColorPairs[i].Item2;

                assSB.AppendLine(
                    $"Style: {i}{subtitleStyleNamePostfix}," +
                    $"{font}," +
                    $"{size}," +
                    $"&H" +
                    $"{transparency}" +
                    $"{color.B.ToString("X2")}" +
                    $"{color.G.ToString("X2")}" +
                    $"{color.R.ToString("X2")}," +
                    $"&H{transparency}00FFFF," +
                    $"&H{transparency}000000," +
                    $"&H{transparency}000000," +
                    $"0,0,0,0,100,100,0,0,1," +
                    // Обводка
                    $"{outline}," +
                    // Тень
                    $"{shadow}," +
                    $"2,10,10," +
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

        private void StartYandexTranslateSubtitles(SubtitlesType subtitlesType, bool wordByWord = false)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];

            if (!CheckYandexTranslatorIsGoodToGo(m_translator))
            {
                return;
            }

            switch (subtitlesType)
            {
                case SubtitlesType.FirstRussian:
                {
                    firstRussianSubtitlesActionLabel.Visible = firstRussianSubtitlesProgressLabel.Visible = firstRussianSubtitlesProgressBar.Visible = true;
                    break;
                }
                case SubtitlesType.SecondRussian:
                {
                    secondRussianSubtitlesActionLabel.Visible = secondRussianSubtitlesProgressLabel.Visible = secondRussianSubtitlesProgressBar.Visible = true;
                    break;
                }
                case SubtitlesType.ThirdRussian:
                {
                    thirdRussianSubtitlesActionLabel.Visible = thirdRussianSubtitlesProgressLabel.Visible = thirdRussianSubtitlesProgressBar.Visible = true;
                    break;
                }
            }

            subtitlesInfo.OutputTextBox.Text = $"Переведенные ";
            if (wordByWord)
                subtitlesInfo.OutputTextBox.Text += "пословно ";
            subtitlesInfo.OutputTextBox.Text += "оригинальные субтитры";

            var yandexTranslateSubtitlesBackgroundWorker = new SubtitlesBackgroundWorker();
            yandexTranslateSubtitlesBackgroundWorker.DoWork += YandexTranslateSubtitles;
            yandexTranslateSubtitlesBackgroundWorker.WorkerReportsProgress = true;
            yandexTranslateSubtitlesBackgroundWorker.ProgressChanged += yandexTranslateSubtitlesBackgroundWorker_ProgressChanged;
            yandexTranslateSubtitlesBackgroundWorker.RunWorkerCompleted += yandexTranslateSubtitlesBackgroundWorker_RunWorkerCompleted;

            subtitlesInfo.SetBackgroundWorker(yandexTranslateSubtitlesBackgroundWorker, subtitlesType);

            subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Minimum;
            subtitlesInfo.ProgressLabel.Text = $"0%";
            subtitlesInfo.ButtonOpen.Enabled = false;
            if (subtitlesInfo.ButtonTranslate != null) // В случае оригинальных, я так полагаю
            {
                subtitlesInfo.ButtonTranslate.Enabled = false;
                subtitlesInfo.ButtonTranslateWordByWord.Enabled = false;
            }

            subtitlesInfo.ActionLabel.Text = SUBTITLES_ARE_TRANSLATING;

            yandexTranslateSubtitlesBackgroundWorker.RunWorkerAsync(wordByWord);
        }

        private void YandexTranslateSubtitles(object sender, DoWorkEventArgs eventArgs)
        {
            var byWord = (bool)eventArgs.Argument;
            var originalSubtitles = m_subtitles[SubtitlesType.Original].Subtitles;

            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            subtitlesInfo.Subtitles = new Subtitle[originalSubtitles.Length];

            for (int i = 0; i < originalSubtitles.Length; i++)
            {
                subtitlesInfo.Subtitles[i] = new Subtitle
                (originalSubtitles[i].Start,
                    originalSubtitles[i].End,
                    YandexTranslateAStringWithChecking(originalSubtitles[i].Text, m_translator, byWord)
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
            {
                subtitlesInfo.ButtonTranslate.Enabled = true;
                subtitlesInfo.ButtonTranslateWordByWord.Enabled = true;
            }

            subtitlesInfo.ActionLabel.Text = SUBTITLES_ARE_TRANSLATED;
            // TODO Ошибки?
        }

        private string YandexTranslateAStringWithChecking(string originalText, Translator translator, bool byWord = false)
        {
            string output = "";
            string tempStr = originalText;
            int countOfTags = originalText.Split('<').Length - 1;
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
                if (byWord == false)
                {
                    var translation = translator.Translate(originalText, new LangPair(Lang.En, Lang.Ru), null, false);
                    output += translation.Text + '\n';
                }
                else
                {
                    var words = originalText.Split(' ');
                    foreach (var word in words)
                    {
                        var translation = translator.Translate(word, new LangPair(Lang.En, Lang.Ru), null, false);
                        output += translation.Text + ' ';
                    }
                }
            }
            catch (Exception ex)
            {
                
                    MessageBox.Show("Строка " + originalText +
                                    "была обработана неверно. \n Вместо перевода будет записан оригинальный текст. \n " +
                                    "Код ошибки: " + ex.Message);
                    output += originalText + '\n';
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

        private bool CheckYandexTranslatorIsGoodToGo(Translator translator)
        {
            try
            {
                var translation = translator.Translate("Hello world", new LangPair(Lang.En, Lang.Ru), null, false);
            }
            catch (Exception ex)
            {
                if (ex.Message == "API key is invalid")
                {
                    MessageBox.Show("Ключ API Яндекс.Переводчика неверен!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Проблема с Яндекс.Переводчиком!\n{ex}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }

            return true;
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
            StartYandexTranslateSubtitles(SubtitlesType.FirstRussian);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(SubtitlesType.SecondRussian);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(SubtitlesType.ThirdRussian);
        }

        private void OpenFileAndReadSubtitlesFromFile(SubtitlesType subtitlesType)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];

            string formats = "Файлы Matroska Video (.mkv), файлы SubRip Text (.srt) |*.mkv; *.srt";

            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = formats;
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
                       (extension.Length));

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
                        BeginInvoke((Action)((() =>
                        {
                            switch (parentBgW.SubtitlesType)
                            {
                                case SubtitlesType.Original:
                                    {
                                        primarySubtitlesActionLabel.Visible = primarySubtitlesProgressLabel.Visible = primarySubtitlesProgressBar.Visible = true;
                                        break;
                                    }
                                case SubtitlesType.FirstRussian:
                                    {
                                        firstRussianSubtitlesActionLabel.Visible = firstRussianSubtitlesProgressLabel.Visible = firstRussianSubtitlesProgressBar.Visible = true;
                                        break;
                                    }
                                case SubtitlesType.SecondRussian:
                                    {
                                        secondRussianSubtitlesActionLabel.Visible = secondRussianSubtitlesProgressLabel.Visible = secondRussianSubtitlesProgressBar.Visible = true;
                                        break;
                                    }
                                case SubtitlesType.ThirdRussian:
                                    {
                                        thirdRussianSubtitlesActionLabel.Visible = thirdRussianSubtitlesProgressLabel.Visible = thirdRussianSubtitlesProgressBar.Visible = true;
                                        break;
                                    }
                            }

                            subtitlesInfo.OutputTextBox.Text = new FileInfo(filePath).Name;
                        })));

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
                            BeginInvoke((Action)((() =>
                                {
                                    switch (parentBgW.SubtitlesType)
                                    {
                                        case SubtitlesType.Original:
                                        {
                                            primarySubtitlesActionLabel.Visible = primarySubtitlesProgressLabel.Visible = primarySubtitlesProgressBar.Visible = true;
                                            break;
                                        }
                                        case SubtitlesType.FirstRussian:
                                        {
                                            firstRussianSubtitlesActionLabel.Visible = firstRussianSubtitlesProgressLabel.Visible = firstRussianSubtitlesProgressBar.Visible = true;
                                            break;
                                        }
                                        case SubtitlesType.SecondRussian:
                                        {
                                            secondRussianSubtitlesActionLabel.Visible = secondRussianSubtitlesProgressLabel.Visible = secondRussianSubtitlesProgressBar.Visible = true;
                                            break;
                                        }
                                        case SubtitlesType.ThirdRussian:
                                        {
                                            thirdRussianSubtitlesActionLabel.Visible = thirdRussianSubtitlesProgressLabel.Visible = thirdRussianSubtitlesProgressBar.Visible = true;
                                            break;
                                        }
                                    }

                                    subtitlesInfo.OutputTextBox.Text =
                                    $"{trackSelectionForm.SelectedTrackTitle} из {new FileInfo(filePath).Name}";

                                })));

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

            if (subtitlesInfo.Subtitles == null)
                return;

            subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Maximum;
            subtitlesInfo.ProgressLabel.Text = $"100%";
            subtitlesInfo.ButtonOpen.Enabled = true;
            if (subtitlesInfo.ButtonTranslate != null)
                subtitlesInfo.ButtonTranslate.Enabled = true;

            subtitlesInfo.ActionLabel.Text = SUBTITLES_ARE_OPENED;

            if (parentBgW.SubtitlesType == SubtitlesType.Original)
            {
                translateToFirstRussianSubtitlesButton.Enabled = translateWordByWordToFirstRussianSubtitlesButton.Enabled =
                    translateToSecondRussianSubtitlesButton.Enabled = translateWordByWordToSecondRussianSubtitlesButton.Enabled =
                        translateToThirdRussianSubtitlesButton.Enabled = translateWordByWordToThirdRussianSubtitlesButton.Enabled =
                            true;
            }

            // TODO Ошибки?
        }

        private void createOriginalAndBilingualSubtitlesFilesButton_Click(object sender, EventArgs e)
        {
            var originalSubtitlesPath =
                finalSubtitlesFilesPathBeginningRichTextBox.Text + originalSubtitlesFileNameEnding.Text;
            var bilingualSubtitlesPath =
                finalSubtitlesFilesPathBeginningRichTextBox.Text + bilingualSubtitlesFileNameEnding.Text;
            var bilingualSubtitlesFileExists = File.Exists(bilingualSubtitlesPath);

            switch (Settings.Default.CreateOriginalSubtitlesFile)
            {
                case true:
                    {
                        var originalSubtitlesFileExist = File.Exists(originalSubtitlesPath);


                        if (originalSubtitlesFileExist && bilingualSubtitlesFileExists)
                        {
                            var result = MessageBox.Show($"Файлы\n{originalSubtitlesPath}\nи\n{bilingualSubtitlesPath}\nуже существуют! Перезаписать их?",
                                String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (result != DialogResult.OK)
                                return;
                        }
                        else if (originalSubtitlesFileExist)
                        {
                            var result = MessageBox.Show($"Файл\n{originalSubtitlesPath}\nуже существует! Перезаписать его?",
                                String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (result != DialogResult.OK)
                                return;
                        }
                        else if (bilingualSubtitlesFileExists)
                        {
                            var result = MessageBox.Show($"Файл\n{bilingualSubtitlesPath}\nуже существует! Перезаписать его?",
                                String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (result != DialogResult.OK)
                                return;
                        }

                        break;
                    }
                case false:
                    {
                        if (bilingualSubtitlesFileExists)
                        {
                            var result = MessageBox.Show($"Файл\n{bilingualSubtitlesPath}\nуже существует! Перезаписать его?",
                                String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                            if (result != DialogResult.OK)
                                return;
                        }
                        break;
                    }
            }

            var originalSubtitles = m_subtitles[SubtitlesType.Original].Subtitles;
            var firstRussianSubtitles = m_subtitles[SubtitlesType.FirstRussian].Subtitles;
            var secondRussianSubtitles = m_subtitles[SubtitlesType.SecondRussian].Subtitles;
            var thirdRussianSubtitles = m_subtitles[SubtitlesType.ThirdRussian].Subtitles;

            StringBuilder ass;

            if (Settings.Default.CreateOriginalSubtitlesFile)
            {
                ass = GenerateASSMarkedupDocument(new[]
                {
                    new Tuple<Subtitle[], Color>(originalSubtitles, primarySubtitlesColorButton.BackColor),
                });

                try
                {
                    File.WriteAllText(
                        originalSubtitlesPath,
                        ass.ToString());
                }
                catch (Exception exception)
                {
                    MessageBox.Show($"Записать файл\n{originalSubtitlesPath}\nне удалось! Исключение:\n{exception}");
                    return;
                }
                
            }

            List<Tuple<Subtitle[], Color>> listSubsPairs = new List<Tuple<Subtitle[], Color>>
            {
                new Tuple<Subtitle[], Color>(originalSubtitles, primarySubtitlesColorButton.BackColor),
                new Tuple<Subtitle[], Color>(firstRussianSubtitles, firstRussianSubtitlesColorButton.BackColor),
                new Tuple<Subtitle[], Color>(secondRussianSubtitles, secondRussianSubtitlesColorButton.BackColor),
                new Tuple<Subtitle[], Color>(thirdRussianSubtitles, thirdRussianSubtitlesColorButton.BackColor)
            };
            ass = GenerateASSMarkedupDocument(listSubsPairs.ToArray());

            try
            {
                File.WriteAllText(bilingualSubtitlesPath, ass.ToString());
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Записать файл\n{bilingualSubtitlesPath}\nне удалось! Исключение:\n{exception}");
                return;
            }

            // Проверка существования итоговых файлов субтитров
            bilingualSubtitlesFileExists = File.Exists(bilingualSubtitlesPath);

            switch (Settings.Default.CreateOriginalSubtitlesFile)
            {
                case true:
                    {
                        var originalSubtitlesFileExist = File.Exists(originalSubtitlesPath);


                        if (originalSubtitlesFileExist && bilingualSubtitlesFileExists)
                        {
                            var result = MessageBox.Show($"Файлы\n{originalSubtitlesPath}\nи\n{bilingualSubtitlesPath}\nуспешно записаны!",
                                String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (result != DialogResult.OK)
                                return;
                        }
                        else if (originalSubtitlesFileExist)
                        {
                            var result = MessageBox.Show($"Файл\n{originalSubtitlesPath}\nуспешно записан!",
                                String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (result != DialogResult.OK)
                                return;
                        }
                        else if (bilingualSubtitlesFileExists)
                        {
                            var result = MessageBox.Show($"Файл\n{bilingualSubtitlesPath}\nуспешно записан!",
                                String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (result != DialogResult.OK)
                                return;
                        }

                        break;
                    }
                case false:
                    {
                        if (bilingualSubtitlesFileExists)
                        {
                            var result = MessageBox.Show($"Файл\n{bilingualSubtitlesPath}\n\nуспешно записан!",
                                String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if (result != DialogResult.OK)
                                return;
                        }
                        break;
                    }
            }

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
            translateToFirstRussianSubtitlesButton.Enabled = translateWordByWordToFirstRussianSubtitlesButton.Enabled =
                translateToSecondRussianSubtitlesButton.Enabled = translateWordByWordToSecondRussianSubtitlesButton.Enabled =
                    translateToThirdRussianSubtitlesButton.Enabled = translateWordByWordToThirdRussianSubtitlesButton.Enabled =
                        false;

            OpenFileAndReadSubtitlesFromFile(SubtitlesType.Original);

            //OpenFileAndReadSubtitlesFromFile(ref m_originalSubtitles,
            //    primarySubtitlesProgressBar, primarySubtitlesProgressLabel, primarySubtitlesActionLabel, primarySubtitlesTextBox,
            //    openPrimarySubtitlesButton);
        }

        private void openFirstRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFile(SubtitlesType.FirstRussian);
            //OpenFileAndReadSubtitlesFromFile(ref m_firstRussianSubtitles,
            //    firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel, firstRussianSubtitlesActionLabel,
            //    firstRussianSubtitlesTextBox, openFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton);
        }

        private void openSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFile(SubtitlesType.SecondRussian);

            //OpenFileAndReadSubtitlesFromFile(ref m_secondRussianSubtitles,
            //    secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel, secondRussianSubtitlesActionLabel,
            //    secondRussianSubtitlesTextBox, openSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton);
        }

        private void openThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
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

        private void settingsButton_Click(object sender, EventArgs e)
        {
            var keySettingForm = new SettingsForm();
            keySettingForm.ShowDialog();
            keySettingForm.Dispose();

            SetProgramSettings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(SubtitlesType.FirstRussian, true);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(SubtitlesType.SecondRussian, true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            StartYandexTranslateSubtitles(SubtitlesType.ThirdRussian, true);
        }


        private void hideThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            thirdRussianSubtitlesGroupBox.Hide();
            thirdRussianSubtitlesColorButton.Hide();
            hideThirdRussianSubtitlesButton.Hide();
            showThirdRussianSubtitlesButton.Show();

            Settings.Default.ThirdRussianSubtitlesIsVisible = false;
            Settings.Default.Save();
        }

        private void hideSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            secondRussianSubtitlesGroupBox.Hide();
            secondRussianSubtitlesColorButton.Hide();
            hideSecondRussianSubtitlesButton.Hide();
            showSecondRussianSubtitlesButton.Show();

            Settings.Default.SecondRussianSubtitlesIsVisible = false;
            Settings.Default.Save();
        }

        private void showSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            secondRussianSubtitlesGroupBox.Show();
            secondRussianSubtitlesColorButton.Show();
            hideSecondRussianSubtitlesButton.Show();
            showSecondRussianSubtitlesButton.Hide();

            Settings.Default.SecondRussianSubtitlesIsVisible = true;
            Settings.Default.Save();
        }

        private void showThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            thirdRussianSubtitlesGroupBox.Show();
            thirdRussianSubtitlesColorButton.Show();
            hideThirdRussianSubtitlesButton.Show();
            showThirdRussianSubtitlesButton.Hide();

            Settings.Default.ThirdRussianSubtitlesIsVisible = true;
            Settings.Default.Save();
        }

        private void selectVideoFileToGetPathForSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string formats = "Все видео файлы |*.dat; *.wmv; *.3g2; *.3gp; *.3gp2; *.3gpp; *.amv; *.asf;  *.avi; *.bin; *.cue; *.divx; *.dv; *.flv; *.gxf; *.iso; " +
                             "*.m1v; *.m2v; *.m2t; *.m2ts; *.m4v; " +
                             " *.mkv; *.mov; *.mp2; *.mp2v; *.mp4; *.mp4v; *.mpa; *.mpe; *.mpeg; *.mpeg1; *.mpeg2; *.mpeg4; " +
                             "*.mpg; *.mpv2; *.mts; *.nsv; *.nuv; *.ogg; *.ogm; *.ogv; *.ogx; *.ps; *.rec; *.rm; *.rmvb; *.tod; *.ts; *.tts; *.vob; *.vro; *.webm";

            openFileDialog.Filter = formats;
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var finalSubtitlesFilesPathFileInfo = new FileInfo(openFileDialog.FileName);
                finalSubtitlesFilesPathBeginningRichTextBox.Text = finalSubtitlesFilesPathFileInfo.FullName.Substring(0,
                    finalSubtitlesFilesPathFileInfo.FullName.Length - finalSubtitlesFilesPathFileInfo.Extension.Length);
            }

            openFileDialog.Dispose();
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
