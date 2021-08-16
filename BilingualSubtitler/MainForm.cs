using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WindowsInput;
using Nikse.SubtitleEdit.Core.ContainerFormats.Matroska;
using NonInvasiveKeyboardHookLibrary;
using YandexLinguistics.NET;
using MessageBox = System.Windows.Forms.MessageBox;
using Settings = BilingualSubtitler.Properties.Settings;
using VirtualKeyCode = WindowsInput.Native.VirtualKeyCode;
using Xceed.Words.NET;
using Octokit;
using Label = System.Windows.Forms.Label;
using System.Threading;
using System.Security.Principal;
using System.Drawing.Text;
using System.IO.Compression;
using Syroot.Windows.IO;

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

        private int m_initialFormWidth;
        private string m_initialOpenOrCloseSubtitlesButtonText;
        private string m_initialOpenSubtitlesGroupBoxText;
        private string m_initialOpenSubtitlesGroupBoxTextBeforeCloseConfirmationDialog;
        private int m_initialOpenSubtitlesButtonLeft;
        private int m_initialExportSubtitlesAsDocxButtonLeft;
        private int m_initialOpenSubtitlesFromDownloadsButtonLeft;
        private int m_initialFirstRussianSubtitlesGroupBoxWidth;
        private int m_initialSecondRussianSubtitlesGroupBoxWidth;
        private int m_initialThirdRussianSubtitlesGroupBoxWidth;
        private int m_initialSecondRussianSubtitlesHideButtonX;
        private int m_initialThirdRussianSubtitlesHideButtonX;

        private Dictionary<SubtitlesType, SubtitlesAndInfo> m_subtitles;

        private Translator m_translator;

        private KeyboardHookManager m_keyboardHookManager;
        private InputSimulator m_inputSimulator;

        private int[] m_biligualSubtitlersHotkeys;

        private int m_changeSubtitlesToBilingualHotkeyCode;
        private VirtualKeyCode? m_changeSubtitlesToBilingualHotkeyModifierKeyVirtualKeyCode;
        private VirtualKeyCode? m_changeSubtitlesToBilingualHotkeyVirtualKeyCode;
        private int m_changeSubtitlesToOriginalHotkeyCode;
        private VirtualKeyCode? m_changeSubtitlesToOriginalHotkeyModifierKeyVirtualKeyCode;
        private VirtualKeyCode? m_changeSubtitlesToOriginalHotkeyVirtualKeyCode;

        private int m_videoplayerPauseHotkey;

        // Состояния видео и субтитров
        private VideoState m_videoState;
        private SubtitlesState m_subtitlesState;
        //
        // Былое
        //
        //private ComboboxItem m_videoPlayingComboBoxItem = new ComboboxItem
        //{ Text = "воспроизводится", Value = VideoState.Playing };
        //private ComboboxItem m_videoPausedComboBoxItem = new ComboboxItem
        //{ Text = "на паузе", Value = VideoState.Paused };
        //private ComboboxItem m_subtitlesOriginalSubtitlesComboBoxItem = new ComboboxItem
        //{ Text = "оригинальными субтитрами", Value = SubtitlesState.Original };
        //private ComboboxItem m_subtitlesBilingualSubtitlesComboBoxItem = new ComboboxItem
        //{ Text = "двуязычными субтитрами", Value = SubtitlesState.Bilingual };
        //private Dictionary<VideoState, ComboboxItem> m_videoStatesAndRelatedComboBoxItems;
        //private Dictionary<SubtitlesState, ComboboxItem> m_subtitlesStatesAndRelatedComboBoxItems;
        //
        /// <summary>
        /// воспроизводится с оригинальными субтитрами
        /// </summary>
        private Tuple<VideoState, SubtitlesState> m_videoPlayingWithOriginalSubtitlesState;
        //
        private ComboboxItem m_videoPlayingWithOriginalSubtitlesComboBoxItem;
        /// <summary>
        /// воспроизводится с двуязычными субтитрами
        /// </summary>
        private Tuple<VideoState, SubtitlesState> m_videoPlayingWithBilingualSubtitlesState;
        //
        private ComboboxItem m_videoPlayingWithBilingualSubtitlesComboBoxItem;
        /// <summary>
        /// на паузе с оригинальными субтитрами
        /// </summary>
        private Tuple<VideoState, SubtitlesState> m_videoPausedWithOriginalSubtitlesState;
        //
        private ComboboxItem m_videoPausedWithOriginalSubtitlesComboBoxItem;
        /// <summary>
        /// на паузе с двуязычными субтитрами
        /// </summary>
        private Tuple<VideoState, SubtitlesState> m_videoPausedWithBilingualSubtitlesState;
        //
        private ComboboxItem m_videoPausedWithBilingualSubtitlesComboBoxItem;
        //
        //
        //private Dictionary<Tuple<VideoState, SubtitlesState>, ComboboxItem> m_videoAndSubtitlesStatesAndRelatedComboBoxItems;

        private List<Button> m_buttons;
        private Color m_previousButtonColor;

        private delegate void ChangeVideoAndSubtitlesComboBoxes();
        private ChangeVideoAndSubtitlesComboBoxes m_changeVideoAndSubtitlesComboBoxesDelegate;

        private delegate void ChangeSubtitlesToBilingual();
        private ChangeSubtitlesToBilingual m_changeSubtitlesToBilingualDelegate;

        private delegate void ChangeSubtitlesToOriginal();
        private ChangeSubtitlesToOriginal m_changeSubtitlesToOriginalDelegate;

        private List<string> m_assHeader = new List<string>()
        {
             "[Script Info]\r\n",
                "; This is an Advanced Sub Station Alpha v4+ script.\r\n",
                "Title: \r\n",
                "ScriptType: v4.00+\r\n",
                "Collisions: Normal\r\n",
                "PlayDepth: 0\r\n",
                "\r\n",
                "[V4+ Styles]\r\n",
                "Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding\r\n"
    };
        private string m_subtitleStyleNamePostfix = "_sub_stream";


        const UInt32 WM_KEYDOWN = 0x0100;

        private string m_videoPlayerProcessName;
        private IntPtr m_videoPlayerProcessMainWindowHandle;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private string m_playVideoButtonDefaultText;

        private bool m_redefineSubtitlesAppearanceSettings => redefineSubtitlesAppearanceSettingsCheckBox.Visible & redefineSubtitlesAppearanceSettingsCheckBox.Checked;


        public MainForm()
        {
            InitializeComponent();

            // Состояния видео / субтитров:
            // воспроизводится с оригинальными субтитрами
            m_videoPlayingWithOriginalSubtitlesState = new Tuple<VideoState, SubtitlesState>
            (VideoState.Playing, SubtitlesState.Original);
            //
            m_videoPlayingWithOriginalSubtitlesComboBoxItem = new ComboboxItem
            { Text = "воспроизводится & с оригинальными субтитрами", Value = m_videoPlayingWithOriginalSubtitlesState };
            // воспроизводится с двуязычными субтитрами
            m_videoPlayingWithBilingualSubtitlesState = new Tuple<VideoState, SubtitlesState>
                (VideoState.Playing, SubtitlesState.Bilingual);
            //
            m_videoPlayingWithBilingualSubtitlesComboBoxItem = new ComboboxItem
            { Text = "воспроизводится & с двуязычными субтитрами", Value = m_videoPlayingWithBilingualSubtitlesState };
            // на паузе с оригинальными субтитрами
            m_videoPausedWithOriginalSubtitlesState = new Tuple<VideoState, SubtitlesState>
                (VideoState.Paused, SubtitlesState.Original);
            //
            m_videoPausedWithOriginalSubtitlesComboBoxItem = new ComboboxItem
            { Text = "на паузе & с оригинальными субтитрами", Value = m_videoPausedWithOriginalSubtitlesState };
            // на паузе с двуязычными субтитрами
            m_videoPausedWithBilingualSubtitlesState =
                new Tuple<VideoState, SubtitlesState>(VideoState.Paused, SubtitlesState.Bilingual);
            //
            m_videoPausedWithBilingualSubtitlesComboBoxItem = new ComboboxItem
            { Text = "на паузе & с двуязычными субтитрами", Value = m_videoPausedWithBilingualSubtitlesState };

            // Графика
            m_initialFormWidth = Width;
            m_initialOpenSubtitlesButtonLeft = openOrClosePrimarySubtitlesButton.Left;
            m_initialOpenSubtitlesGroupBoxText = m_initialOpenSubtitlesGroupBoxTextBeforeCloseConfirmationDialog = openOrCloseFirstRussianSubtitlesGroupBox.Text;
            m_initialOpenOrCloseSubtitlesButtonText = openOrClosePrimarySubtitlesButton.Text;
            m_initialOpenSubtitlesFromDownloadsButtonLeft = openPrimarySubtitlesFromDownloadsButton.Left;
            m_initialExportSubtitlesAsDocxButtonLeft = primarySubtitlesExportAsDocxButton.Left;
            m_initialFirstRussianSubtitlesGroupBoxWidth = firstRussianSubtitlesGroupBox.Width;
            m_initialSecondRussianSubtitlesGroupBoxWidth = secondRussianSubtitlesGroupBox.Width;
            m_initialThirdRussianSubtitlesGroupBoxWidth = thirdRussianSubtitlesGroupBox.Width;
            m_initialSecondRussianSubtitlesHideButtonX = hideSecondRussianSubtitlesButton.Location.X;
            m_initialThirdRussianSubtitlesHideButtonX = hideThirdRussianSubtitlesButton.Location.X;

            m_playVideoButtonDefaultText = playVideoButton.Text;
            notifyIcon.ContextMenu = new ContextMenu(new MenuItem[] {
                new MenuItem("Свернуть в трей", ((sender, e) =>
                {
                    // прячем наше окно из панели
                this.ShowInTaskbar = false; 
                //сворачиваем окно
                WindowState = FormWindowState.Minimized;
            })),

                new MenuItem("Развернуть", ((sender, e) =>
                {
                    // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
                })),

                new MenuItem("Завершить работу Bilingual Subtitler", ((sender, e) => System.Windows.Forms.Application.Exit()))
            });

            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();

                Properties.SubtitlesAppearanceSettings.Default.Upgrade();
                Properties.SubtitlesAppearanceSettings.Default.Save();
            }

            if (Settings.Default.FirstLaunch)
            {
                var videoplayerPauseKey = new Hotkey(Settings.Default.VideoPlayerPauseButtonString).KeyValue;
                var videoplayerNextSubtitles = new Hotkey(Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString).KeyValue;
                var videoplayerPreviousSubtitles = new Hotkey(Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString).KeyValue;

                var bilingualSubtitlesHotkeys = string.Empty;
                foreach (var hotkeyString in Settings.Default.Hotkeys)
                {
                    if (string.IsNullOrWhiteSpace(bilingualSubtitlesHotkeys))
                        bilingualSubtitlesHotkeys += $"{new Hotkey(hotkeyString).KeyValue}";
                    else
                        bilingualSubtitlesHotkeys += $", {new Hotkey(hotkeyString).KeyValue}";
                }

                MessageBox.Show("Вас приветствует Bilingual Subtitler!\n\n" +
                    "Это ваш первый запук Bilingual Subtitler, поэтому для главного окна выставлен облегченный режим компоновки. Для того, чтобы включить больше возможностей — переключитесь на \"расширенный режим\" в настройках программы.\n\n" +
                                "Параметры видеоплеера (для просмотра с динамически подключаемыми русскими субтитрами) сейчас таковы:\n" +
                                $"Имя процесса видеоплеера: {Settings.Default.VideoPlayerProcessName}\n" +
                                $"Горячие клавиши видеоплеера:\n" +
                                $"Паузы — {videoplayerPauseKey}, смены на следующие субтитры — {videoplayerNextSubtitles}, " +
                                $"на предыдущие — {videoplayerPreviousSubtitles}.\n\n" +
                                $"Горячие клавиши Bilingual Subtitler: {bilingualSubtitlesHotkeys}\n\n" +
                                $"Для работы горячих клавиш Bilingual Subtitler требуется запуск от имени администратора!\n" +
                                $"Проверьте, возможно параметры вашего видеоплеера отличаются от заданных по умолчанию " +
                                $"(параметры по умолчанию — для немодифицированного 64-разрядного Media Player Classic Homecinema.",
                                $"Первый запуск Bilingual Subtitler", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Settings.Default.FirstLaunch = false;
                Settings.Default.Save();
            }

            // Состояния видео и субтитров
            videoAndSubtitlesStateComboBox.Items.Add(m_videoPlayingWithOriginalSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBox.Items.Add(m_videoPlayingWithBilingualSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBox.Items.Add(m_videoPausedWithOriginalSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBox.Items.Add(m_videoPausedWithBilingualSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBox.SelectedIndex = 0;
            //
            //m_videoAndSubtitlesStatesAndRelatedComboBoxItems = new Dictionary<Tuple<VideoState, SubtitlesState>, ComboboxItem>
            //{
            //    { m_videoPlayingWithOriginalSubtitlesState, m_videoPlayingWithOriginalSubtitlesComboBoxItem},
            //    { m_videoPlayingWithBilingualSubtitlesState, m_videoPlayingWithBilingualSubtitlesComboBoxItem},
            //    { m_videoPausedWithOriginalSubtitlesState, m_videoPausedWithOriginalSubtitlesComboBoxItem},
            //    { m_videoPausedWithBilingualSubtitlesState, m_videoPausedWithBilingualSubtitlesComboBoxItem}
            //};
            #region Былое
            //videoStateComboBox.Items.Add(m_videoPlayingComboBoxItem);
            //videoStateComboBox.Items.Add(m_videoPausedComboBoxItem);
            //videoStateComboBox.SelectedIndex = 0;

            //subtitlesStateComboBox.Items.Add(m_subtitlesOriginalSubtitlesComboBoxItem);
            //subtitlesStateComboBox.Items.Add(m_subtitlesBilingualSubtitlesComboBoxItem);
            //subtitlesStateComboBox.SelectedIndex = 0;

            //m_videoStatesAndRelatedComboBoxItems = new Dictionary<VideoState, ComboboxItem>
            //{
            //    {VideoState.Playing, m_videoPlayingComboBoxItem},
            //    { VideoState.Paused, m_videoPausedComboBoxItem}
            //};

            //m_subtitlesStatesAndRelatedComboBoxItems = new Dictionary<SubtitlesState, ComboboxItem>
            //{
            //    {SubtitlesState.Original, m_subtitlesOriginalSubtitlesComboBoxItem},
            //    { SubtitlesState.Bilingual, m_subtitlesBilingualSubtitlesComboBoxItem}
            //};
            #endregion

            m_subtitles = new Dictionary<SubtitlesType, SubtitlesAndInfo>
            {
                {
                    SubtitlesType.Original, new SubtitlesAndInfo(
                        primarySubtitlesProgressBar, primarySubtitlesProgressLabel,
                        openOrClosePrimarySubtitlesButton, null, null,
                        primarySubtitlesActionLabel, primarySubtitlesTextBox, primarySubtitlesColorButton,
                        openOrClosePrimarySubtitlesGroupBox, primarySubtitlesExportAsDocxGroupBox, openPrimarySubtitlesFromDownloadsButton, openPrimarySubtitlesFromDefaultFolderButton, primarySubtitlesExportAsDocxButton, primarySubtitlesExportAsDocxIntoDownloadsButton)
                },
                {
                    SubtitlesType.FirstRussian, new SubtitlesAndInfo(
                        firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel,
                        openOrCloseFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton, translateWordByWordToFirstRussianSubtitlesButton,
                        firstRussianSubtitlesActionLabel, firstRussianSubtitlesTextBox, firstRussianSubtitlesColorButton,
                        openOrCloseFirstRussianSubtitlesGroupBox, firstRussianSubtitlesExportAsDocxGroupBox, firstRussianSubtitlesOpenFromDownloadsButton, openFirstRussianSubtitlesFromDefaultFolderButton,
                        firstRussianSubtitlesExportAsDocxButton, firstRussianSubtitlesExportAsDocxIntoDownloadsButton)
                },
                {
                    SubtitlesType.SecondRussian, new SubtitlesAndInfo(
                        secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel,
                        openOrCloseSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton, translateWordByWordToSecondRussianSubtitlesButton,
                        secondRussianSubtitlesActionLabel, secondRussianSubtitlesTextBox, secondRussianSubtitlesColorButton,
                        openOrCloseSecondRussianSubtitlesGroupBox, secondRussianSubtitlesExportAsDocxGroupBox, secondRussianSubtitlesOpenFromDownloadsButton, openSecondRussianSubtitlesFromDefaultFolderButton, secondRussianSubtitlesExportAsDocxButton, secondRussianSubtitlesExportAsDocxIntoDownloadsButton)
                },
                {
                    SubtitlesType.ThirdRussian, new SubtitlesAndInfo(
                        thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel,
                        openOrCloseThirdRussianSubtitlesButton, translateToThirdRussianSubtitlesButton, translateWordByWordToThirdRussianSubtitlesButton,
                        thirdRussianSubtitlesActionLabel, thirdRussianSubtitlesTextBox, thirdRussianSubtitlesColorButton,
                        openOrCloseThirdRussianSubtitlesGroupBox, thirdRussianSubtitlesExportAsDocxGroupBox, thirdRussianSubtitlesOpenFromDownloadsButton, openThirdRussianSubtitlesFromDefaultFolderButton, thirdRussianSubtitlesExportAsDocxButton, thirdRussianSubtitlesExportAsDocxIntoDownloadsButton)
                }
            };

            m_buttons = new List<Button>
            {
                openOrClosePrimarySubtitlesButton,
                openOrCloseFirstRussianSubtitlesButton,
                openOrCloseSecondRussianSubtitlesButton,
                openOrCloseThirdRussianSubtitlesButton,
                //translateToPrimarySubtitlesButton,
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

            //m_keyboardHookManager.RegisterHotkey((int)VirtualKeyCode.SPACE, ActionForHotkeyThatArePauseButton);
            //
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

            // Работаем с настройками
            try
            {
                SetProgramAccordingToSettings(atLaunch: true);

                using var settingsForm = new SettingsForm(this);
            }
            catch (BilingualSubtitlerPropertiesLoadingException e)
            {
                var result = MessageBox.Show($"Во время считывания настроек произошла ошибка. Сбросить настройки к значениям по умолчанию и попытаться " +
                    $"считать их вновь?\nКнопка \"Нет\" завершит программу\n\n\nОшибка: {e.InnerException}", "Во время считывания настроек произошла ошибка", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);

                if (result == DialogResult.No)
                    this.Close();
                else
                {
                    Properties.Settings.Default.Reset();
                    Properties.Settings.Default.Save();

                    Properties.SubtitlesAppearanceSettings.Default.Reset();
                    Properties.SubtitlesAppearanceSettings.Default.Save();

                    try
                    {
                        SetProgramAccordingToSettings(atLaunch: true);

                        using var settingsForm = new SettingsForm(this);
                    }
                    catch (BilingualSubtitlerPropertiesLoadingException ex)
                    {
                        result = MessageBox.Show($"Во время считывания настроек вновь произошла ошибка. " +
                            $"По нажатию \"Ок\" можно попытаться продолжить работу программы. По кнопке \"Отмена\" программа будет закрыта.\n\n\n" +
                            $"Ошибка: {ex.InnerException}",
                            "Во время считывания настроек произошла ошибка",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error);

                        if (result == DialogResult.Cancel)
                            this.Close();
                    }
                }
            }

            m_changeVideoAndSubtitlesComboBoxesDelegate = ChangeVideoAndSubtitlesComboBoxesHandler;

            appIsRunningsAsAdministratorPanel.Visible = AppIsRunningAsAdministrator();
            appNotRunningAsAdministratorPanel.Visible = !AppIsRunningAsAdministrator();

            // Проверка обновлений
            var checkUpdatesBgW = new BackgroundWorker();
            checkUpdatesBgW.DoWork += CheckUpdatesBgW_DoWork;
            checkUpdatesBgW.RunWorkerCompleted += CheckUpdatesBgW_RunWorkerCompleted;
            checkUpdatesBgW.RunWorkerAsync();

            openOrClosePrimarySubtitlesButton.Focus();
            openOrClosePrimarySubtitlesButton.Select();
        }

        public void CheckUpdatesBgW_DoWork(object sender, DoWorkEventArgs e)
        {
            // Проверяем наличие новой версии
            if (Settings.Default.CheckUpdates)
            {
                var currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                if (currentVersion > Version.Parse(Properties.Settings.Default.LatestSeenVersion))
                {
                    Settings.Default.LatestSeenVersion = currentVersion.ToString();
                    Settings.Default.Save();
                }

                bool infoFromGitHubIsGet = false;
                string latestVersionOnGitHub = null;
                Release latestReleaseOnGitHub = null;
                try
                {
                    GitHubClient client = new GitHubClient(new ProductHeaderValue("BilingualSubtitler"));
                    latestReleaseOnGitHub = client.Repository.Release.GetLatest(56989530).Result;
                    var latestReleaseOnGitHubName = latestReleaseOnGitHub.Name;
                    latestVersionOnGitHub = latestReleaseOnGitHubName.Substring("Bilingual Subtitler ".Length, latestReleaseOnGitHubName.Length - "Bilingual Subtitler ".Length);
                    infoFromGitHubIsGet = true;
                }
                catch (Exception ex)
                {
                    e.Result = ex;
                }

                if (infoFromGitHubIsGet)
                {
                    e.Result = new Tuple<string, Octokit.Release>(latestVersionOnGitHub, latestReleaseOnGitHub);
                }
            }
        }

        private void CheckUpdatesBgW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Tuple<string, Octokit.Release>)
            {
                var latestVersionOnGitHub = ((Tuple<string, Octokit.Release>)e.Result).Item1;
                var latestReleaseOnGitHub = ((Tuple<string, Octokit.Release>)e.Result).Item2;


                if (Version.Parse(latestVersionOnGitHub) > Version.Parse(Settings.Default.LatestSeenVersion))
                {
                    var result = MessageBox.Show($"Появилась новая версия программы — {latestVersionOnGitHub}!\n\n" +
                        $"Изменения:\n{latestReleaseOnGitHub.Body}\n\n" +
                        "Перейти на страницу скачки?", "Появилась новая версия программы", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("https://github.com/0xotHik/BilingualSubtitler/releases/latest");
                    }

                    Settings.Default.LatestSeenVersion = latestVersionOnGitHub;
                    Settings.Default.Save();
                }
            }
            else if (e.Result is Exception)
            {
                var exception = (Exception)e.Result;
                MessageBox.Show($"Не удалось получить информацию о новых версиях\n\n\nОШибка:{exception.Message}",
                        "Не удалось получить информацию о новых версиях",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetProgramAccordingToSettings(bool atLaunch = false)
        {
            try
            {
                using (var p = Process.GetCurrentProcess())
                {
                    p.PriorityClass = Properties.Settings.Default.ProcessPriority;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Выставление приоритета процесса не удалось.\n\n\nОшибка:{ex}", "Выставление приоритета процесса не удалось",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // Хоткеи программы
                m_keyboardHookManager.Stop();
                m_keyboardHookManager.UnregisterAll();
                //
                var videoPlayerPauseHotkey = new Hotkey(Settings.Default.VideoPlayerPauseButtonString);

                m_biligualSubtitlersHotkeys = new int[Settings.Default.Hotkeys.Count];
                for (int i = 0; i < Settings.Default.Hotkeys.Count; i++)
                {
                    var hotkey = new Hotkey(Settings.Default.Hotkeys[i]);
                    m_biligualSubtitlersHotkeys[i] = hotkey.KeyCode;

                }

                foreach (var keyCode in m_biligualSubtitlersHotkeys)
                {
                    if (keyCode != videoPlayerPauseHotkey.KeyCode)
                        m_keyboardHookManager.RegisterHotkey(keyCode, ActionForHotkeyThatAreNotPauseButton);
                    else
                        m_keyboardHookManager.RegisterHotkey(keyCode, ActionForHotkeyThatArePauseButton);
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

                // Файлы субтитров
                originalSubtitlesFileNameEnding.Text = Properties.Settings.Default.OriginalSubtitlesFileNameEnding;
                bilingualSubtitlesFileNameEnding.Text = Properties.Settings.Default.BilingualSubtitlesFileNameEnding;
                //
                originalSubtitlesFileNameEnding.Visible =
                    originalSubtitlesFileNameEndingLabel.Visible =
                        Settings.Default.CreateOriginalSubtitlesFile;
                //
                bilingualSubtitlesFileNameEnding.Visible =
                     bilingualSubtitlesFileNameEndingLabel.Visible =
                        Settings.Default.CreateBilingualSubtitlesFile;


                m_videoPlayerProcessName = Properties.Settings.Default.VideoPlayerProcessName;

                secondRussianSubtitlesGroupBox.Visible = hideSecondRussianSubtitlesButton.Visible =
                    Settings.Default.SecondRussianSubtitlesIsVisible;
                showSecondRussianSubtitlesButton.Visible = !Settings.Default.SecondRussianSubtitlesIsVisible;

                thirdRussianSubtitlesGroupBox.Visible = hideThirdRussianSubtitlesButton.Visible =
                    Settings.Default.ThirdRussianSubtitlesIsVisible;
                showThirdRussianSubtitlesButton.Visible = !Settings.Default.ThirdRussianSubtitlesIsVisible;

                m_translator = new Translator(Properties.Settings.Default.YandexTranslatorAPIKey);

                translateToRussianSubtitlesGroupBox.Visible =
                translateToFirstRussianSubtitlesGroupBox.Visible =
                    translateToSecondRussianSubtitlesGroupBox.Visible =
                    translateToThirdRussianSubtitlesGroupBox.Visible =
                    Settings.Default.YandexTranslatorAPIEnabled;

                //docXTranslationGroupBox.Visible = !Settings.Default.YandexTranslatorAPIEnabled;

                // Настройки вида субтитров:
                // Системные шрифты
                using InstalledFontCollection fontsCollection = new InstalledFontCollection();
                FontFamily[] fontFamilies = fontsCollection.Families;
                foreach (FontFamily font in fontFamilies)
                {
                    originalSubtitlesFontComboBox.Items.Add(font.Name);
                    firstRussianSubtitlesFontComboBox.Items.Add(font.Name);
                    secondRussianSubtitlesFontComboBox.Items.Add(font.Name);
                    thirdRussianSubtitlesFontComboBox.Items.Add(font.Name);
                }

                if (atLaunch || !m_redefineSubtitlesAppearanceSettings)
                    SetSubtitlesAppearanceBoxesAccordingToSettings();

                SetNewRedefineSubtitlesAppearanceSettingsSetting(Settings.Default.RedefineSubtitlesAppearanceSettings);

                // Advanced Mode
                var advancedMode = Settings.Default.AdvancedMode;
                //
                firstRussianSubtitlesExportAsDocxButton.Visible =
                    secondRussianSubtitlesExportAsDocxButton.Visible =
                    thirdRussianSubtitlesExportAsDocxButton.Visible =

                    googleTranslatorLinkLabel.Visible =
                    redefineSubtitlesAppearanceSettingsCheckBox.Visible =
                    subtitlesAppearanceGroupBox.Visible =
                    advancedMode;
                //
                var buttonOpenSubtitlesLeft = advancedMode ? m_initialOpenSubtitlesButtonLeft : (openOrClosePrimarySubtitlesGroupBox.Width / 2) - (openOrClosePrimarySubtitlesButton.Width / 2);
                var exportAsDocxSubtitlesLeft = advancedMode ? m_initialExportSubtitlesAsDocxButtonLeft : (primarySubtitlesExportAsDocxGroupBox.Width / 2) - (primarySubtitlesExportAsDocxButton.Width / 2);
                //
                foreach (var subtitles in m_subtitles)
                {
                    var subtitlesWithInfo = subtitles.Value;

                    subtitlesWithInfo.ButtonOpenOrClose.Left = buttonOpenSubtitlesLeft;
                    subtitlesWithInfo.ExportAsDocxButton.Left = buttonOpenSubtitlesLeft;

                    subtitlesWithInfo.OpenFromDownloadsButton.Visible = advancedMode;
                    subtitlesWithInfo.OpenFromDefaultFolderButton.Visible = advancedMode;
                    subtitlesWithInfo.ExportAsDocxIntoDownloadsButton.Visible = advancedMode;

                    if (subtitles.Key != SubtitlesType.Original)
                    {
                        subtitlesWithInfo.ExportAsDocxGroupBox.Visible = advancedMode;
                    }
                }
                //
                if (!advancedMode)
                {
                    //hideSecondRussianSubtitlesButton.Location = new Point(secondRussianSubtitlesColorButton.Right + 20, hideSecondRussianSubtitlesButton.Location.Y);
                    //secondRussianSubtitlesGroupBox.Width = secondRussianSubtitlesGroupBox.Width - translateToSecondRussianSubtitlesGroupBox.Width -
                    //    secondRussianSubtitlesExportAsDocx.Width + 40;

                    //thirdRussianSubtitlesGroupBox.Width = thirdRussianSubtitlesGroupBox.Width - translateToThirdRussianSubtitlesGroupBox.Width -
                    //   thirdRussianSubtitlesExportAsDocx.Width + 40;

                    //firstRussianSubtitlesGroupBox.Width = firstRussianSubtitlesGroupBox.Width - translateToFirstRussianSubtitlesGroupBox.Width -
                    // firstRussianSubtitlesExportAsDocx.Width + 40;

                    secondRussianSubtitlesGroupBox.Width = thirdRussianSubtitlesGroupBox.Width = firstRussianSubtitlesGroupBox.Width = primarySubtitlesColorGroupBox.Right + hideSecondRussianSubtitlesButton.Width + 10;

                    hideThirdRussianSubtitlesButton.Location = new Point(thirdRussianSubtitlesGroupBox.Width - hideThirdRussianSubtitlesButton.Width, hideThirdRussianSubtitlesButton.Location.Y);
                    hideSecondRussianSubtitlesButton.Location = new Point(secondRussianSubtitlesGroupBox.Width - hideSecondRussianSubtitlesButton.Width, hideSecondRussianSubtitlesButton.Location.Y);
                }
                else
                {
                    secondRussianSubtitlesGroupBox.Width = thirdRussianSubtitlesGroupBox.Width = firstRussianSubtitlesGroupBox.Width = m_initialFirstRussianSubtitlesGroupBoxWidth;

                    hideThirdRussianSubtitlesButton.Location = new Point(m_initialThirdRussianSubtitlesHideButtonX, hideThirdRussianSubtitlesButton.Location.Y);
                    hideSecondRussianSubtitlesButton.Location = new Point(m_initialSecondRussianSubtitlesHideButtonX, hideSecondRussianSubtitlesButton.Location.Y);
                }
                //
                this.Width = advancedMode ? m_initialFormWidth : m_initialFormWidth - subtitlesAppearanceGroupBox.Width;

                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DownloadsFolder))
                {
                    Properties.Settings.Default.DownloadsFolder = new KnownFolder(KnownFolderType.Downloads).Path;
                    Properties.Settings.Default.Save();
                }

            }
            catch (Exception e)
            {
                throw new BilingualSubtitlerPropertiesLoadingException(e);
            }
        }

        private void SetSubtitlesAppearanceBoxesAccordingToSettings()
        {
            changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked =
               Properties.Settings.Default.ChangeRussianSubtitlesStylesAccordingToOriginal;
            secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreen;
            secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled = Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreenEnabled;

            var originalSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.OriginalSubtitlesStyleString.Split(';');
            foreach (var fontItem in originalSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == originalSubtitlesStyle[0])
                {
                    originalSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(originalSubtitlesFontComboBox.Text))
                originalSubtitlesFontComboBox.Text = originalSubtitlesStyle[0];
            originalSubtitlesMarginNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[1]);
            originalSubtitlesSizeNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[2]);
            originalSubtitlesOutlineNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[3]);
            originalSubtitlesShadowNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[4]);
            originalSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[5]);
            originalSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[6]);
            originalSubtitlesInOneLineCheckBox.Checked = originalSubtitlesStyle[7] == "1";

            var firstRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.FirstRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in firstRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == firstRussianSubtitlesStyle[0])
                {
                    firstRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(firstRussianSubtitlesFontComboBox.Text))
                firstRussianSubtitlesFontComboBox.Text = firstRussianSubtitlesStyle[0];
            firstRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[1]);
            firstRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[2]);
            firstRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[3]);
            firstRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[4]);
            firstRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[5]);
            firstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[6]);
            firstRussianSubtitlesInOneLineCheckBox.Checked = firstRussianSubtitlesStyle[7] == "1";

            var secondRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.SecondRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in secondRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == secondRussianSubtitlesStyle[0])
                {
                    secondRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(secondRussianSubtitlesFontComboBox.Text))
                secondRussianSubtitlesFontComboBox.Text = secondRussianSubtitlesStyle[0];
            secondRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[1]);
            secondRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[2]);
            secondRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[3]);
            secondRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[4]);
            secondRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[5]);
            secondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[6]);
            secondRussianSubtitlesInOneLineCheckBox.Checked = secondRussianSubtitlesStyle[7] == "1";

            var thirdRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.ThirdRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in thirdRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == thirdRussianSubtitlesStyle[0])
                {
                    thirdRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(thirdRussianSubtitlesFontComboBox.Text))
                thirdRussianSubtitlesFontComboBox.Text = thirdRussianSubtitlesStyle[0];
            thirdRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[1]);
            thirdRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[2]);
            thirdRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[3]);
            thirdRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[4]);
            thirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[5]);
            thirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[6]);
            thirdRussianSubtitlesInOneLineCheckBox.Checked = thirdRussianSubtitlesStyle[7] == "1";
        }

        private void SetNewRedefineSubtitlesAppearanceSettingsSetting(bool newRedefineSubtitlesAppearanceSettingsValue)
        {
            redefineSubtitlesAppearanceSettingsCheckBox.Checked = subtitlesAppearanceGroupBox.Enabled = newRedefineSubtitlesAppearanceSettingsValue;
            redefineSubtitlesAppearanceSettingsCheckBox.Enabled = true;

            if (newRedefineSubtitlesAppearanceSettingsValue != Settings.Default.RedefineSubtitlesAppearanceSettings)
            {
                Settings.Default.RedefineSubtitlesAppearanceSettings = newRedefineSubtitlesAppearanceSettingsValue;
                Settings.Default.Save();
            }
        }

        public void CheckUpdates()
        {
            // Проверяем наличие новой версии
            if (Settings.Default.CheckUpdates)
            {
                var currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                if (currentVersion > Version.Parse(Properties.Settings.Default.LatestSeenVersion))
                {
                    Settings.Default.LatestSeenVersion = currentVersion.ToString();
                    Settings.Default.Save();
                }

                bool infoFromGitHubIsGet = false;
                string latestVersionOnGitHub = null;
                Release latestReleaseOnGitHub = null;
                try
                {
                    GitHubClient client = new GitHubClient(new ProductHeaderValue("BilingualSubtitler"));
                    latestReleaseOnGitHub = client.Repository.Release.GetLatest(56989530).Result;
                    var latestReleaseOnGitHubName = latestReleaseOnGitHub.Name;
                    latestVersionOnGitHub = latestReleaseOnGitHubName.Substring("Bilingual Subtitler ".Length, latestReleaseOnGitHubName.Length - "Bilingual Subtitler ".Length);
                    infoFromGitHubIsGet = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось получить информацию о новых версиях\n\n\nОшибка: {ex.Message}",
                        "Не удалось получить информацию о новых версиях",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (infoFromGitHubIsGet)
                {
                    if (Version.Parse(latestVersionOnGitHub) > Version.Parse(Settings.Default.LatestSeenVersion))
                    {
                        var result = MessageBox.Show($"Появилась новая версия программы — {latestVersionOnGitHub}!\n\n" +
                            $"Изменения:\n{latestReleaseOnGitHub.Body}\n\n" +
                            "Перейти на страницу скачки?", "Появилась новая версия программы", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start("https://github.com/0xotHik/BilingualSubtitler/releases/latest");
                        }

                        Settings.Default.LatestSeenVersion = latestVersionOnGitHub;
                        Settings.Default.Save();
                    }
                }
            }
        }

        private void ChangeVideoAndSubtitlesComboBoxesHandler()
        {
            videoAndSubtitlesStateComboBox.SelectedValueChanged -= videoAndSubtitlesStateComboBox_SelectedValueChanged;

            // Ну вот здесь по-хорошему, если с точки зрения верности кода, надо по-другому написать, наверное. Примерно как было.
            // Но у меня приоритет в скорости, структура состояний видео/субтитров у меня не поменяется (не должна).
            // Так что напишем так
            if (m_videoState == VideoState.Playing)
            {
                if (m_subtitlesState == SubtitlesState.Original)
                    videoAndSubtitlesStateComboBox.SelectedItem = m_videoPlayingWithOriginalSubtitlesComboBoxItem;
                else
                    videoAndSubtitlesStateComboBox.SelectedItem = m_videoPlayingWithBilingualSubtitlesComboBoxItem;
            }
            else // На паузе
            {
                if (m_subtitlesState == SubtitlesState.Original)
                    videoAndSubtitlesStateComboBox.SelectedItem = m_videoPausedWithOriginalSubtitlesComboBoxItem;
                else
                    videoAndSubtitlesStateComboBox.SelectedItem = m_videoPausedWithBilingualSubtitlesComboBoxItem;
            }

            videoAndSubtitlesStateComboBox.SelectedValueChanged += videoAndSubtitlesStateComboBox_SelectedValueChanged;

            #region Былое
            //videoStateComboBox.SelectedValueChanged -= videoStateComboBox_SelectedValueChanged;
            //subtitlesStateComboBox.SelectedValueChanged -= subtitlesStateComboBox_SelectedValueChanged;

            //if (m_videoState == VideoState.Playing)

            //    videoStateComboBox.SelectedItem = m_videoStatesAndRelatedComboBoxItems[m_videoState];
            //subtitlesStateComboBox.SelectedItem = m_subtitlesStatesAndRelatedComboBoxItems[m_subtitlesState];

            //videoStateComboBox.SelectedValueChanged += videoStateComboBox_SelectedValueChanged;
            //subtitlesStateComboBox.SelectedValueChanged += subtitlesStateComboBox_SelectedValueChanged;
            #endregion
        }

        private Subtitle[] ReadDocx(string pathToDOCXFile)
        {
            DocX doc = null;
            try
            {
                doc = DocX.Load(pathToDOCXFile);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Произошла ошибка во время считывания DocX", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return null;
            }

            return ReadSRTMarkupInDocx(doc.Paragraphs);
        }


        private Subtitle[] ReadSRTFile(string pathToSRTFile)
        {
            return ReadSRTMarkup(File.ReadAllLines(pathToSRTFile));
        }

        private Subtitle[] ReadSRTMarkup(string[] readedLines)
        {
            var subsLines = 0;

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

        private Subtitle[] ReadSRTMarkupInDocx(System.Collections.ObjectModel.ReadOnlyCollection<Xceed.Document.NET.Paragraph> readedLines)
        {
            var subsLines = 0;

            foreach (var line in readedLines)
            {
                if (line.Text.Contains("->"))
                    subsLines++;
            }

            var subtitles = new Subtitle[subsLines];
            int currentSubtitleIndex = 0;
            for (int i = 0; i < readedLines.Count - 1; i++)
            {
                if (readedLines[i].Text.Contains("->"))
                {
                    try
                    {
                        subtitles[currentSubtitleIndex] = new Subtitle(
                            readedLines[i].Text,
                            (readedLines[i + 1].Text));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удался парсинг субтитра из\n{readedLines[i].Text}\n{readedLines[i + 1].Text}\n!\n\nОшибка:{ex.ToString()}",
                            "Не удался парсинг субтитра", MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }

                    i += 2;

                    while ((i < readedLines.Count) && (!string.IsNullOrWhiteSpace(readedLines[i].Text)))
                    {
                        subtitles[currentSubtitleIndex].Text += $"\n{readedLines[i]}";

                        i++;
                    }

                    currentSubtitleIndex++;
                }
            }

            return subtitles;
        }

        private void ReadASSMarkedupDocumentWithBilingualSubtitles(string filePath)
        {
            var lines = File.ReadAllLines(filePath);


            var currentStringIndex = 0;
            // Header
            currentStringIndex += m_assHeader.Count;

            // Стили
            // Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
            //Style: 0_sub_stream,Arial,20,&H00FFFFFF,&H0000FFFF,&H00000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,42,1
            //Style: 1_sub_stream,Times New Roman,20,&H000000FF,&H0000FFFF,&H00000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,0,1
            //Style: 2_sub_stream,Times New Roman,20,&H668000FF,&H6600FFFF,&H66000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,206,1
            //Style: 3_sub_stream,Times New Roman,20,&H6600D7FF,&H6600FFFF,&H66000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,248,1

            SubtitlesStyle originalSubtitlesStyle = null;
            SubtitlesStyle firstRussianSubtitlesStyle = null;
            SubtitlesStyle secondRussianSubtitlesStyle = null;
            SubtitlesStyle thirdRussianSubtitlesStyle = null;

            originalSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);
            currentStringIndex++;
            if (lines[currentStringIndex] == "[Events]") // Выходим, стили закончились
            {
            }
            else
            {
                firstRussianSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);
                currentStringIndex++;
                if (lines[currentStringIndex] == "[Events]") // Выходим, стили закончились
                {
                }
                else
                {
                    secondRussianSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);
                    currentStringIndex++;
                    if (lines[currentStringIndex] == "[Events]") // Выходим, стили закончились
                    {
                    }
                    else
                    {
                        thirdRussianSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);
                        currentStringIndex++;
                    }
                }
            }

            if (m_redefineSubtitlesAppearanceSettings)
            {
                WriteSubtitlesStyleToFormControls(originalSubtitlesStyle, SubtitlesType.Original);
                if (firstRussianSubtitlesStyle != null)
                    WriteSubtitlesStyleToFormControls(firstRussianSubtitlesStyle, SubtitlesType.FirstRussian);
                if (secondRussianSubtitlesStyle != null)
                    WriteSubtitlesStyleToFormControls(secondRussianSubtitlesStyle, SubtitlesType.SecondRussian);
                if (thirdRussianSubtitlesStyle != null)
                    WriteSubtitlesStyleToFormControls(thirdRussianSubtitlesStyle, SubtitlesType.ThirdRussian);
            }

            //[Events]
            currentStringIndex++;

            //Format: Layer, Start, End, Style, Actor, MarginL, MarginR, MarginV, Effect, Text
            currentStringIndex++;

            // Начался текст
            var originalSubStream = new List<Subtitle>();
            var firstRussianSubStream = new List<Subtitle>();
            var secondRussianSubStream = new List<Subtitle>();
            var thirdRussianSubStream = new List<Subtitle>();
            // Dialogue: 0, --0
            //0:00:04.96, --1
            //    0:00:06.28, --2
            //    0_sub_stream, --3
            //,
            //    0,
            //    0,
            //    0,
            //,
            //Why today? --9+
            for (int i = currentStringIndex; i < lines.Length; i++)
            {
                var line = lines[i];
                var components = line.Split(',');

                // Собираем текст
                var subtitleTextSB = new StringBuilder();
                for (int j = 9; j < components.Length; j++)
                {
                    subtitleTextSB.Append(components[j]);

                    if (j + 1 < components.Length)
                    {
                        subtitleTextSB.Append(",");
                    }
                }

                var subtitle = new Subtitle(components[1], components[2], subtitleTextSB.ToString());

                var originalSubStreamName = "0" + m_subtitleStyleNamePostfix;
                var firstRussianSubStreamName = $"1{m_subtitleStyleNamePostfix}";
                var secondRussianSubStreamName = $"2{m_subtitleStyleNamePostfix}";
                var thirdRussianSubStreamName = $"3{m_subtitleStyleNamePostfix}";

                if (components[3] == originalSubStreamName)
                    originalSubStream.Add(subtitle);
                else if (components[3] == firstRussianSubStreamName)
                    firstRussianSubStream.Add(subtitle);
                else if (components[3] == secondRussianSubStreamName)
                    secondRussianSubStream.Add(subtitle);
                else if (components[3] == thirdRussianSubStreamName)
                    thirdRussianSubStream.Add(subtitle);

            }

            if (originalSubStream.Count != 0)
            {
                var originalSubtitlesFileFI = new FileInfo(filePath);
                var extension = originalSubtitlesFileFI.Extension;
                var originalFilePathPart = originalSubtitlesFileFI.FullName.Substring(0,
                    originalSubtitlesFileFI.FullName.Length -
                   (extension.Length));

                SetSubtitlesAndVideoFilePaths(originalSubtitlesFileFI.FullName, false);

                translateToFirstRussianSubtitlesButton.Enabled = translateWordByWordToFirstRussianSubtitlesButton.Enabled =
                    translateToSecondRussianSubtitlesButton.Enabled = translateWordByWordToSecondRussianSubtitlesButton.Enabled =
                        translateToThirdRussianSubtitlesButton.Enabled = translateWordByWordToThirdRussianSubtitlesButton.Enabled =
                            true;

                WriteReadFromAssSubtitlesIntoStructure(SubtitlesType.Original, originalSubStream, filePath);
            }
            if (firstRussianSubStream.Count != 0)
            {
                WriteReadFromAssSubtitlesIntoStructure(SubtitlesType.FirstRussian, firstRussianSubStream, filePath);
            }
            if (secondRussianSubStream.Count != 0)
            {
                WriteReadFromAssSubtitlesIntoStructure(SubtitlesType.SecondRussian, secondRussianSubStream, filePath);
            }
            if (thirdRussianSubStream.Count != 0)
            {
                WriteReadFromAssSubtitlesIntoStructure(SubtitlesType.ThirdRussian, thirdRussianSubStream, filePath);
            }


        }

        private void WriteReadFromAssSubtitlesIntoStructure(SubtitlesType type, List<Subtitle> listOfSubtitles, string filePath)
        {
            var subtitlesAndInfo = m_subtitles[type];

            subtitlesAndInfo.Subtitles = listOfSubtitles.ToArray();

            subtitlesAndInfo.ProgressLabel.Visible = subtitlesAndInfo.ProgressBar.Visible = subtitlesAndInfo.ActionLabel.Visible = true;

            SetGUIContolsToSubtitlesWasSuccessfullyLoaded(subtitlesAndInfo);

            subtitlesAndInfo.SetOriginalFile(filePath, false);
        }

        private void SetGUIContolsToSubtitlesWasSuccessfullyLoaded(SubtitlesAndInfo subtitlesAndInfo)
        {
            subtitlesAndInfo.ProgressBar.Value = primarySubtitlesProgressBar.Maximum;
            subtitlesAndInfo.ProgressLabel.Text = $"100%";
            subtitlesAndInfo.ActionLabel.Text = SUBTITLES_ARE_OPENED;
            subtitlesAndInfo.ButtonOpenOrClose.Text = $"x\nУбрать";

            subtitlesAndInfo.ButtonOpenOrClose.Left = (openOrClosePrimarySubtitlesGroupBox.Width / 2) - (openOrClosePrimarySubtitlesButton.Width / 2);

            subtitlesAndInfo.OpenSubtitlesGroupBox.Text = "Поток субтитров";
            m_initialOpenSubtitlesGroupBoxTextBeforeCloseConfirmationDialog = subtitlesAndInfo.OpenSubtitlesGroupBox.Text;
            subtitlesAndInfo.OpenFromDownloadsButton.Visible =
            subtitlesAndInfo.OpenFromDefaultFolderButton.Visible =
            subtitlesAndInfo.OpenFromDownloadsButton.Enabled =
            subtitlesAndInfo.OpenFromDefaultFolderButton.Enabled = false;
        }

        private void WriteSubtitlesStyleToFormControls(SubtitlesStyle style, SubtitlesType subtitlesType)
        {

            ComboBox targetSubtitlesFontComboBox = null;
            NumericUpDown targetSubtitlesMarginNumericUpDown = null;
            NumericUpDown targetSubtitlesSizeNumericUpDown = null;
            NumericUpDown targetSubtitlesOutlineNumericUpDown = null;
            NumericUpDown targetSubtitlesShadowNumericUpDown = null;
            NumericUpDown targetSubtitlesTransparencyPercentageNumericUpDown = null;
            NumericUpDown targetSubtitlesShadowTransparencyPercentageNumericUpDown = null;
            CheckBox targetSubtitlesInOneLineCheckBox = null;
            Button targetColorButton = null;

            switch (subtitlesType)
            {
                case SubtitlesType.Original:
                    {
                        targetSubtitlesFontComboBox = originalSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = originalSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = originalSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = originalSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = originalSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = originalSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = originalSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = originalSubtitlesInOneLineCheckBox;
                        targetColorButton = primarySubtitlesColorButton;

                        break;
                    }
                case SubtitlesType.FirstRussian:
                    {
                        targetSubtitlesFontComboBox = firstRussianSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = firstRussianSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = firstRussianSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = firstRussianSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = firstRussianSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = firstRussianSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = firstRussianSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = firstRussianSubtitlesInOneLineCheckBox;
                        targetColorButton = firstRussianSubtitlesColorButton;

                        break;
                    }
                case SubtitlesType.SecondRussian:
                    {
                        targetSubtitlesFontComboBox = secondRussianSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = secondRussianSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = secondRussianSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = secondRussianSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = secondRussianSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = secondRussianSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = secondRussianSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = secondRussianSubtitlesInOneLineCheckBox;
                        targetColorButton = secondRussianSubtitlesColorButton;

                        break;
                    }
                case SubtitlesType.ThirdRussian:
                    {
                        targetSubtitlesFontComboBox = thirdRussianSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = thirdRussianSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = thirdRussianSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = thirdRussianSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = thirdRussianSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = thirdRussianSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = thirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = thirdRussianSubtitlesInOneLineCheckBox;
                        targetColorButton = thirdRussianSubtitlesColorButton;

                        break;
                    }
            }

            foreach (var fontItem in targetSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == style.Font)
                {
                    targetSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(targetSubtitlesFontComboBox.Text))
                targetSubtitlesFontComboBox.Text = style.Font;
            targetSubtitlesMarginNumericUpDown.Value = style.MarginV;
            targetSubtitlesSizeNumericUpDown.Value = style.Size;
            targetSubtitlesOutlineNumericUpDown.Value = style.OutlineSize;
            targetSubtitlesShadowNumericUpDown.Value = style.ShadowSize;
            targetSubtitlesTransparencyPercentageNumericUpDown.Value = style.TransparencyPercentage;
            targetSubtitlesShadowTransparencyPercentageNumericUpDown.Value = style.ShadowTransparencyPercentage;
            targetColorButton.BackColor = style.Color;
        }

        private StringBuilder GenerateASSMarkedupDocument(Tuple<Subtitle[], Color>[] subtitlesAndTheirColorsPairs)
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
            foreach (var line in m_assHeader)
                assSB.Append(line);


            // Style: Default,Arial,20,&H00FFFFFF,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,10,1
            // Style: Копировать из Default,Arial,20,&H00C26F03,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,55,1
            // Style: Копировать из Копировать из Default,Arial,20,&H000C15DC,&H0300FFFF,&H00000000,&H02000000,0,0,0,0,100,100,0,0,1,2,1,2,10,10,100,1


            var subtitleInOneLine = new bool[4];
            for (int i = 0; i < subtitlesAndTheirColorsPairs.Length; i++)
            {
                string font = null;
                string marginV = null;
                string size = null;
                string outline = null;
                string shadow = null;

                decimal transparencyPercentage = 0;
                string transparency = null;
                decimal shadowTransparencyPercentage = 0;
                string shadowTransparency = null;

                Color? color = null;

                if (m_redefineSubtitlesAppearanceSettings)
                {
                    switch (i)
                    {
                        case 0: //Оригинальные
                            {
                                font = originalSubtitlesFontComboBox.Text;
                                marginV = originalSubtitlesMarginNumericUpDown.Text;
                                size = originalSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = originalSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = originalSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = originalSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(float.Parse(transparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");
                                shadowTransparencyPercentage = originalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");

                                subtitleInOneLine[i] = (originalSubtitlesInOneLineCheckBox.Checked ? true : false);

                                color = primarySubtitlesColorButton.BackColor;

                                break;
                            }
                        case 1: //1-е переведенные
                            {
                                font = firstRussianSubtitlesFontComboBox.Text;
                                marginV = firstRussianSubtitlesMarginNumericUpDown.Text;
                                size = firstRussianSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = firstRussianSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = firstRussianSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = firstRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(float.Parse(transparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");
                                shadowTransparencyPercentage = firstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");

                                subtitleInOneLine[i] = (firstRussianSubtitlesInOneLineCheckBox.Checked ? true : false);

                                color = firstRussianSubtitlesColorButton.BackColor;

                                break;
                            }
                        case 2: //2-е переведенные
                            {
                                font = secondRussianSubtitlesFontComboBox.Text;
                                marginV = secondRussianSubtitlesMarginNumericUpDown.Text;
                                size = secondRussianSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = secondRussianSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = secondRussianSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = secondRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(float.Parse(transparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");
                                shadowTransparencyPercentage = secondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");

                                subtitleInOneLine[i] = (secondRussianSubtitlesInOneLineCheckBox.Checked ? true : false);

                                color = secondRussianSubtitlesColorButton.BackColor;

                                break;
                            }
                        case 3: //3-и переведенные
                            {
                                font = thirdRussianSubtitlesFontComboBox.Text;
                                marginV = thirdRussianSubtitlesMarginNumericUpDown.Text;
                                size = thirdRussianSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = thirdRussianSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = thirdRussianSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = thirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(float.Parse(transparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");
                                shadowTransparencyPercentage = thirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");

                                subtitleInOneLine[i] = (thirdRussianSubtitlesInOneLineCheckBox.Checked ? true : false);

                                color = thirdRussianSubtitlesColorButton.BackColor;

                                break;
                            }
                    }


                    //((int)((int.Parse(transparencyPercentage) == 0 ? 100f : float.Parse(transparencyPercentage)) / 100f
                    //// Иначе при прозрачности в 0 и тень становится полностью непрозрачной
                    //* float.Parse(shadowTransparencyPercentage) / 100f
                    //* 255f)).ToString("X2");

                    //var transparency = i == 0 ? "00" : "64";
                    //var marginV = i == 3 ? 0
                    //    : (2*20 + 5) + i * (2 * 20 + 5);
                    //var outline = 2;
                    //var shadow = 1;

                }
                else
                {
                    string[] styleComponents = null;
                    switch (i)
                    {
                        case 0:
                            {
                                styleComponents = Properties.SubtitlesAppearanceSettings.Default.OriginalSubtitlesStyleString.Split(';');
                                break;
                            }
                        case 1:
                            {
                                styleComponents = Properties.SubtitlesAppearanceSettings.Default.FirstRussianSubtitlesStyleString.Split(';');
                                break;
                            }
                        case 2:
                            {
                                styleComponents = Properties.SubtitlesAppearanceSettings.Default.SecondRussianSubtitlesStyleString.Split(';');
                                break;
                            }
                        case 3:
                            {
                                styleComponents = Properties.SubtitlesAppearanceSettings.Default.ThirdRussianSubtitlesStyleString.Split(';');
                                break;
                            }
                    }

                    font = styleComponents[0];
                    marginV = styleComponents[1];
                    size = styleComponents[2];
                    outline = styleComponents[3];
                    shadow = styleComponents[4];

                    transparencyPercentage = decimal.Parse(styleComponents[5]);
                    transparency = ((int)(float.Parse(transparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");
                    shadowTransparencyPercentage = decimal.Parse(styleComponents[6]);
                    shadowTransparency = ((int)(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f)).ToString("X2");

                    subtitleInOneLine[i] = (styleComponents[7] == "1");

                    //((int)((int.Parse(transparencyPercentage) == 0 ? 100f : float.Parse(transparencyPercentage)) / 100f
                    //// Иначе при прозрачности в 0 и тень становится полностью непрозрачной
                    //* float.Parse(shadowTransparencyPercentage) / 100f
                    //* 255f)).ToString("X2");

                    //var transparency = i == 0 ? "00" : "64";
                    //var marginV = i == 3 ? 0
                    //    : (2*20 + 5) + i * (2 * 20 + 5);
                    //var outline = 2;
                    //var shadow = 1;

                    color = subtitlesAndTheirColorsPairs[i].Item2;

                }

                assSB.AppendLine(
                    $"Style: {i}{m_subtitleStyleNamePostfix}," +
                    $"{font}," +
                    $"{size}," +
                    $"&H" +
                    $"{transparency}" +
                    $"{color.Value.B.ToString("X2")}" +
                    $"{color.Value.G.ToString("X2")}" +
                    $"{color.Value.R.ToString("X2")}," +
                    $"&H{transparency}00FFFF," +
                    $"&H{transparency}000000," +
                    $"&H{shadowTransparency}000000," +
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
            for (int i = 0; i < subtitlesAndTheirColorsPairs.Length; i++)
            {
                var subtitles = subtitlesAndTheirColorsPairs[i].Item1;
                if (subtitles != null)
                {
                    foreach (var subtitle in subtitles)
                    {
                        if (subtitle != null)
                        {
                            string subtitleText;

                            // Перенос
                            if (subtitle.Text.Contains("\n"))
                            {
                                subtitleText = (string)subtitle.Text.Clone();

                                if (subtitleInOneLine[i])
                                {
                                    subtitleText = subtitleText.Replace("\n", " ");
                                }
                                else
                                {
                                    subtitleText = subtitleText.Replace("\n", "\\N");
                                }
                            }
                            else
                            {
                                subtitleText = subtitle.Text;
                            }

                            assSB.AppendLine($"Dialogue: 0," +
                                             $"{subtitle.Start.ToString(assTimeFormat)}," +
                                             $"{subtitle.End.ToString(assTimeFormat)}," +
                                             $"{i}{m_subtitleStyleNamePostfix}," +
                                             $",0,0,0,," +
                                             $"{subtitleText}");
                        }
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
            subtitlesInfo.ButtonOpenOrClose.Enabled = false;
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
            subtitlesInfo.ButtonOpenOrClose.Enabled = true;
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

            try
            {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"При обработке переведенного текста возникла ошибка. Будет записан оригинальный текст:\n«{originalText}»\n\n" +
                    $"Ошибка: {ex.ToString()}", "При обработке переведенного текста возникла ошибка", MessageBoxButtons.OK, icon: MessageBoxIcon.Error);

                return originalText;
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
                    if (string.IsNullOrWhiteSpace(Properties.Settings.Default.YandexTranslatorAPIKey))
                    {
                        MessageBox.Show("Для выполнения перевода оригинальных субтитров нужно ввести ключ для API Яндекс.Переводчика" +
                                        "в разделе \"Ключ Яндекс.Переводчика\" в настройках программы!",
                            "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
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

        private void OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType subtitlesType,
            bool fromDownloadsFolder = false, bool fromDefaultFolder = false)
        {
            // Чекаем пути (непутю)
            if (fromDownloadsFolder)
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.DownloadsFolder))
                {
                    MessageBox.Show("Путь к папке \"Загрузки\" задан неверно! Проверьте настройки программы.", "Путь к папке \"Загрузки\" задан неверно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
            //
            if (fromDefaultFolder)
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.FolderToOpenFilesByDefaultFrom))
                {
                    MessageBox.Show("Путь к папке для открытия файлов по умолчанию задан неверно! Проверьте настройки программы.", "Путь к папке для открытия файлов по умолчанию задан неверно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }


            if (subtitlesType == SubtitlesType.Original)
            {
                translateToFirstRussianSubtitlesButton.Enabled = translateWordByWordToFirstRussianSubtitlesButton.Enabled =
                translateToSecondRussianSubtitlesButton.Enabled = translateWordByWordToSecondRussianSubtitlesButton.Enabled =
                    translateToThirdRussianSubtitlesButton.Enabled = translateWordByWordToThirdRussianSubtitlesButton.Enabled =
                        false;
            }

            var subtitlesWithInfo = m_subtitles[subtitlesType];

            if (subtitlesWithInfo.Subtitles == null) //Открываем субтитры
            {
                string formats = "Файлы Matroska Video (.mkv); файлы SubRip Text (.srt); файлы DocX (.docx); архивы Zip, содержащие субтитры в формате SubRip Text (.zip, внутри .srt) |*.mkv; *.srt; *.docx; *.zip";

                using var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = formats;

                // Папки по умолчанию
                if (fromDownloadsFolder)
                {
                    string downloadsFolderPath = Properties.Settings.Default.DownloadsFolder;
                    openFileDialog.InitialDirectory = downloadsFolderPath;
                }
                else if (fromDefaultFolder)
                    openFileDialog.InitialDirectory = Settings.Default.FolderToOpenFilesByDefaultFrom;

                var result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ReadSubtitlesFromFile(openFileDialog.FileName, subtitlesType);
                }
            }
            // Субтитры есть
            // Значит нажали на кнопку, когда она была в состоянии "Закрыть поток"
            else // Закрываем поток
            {
                // Создаем кнопки для подтверждения / отмены
                var closeSubtitleStreamConfimationButton = new Button
                {
                    Tag = subtitlesType,
                    Size = subtitlesWithInfo.ButtonOpenOrClose.Size,
                    Top = subtitlesWithInfo.ButtonOpenOrClose.Top,
                    Left = m_initialOpenSubtitlesButtonLeft,
                    BackColor = Color.Red,
                    Text = "🗑\nУбрать",
                    ForeColor = Color.White
                };
                //
                subtitlesWithInfo.OpenSubtitlesGroupBox.Text = "Точно убрать?";
                //
                subtitlesWithInfo.ButtonOpenOrClose.Hide();
                closeSubtitleStreamConfimationButton.Click += CloseSubtitleStreamConfimationButton_Click;
                subtitlesWithInfo.ButtonOpenOrClose.Parent.Controls.Add(closeSubtitleStreamConfimationButton);
                closeSubtitleStreamConfimationButton.Show();
                //
                var closeSubtitleStreamCancellationButton = new Button
                {
                    Tag = subtitlesType,
                    Size = closeSubtitleStreamConfimationButton.Size,
                    Top = subtitlesWithInfo.ButtonOpenOrClose.Top,
                    Left = m_initialOpenSubtitlesFromDownloadsButtonLeft,
                    BackColor = Color.LightGreen,
                    Text = "↩\nОтмена",
                    //ForeColor = Color.White
                };
                //subtitlesWithInfo.ColorPickingButton.Hide();
                closeSubtitleStreamCancellationButton.Click += CloseSubtitleStreamCancellationButton_Click;
                subtitlesWithInfo.ButtonOpenOrClose.Parent.Controls.Add(closeSubtitleStreamCancellationButton);
                closeSubtitleStreamCancellationButton.Show();
                closeSubtitleStreamCancellationButton.BringToFront();
                //
                subtitlesWithInfo.CloseSubtitleStreamConfimationButton = closeSubtitleStreamConfimationButton;
                subtitlesWithInfo.CloseSubtitleStreamCancellationButton = closeSubtitleStreamCancellationButton;

                //var result = MessageBox.Show($"Вы уверены, что хотите убрать поток субтитров \"{subtitlesWithInfo.OutputTextBox.Text}\"?", "Убрать поток субтитров?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); ;

                //if (result == DialogResult.Yes)
                //{
                //    subtitlesWithInfo.Subtitles = null;
                //    subtitlesWithInfo.ButtonOpen.Text = $"📁\nОткрыть\nиз файла";
                //    subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
                //    subtitlesWithInfo.ProgressLabel.Text = $"0%";
                //    subtitlesWithInfo.ActionLabel.Text = "Поток субтитров был убран";
                //    subtitlesWithInfo.OutputTextBox.Text = string.Empty;
                //}
            }

        }

        private void CloseSubtitleStreamConfimationButton_Click(object sender, EventArgs e)
        {
            var closeSubtitleStreamConfimationButton = (Button)sender;
            var subtitlesType = (SubtitlesType)closeSubtitleStreamConfimationButton.Tag;
            var subtitlesWithInfo = m_subtitles[subtitlesType];

            subtitlesWithInfo.Subtitles = null;
            subtitlesWithInfo.ButtonOpenOrClose.Text = m_initialOpenOrCloseSubtitlesButtonText;
            subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
            subtitlesWithInfo.ProgressLabel.Text = $"0%";
            subtitlesWithInfo.ActionLabel.Text = "Поток субтитров был убран";
            subtitlesWithInfo.OutputTextBox.Text = string.Empty;

            // Загруженных субтитров не осталось:
            subtitlesWithInfo.OpenSubtitlesGroupBox.Text = m_initialOpenSubtitlesGroupBoxText;
            //
            subtitlesWithInfo.OpenFromDownloadsButton.Visible =
            subtitlesWithInfo.OpenFromDefaultFolderButton.Visible =
            Properties.Settings.Default.AdvancedMode;
            //
            subtitlesWithInfo.OpenFromDownloadsButton.Enabled =
            subtitlesWithInfo.OpenFromDefaultFolderButton.Enabled = true;
            //
            if (Properties.Settings.Default.AdvancedMode)
                subtitlesWithInfo.ButtonOpenOrClose.Left =  m_initialOpenSubtitlesButtonLeft;

            CloseSubtitleStreamConfirmationOrCancellationButtonHasBeenClicked(subtitlesWithInfo);
        }

        private void CloseSubtitleStreamCancellationButton_Click(object sender, EventArgs e)
        {
            var closeSubtitleStreamConfimationButton = (Button)sender;
            var subtitlesType = (SubtitlesType)closeSubtitleStreamConfimationButton.Tag;
            var subtitlesWithInfo = m_subtitles[subtitlesType];

            // Загруженные субтитры остались:
            subtitlesWithInfo.OpenSubtitlesGroupBox.Text = m_initialOpenSubtitlesGroupBoxTextBeforeCloseConfirmationDialog;

            CloseSubtitleStreamConfirmationOrCancellationButtonHasBeenClicked(subtitlesWithInfo);

        }

        private void CloseSubtitleStreamConfirmationOrCancellationButtonHasBeenClicked(SubtitlesAndInfo subtitlesWithInfo)
        {
            subtitlesWithInfo.ButtonOpenOrClose.Show();
            //subtitlesWithInfo.ColorPickingButton.Show();

            subtitlesWithInfo.ButtonOpenOrClose.Parent.Controls.Remove(subtitlesWithInfo.CloseSubtitleStreamConfimationButton);
            subtitlesWithInfo.ButtonOpenOrClose.Parent.Controls.Remove(subtitlesWithInfo.CloseSubtitleStreamCancellationButton);

            subtitlesWithInfo.CloseSubtitleStreamConfimationButton = null;
            subtitlesWithInfo.CloseSubtitleStreamCancellationButton = null;


        }


        private void ReadSubtitlesFromFile(string fileName, SubtitlesType subtitlesType)
        {
            var subtitlesWithInfo = m_subtitles[subtitlesType];


            var readSubtitlesBackgroundWorker = new SubtitlesBackgroundWorker { WorkerReportsProgress = true };
            readSubtitlesBackgroundWorker.DoWork += readSubtitlesBackgroundWorker_DoWork;
            readSubtitlesBackgroundWorker.ProgressChanged += readSubtitlesBackgroundWorker_ProgressChanged;
            readSubtitlesBackgroundWorker.RunWorkerCompleted += readSubtitlesBackgroundWorker_RunWorkerCompleted;

            subtitlesWithInfo.SetBackgroundWorker(readSubtitlesBackgroundWorker, subtitlesType);

            subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
            subtitlesWithInfo.ProgressLabel.Text = $"0%";

            subtitlesWithInfo.ButtonOpenOrClose.Enabled =
                subtitlesWithInfo.OpenFromDownloadsButton.Enabled =
                    subtitlesWithInfo.OpenFromDefaultFolderButton.Enabled = false;
            if (subtitlesWithInfo.ButtonTranslate != null)
                subtitlesWithInfo.ButtonTranslate.Enabled = false;
            subtitlesWithInfo.ActionLabel.Text = SUBTITLES_ARE_OPENING;

            if (subtitlesType == SubtitlesType.Original)
            {
                var extension = new FileInfo(fileName).Extension;

                SetSubtitlesAndVideoFilePaths(fileName, extension == ".mkv" ? true : false);
            }

            subtitlesWithInfo.BackgroundWorker.RunWorkerAsync(fileName);

        }

        private void readSubtitlesBackgroundWorker_DoWork(object sender, DoWorkEventArgs eventArgs)
        {
            var filePath = (string)eventArgs.Argument;

            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            var sourceFileFI = new FileInfo(filePath);
            var extension = sourceFileFI.Extension;

            switch (extension)
            {
                case ".srt":
                    {
                        // Заполняеми информацию
                        FillTheBasicSubtitlesInformation(filePath, subtitlesInfo);
                        subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                        //GUI
                        var outputTextBoxText = subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention;
                        //
                        BeginInvoke((Action)((() =>
                        {
                            DoGUIActions(parentBgW.SubtitlesType, outputTextBoxText);
                        })));

                        subtitlesInfo.Subtitles = ReadSRTFile(filePath);

                        break;
                    }
                case ".docx":
                    {
                        // Заполняеми информацию
                        FillTheBasicSubtitlesInformation(filePath, subtitlesInfo);
                        subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                        //GUI
                        var outputTextBoxText = subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention;
                        //
                        BeginInvoke((Action)((() =>
                        {
                            DoGUIActions(parentBgW.SubtitlesType, outputTextBoxText);
                        })));

                        subtitlesInfo.Subtitles = ReadDocx(filePath);

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
                            // Заполняеми информацию
                            FillTheBasicSubtitlesInformation(filePath, subtitlesInfo);
                            //
                            var trackInfo = $"Трек {trackSelectionForm.SelectedTrackIdLangTitle.Item1}, {trackSelectionForm.SelectedTrackIdLangTitle.Item2}";
                            if (!string.IsNullOrWhiteSpace((trackSelectionForm.SelectedTrackIdLangTitle.Item3)))
                                trackInfo += $", \"{trackSelectionForm.SelectedTrackIdLangTitle.Item3}\"";
                            //
                            subtitlesInfo.TrackNumber = trackSelectionForm.SelectedTrackIdLangTitle.Item1;
                            subtitlesInfo.TrackLanguage = trackSelectionForm.SelectedTrackIdLangTitle.Item2;
                            subtitlesInfo.TrackName = trackSelectionForm.SelectedTrackIdLangTitle.Item3;

                            //GUI
                            var outputTextBoxText = $"{trackInfo} из {subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention}";
                            //
                            BeginInvoke((Action)((() =>
                            {
                                DoGUIActions(parentBgW.SubtitlesType, outputTextBoxText);
                            })));

                            var mkvTrackInfo =
                                tracks.Find(x => x.TrackNumber == trackSelectionForm.SelectedTrackNumber);
                            var mkvSubtitles = mkvFile.GetSubtitle(trackSelectionForm.SelectedTrackNumber,
                                (position, total) =>
                                {
                                    parentBgW.ReportProgress((int)((100 * position) / total));
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
                case ".zip":
                    {
                        var text = string.Empty;
                        var zippedFiles = new List<string>();

                        using (var file = File.OpenRead(filePath))
                        using (var zip = new System.IO.Compression.ZipArchive(file, ZipArchiveMode.Read))
                        {
                            foreach (var entry in zip.Entries)
                            {
                                zippedFiles.Add(entry.Name);
                            }
                        }

                        using var fileSelectionForm = new FileToUseFromZipForm(zippedFiles);
                        var dialogResult = fileSelectionForm.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            // Заполняеми информацию
                            FillTheBasicSubtitlesInformation(filePath, subtitlesInfo);
                            //
                            subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                            //GUI
                            var outputTextBoxText = $"{fileSelectionForm.SelectedFileName} из {subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention}";
                            //
                            BeginInvoke((Action)((() =>
                            {
                                DoGUIActions(parentBgW.SubtitlesType, outputTextBoxText);
                            })));

                            using (var file = File.OpenRead(filePath))
                            using (var zip = new System.IO.Compression.ZipArchive(file, ZipArchiveMode.Read))
                            {
                                foreach (var entry in zip.Entries)
                                {
                                    if (entry.Name == fileSelectionForm.SelectedFileName)
                                    {
                                        using (var stream = entry.Open())
                                        {
                                            using (var ms = new MemoryStream())
                                            {
                                                stream.CopyTo(ms);
                                                var unzippedArray = ms.ToArray();

                                                text = Encoding.UTF8.GetString(unzippedArray);
                                            }
                                        }

                                        break;
                                    }
                                }
                            }

                            var lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                            subtitlesInfo.Subtitles = ReadSRTMarkup(lines);
                        }


                        break;
                    }
                default:
                    {
                        throw new Exception();
                    }
            }
        }

        private void FillTheBasicSubtitlesInformation(string filePath, SubtitlesAndInfo subtitlesInfo)
        {
            // Заполняеми информацию
            var fileName = new FileInfo(filePath).Name;
            var fileExt = new FileInfo(filePath).Extension;
            //
            subtitlesInfo.FileNameWithoutExtention = fileName.Substring(0, fileName.Length - fileExt.Length);
            subtitlesInfo.FileExtention = fileExt;
        }

        private void DoGUIActions(SubtitlesType subtitlesType, string outputTextBoxText)
        {
            switch (subtitlesType)
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

            var subtitlesInfo = m_subtitles[subtitlesType];
            //
            subtitlesInfo.OutputTextBox.Text = outputTextBoxText;
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

            if (subtitlesInfo.Subtitles != null)
            {
                subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Maximum;
                subtitlesInfo.ProgressLabel.Text = $"100%";

                if (parentBgW.SubtitlesType == SubtitlesType.Original)
                {
                    translateToFirstRussianSubtitlesButton.Enabled = translateWordByWordToFirstRussianSubtitlesButton.Enabled =
                        translateToSecondRussianSubtitlesButton.Enabled = translateWordByWordToSecondRussianSubtitlesButton.Enabled =
                            translateToThirdRussianSubtitlesButton.Enabled = translateWordByWordToThirdRussianSubtitlesButton.Enabled =
                                true;
                }

                // GUI
                SetGUIContolsToSubtitlesWasSuccessfullyLoaded(subtitlesInfo);

            }
            else
            {
                subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Minimum;
                subtitlesInfo.ProgressLabel.Text = $"0%";
                subtitlesInfo.ActionLabel.Text = "Произошла ошибка во время открытия субтитров";
            }

            subtitlesInfo.ButtonOpenOrClose.Enabled =
                subtitlesInfo.OpenFromDownloadsButton.Enabled =
                    subtitlesInfo.OpenFromDefaultFolderButton.Enabled = true;
            if (subtitlesInfo.ButtonTranslate != null)
                subtitlesInfo.ButtonTranslate.Enabled = true;

            // TODO Ошибки?
        }

        private void SetSubtitlesAndVideoFilePaths(string fileName, bool videoFile)
        {
            var finalSubtitlesFilesPathFileInfo = new FileInfo(fileName);

            if (!string.IsNullOrWhiteSpace(finalSubtitlesFilesPathBeginningRichTextBox.Text))
            {
                var rewritePathDialogResult = MessageBox.Show("Путь итоговых файлов субтитров / путь до файла видео уже задан! Перезаписать?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (rewritePathDialogResult == DialogResult.No)
                {
                    return;
                }
            }

            SetSubtitlesAndVideoFilePathBeginnging(finalSubtitlesFilesPathFileInfo.FullName.Substring(0,
                    finalSubtitlesFilesPathFileInfo.FullName.Length - finalSubtitlesFilesPathFileInfo.Extension.Length));

            if (videoFile)
                SetVideoFileExtention(finalSubtitlesFilesPathFileInfo.Extension);
        }

        /// <summary>
        /// Внутряк
        /// </summary>
        /// <param name="subtitlesAndVideoFilePathBeginnging"></param>
        private void SetSubtitlesAndVideoFilePathBeginnging(string subtitlesAndVideoFilePathBeginnging)
        {
            finalSubtitlesFilesPathBeginningRichTextBox.Text = subtitlesAndVideoFilePathBeginnging;
        }



        private void SetVideoFileExtention(string extention)
        {
            videoFileExtentionTextBox.Text = extention.Substring(1);

            // Былое
            //
            //finalSubtitlesFilesPathBeginningRichTextBox.Tag = extension;
            //playVideoButton.Text = $"{m_playVideoButtonDefaultText}\n({extension})";
        }

        private string GetVideoFileExtention()
        {
            return $".{videoFileExtentionTextBox.Text}";

            // Былое
            //
            //finalSubtitlesFilesPathBeginningRichTextBox.Tag
        }

        private void createOriginalAndBilingualSubtitlesFilesButton_Click(object sender, EventArgs e)
        {
            var originalSubtitlesPath =
                finalSubtitlesFilesPathBeginningRichTextBox.Text + originalSubtitlesFileNameEnding.Text;
            var bilingualSubtitlesPath =
                finalSubtitlesFilesPathBeginningRichTextBox.Text + bilingualSubtitlesFileNameEnding.Text;
            var bilingualSubtitlesFileExists = File.Exists(bilingualSubtitlesPath);
            var originalSubtitlesFileExist = File.Exists(originalSubtitlesPath);

            // Проверки
            if (Settings.Default.CreateOriginalSubtitlesFile && Settings.Default.CreateBilingualSubtitlesFile)
            {
                if (originalSubtitlesFileExist && bilingualSubtitlesFileExists)
                {
                    var result = MessageBox.Show($"Файлы\n\n• {originalSubtitlesPath}\n\nи\n\n• {bilingualSubtitlesPath}\n\nуже существуют! Перезаписать их?",
                        String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result != DialogResult.OK)
                        return;
                }
                else if (originalSubtitlesFileExist)
                {
                    var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуже существует! Перезаписать его?",
                        String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result != DialogResult.OK)
                        return;
                }
                else if (bilingualSubtitlesFileExists)
                {
                    var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуже существует! Перезаписать его?",
                        String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result != DialogResult.OK)
                        return;
                }
            }
            else if (Settings.Default.CreateOriginalSubtitlesFile && !Settings.Default.CreateBilingualSubtitlesFile)
            {
                if (originalSubtitlesFileExist)
                {
                    var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуже существует! Перезаписать его?",
                        String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result != DialogResult.OK)
                        return;
                }
            }
            else if (Settings.Default.CreateBilingualSubtitlesFile && !Settings.Default.CreateOriginalSubtitlesFile)
            {
                if (bilingualSubtitlesFileExists)
                {
                    var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуже существует! Перезаписать его?",
                        String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result != DialogResult.OK)
                        return;
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
                    MessageBox.Show($"Записать файл\n\n• {originalSubtitlesPath}\n\nне удалось! Исключение:\n• {exception}");
                    return;
                }

            }

            if (Settings.Default.CreateBilingualSubtitlesFile)
            {
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
                    MessageBox.Show($"Записать файл\n\n• {bilingualSubtitlesPath}\n\nне удалось! Исключение:\n• {exception}");
                    return;
                }
            }

            // Проверка существования итоговых файлов субтитров
            originalSubtitlesFileExist = File.Exists(originalSubtitlesPath);
            bilingualSubtitlesFileExists = File.Exists(bilingualSubtitlesPath);
            //
            if (Settings.Default.CreateOriginalSubtitlesFile && Settings.Default.CreateBilingualSubtitlesFile)
            {
                if (originalSubtitlesFileExist && bilingualSubtitlesFileExists)
                {
                    var result = MessageBox.Show($"Файлы\n\n• {originalSubtitlesPath}\n\nи\n\n• {bilingualSubtitlesPath}\n\nуспешно записаны!",
                        String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result != DialogResult.OK)
                        return;
                }
                else if (originalSubtitlesFileExist)
                {
                    var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуспешно записан!",
                        String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result != DialogResult.OK)
                        return;
                }
                else if (bilingualSubtitlesFileExists)
                {
                    var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуспешно записан!",
                        String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result != DialogResult.OK)
                        return;
                }
            }
            else if (Settings.Default.CreateOriginalSubtitlesFile && !Settings.Default.CreateBilingualSubtitlesFile)
            {
                if (originalSubtitlesFileExist)
                {
                    var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуспешно записан!",
                        String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result != DialogResult.OK)
                        return;
                }
            }
            else if (Settings.Default.CreateBilingualSubtitlesFile && !Settings.Default.CreateOriginalSubtitlesFile)
            {
                if (bilingualSubtitlesFileExists)
                {
                    var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуспешно записан!",
                        String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result != DialogResult.OK)
                        return;
                }
            }

        }


        private void colorPickingButton_Click(object sender, EventArgs e)
        {
            var senderButton = (Button)sender;

            var colorPickingDialog = new ColorDialog();
            colorPickingDialog.Color = senderButton.BackColor;
            colorPickingDialog.CustomColors = new int[] {
                ColorTranslator.ToOle(Color.Gold),
                ColorTranslator.ToOle(Color.DeepSkyBlue)
            };
            colorPickingDialog.FullOpen = true;
            var dialogResult = colorPickingDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                senderButton.BackColor = colorPickingDialog.Color;

                if (senderButton == primarySubtitlesColorButton)
                    Properties.Settings.Default.PrimarySubtitlesColor = primarySubtitlesColorButton.BackColor;
                else if (senderButton == firstRussianSubtitlesColorButton)
                    Properties.Settings.Default.FirstRussianSubtitlesColor = firstRussianSubtitlesColorButton.BackColor;
                else if (senderButton == secondRussianSubtitlesColorButton)
                    Properties.Settings.Default.SecondRussianSubtitlesColor = secondRussianSubtitlesColorButton.BackColor;
                else if (senderButton == thirdRussianSubtitlesColorButton)
                    Properties.Settings.Default.ThirdRussianSubtitlesColor = thirdRussianSubtitlesColorButton.BackColor;

                Properties.Settings.Default.Save();
            }
        }

        private void openSubtitles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void openPrimarySubtitles_DragDrop(object sender, DragEventArgs e)
        {
            translateToFirstRussianSubtitlesButton.Enabled = translateWordByWordToFirstRussianSubtitlesButton.Enabled =
                translateToSecondRussianSubtitlesButton.Enabled = translateWordByWordToSecondRussianSubtitlesButton.Enabled =
                    translateToThirdRussianSubtitlesButton.Enabled = translateWordByWordToThirdRussianSubtitlesButton.Enabled =
                        false;

            var fileName = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            ReadSubtitlesFromFile(fileName, SubtitlesType.Original);
        }

        private void openFirstRussianSubtitles_DragDrop(object sender, DragEventArgs e)
        {
            var fileName = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            ReadSubtitlesFromFile(fileName, SubtitlesType.FirstRussian);
        }

        private void openSecondRussianSubtitles_DragDrop(object sender, DragEventArgs e)
        {
            var fileName = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            ReadSubtitlesFromFile(fileName, SubtitlesType.SecondRussian);
        }

        private void openThirdRussianSubtitles_DragDrop(object sender, DragEventArgs e)
        {
            var fileName = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            ReadSubtitlesFromFile(fileName, SubtitlesType.ThirdRussian);
        }

        private void openOrClosePrimarySubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.Original);

            //OpenFileAndReadSubtitlesFromFile(ref m_originalSubtitles,
            //    primarySubtitlesProgressBar, primarySubtitlesProgressLabel, primarySubtitlesActionLabel, primarySubtitlesTextBox,
            //    openPrimarySubtitlesButton);
        }

        private void openOrCloseFirstRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.FirstRussian);
            //OpenFileAndReadSubtitlesFromFile(ref m_firstRussianSubtitles,
            //    firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel, firstRussianSubtitlesActionLabel,
            //    firstRussianSubtitlesTextBox, openFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton);
        }

        private void openOrCloseSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.SecondRussian);

            //OpenFileAndReadSubtitlesFromFile(ref m_secondRussianSubtitles,
            //    secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel, secondRussianSubtitlesActionLabel,
            //    secondRussianSubtitlesTextBox, openSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton);
        }

        private void openOrCloseThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.ThirdRussian);

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
            var keySettingForm = new SettingsForm(this);
            keySettingForm.ShowDialog();
            keySettingForm.Dispose();

            SetProgramAccordingToSettings();
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
                SetSubtitlesAndVideoFilePaths(openFileDialog.FileName, true);
            }

            openFileDialog.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using var formAbout = new FormAbout();
            formAbout.ShowDialog();
        }

        private void playVideoButton_Click(object sender, EventArgs e)
        {
            var videoFileName = finalSubtitlesFilesPathBeginningRichTextBox.Text +
                    GetVideoFileExtention();
            try
            {
                System.Diagnostics.Process.Start(videoFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Попытка открытия файла\n• {videoFileName}\n\n не удалась!\n\n\nВы можете запустить воспроизведение видео обычным способом, не из Bilingual Subtitler — динамически подключаемые субтитры все равно будут работать.\n\n\nОшибка: {ex.Message}", "Попытка открытия файла не удалась", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void primarySubtitlesExportAsDocxButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.Original);
        }

        private void firstRussianSubtitlesExportAsDocxButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.FirstRussian);
        }

        private void secondRussianSubtitlesExportAsDocxButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.SecondRussian);
        }

        private void thirdRussianSubtitlesExportAsDocxButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.ThirdRussian);
        }

        private void ExportSubtitlesToDocx(SubtitlesType subtitlesType, bool intoDownloads = false)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];

            string formats = "Файл DocX |*.docx";
            var defaultFileName = string.IsNullOrWhiteSpace(subtitlesInfo.TrackLanguage) ? subtitlesInfo.FileNameWithoutExtention
                : $"{subtitlesInfo.FileNameWithoutExtention}.Track{subtitlesInfo.TrackNumber}.Subtitles." +
                $"{subtitlesInfo.TrackLanguage.ToUpper()}.BilingualSubtitlerExport";

            bool goodToGo = false;
            string resultingDocXFileName = string.Empty;

            if (intoDownloads)
            {
                goodToGo = true;
                string downloadsFolderPath = Properties.Settings.Default.DownloadsFolder;
                resultingDocXFileName = Path.Combine(downloadsFolderPath, $"{defaultFileName}.docx");
            }
            else
            {
                using var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = formats;
                saveFileDialog.FileName = defaultFileName;

                var result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    goodToGo = true;
                    resultingDocXFileName = saveFileDialog.FileName;
                }
            }

            if (goodToGo == true)
            {
                var timeFormat = @"hh\:mm\:ss\,fff";

                var doc = DocX.Create(resultingDocXFileName);
                for (int i = 0; i < subtitlesInfo.Subtitles.Length; i++)
                {
                    var subtitle = subtitlesInfo.Subtitles[i];

                    doc.InsertParagraph((i + 1).ToString());
                    doc.InsertParagraph($"{subtitle.Start.ToString(timeFormat)} --> {subtitle.End.ToString(timeFormat)}");
                    doc.InsertParagraph(subtitle.Text);
                    doc.InsertParagraph("");
                }
                doc.Save();
            }

            if (intoDownloads)
            {
                if (File.Exists(resultingDocXFileName))
                    MessageBox.Show($"Субтитры были сохранены в файл {resultingDocXFileName}", "Субтитры были сохранены успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void firstRussianSubtitlesTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://translate.yandex.ru/doc");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://translate.google.com/#view=home&op=docs&sl=en&tl=ru");
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
        }

        public static bool AppIsRunningAsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent()))
                      .IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void redefineSubtitlesAppearanceSettingsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetNewRedefineSubtitlesAppearanceSettingsSetting(((CheckBox)sender).Checked);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SetSubtitlesAppearanceBoxesAccordingToSettings();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string formats = "Субтитры, созданные через Bilingual Subtitler (.ass) |*.ass";

            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = formats;
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ReadASSMarkedupDocumentWithBilingualSubtitles(openFileDialog.FileName);
            }
        }

        private void changeRussianSubtitlesStylesAccordingToOriginalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled =
            //
            //firstRussianSubtitlesFontComboBox.Enabled = 
            firstRussianSubtitlesMarginNumericUpDown.Enabled =
                firstRussianSubtitlesShadowNumericUpDown.Enabled =
                    firstRussianSubtitlesOutlineNumericUpDown.Enabled = firstRussianSubtitlesSizeNumericUpDown.Enabled =
                            // firstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
                            // Вторые
                            secondRussianSubtitlesFontComboBox.Enabled =
                                secondRussianSubtitlesMarginNumericUpDown.Enabled =
                                    secondRussianSubtitlesShadowNumericUpDown.Enabled =
                                        secondRussianSubtitlesOutlineNumericUpDown.Enabled =
                                            secondRussianSubtitlesSizeNumericUpDown.Enabled =
                                                secondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
                                                secondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
                                                    // Третьи
                                                    thirdRussianSubtitlesFontComboBox.Enabled =
                                                        thirdRussianSubtitlesMarginNumericUpDown.Enabled =
                                                            thirdRussianSubtitlesShadowNumericUpDown.Enabled =
                                                                thirdRussianSubtitlesOutlineNumericUpDown.Enabled =
                                                                    thirdRussianSubtitlesSizeNumericUpDown.Enabled =
                                                                        thirdRussianSubtitlesTransparencyPercentageNumericUpDown
                                                                                .Enabled =
                                                                                thirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown
                                                                                .Enabled =
                                                                            //
                                                                            !changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;

            secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled = changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;
        }

        private void firstRussianSubtitlesFontComboBox_TextChanged(object sender, EventArgs e)
        {
            if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                secondRussianSubtitlesFontComboBox.Text =
                    thirdRussianSubtitlesFontComboBox.Text =
                        firstRussianSubtitlesFontComboBox.Text;
            }
        }

        private void ChangeMargin()
        {
            if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                if (secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled && secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked)
                {
                    var firstRussianSubtitlesMargin = originalSubtitlesMarginNumericUpDown.Value -
                                                      (2 * originalSubtitlesSizeNumericUpDown.Value +
                                                       originalSubtitlesSizeNumericUpDown.Value / 10);
                    firstRussianSubtitlesMarginNumericUpDown.Value =
                        firstRussianSubtitlesMargin < 0 ? 0 : firstRussianSubtitlesMargin;

                    thirdRussianSubtitlesMarginNumericUpDown.Value = 290 - (2 * originalSubtitlesSizeNumericUpDown.Value +
                                                       originalSubtitlesSizeNumericUpDown.Value / 10);
                    secondRussianSubtitlesMarginNumericUpDown.Value = thirdRussianSubtitlesMarginNumericUpDown.Value -
                        (2 * originalSubtitlesSizeNumericUpDown.Value +
                                                       originalSubtitlesSizeNumericUpDown.Value / 10);
                }
                else
                {
                    firstRussianSubtitlesMarginNumericUpDown.Value =
                        originalSubtitlesMarginNumericUpDown.Value +
                        (2 * originalSubtitlesSizeNumericUpDown.Value +
                         originalSubtitlesSizeNumericUpDown.Value / 10);
                    secondRussianSubtitlesMarginNumericUpDown.Value = originalSubtitlesMarginNumericUpDown.Value +
                                                                      (2 * originalSubtitlesSizeNumericUpDown.Value +
                                                                       originalSubtitlesSizeNumericUpDown.Value / 10) * 2;

                    var thirdRussianSubtitlesMargin = originalSubtitlesMarginNumericUpDown.Value -
                                                      (2 * originalSubtitlesSizeNumericUpDown.Value +
                                                       originalSubtitlesSizeNumericUpDown.Value / 10);
                    thirdRussianSubtitlesMarginNumericUpDown.Value =
                        thirdRussianSubtitlesMargin < 0 ? 0 : thirdRussianSubtitlesMargin;
                }

            }
        }

        private void originalSubtitlesMarginNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            ChangeMargin();
        }

        private void originalSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                firstRussianSubtitlesSizeNumericUpDown.Value =
                    secondRussianSubtitlesSizeNumericUpDown.Value =
                        thirdRussianSubtitlesSizeNumericUpDown.Value =
                            originalSubtitlesSizeNumericUpDown.Value;
            }

            ChangeMargin();
        }

        private void originalSubtitlesOutlineNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                firstRussianSubtitlesOutlineNumericUpDown.Value =
                    secondRussianSubtitlesOutlineNumericUpDown.Value =
                        thirdRussianSubtitlesOutlineNumericUpDown.Value =
                            originalSubtitlesOutlineNumericUpDown.Value;
            }
        }

        private void originalSubtitlesShadowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                firstRussianSubtitlesShadowNumericUpDown.Value =
                    secondRussianSubtitlesShadowNumericUpDown.Value =
                        thirdRussianSubtitlesShadowNumericUpDown.Value =
                            originalSubtitlesShadowNumericUpDown.Value;
            }
        }

        private void firstRussianSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                secondRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                thirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                    firstRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
            }
        }

        private void firstRussianSubtitlesShadowTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                secondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                thirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                    firstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            }
        }

        private void subtitlesFileNameEnding_Enter(object sender, EventArgs e)
        {
            HideCaret(this.Handle);
        }

        private void finalSubtitlesFilesPathBeginningRichTextBox_Leave(object sender, EventArgs e)
        {
            var textBox = (RichTextBox)sender;
            textBox.Text = textBox.Text.Replace("\r\n", "").Replace("\n", "");
        }

        private void openPrimarySubtitlesFromDownloadsButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.Original, true);
        }

        private void primarySubtitlesExportAsDocxIntoDownloadsButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.Original, true);
        }

        private void firstRussianSubtitlesOpenFromDownloadsButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.FirstRussian, true);
        }

        private void secondRussianSubtitlesOpenFromDownloadsButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.SecondRussian, true);
        }

        private void thirdRussianSubtitlesOpenFromDownloadsButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.ThirdRussian, true);
        }

        private void firstRussianSubtitlesExportAsDocxIntoDownloadsButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.FirstRussian, true);
        }

        private void secondRussianSubtitlesExportAsDocxIntoDownloadsButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.SecondRussian, true);
        }

        private void thirdRussianSubtitlesExportAsDocxIntoDownloadsButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.ThirdRussian, true);
        }

        private void videoAndSubtitlesStateComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            m_videoState = ((Tuple<VideoState, SubtitlesState>)((ComboboxItem)((ComboBox)sender).SelectedItem).Value).Item1;
            m_subtitlesState = ((Tuple<VideoState, SubtitlesState>)((ComboboxItem)((ComboBox)sender).SelectedItem).Value).Item2;
        }

        private void firstRussianSubtitlesExportAsDocx_Click(object sender, EventArgs e)
        {

        }

        private void openPrimarySubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.Original, fromDefaultFolder: true);
        }

        private void openFirstRussianSubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.FirstRussian, fromDefaultFolder: true);
        }

        private void openSecondRussianSubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.SecondRussian, fromDefaultFolder: true);
        }

        private void openThirdRussianSubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenFileAndReadSubtitlesFromFileOrRemoveTheSubStream(SubtitlesType.ThirdRussian, fromDefaultFolder: true);
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

    public class BilingualSubtitlerPropertiesLoadingException : Exception
    {
        public BilingualSubtitlerPropertiesLoadingException(Exception e) : base("Во время считывания настроек произошла ошибка", e)
        {
        }
    }


}
