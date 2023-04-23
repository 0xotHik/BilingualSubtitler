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
using NonInvasiveKeyboardHookLibrary;
using Gma.System.MouseKeyHook;
using System.Linq;
using Aspose.Zip.Rar;
using Aspose.Zip;
using Nikse.SubtitleEdit.Core.Common;

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

        private const string TITLE_CONTAINING_COMMENTARY_STRING_BEGINNING = "; Это комментарий Bilingual Subtitler, который закрепляет, что данный поток субтитров был (и будет в дальнейшем) считан Bilingual Subtitler как: ";

        private const string SUCCESS_MESSAGE_BOX_HEADER = "✅ Успешно";
        private const MessageBoxIcon SUCCESS_MESSAGE_BOX_ICON = MessageBoxIcon.None;

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
        private int m_initialDocXTranslationGroupBoxHeight;
        private int m_initialOpenBilingualsTubtitlesButtonLeft;
        private int m_initialOpenStylesFromBilingualsTubtitlesButtonLeft;

        private Dictionary<SubtitlesType, SubtitlesAndInfo> m_subtitles;

        private Translator m_translator;



        private int m_changeSubtitlesToBilingualHotkeyCode;
        private VirtualKeyCode? m_changeSubtitlesToBilingualHotkeyModifierKeyVirtualKeyCode;
        private VirtualKeyCode? m_changeSubtitlesToBilingualHotkeyVirtualKeyCode;
        private int m_changeSubtitlesToOriginalHotkeyCode;
        private VirtualKeyCode? m_changeSubtitlesToOriginalHotkeyModifierKeyVirtualKeyCode;
        private VirtualKeyCode? m_changeSubtitlesToOriginalHotkeyVirtualKeyCode;

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

        // ASS
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

            // Для 1251
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

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
            m_initialDocXTranslationGroupBoxHeight = docXTranslationGroupBox.Height;
            m_initialOpenBilingualsTubtitlesButtonLeft = openBilingualSubtitlerButton.Left;
            m_initialOpenStylesFromBilingualsTubtitlesButtonLeft = openStylesFromBilingualSubtitlerButton.Left;

            m_playVideoButtonDefaultText = playVideoButton.Text;
            notifyIcon.ContextMenuStrip = new ContextMenuStrip();
            //
            notifyIcon.ContextMenuStrip.Items.AddRange(
            new ToolStripMenuItem[] {
                new ToolStripMenuItem("Свернуть в трей", null, ((sender, e) =>
                {
                    // прячем наше окно из панели
                this.ShowInTaskbar = false; 
                //сворачиваем окно
                WindowState = FormWindowState.Minimized;
            })),

                new ToolStripMenuItem("Развернуть", null, ((sender, e) =>
                {
                    // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
                })),

                new ToolStripMenuItem("Завершить работу Bilingual Subtitler", null, ((sender, e) =>
                {
                    System.Windows.Forms.Application.Exit();
                    }))
            });

            var currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            if (currentVersion > Version.Parse(Properties.Settings.Default.LatestInstalledVersion))
            {
                Settings.Default.Upgrade();
                //
                Settings.Default.LatestInstalledVersion = currentVersion.ToString();
                //
                Settings.Default.Save();

                Properties.SubtitlesAppearanceSettings.Default.Upgrade();
                Properties.SubtitlesAppearanceSettings.Default.Save();
            }

            if (Settings.Default.FirstLaunch)
            {
                var videoplayerPauseKey = new Hotkey(Settings.Default.VideoPlayerPauseButtonString).KeyValue;
                var videoplayerNextSubtitles = new Hotkey(Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString).KeyValue;
                var videoplayerPreviousSubtitles = new Hotkey(Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString).KeyValue;

                var bilingualSubtitlesHotkeysString = string.Empty;
                var bilingualSubtitlesHotkeysKeyValuesList = new List<string>();
                foreach (var hotkeyString in Settings.Default.Hotkeys)
                {
                    bilingualSubtitlesHotkeysKeyValuesList.Add(new Hotkey(hotkeyString).KeyValue);
                }
                //
                // Пробел
                if (bilingualSubtitlesHotkeysKeyValuesList.Contains("Space"))
                {
                    bilingualSubtitlesHotkeysString = "Пробел";
                    bilingualSubtitlesHotkeysKeyValuesList.Remove("Space");
                }
                //
                foreach (var hotkeyString in bilingualSubtitlesHotkeysKeyValuesList)
                {
                    if (string.IsNullOrWhiteSpace(bilingualSubtitlesHotkeysString))
                        bilingualSubtitlesHotkeysString += $"{hotkeyString}";
                    else
                        bilingualSubtitlesHotkeysString += $", {hotkeyString}";
                }

                MessageBox.Show("Вас приветствует Bilingual Subtitler!\n\n" +
                    "Это ваш первый запуcк Bilingual Subtitler, поэтому для главного окна выставлен облегченный режим компоновки. Для того, чтобы включить больше возможностей — переключитесь на \"расширенный режим\" в настройках программы.\n\n" +
                                $"Для работы горячих клавиш Bilingual Subtitler требуется запуск от имени администратора!\n" +
                                $"Горячие клавиши Bilingual Subtitler: {bilingualSubtitlesHotkeysString}\n\n" +
                                $"Параметры видеоплеера (для просмотра с динамически подключаемыми русскими субтитрами) установлены по умолчанию — для Media Player Classic Homecinema, 64-bit, немодифицированного*." +
                                "\n\n\n\n" +
                                $"(* — Имя процесса видеоплеера: {Settings.Default.VideoPlayerProcessName}\n" +
                                $"Горячие клавиши видеоплеера:\n" +
                                $"Паузы — {videoplayerPauseKey}, смены на следующие субтитры — {videoplayerNextSubtitles}, " +
                                $"на предыдущие — {videoplayerPreviousSubtitles}.\n\n" +
                                $"В случае неверной работы Bilingual Subtitler — проверьте, возможно параметры вашего видеоплеера отличаются от заданных по умолчанию)",
                                $"Первый запуск Bilingual Subtitler", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Settings.Default.FirstLaunch = false;
                Settings.Default.Save();
            }

            // Состояния видео и субтитров
            videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.Items.Add(m_videoPlayingWithOriginalSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.Items.Add(m_videoPlayingWithBilingualSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.Items.Add(m_videoPausedWithOriginalSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.Items.Add(m_videoPausedWithBilingualSubtitlesComboBoxItem);
            videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.SelectedIndex = 0;
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
                        openOrClosePrimarySubtitlesGroupBox, primarySubtitlesExportAsDocxGroupBox, openPrimarySubtitlesFromDownloadsButton, openPrimarySubtitlesFromDefaultFolderButton, originalSubtitlesOpenFromClipboardButton,
                        primarySubtitlesExportAsDocxButton, primarySubtitlesExportAsDocxIntoDownloadsButton,
                        openPrimarySubtitlesIn1251Button)
                },
                {
                    SubtitlesType.FirstRussian, new SubtitlesAndInfo(
                        firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel,
                        openOrCloseFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton, translateWordByWordToFirstRussianSubtitlesButton,
                        firstRussianSubtitlesActionLabel, firstRussianSubtitlesTextBox, firstRussianSubtitlesColorButton,
                        openOrCloseFirstRussianSubtitlesGroupBox, firstRussianSubtitlesExportAsDocxGroupBox, firstRussianSubtitlesOpenFromDownloadsButton, openFirstRussianSubtitlesFromDefaultFolderButton, firstRussianSubtitlesOpenFromClipboardButton,
                        firstRussianSubtitlesExportAsDocxButton, firstRussianSubtitlesExportAsDocxIntoDownloadsButton,
                        openFirstRussianSubtitlesIn1251Button)
                },
                {
                    SubtitlesType.SecondRussian, new SubtitlesAndInfo(
                        secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel,
                        openOrCloseSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton, translateWordByWordToSecondRussianSubtitlesButton,
                        secondRussianSubtitlesActionLabel, secondRussianSubtitlesTextBox, secondRussianSubtitlesColorButton,
                        openOrCloseSecondRussianSubtitlesGroupBox, secondRussianSubtitlesExportAsDocxGroupBox, secondRussianSubtitlesOpenFromDownloadsButton, openSecondRussianSubtitlesFromDefaultFolderButton, secondRussianSubtitlesOpenFromClipboardButton,
                        secondRussianSubtitlesExportAsDocxButton, secondRussianSubtitlesExportAsDocxIntoDownloadsButton,
                        openSecondRussianSubtitlesIn1251Button)
                },
                {
                    SubtitlesType.ThirdRussian, new SubtitlesAndInfo(
                        thirdRussianSubtitlesProgressBar, thirdRussianSubtitlesProgressLabel,
                        openOrCloseThirdRussianSubtitlesButton, translateToThirdRussianSubtitlesButton, translateWordByWordToThirdRussianSubtitlesButton,
                        thirdRussianSubtitlesActionLabel, thirdRussianSubtitlesTextBox, thirdRussianSubtitlesColorButton,
                        openOrCloseThirdRussianSubtitlesGroupBox, thirdRussianSubtitlesExportAsDocxGroupBox, thirdRussianSubtitlesOpenFromDownloadsButton, openThirdRussianSubtitlesFromDefaultFolderButton, thirdRussianSubtitlesOpenFromClipboardButton,
                        thirdRussianSubtitlesExportAsDocxButton, thirdRussianSubtitlesExportAsDocxIntoDownloadsButton,
                        openThirdRussianSubtitlesIn1251Button)
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

            InputHandlingConstructor();

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

            // "Программа запущена от админа" / "не от админа"
            var appNotRunningAsAdministratorPanelControl = new AppNotRunningAsAdministratorPanelControl();
            appNotRunningAsAdministratorPanelControl.Location = appIsRunningInAdministrativeModePanelControl.Location;
            appNotRunningAsAdministratorPanelControl.Size = appIsRunningInAdministrativeModePanelControl.Size;
            ((Control)appIsRunningInAdministrativeModePanelControl.Parent).Controls.Add(appNotRunningAsAdministratorPanelControl);
            //
            appIsRunningInAdministrativeModePanelControl.Visible = AppIsRunningAsAdministrator();
            appNotRunningAsAdministratorPanelControl.Visible = !AppIsRunningAsAdministrator();

            // Проверка обновлений
            var checkUpdatesBgW = new BackgroundWorker();
            checkUpdatesBgW.DoWork += CheckUpdatesBgW_DoWork;
            checkUpdatesBgW.RunWorkerCompleted += CheckUpdatesBgW_RunWorkerCompleted;
            checkUpdatesBgW.RunWorkerAsync();

            openOrClosePrimarySubtitlesButton.Focus();
            openOrClosePrimarySubtitlesButton.Select();


        }


        [DllImport("user32.dll")]
        static extern int MapVirtualKey(uint uCode, uint uMapType);

        public char GetCharProducingByPressingKeyWithThisCode(int keyCode)
        {
            // 2 is used to translate into an unshifted character value 
            int nonVirtualKey = MapVirtualKey((uint)keyCode, 2);

            char mappedChar = Convert.ToChar(nonVirtualKey);

            return mappedChar;

            //char c = '\0';
            //if ((key >= Keys.A) && (key <= Keys.Z))
            //{
            //    c = (char)((int)'a' + (int)(key - Keys.A));
            //}

            //else if ((key >= Keys.D0) && (key <= Keys.D9))
            //{
            //    c = (char)((int)'0' + (int)(key - Keys.D0));
            //}

            //return c;
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
                        "Перейти к скачиванию?", "Появилась новая версия программы", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        OpenUrl("https://0xothik.wordpress.com/bilingual-subtitler-latest-release/");
                    }

                    Settings.Default.LatestSeenVersion = latestVersionOnGitHub;
                    Settings.Default.Save();
                }
            }
            else if (e.Result is Exception)
            {
                var exception = (Exception)e.Result;
                MessageBox.Show($"Не удалось получить информацию о новых версиях\n\n\nОшибка:{exception.Message}",
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
                m_videoPlayerProcessName = Properties.Settings.Default.VideoPlayerProcessName;

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
                //
                SetInputHandlingAccordiongToSettings();


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
                    subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox.Items.Add(font.Name);
                    subtitlesAppearanceSettingsControl.FirstRussianSubtitlesFontComboBox.Items.Add(font.Name);
                    subtitlesAppearanceSettingsControl.SecondRussianSubtitlesFontComboBox.Items.Add(font.Name);
                    subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesFontComboBox.Items.Add(font.Name);
                }

                // Внешний вид субтитров
                SetNewRedefineSubtitlesAppearanceSettingsSetting(Settings.Default.RedefineSubtitlesAppearanceSettings);
                //
                if (atLaunch || !m_redefineSubtitlesAppearanceSettings)
                    subtitlesAppearanceSettingsControl.SetAccordingToPropertiesSettings();

                // Advanced Mode
                var advancedMode = Settings.Default.AdvancedMode;
                //
                firstRussianSubtitlesExportAsDocxButton.Visible =
                    secondRussianSubtitlesExportAsDocxButton.Visible =
                    thirdRussianSubtitlesExportAsDocxButton.Visible =

                    primarySubtitlesExportAsSrtButton.Visible =
                    firstRussianSubtitlesExportAsSrtButton.Visible =
                    secondRussianSubtitlesExportAsSrtButton.Visible =
                    thirdRussianSubtitlesExportAsSrtButton.Visible =

                    originalSubtitlesOpenFromClipboardButton.Visible =
                    firstRussianSubtitlesOpenFromClipboardButton.Visible =
                    secondRussianSubtitlesOpenFromClipboardButton.Visible =
                    thirdRussianSubtitlesOpenFromClipboardButton.Visible =

                    googleTranslatorLinkLabel.Visible =
                    redefineSubtitlesAppearanceSettingsCheckBox.Visible =
                    subtitlesAppearanceSettingsControl.Visible =

                    showSubtitlesButton.Visible =

                    openStylesFromBilingualSubtitlerButton.Visible =

                    additionalOpenExportSubtitlesButtonsLabel.Visible =
                    additionalOpenExportSubtitlesButtonsGroupBox.Visible =

                    removePostfixGroupBox.Visible =
                    openSubtitlesIn1251GroupBox.Visible =
                    openSubtitlesFromDefaultFolderGroupBox.Visible =

                    additionalSelectVideoFileToGetPathForSubtitlesButton.Visible =

                    advancedMode;
                //
                openBilingualSubtitlerButton.Left = advancedMode ? m_initialOpenBilingualsTubtitlesButtonLeft : (openBilignualSubtitlesGroupBox.Width / 2) - (openBilingualSubtitlerButton.Width / 2);
                //
                var buttonOpenSubtitlesLeft = advancedMode ? m_initialOpenSubtitlesButtonLeft : (openOrClosePrimarySubtitlesGroupBox.Width / 2) - (openOrClosePrimarySubtitlesButton.Width / 2);
                var exportAsDocxSubtitlesLeft = advancedMode ? m_initialExportSubtitlesAsDocxButtonLeft : (primarySubtitlesExportAsDocxGroupBox.Width / 2) - (primarySubtitlesExportAsDocxButton.Width / 2);
                // Для всех потоков субтитров
                foreach (var subtitles in m_subtitles)
                {
                    var subtitlesWithInfo = subtitles.Value;

                    subtitlesWithInfo.ButtonOpenOrClose.Left = buttonOpenSubtitlesLeft;
                    subtitlesWithInfo.ExportAsDocxButton.Left = buttonOpenSubtitlesLeft;

                    subtitlesWithInfo.OpenFromDownloadsButton.Visible = advancedMode && (!ThereIsSubtitles(subtitlesWithInfo.Subtitles));
                    //subtitlesWithInfo.OpenFromDefaultFolderButton.Visible = advancedMode && (!ThereIsSubtitles(subtitlesWithInfo.Subtitles));
                    subtitlesWithInfo.OpenFromClipboardButton.Visible = advancedMode && (!ThereIsSubtitles(subtitlesWithInfo.Subtitles));
                    // Если у нас есть субтитры, предполагается, что кнопка открытия/закрытия субтитров — в режиме "Убрать?"
                    // А значит, доп.кнопки открытия нам не нужны

                    subtitlesWithInfo.ExportAsDocxIntoDownloadsButton.Visible = advancedMode;

                    if (subtitles.Key != SubtitlesType.Original)
                    {
                        subtitlesWithInfo.ExportAsDocxGroupBox.Visible = advancedMode;
                    }
                }
                //
                if (!advancedMode)
                {
                    secondRussianSubtitlesGroupBox.Width = thirdRussianSubtitlesGroupBox.Width = firstRussianSubtitlesGroupBox.Width = primarySubtitlesColorGroupBox.Right +
                        (primarySubtitlesExportAsDocxGroupBox.Left - primarySubtitlesColorGroupBox.Right)
                        + 1 // Толщина рамки
                        ;

                    //secondRussianSubtitlesGroupBox.Width = thirdRussianSubtitlesGroupBox.Width = firstRussianSubtitlesGroupBox.Width = primarySubtitlesColorGroupBox.Right + hideSecondRussianSubtitlesButton.Width + 10;

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
                this.Width = advancedMode ? m_initialFormWidth : m_initialFormWidth - subtitlesAppearanceSettingsControl.Width;
                docXTranslationGroupBox.Height = advancedMode ? m_initialDocXTranslationGroupBoxHeight : primarySubtitlesGroupBox.Height;

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

        private void SetSubtitlesAppearanceSettingsAccordingToStoredValues()
        {
            // TODO v11
            //subtitlesAppearanceSettingsControl.ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked =
            //   Properties.Settings.Default.ChangeRussianSubtitlesStylesAccordingToOriginal;
            //subtitlesAppearanceSettingsControl.SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreen;
            //subtitlesAppearanceSettingsControl.SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled = Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreenEnabled;

            var originalSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.OriginalSubtitlesStyleString.Split(';');
            foreach (var fontItem in subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == originalSubtitlesStyle[0])
                {
                    subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox.Text))
                subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox.Text = originalSubtitlesStyle[0];
            subtitlesAppearanceSettingsControl.OriginalSubtitlesMarginNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[1]);
            subtitlesAppearanceSettingsControl.OriginalSubtitlesSizeNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[2]);
            subtitlesAppearanceSettingsControl.OriginalSubtitlesOutlineNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[3]);
            subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[4]);
            subtitlesAppearanceSettingsControl.OriginalSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[5]);
            subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[6]);
            subtitlesAppearanceSettingsControl.OriginalSubtitlesInOneLineCheckBox.Checked = originalSubtitlesStyle[7] == "1";

            var firstRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.FirstRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in subtitlesAppearanceSettingsControl.FirstRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == firstRussianSubtitlesStyle[0])
                {
                    subtitlesAppearanceSettingsControl.FirstRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(subtitlesAppearanceSettingsControl.FirstRussianSubtitlesFontComboBox.Text))
                subtitlesAppearanceSettingsControl.FirstRussianSubtitlesFontComboBox.Text = firstRussianSubtitlesStyle[0];
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[1]);
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[2]);
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[3]);
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[4]);
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[5]);
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[6]);
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesInOneLineCheckBox.Checked = firstRussianSubtitlesStyle[7] == "1";

            var secondRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.SecondRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in subtitlesAppearanceSettingsControl.SecondRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == secondRussianSubtitlesStyle[0])
                {
                    subtitlesAppearanceSettingsControl.SecondRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(subtitlesAppearanceSettingsControl.SecondRussianSubtitlesFontComboBox.Text))
                subtitlesAppearanceSettingsControl.SecondRussianSubtitlesFontComboBox.Text = secondRussianSubtitlesStyle[0];
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[1]);
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[2]);
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[3]);
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[4]);
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[5]);
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[6]);
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox.Checked = secondRussianSubtitlesStyle[7] == "1";

            var thirdRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.ThirdRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == thirdRussianSubtitlesStyle[0])
                {
                    subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesFontComboBox.Text))
                subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesFontComboBox.Text = thirdRussianSubtitlesStyle[0];
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[1]);
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[2]);
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[3]);
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[4]);
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[5]);
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[6]);
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox.Checked = thirdRussianSubtitlesStyle[7] == "1";
        }

        private void SetNewRedefineSubtitlesAppearanceSettingsSetting(bool newRedefineSubtitlesAppearanceSettingsValue)
        {
            redefineSubtitlesAppearanceSettingsCheckBox.Checked = subtitlesAppearanceSettingsControl.Enabled = newRedefineSubtitlesAppearanceSettingsValue;
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
                            OpenUrl("https://github.com/0xotHik/BilingualSubtitler/releases/latest");
                        }

                        Settings.Default.LatestSeenVersion = latestVersionOnGitHub;
                        Settings.Default.Save();
                    }
                }
            }
        }

        private void ChangeVideoAndSubtitlesComboBoxesHandler()
        {
            videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.SelectedValueChanged -= videoAndSubtitlesStateComboBox_SelectedValueChanged;

            // Ну вот здесь по-хорошему, если с точки зрения верности кода, надо по-другому написать, наверное. Примерно как было.
            // Но у меня приоритет в скорости, структура состояний видео/субтитров у меня не поменяется (не должна).
            // Так что напишем так
            if (m_videoState == VideoState.Playing)
            {
                if (m_subtitlesState == SubtitlesState.Original)
                    videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.SelectedItem = m_videoPlayingWithOriginalSubtitlesComboBoxItem;
                else
                    videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.SelectedItem = m_videoPlayingWithBilingualSubtitlesComboBoxItem;
            }
            else // На паузе
            {
                if (m_subtitlesState == SubtitlesState.Original)
                    videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.SelectedItem = m_videoPausedWithOriginalSubtitlesComboBoxItem;
                else
                    videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.SelectedItem = m_videoPausedWithBilingualSubtitlesComboBoxItem;
            }

            videoAndSubtitlesStateComboBoxWithBorder.VideoAndSubtitlesStateComboBox.SelectedValueChanged += videoAndSubtitlesStateComboBox_SelectedValueChanged;

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

            return ReadSrtMarkupFromDocxLines(doc.Paragraphs);
        }


        private Subtitle[] ReadSrtFile(string pathToSRTFile, Encoding encoding)
        {
            return ReadSrtMarkup(File.ReadAllLines(pathToSRTFile, encoding));
        }

        /// <remarks>
        /// Правя здесь, не забудь поправить <see cref="ReadSrtMarkupFromDocxLines"/>, падаван юный
        /// </remarks>
        private Subtitle[] ReadSrtMarkup(string[] readedLines)
        {
            // TODO
            // Обработка ошибок?

            // Такая возня с пустыми строками — потому что Я.Переводчик отдает с пустыми строками.

            // Удаляем все пустые строки
            var rawLines = new List<string>();
            //
            foreach (var line in readedLines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                    rawLines.Add(line);
            }
            // Удаляем все строки, предшествующие строке с "-->" — там номер субтитра, он нам не интересен
            var linesThatAreSubtitleNumberIndexes = new List<int>();
            //
            for (int i = 0; i < rawLines.Count; i++)
            {
                var line = rawLines[i];

                if (line.Contains("-->"))
                    linesThatAreSubtitleNumberIndexes.Add(i - 1);
            }
            //
            var lines = new List<string>();
            for (int i = 0; i < rawLines.Count; i++)
            {
                if (!(linesThatAreSubtitleNumberIndexes.Contains(i)))
                    lines.Add(rawLines[i]);
            }

            var subtitles = new List<Subtitle>();
            var thatsGotToBeFirstLineOfText = true;
            //
            for (int i = 0; i < lines.Count; i++)
            {
                Subtitle subtitle;

                if (lines[i].Contains("-->"))
                {
                    subtitle = new Subtitle(lines[i], string.Empty);
                    i++;

                    // Считываем текст
                    //
                    // Сначала проверка границ
                    while ((i < lines.Count)
                        && (!(lines[i].Contains("-->"))))
                    {
                        // В случае с первой строкой текста нам не нужно добавлять перенос. Иначе — нужно.
                        if (thatsGotToBeFirstLineOfText)
                        {
                            subtitle.Text += lines[i];
                            thatsGotToBeFirstLineOfText = false;
                        }
                        else
                        {
                            if (Properties.Settings.Default.FixDotOrCommaAsTheFisrtCharOfNewLIne)
                            {
                                // Если у нас первым символом точка или запятая, и это уже не первая строка, значит это ошибка разметки, и ее надо добавить к первой строке
                                if (lines[i].Length > 0)
                                {
                                    if ((lines[i])[0] == '.')
                                    {
                                        subtitle.Text += $".";
                                        if (lines[i].Length > 1)
                                            subtitle.Text += $"\n{lines[i].Substring(1)}";
                                    }
                                    else if ((lines[i])[0] == ',')
                                    {
                                        subtitle.Text += $",";
                                        if (lines[i].Length > 1)
                                            subtitle.Text += $"\n{lines[i].Substring(1)}";
                                    }
                                    else if ((lines[i])[0] == '?')
                                    {
                                        subtitle.Text += $"?";
                                        if (lines[i].Length > 1)
                                            subtitle.Text += $"\n{lines[i].Substring(1)}";
                                    }
                                    else if ((lines[i])[0] == '!')
                                    {
                                        subtitle.Text += $"!";
                                        if (lines[i].Length > 1)
                                            subtitle.Text += $"\n{lines[i].Substring(1)}";
                                    }
                                    else
                                    {
                                        subtitle.Text += $"\n{lines[i]}";
                                    }
                                }
                                else
                                {
                                    subtitle.Text += $"\n{lines[i]}";
                                }
                            }
                            else
                                subtitle.Text += $"\n{lines[i]}";

                        }

                        i++;

                    }

                    subtitles.Add(subtitle);

                    // Чистка
                    thatsGotToBeFirstLineOfText = true;
                    i--;
                }
            }


            #region Былое

            //// Подсчитаем количество субтитров, которые у нас имеются
            //var subsLines = 0;
            ////
            //foreach (string line in readedLines)
            //{
            //    if (line.Contains("-->"))
            //        subsLines++;
            //}

            //// Считаем субтитры
            //var subtitles = new Subtitle[subsLines];
            //int currentSubtitleIndex = 0;
            //for (int currentLine = 0; currentLine < readedLines.Length - 1; currentLine++)
            //{
            //    if (readedLines[currentLine].Contains("-->"))
            //    {
            //        subtitles[currentSubtitleIndex] = new Subtitle(
            //            readedLines[currentLine], // Тайминг
            //            (readedLines[currentLine + 1]) // Первая строка текста
            //            );

            //        currentLine += 2;

            //        // Если у нас остался еще текст — допишем
            //        while ((currentLine < readedLines.Length) && 
            //            // Субтитры друг от друга отделяются пустой строкой
            //            (!string.IsNullOrWhiteSpace(readedLines[currentLine])))
            //        {
            //            subtitles[currentSubtitleIndex].Text += $"\n{readedLines[currentLine]}";

            //            currentLine++;
            //        }

            //        currentSubtitleIndex++;
            //    }
            //}

            //return subtitles
            #endregion

            return subtitles.ToArray();
        }

        /// <remarks>
        /// Не вызывает просто <see cref="ReadSrtMarkup"/>, потому что нужна обработка исключений
        /// </remarks>
        private Subtitle[] ReadSrtMarkupFromDocxLines(System.Collections.ObjectModel.ReadOnlyCollection<Xceed.Document.NET.Paragraph> readedLines)
        {

            // TODO
            //
            // Здесь бы заменить на тот механизм, что в ReadSrtMarkup, но и этот работает, а поменяю — нужно тестировать. Пока так.

            var subsLines = 0;

            foreach (var line in readedLines)
            {
                if (line.Text.Contains("->"))
                    subsLines++;
            }

            var subtitles = new Subtitle[subsLines];
            int currentSubtitleIndex = 0;
            for (int currentLine = 0; currentLine < readedLines.Count - 1; currentLine++)
            {
                if (readedLines[currentLine].Text.Contains("->"))
                {
                    try
                    {
                        subtitles[currentSubtitleIndex] = new Subtitle(
                            readedLines[currentLine].Text,
                            (readedLines[currentLine + 1].Text));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удался парсинг субтитра из\n{readedLines[currentLine].Text}\n{readedLines[currentLine + 1].Text}\n!\n\nОшибка:{ex.ToString()}",
                            "Не удался парсинг субтитра", MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
                    }

                    currentLine += 2;

                    while ((currentLine < readedLines.Count) && (!string.IsNullOrWhiteSpace(readedLines[currentLine].Text)))
                    {
                        subtitles[currentSubtitleIndex].Text += $"\n{readedLines[currentLine]}";

                        currentLine++;
                    }

                    currentSubtitleIndex++;
                }
            }

            return subtitles;
        }

        /// <summary>
        /// Стили
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="currentStringIndex"></param>
        /// <returns>Индекс строки "[Events]"</returns>
        private int ReadStylesSectionAndThereAreTitlesOfOriginThereFromASSMarkedupDocumentWithBilingualSubtitles(string[] lines, int currentStringIndex,
            bool readTitlesOfOrigin = false)
        {
            //Style: 0_sub_stream,Arial,20,&H00FFFFFF,&H0000FFFF,&H00000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,42,1
            //Style: 1_sub_stream,Times New Roman,20,&H000000FF,&H0000FFFF,&H00000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,0,1
            //Style: 2_sub_stream,Times New Roman,20,&H668000FF,&H6600FFFF,&H66000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,206,1
            //Style: 3_sub_stream,Times New Roman,20,&H6600D7FF,&H6600FFFF,&H66000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,248,1

            string originalSubtitlesTitleOfOrigin = null;
            string firstRussianSubtitlesTitleOfOrigin = null;
            string secondRussianSubtitlesTitleOfOrigin = null;
            string thirdRussianSubtitlesTitleOfOrigin = null;

            SubtitlesStyle originalSubtitlesStyle = null;
            SubtitlesStyle firstRussianSubtitlesStyle = null;
            SubtitlesStyle secondRussianSubtitlesStyle = null;
            SubtitlesStyle thirdRussianSubtitlesStyle = null;

            // пока currentString не "[Events]" - смотрим, строка содержит ли Style: или наш комментарий про перевод; индексСтроки++
            // Порядок, четкий порядок. Тайтл → Стайл. Оригинальные / 1-е переведенные / 2-е / 3-и. 
            while (lines[currentStringIndex].Trim() != "[Events]")
            {
                if (string.IsNullOrWhiteSpace(lines[currentStringIndex]))
                { }
                else
                {
                    // Комментарий с тайтлом
                    if (lines[currentStringIndex].StartsWith(TITLE_CONTAINING_COMMENTARY_STRING_BEGINNING))
                    {
                        if (lines[currentStringIndex + 1].StartsWith("Style: "))
                        {
                            var titleOfOrigin = GetTitleFromTitleOfOriginContainingLine(lines[currentStringIndex]);
                            var numberFirstCharacterInStyleName = int.Parse(lines[currentStringIndex + 1].Substring("Style: ".Length, 1));
                            switch (numberFirstCharacterInStyleName)
                            {
                                case 0:
                                    {
                                        originalSubtitlesTitleOfOrigin = titleOfOrigin;
                                        break;
                                    }
                                case 1:
                                    {
                                        firstRussianSubtitlesTitleOfOrigin = titleOfOrigin;
                                        break;
                                    }
                                case 2:
                                    {
                                        secondRussianSubtitlesTitleOfOrigin = titleOfOrigin;
                                        break;
                                    }
                                case 3:
                                    {
                                        thirdRussianSubtitlesTitleOfOrigin = titleOfOrigin;
                                        break;
                                    }
                            }

                        }
                        else
                        {
                            MessageBox.Show("Произошла ошибка со считыванием названия потока субтитров", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    else
                    // Стиль
                    if (lines[currentStringIndex].StartsWith("Style: "))
                    {
                        var style = new SubtitlesStyle(lines[currentStringIndex]);

                        var numberFirstCharacterInStyleName = int.Parse(lines[currentStringIndex].Substring("Style: ".Length, 1));
                        switch (numberFirstCharacterInStyleName)
                        {
                            case 0:
                                {
                                    originalSubtitlesStyle = style;
                                    break;
                                }
                            case 1:
                                {
                                    firstRussianSubtitlesStyle = style;
                                    break;
                                }
                            case 2:
                                {
                                    secondRussianSubtitlesStyle = style;
                                    break;
                                }
                            case 3:
                                {
                                    thirdRussianSubtitlesStyle = style;
                                    break;
                                }
                        }
                    }
                }

                currentStringIndex++;
            }

            if (readTitlesOfOrigin && Properties.Settings.Default.ReadAndWriteTitlesOfOriginIntoFinalFiles)
            {
                m_subtitles[SubtitlesType.Original].TitleOfOrigin = originalSubtitlesTitleOfOrigin;
                m_subtitles[SubtitlesType.FirstRussian].TitleOfOrigin = firstRussianSubtitlesTitleOfOrigin;
                m_subtitles[SubtitlesType.SecondRussian].TitleOfOrigin = secondRussianSubtitlesTitleOfOrigin;
                m_subtitles[SubtitlesType.ThirdRussian].TitleOfOrigin = thirdRussianSubtitlesTitleOfOrigin;
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

            #region Былое

            //    // Оригинальные субтитры - тайтл
            //    string titleFromTheLineIfThisIsTheTitleContainingOne = TitleFromTheLineIfThisIsTheTitleContainingOne(lines[currentStringIndex]);
            //    if (titleFromTheLineIfThisIsTheTitleContainingOne != null) // Тайтл был считан
            //    {
            //        originalSubtitlesOgTitle = titleFromTheLineIfThisIsTheTitleContainingOne;
            //        currentStringIndex++;
            //    }
            //    if (!WeNeedToStopStylesAndTitlesAreOver(lines[currentStringIndex]))
            //    {
            //        // Оригинальные субтитры - стиль
            //        originalSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);

            //        currentStringIndex++;
            //        if (!WeNeedToStopStylesAndTitlesAreOver(lines[currentStringIndex]))
            //        {
            //            // 1-й поток русских субтитов - тайтл
            //            titleFromTheLineIfThisIsTheTitleContainingOne = TitleFromTheLineIfThisIsTheTitleContainingOne(lines[currentStringIndex]);
            //            if (titleFromTheLineIfThisIsTheTitleContainingOne != null) // Тайтл был считан
            //            {
            //                firstRussianSubtitlesOgTitle = titleFromTheLineIfThisIsTheTitleContainingOne;
            //                currentStringIndex++;
            //            }
            //            if (!WeNeedToStopStylesAndTitlesAreOver(lines[currentStringIndex]))
            //            {
            //                // 1-й поток русских субтитов - стиль
            //                firstRussianSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);

            //                currentStringIndex++;
            //                if (!WeNeedToStopStylesAndTitlesAreOver(lines[currentStringIndex]))
            //                {
            //                    // 2-й поток русских субтитов - тайтл
            //                    titleFromTheLineIfThisIsTheTitleContainingOne = TitleFromTheLineIfThisIsTheTitleContainingOne(lines[currentStringIndex]);
            //                    if (titleFromTheLineIfThisIsTheTitleContainingOne != null) // Тайтл был считан
            //                    {
            //                        secondRussianSubtitlesOgTitle = titleFromTheLineIfThisIsTheTitleContainingOne;
            //                        currentStringIndex++;
            //                    }
            //                    if (!WeNeedToStopStylesAndTitlesAreOver(lines[currentStringIndex]))
            //                    {
            //                        // 2-й поток русских субтитов - стиль
            //                        secondRussianSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);

            //                        currentStringIndex++;
            //                        if (!WeNeedToStopStylesAndTitlesAreOver(lines[currentStringIndex]))
            //                        {
            //                            // 3-й поток русских субтитов - тайтл
            //                            titleFromTheLineIfThisIsTheTitleContainingOne = TitleFromTheLineIfThisIsTheTitleContainingOne(lines[currentStringIndex]);
            //                            if (titleFromTheLineIfThisIsTheTitleContainingOne != null) // Тайтл был считан
            //                            {
            //                                thirdRussianSubtitlesOgTitle = titleFromTheLineIfThisIsTheTitleContainingOne;
            //                                currentStringIndex++;
            //                            }
            //                            if (!WeNeedToStopStylesAndTitlesAreOver(lines[currentStringIndex]))
            //                            {
            //                                // 3-й поток русских субтитов - стиль
            //                                thirdRussianSubtitlesStyle = new SubtitlesStyle(lines[currentStringIndex]);

            //                                currentStringIndex++;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}



            #endregion

            return currentStringIndex;
        }

        /// <summary>
        /// Выходим, стили и названия потоков закончились
        /// </summary>
        /// <returns></returns>
        private bool WeNeedToStopStylesAndTitlesAreOver(string line)
        {
            if (line.Trim() == "[Events]")
                return true;
            // Выходим, стили закончились

            //Default
            return false;

            //if ((line == "[Events]") || (string.IsNullOrWhiteSpace(line)))
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns>Не null — если строка начинается с подстроки про то, что это строка комментария с именованием потока субтитров, <see cref="TITLE_CONTAINING_COMMENTARY_STRING_BEGINNING"/>; null - если это не так</returns>
        private string TitleFromTheLineIfThisIsTheTitleContainingOne(string line)
        {
            if (line.StartsWith(TITLE_CONTAINING_COMMENTARY_STRING_BEGINNING))
            {
                return GetTitleFromTitleOfOriginContainingLine(line);
            }

            // Default
            return null;
        }

        private string GetTitleFromTitleOfOriginContainingLine(string line)
        {
            return
                                line.Substring
                                (TITLE_CONTAINING_COMMENTARY_STRING_BEGINNING.Length,
                                line.Length - TITLE_CONTAINING_COMMENTARY_STRING_BEGINNING.Length);
        }

        private int AdjustCaretToStylesSectionOfAssFile(string[] lines)
        {
            var indexofStylesSection = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim() == "[V4+ Styles]")
                {
                    indexofStylesSection = i;
                    break;
                }
            }

            return indexofStylesSection;
        }

        private int AdjustCaretUnderHeaderInASSMarkedupDocumentWithBilingualSubtitles(int currentStringIndex)
        {
            return currentStringIndex + m_assHeader.Count;
        }

        private void ReadASSMarkedupDocumentWithBilingualSubtitles(string filePath)
        {
            //GUI
            SetMinValueOfProgressOnTasbarForTheCaseOfReadingSubtitlesFromTextFileOrClipboard();
            //
            foreach (var subtitlesType in m_subtitles.Keys)
            {
                if (ThereIsSubtitles(m_subtitles[subtitlesType].Subtitles))
                {
                    CloseSubtitleStream(subtitlesType);
                }
            }

            var lines = File.ReadAllLines(filePath);

            var currentStringIndex = 0;
            // Header
            var newCurrentStringIndex = AdjustCaretToStylesSectionOfAssFile(lines);
            currentStringIndex = newCurrentStringIndex + 1;
            // Вообще можно было сразу присваивать currentStringIndex значение, возвращенное из функции, наверное...

            // Стили
            newCurrentStringIndex = ReadStylesSectionAndThereAreTitlesOfOriginThereFromASSMarkedupDocumentWithBilingualSubtitles(lines, currentStringIndex, true);
            currentStringIndex = newCurrentStringIndex;

            // Мы — на строке "[Events]"

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

            SetTaskbarProgress();
        }

        private void WriteReadFromAssSubtitlesIntoStructure(SubtitlesType type, List<Subtitle> listOfSubtitles, string filePath)
        {
            var subtitlesAndInfo = m_subtitles[type];

            subtitlesAndInfo.Subtitles = listOfSubtitles.ToArray();

            subtitlesAndInfo.ProgressLabel.Visible = subtitlesAndInfo.ProgressBar.Visible = subtitlesAndInfo.ActionLabel.Visible = true;

            SetGUIContolsToSubtitlesWasSuccessfullyLoaded(subtitlesAndInfo);

            subtitlesAndInfo.SetOriginalFile(filePath);
        }

        private void SetGUIContolsToSubtitlesWasSuccessfullyLoaded(SubtitlesAndInfo subtitlesAndInfo)
        {
            subtitlesAndInfo.ProgressBar.Value = primarySubtitlesProgressBar.Maximum;
            subtitlesAndInfo.ProgressLabel.Text = $"100%";
            subtitlesAndInfo.ActionLabel.Text = SUBTITLES_ARE_OPENED;
            subtitlesAndInfo.ButtonOpenOrClose.Text = $"x\nУбрать";
            // Ну вот... Я не создаю другую кнопку для закрытия открытых субтитров, скрывая кнопку открыть, а переделываю оную.
            // Не очень нравится такой вариант. Не очень :\

            //subtitlesAndInfo.ButtonOpenOrClose.Width = openOrClosePrimarySubtitlesGroupBox.Width - 20;
            //subtitlesAndInfo.ButtonOpenOrClose.Left = openOrClosePrimarySubtitlesGroupBox.Left + 10;
            //subtitlesAndInfo.ButtonOpenOrClose.Left = (openOrClosePrimarySubtitlesGroupBox.Width / 2) - (openOrClosePrimarySubtitlesButton.Width / 2);

            subtitlesAndInfo.OpenSubtitlesGroupBox.Text = "Поток субтитров";
            m_initialOpenSubtitlesGroupBoxTextBeforeCloseConfirmationDialog = subtitlesAndInfo.OpenSubtitlesGroupBox.Text;
            subtitlesAndInfo.OpenFromDownloadsButton.Visible =
            subtitlesAndInfo.OpenFromDownloadsButton.Enabled =
            subtitlesAndInfo.OpenFromClipboardButton.Visible =
            subtitlesAndInfo.OpenFromClipboardButton.Enabled =


            false;
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
                        targetSubtitlesFontComboBox = subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = subtitlesAppearanceSettingsControl.OriginalSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = subtitlesAppearanceSettingsControl.OriginalSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = subtitlesAppearanceSettingsControl.OriginalSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.OriginalSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = subtitlesAppearanceSettingsControl.OriginalSubtitlesInOneLineCheckBox;
                        targetColorButton = primarySubtitlesColorButton;

                        break;
                    }
                case SubtitlesType.FirstRussian:
                    {
                        targetSubtitlesFontComboBox = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesInOneLineCheckBox;
                        targetColorButton = firstRussianSubtitlesColorButton;

                        break;
                    }
                case SubtitlesType.SecondRussian:
                    {
                        targetSubtitlesFontComboBox = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox;
                        targetColorButton = secondRussianSubtitlesColorButton;

                        break;
                    }
                case SubtitlesType.ThirdRussian:
                    {
                        targetSubtitlesFontComboBox = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesFontComboBox;
                        targetSubtitlesMarginNumericUpDown = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesMarginNumericUpDown;
                        targetSubtitlesSizeNumericUpDown = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesSizeNumericUpDown;
                        targetSubtitlesOutlineNumericUpDown = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesOutlineNumericUpDown;
                        targetSubtitlesShadowNumericUpDown = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesShadowNumericUpDown;
                        targetSubtitlesTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesTransparencyPercentageNumericUpDown;
                        targetSubtitlesShadowTransparencyPercentageNumericUpDown = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown;
                        targetSubtitlesInOneLineCheckBox = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox;
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
                string titleOfOrigin = null;
                SubtitlesAndInfo currentSubtitles = null;

                if (m_redefineSubtitlesAppearanceSettings)
                {
                    switch (i)
                    {
                        case 0: //Оригинальные
                            {
                                font = subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox.Text;
                                marginV = subtitlesAppearanceSettingsControl.OriginalSubtitlesMarginNumericUpDown.Text;
                                size = subtitlesAppearanceSettingsControl.OriginalSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = subtitlesAppearanceSettingsControl.OriginalSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = subtitlesAppearanceSettingsControl.OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(Math.Round(float.Parse(transparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");
                                shadowTransparencyPercentage = subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(Math.Round(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");

                                subtitleInOneLine[i] = (subtitlesAppearanceSettingsControl.OriginalSubtitlesInOneLineCheckBox.Checked ? true : false);

                                color = primarySubtitlesColorButton.BackColor;

                                break;
                            }
                        case 1: //1-е переведенные
                            {
                                font = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesFontComboBox.Text;
                                marginV = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesMarginNumericUpDown.Text;
                                size = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(Math.Round(float.Parse(transparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");
                                shadowTransparencyPercentage = subtitlesAppearanceSettingsControl.FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(Math.Round(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");

                                subtitleInOneLine[i] = (subtitlesAppearanceSettingsControl.FirstRussianSubtitlesInOneLineCheckBox.Checked ? true : false);

                                color = firstRussianSubtitlesColorButton.BackColor;

                                break;
                            }
                        case 2: //2-е переведенные
                            {
                                font = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesFontComboBox.Text;
                                marginV = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesMarginNumericUpDown.Text;
                                size = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(Math.Round(float.Parse(transparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");
                                shadowTransparencyPercentage = subtitlesAppearanceSettingsControl.SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(Math.Round(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");

                                subtitleInOneLine[i] = (subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox.Checked ? true : false);

                                color = secondRussianSubtitlesColorButton.BackColor;

                                break;
                            }
                        case 3: //3-и переведенные
                            {
                                font = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesFontComboBox.Text;
                                marginV = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesMarginNumericUpDown.Text;
                                size = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesSizeNumericUpDown.Value.ToString();
                                outline = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesOutlineNumericUpDown.Value.ToString();
                                shadow = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesShadowNumericUpDown.Value.ToString();

                                transparencyPercentage = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
                                transparency = ((int)(Math.Round(float.Parse(transparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");
                                shadowTransparencyPercentage = subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                                shadowTransparency = ((int)(Math.Round(float.Parse(shadowTransparencyPercentage.ToString()) / 100f * 255f))).ToString("X2");

                                subtitleInOneLine[i] = (subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox.Checked ? true : false);

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

                if (Properties.Settings.Default.ReadAndWriteTitlesOfOriginIntoFinalFiles) // Если включено "Сохранять названия потоков субтитров"
                {
                    switch (i)
                    {
                        case 0:
                            {
                                currentSubtitles = m_subtitles[SubtitlesType.Original];
                                // Нда. Тут надо бы переписать вообще, эта функция давняя, но пока — только костыли

                                break;
                            }
                        case 1:
                            {
                                currentSubtitles = m_subtitles[SubtitlesType.FirstRussian];
                                break;
                            }
                        case 2:
                            {
                                currentSubtitles = m_subtitles[SubtitlesType.SecondRussian];
                                break;
                            }
                        case 3:
                            {
                                currentSubtitles = m_subtitles[SubtitlesType.ThirdRussian];
                                break;
                            }
                    }

                    // Если в ТекстБоксе у нас "«Поток такой-то» из файла ruseng.ass", и только это, без каких-либо пользовательских правок и т.п. — то нам нужно сохранять только само "«Поток такой-то»"
                    if (currentSubtitles.OutputTextBox.Text == SubtitlesAndInfo.ConstructTitleInfoForTrackFromFile(currentSubtitles.TitleOfOrigin, currentSubtitles.FileNameWithoutExtention, currentSubtitles.FileExtention))
                    {
                        titleOfOrigin = currentSubtitles.TitleOfOrigin;
                    }
                    else
                    {
                        titleOfOrigin = currentSubtitles.OutputTextBox.Text;
                    }

                    assSB.AppendLine($"{TITLE_CONTAINING_COMMENTARY_STRING_BEGINNING}{titleOfOrigin}");
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
                    for (int j = 0; j < subtitles.Length; j++)
                    {
                        var subtitle = subtitles[j];
                        if (subtitle != null)
                        {
                            string subtitleText = null;

                            // Если активно "Подцеплять"
                            if ((checkBox1.Checked) && (i != 0) && (i != 1))
                            {
                                if (j + 1 < subtitles.Length)
                                {
                                    if ((subtitles[j + 1].Start - subtitle.End) < TimeSpan.FromSeconds(2))
                                    {
                                        subtitleText = ((string)subtitle.Text.Clone()).Replace("\n", " ")
                                            + " {\\i1}"
                                            + ((string)subtitles[j + 1].Text.Clone()).Replace("\n", " ")
                                            + "{\\i0}";
                                    }
                                }
                            }
                            else
                            {

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
            //notifyIcon.Icon.Dispose();
            //notifyIcon.Dispose();

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

        private void OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType subtitlesType,
            bool fromDownloadsFolder = false, bool fromDefaultFolder = false, bool fromClipboard = false, bool enc1251 = false)
        {
            // Чекаем
            if (fromClipboard)
            {
                var clipboardText = Clipboard.GetText().Split("\r\n");

                var messageBoxText = "Использовать следующий текст буфера обмена:\n";
                for (int i = 0; i < 10; i++)
                {
                    if (i < clipboardText.Length)
                        messageBoxText += $"{clipboardText[i]}\n\n";
                }
                if (clipboardText.Length >= 10)
                {
                    messageBoxText += "и т.д.";
                }
                messageBoxText += "?";
                //+
                var result = MessageBox.Show(messageBoxText, "Буфер обмена", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                //+
                if (result != DialogResult.Yes)
                    return;
            }
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

            //Открываем субтитры
            if (!ThereIsSubtitles(subtitlesWithInfo.Subtitles))
            {
                if (fromClipboard)
                {
                    subtitlesWithInfo.FromClipboard = true;

                    ReadSubtitlesFromClipboard(subtitlesType);
                }
                else
                {
                    string formats = "Файлы Matroska Video (.mkv); файлы SubRip Text (.srt); файлы DocX (.docx); архивы Zip, содержащие субтитры в формате SubRip Text (.zip, внутри .srt); архивы Rar, содержащие субтитры в формате SubRip Text (.rar, внутри .srt) |*.mkv; *.srt; *.docx; *.zip; *.rar";
                    //
                    if (enc1251) // Не знаю, что там по кодировке в вариантах с MKV и DOCX; мельком посмотрел - не увидел
                    {
                        formats = "Файлы SubRip Text (.srt); архивы Zip, содержащие субтитры в формате SubRip Text (.zip, внутри .srt); архивы Rar, содержащие субтитры в формате SubRip Text (.rar, внутри .srt) |*.srt; *.zip; *.rar";
                    }

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
                        ReadSubtitlesFromFile(openFileDialog.FileName, subtitlesType, enc1251);
                    }
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

            CloseSubtitleStream(subtitlesType);

        }

        private void CloseSubtitleStream(SubtitlesType subtitlesType)
        {
            var subtitlesWithInfo = m_subtitles[subtitlesType];

            subtitlesWithInfo.ButtonOpenOrClose.Text = m_initialOpenOrCloseSubtitlesButtonText;
            subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
            subtitlesWithInfo.ProgressBar.Visible = false;
            subtitlesWithInfo.ProgressLabel.Text = $"0%";
            subtitlesWithInfo.ProgressLabel.Visible = false;
            subtitlesWithInfo.ActionLabel.Text = "Поток субтитров был убран";
            subtitlesWithInfo.OutputTextBox.Text = string.Empty;

            subtitlesWithInfo.SubtitleStreamWasClosedInitializeEmptySubtitlesStream();

            // GUI
            subtitlesWithInfo.OpenSubtitlesGroupBox.Text = m_initialOpenSubtitlesGroupBoxText;
            //
            subtitlesWithInfo.OpenFromDownloadsButton.Visible =
            subtitlesWithInfo.OpenFromClipboardButton.Visible =
            Properties.Settings.Default.AdvancedMode;
            //
            subtitlesWithInfo.OpenFromDownloadsButton.Enabled =
            subtitlesWithInfo.OpenFromDefaultFolderButton.Enabled =
            subtitlesWithInfo.OpenFromClipboardButton.Enabled =
            subtitlesWithInfo.OpenIn1251Button.Enabled =
            true;
            //
            if (Properties.Settings.Default.AdvancedMode)
                subtitlesWithInfo.ButtonOpenOrClose.Left = m_initialOpenSubtitlesButtonLeft;


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

        /// <summary>
        /// Была нажата или "Убрать — да, убрать" или "Убрать — нет, не убирать"
        /// </summary>
        /// <param name="subtitlesWithInfo"></param>
        private void CloseSubtitleStreamConfirmationOrCancellationButtonHasBeenClicked(SubtitlesAndInfo subtitlesWithInfo)
        {
            subtitlesWithInfo.ButtonOpenOrClose.Show();
            //subtitlesWithInfo.ColorPickingButton.Show();

            subtitlesWithInfo.ButtonOpenOrClose.Parent.Controls.Remove(subtitlesWithInfo.CloseSubtitleStreamConfimationButton);
            subtitlesWithInfo.ButtonOpenOrClose.Parent.Controls.Remove(subtitlesWithInfo.CloseSubtitleStreamCancellationButton);

            subtitlesWithInfo.CloseSubtitleStreamConfimationButton = null;
            subtitlesWithInfo.CloseSubtitleStreamCancellationButton = null;


        }


        private void ReadSubtitlesFromFile(string fileName, SubtitlesType subtitlesType, bool enc1251 = false)
        {
            var subtitlesWithInfo = m_subtitles[subtitlesType];

            var readSubtitlesFromFileBackgroundWorker = new SubtitlesBackgroundWorker { WorkerReportsProgress = true };
            readSubtitlesFromFileBackgroundWorker.ProgressChanged += readSubtitlesFromFileBackgroundWorker_ProgressChanged;
            readSubtitlesFromFileBackgroundWorker.RunWorkerCompleted += readSubtitlesFromFileBackgroundWorker_RunWorkerCompleted;
            readSubtitlesFromFileBackgroundWorker.DoWork += (
                ((object sender, DoWorkEventArgs eventArgs) =>
                //
                // Считывание
                //
            {
                var filePath = (string)eventArgs.Argument;

                var parentBgW = (SubtitlesBackgroundWorker)sender;
                var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

                var sourceFileFI = new FileInfo(filePath);
                var extension = sourceFileFI.Extension;

                var encoding = Encoding.UTF8;
                //
                if (enc1251) encoding = Encoding.GetEncoding("windows-1251");

                switch (extension)
                {
                    case ".mkv":
                        {
                            var mkvFile = new MatroskaFile(filePath);
                            var tracks = mkvFile.GetTracks(true);

                            // Вызов формы для выбора трека субтитров
                            var trackSelectionForm = new TrackToExtractFromMKVForm(tracks);
                            //
                            var dialogResult = trackSelectionForm.ShowDialogInForeground();
                            if (dialogResult == DialogResult.OK)
                            {
                                // Заполняеми информацию
                                FillTheBasicSubtitlesInformationFromBackgroundWorker(filePath, subtitlesInfo);
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
                                    DoGUIActionsInTheBeginningOfSubtitlesReading(parentBgW.SubtitlesType, outputTextBoxText, false);
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
                    case ".srt":
                        {
                            // Заполняеми информацию
                            FillTheBasicSubtitlesInformationFromBackgroundWorker(filePath, subtitlesInfo);
                            subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                            //GUI
                            var outputTextBoxText = subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention;
                            //
                            Invoke((Action)((() =>
                            {
                                DoGUIActionsInTheBeginningOfSubtitlesReading(parentBgW.SubtitlesType, outputTextBoxText, true);
                            })));

                            subtitlesInfo.Subtitles = ReadSrtFile(filePath, encoding);

                            break;
                        }
                    case ".docx":
                        {
                            // Заполняеми информацию
                            FillTheBasicSubtitlesInformationFromBackgroundWorker(filePath, subtitlesInfo);
                            subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                            //GUI
                            var outputTextBoxText = subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention;
                            //
                            Invoke((Action)((() =>
                            {
                                DoGUIActionsInTheBeginningOfSubtitlesReading(parentBgW.SubtitlesType, outputTextBoxText, true);
                            })));

                            subtitlesInfo.Subtitles = ReadDocx(filePath);

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
                                FillTheBasicSubtitlesInformationFromBackgroundWorker(filePath, subtitlesInfo);
                                //
                                subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                                //GUI
                                var outputTextBoxText = $"{fileSelectionForm.SelectedFileName} из {subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention}";
                                //
                                Invoke((Action)((() =>
                                {
                                    DoGUIActionsInTheBeginningOfSubtitlesReading(parentBgW.SubtitlesType, outputTextBoxText, true);
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

                                                    text = encoding.GetString(unzippedArray);
                                                }
                                            }

                                            break;
                                        }
                                    }
                                }

                                var lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                                subtitlesInfo.Subtitles = ReadSrtMarkup(lines);
                            }


                            break;
                        }
                    case ".rar":
                        {
                            // Сначала сделаем жесткий костыль и сконвертим .rar в .zip, потому что иначе у меня открыть сабы в 1251 не получилось :\
                            var zipStream = new MemoryStream();
                            //
                            using (Archive zip = new Archive())
                            {
                                // Load the RAR archive
                                using (RarArchive rar = new RarArchive(filePath))
                                {
                                    // Loop through entries of RAR file
                                    for (int i = 0; i < rar.Entries.Count; i++)
                                    {
                                        // Copy each entry from RAR to ZIP
                                        if (!rar.Entries[i].IsDirectory)
                                        {
                                            var ms = new MemoryStream();
                                            rar.Entries[i].Extract(ms);
                                            ms.Seek(0, SeekOrigin.Begin);
                                            zip.CreateEntry(rar.Entries[i].Name, ms);
                                        }
                                        else
                                            zip.CreateEntry(rar.Entries[i].Name + "/", Stream.Null);
                                    }
                                }
                                // Save the resultant ZIP archive
                                zip.Save(zipStream);
                            }
                            zipStream.Position = 0;
                            // Трахался, трахался, открывал через 2 разных библиотеки файл в кодировке 1251 из .rar архива — ни черта.
                            // Содержимое архива — норм, показывает, говоришь "открой теперь вот этот entry из архива" — фига. UTF8 из .rar — спокойно, всё пучком. 1251 из .zip стандартной либой c# — тоже.
                            // Через ту же библиотеку просто сконвертил .rar в .zip, и дальше открыл уже готовым путем.
                            // Работает)))) Почему...

                            var text = string.Empty;
                            var zippedFiles = new List<string>();

                            var zipToReadEntries = new System.IO.Compression.ZipArchive(zipStream, ZipArchiveMode.Read);
                            foreach (var entry in zipToReadEntries.Entries)
                            {
                                zippedFiles.Add(entry.Name);
                            }
                            //
                            zipStream.Position = 0;

                            using var fileSelectionForm = new FileToUseFromZipForm(zippedFiles);
                            var dialogResult = fileSelectionForm.ShowDialog();
                            if (dialogResult == DialogResult.OK)
                            {
                                // Заполняеми информацию
                                FillTheBasicSubtitlesInformationFromBackgroundWorker(filePath, subtitlesInfo);
                                //
                                subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                                //GUI
                                var outputTextBoxText = $"{fileSelectionForm.SelectedFileName} из {subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention}";
                                //
                                Invoke((Action)((() =>
                                {
                                    DoGUIActionsInTheBeginningOfSubtitlesReading(parentBgW.SubtitlesType, outputTextBoxText, true);
                                })));

                                using (var zip = new System.IO.Compression.ZipArchive(zipStream, ZipArchiveMode.Read))
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


                                                    text = encoding.GetString(unzippedArray);
                                                }
                                            }

                                            break;
                                        }
                                    }
                                }

                                var lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                                subtitlesInfo.Subtitles = ReadSrtMarkup(lines);

                                zipStream.Close();
                                zipStream.Dispose();
                            }



                            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                            //var text = string.Empty;
                            //var zippedFiles = new List<string>();

                            //using (var file = File.OpenRead(filePath))
                            //using (var zip = new RarArchive(file))
                            //{
                            //    foreach (var entry in zip.Entries)
                            //    {
                            //        zippedFiles.Add(entry.Name);
                            //    }
                            //}

                            //using var fileSelectionForm = new FileToUseFromZipForm(zippedFiles);
                            //var dialogResult = fileSelectionForm.ShowDialog();
                            //if (dialogResult == DialogResult.OK)
                            //{
                            //    // Заполняеми информацию
                            //    FillTheBasicSubtitlesInformationFromBackgroundWorker(filePath, subtitlesInfo);
                            //    //
                            //    subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

                            //    //GUI
                            //    var outputTextBoxText = $"{fileSelectionForm.SelectedFileName} из {subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention}";
                            //    //
                            //    Invoke((Action)((() =>
                            //    {
                            //        DoGUIActionsInTheBeginningOfSubtitlesReading(parentBgW.SubtitlesType, outputTextBoxText, true);
                            //    })));

                            //    using (var file = File.OpenRead(filePath))
                            //    using (var zip = new RarArchive(file))
                            //    {
                            //        foreach (var entry in zip.Entries)
                            //        {
                            //            if (entry.Name == fileSelectionForm.SelectedFileName)
                            //            {
                            //                using (var ms = new MemoryStream())
                            //                {
                            //                    entry.Extract(ms);
                            //                    var unzippedArray = ms.ToArray();

                            //                    text = Encoding.UTF8.GetString(unzippedArray);
                            //                }

                            //                break;
                            //            }
                            //        }
                            //    }

                            //    var lines = text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                            //    subtitlesInfo.Subtitles = ReadSrtMarkup(lines);
                            //}


                            break;
                        }

                    default:
                        {
                            throw new Exception();
                        }
                }
            }));

            subtitlesWithInfo.SetBackgroundWorker(readSubtitlesFromFileBackgroundWorker, subtitlesType);

            //subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
            //subtitlesWithInfo.ProgressLabel.Text = $"0%";

            //subtitlesWithInfo.ButtonOpenOrClose.Enabled =
            //    subtitlesWithInfo.OpenFromDownloadsButton.Enabled =
            //        subtitlesWithInfo.OpenFromDefaultFolderButton.Enabled = false;
            //if (subtitlesWithInfo.ButtonTranslate != null)
            //    subtitlesWithInfo.ButtonTranslate.Enabled = false;
            //subtitlesWithInfo.ActionLabel.Text = SUBTITLES_ARE_OPENING;

            if (subtitlesType == SubtitlesType.Original)
            {
                var extension = new FileInfo(fileName).Extension;

                SetSubtitlesAndVideoFilePaths(fileName, extension == ".mkv" ? true : false);
            }

            subtitlesWithInfo.BackgroundWorker.RunWorkerAsync(fileName);

        }

        private void ReadSubtitlesFromClipboard(SubtitlesType subtitlesType)
        {
            var subtitlesWithInfo = m_subtitles[subtitlesType];

            DoGUIActionsInTheBeginningOfSubtitlesReading(subtitlesType, "Из буфера обмена", true);

            //subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
            //subtitlesWithInfo.ProgressLabel.Text = $"0%";

            //subtitlesWithInfo.ButtonOpenOrClose.Enabled =
            //    subtitlesWithInfo.OpenFromDownloadsButton.Enabled =
            //        subtitlesWithInfo.OpenFromDefaultFolderButton.Enabled = false;
            //if (subtitlesWithInfo.ButtonTranslate != null)
            //    subtitlesWithInfo.ButtonTranslate.Enabled = false;
            //subtitlesWithInfo.ActionLabel.Text = SUBTITLES_ARE_OPENING;

            // Чтение
            var text = Clipboard.GetText().Split("\r\n");
            subtitlesWithInfo.Subtitles = ReadSrtMarkup(text);

            SubtitlesReadingHasEnded(subtitlesType);
        }

        /// <summary>
        /// Запускался из <see cref="ReadSubtitlesFromFile"/>
        /// </summary>
        private void readSubtitlesFromFileBackgroundWorker_DoWork(object sender, DoWorkEventArgs eventArgs)
        {
        }

        private void FillTheBasicSubtitlesInformationFromBackgroundWorker(string filePath, SubtitlesAndInfo subtitlesInfo)
        {
            Invoke(() =>
            {
                subtitlesInfo.SetOriginalFile(filePath);
            });
        }

        /// <summary>
        /// В начале считывания — здесь работаем с ГУЕм.
        /// По окончанию — в <see cref="SubtitlesReadingHasEnded"/>
        /// </summary>
        private void DoGUIActionsInTheBeginningOfSubtitlesReading(SubtitlesType subtitlesType, string outputTextBoxText,
            bool readingSubtitlesFromTextFileOrClipboard)
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

            var subtitlesWithInfo = m_subtitles[subtitlesType];
            //
            subtitlesWithInfo.OutputTextBox.Text = outputTextBoxText;

            subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
            subtitlesWithInfo.ProgressLabel.Text = $"0%";

            // !
            subtitlesWithInfo.ButtonOpenOrClose.Enabled =
                subtitlesWithInfo.OpenFromDownloadsButton.Enabled =
                    subtitlesWithInfo.OpenFromDefaultFolderButton.Enabled =
                    subtitlesWithInfo.OpenFromClipboardButton.Enabled =
                    subtitlesWithInfo.OpenIn1251Button.Enabled =
                        false;

            if (subtitlesWithInfo.ButtonTranslate != null)
                subtitlesWithInfo.ButtonTranslate.Enabled = false;
            subtitlesWithInfo.ActionLabel.Text = SUBTITLES_ARE_OPENING;

            if (readingSubtitlesFromTextFileOrClipboard)
                SetMinValueOfProgressOnTasbarForTheCaseOfReadingSubtitlesFromTextFileOrClipboard();
        }

        /// <summary>
        /// В случае с считыванием субтитров из их файла, не из Матрешки, не вижу смысла писать нормальное изменение прогресса прогрессБара, потому что они считываются почти мгновенно.
        /// Но на всякий случай я обозначу то, что начался какой-то процесс, и он в самой начальной своей стадии минимальным значением — 1.
        /// </summary>
        /// <returns></returns>
        private void SetMinValueOfProgressOnTasbarForTheCaseOfReadingSubtitlesFromTextFileOrClipboard()
        {
            SetTaskbarProgress(true);
        }

        private void readSubtitlesFromFileBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs eventArgs)
        {
            var parentBgW = (SubtitlesBackgroundWorker)sender;
            var subtitlesInfo = m_subtitles[parentBgW.SubtitlesType];

            subtitlesInfo.ProgressBar.Value = eventArgs.ProgressPercentage;
            subtitlesInfo.ProgressLabel.Text = $"{eventArgs.ProgressPercentage}%";

            SetTaskbarProgress();
        }

        private void SetTaskbarProgress(bool setMinProgressBarValue = false)
        {
            #region Схема с прогрессБаром по минимальному значению
            int minProgressBarValue = setMinProgressBarValue ? 1 // См. вызывающую функцию в случае такого флага
                : 100;

            foreach (var subtitlesWithInfo in m_subtitles.Values)
            {
                if (ThisProgressBarHasMeaningToAffectTaskbarProgress(subtitlesWithInfo.ProgressBar) && subtitlesWithInfo.ProgressBar.Value < minProgressBarValue)
                    minProgressBarValue = subtitlesWithInfo.ProgressBar.Value;
            }

            if (minProgressBarValue == 0)
                minProgressBarValue = 1;

            // Если что-то еще делается, изменим прогресс на Таскбаре. Иначе — очистим
            if (minProgressBarValue != 100)
                Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressValue(minProgressBarValue, 100);
            else
                Microsoft.WindowsAPICodePack.Taskbar.TaskbarManager.Instance.SetProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.NoProgress);
            #endregion
        }

        /// <summary>
        /// Надо ли нам учитывать этот прогрессБар при изменении прогресса на Таскбаре
        /// </summary>
        /// <param name="progressBar"></param>
        private bool ThisProgressBarHasMeaningToAffectTaskbarProgress(ProgressBar progressBar)
        {
            return progressBar.Visible;
        }

        private void readSubtitlesFromFileBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs eventArgs)
        {
            var parentBgW = (SubtitlesBackgroundWorker)sender;
            SubtitlesReadingHasEnded(parentBgW.SubtitlesType);
        }

        /// <summary>
        /// В конце считывания работаем с ГУЕм здесь
        /// В начале — в <see cref="DoGUIActionsInTheBeginningOfSubtitlesReading">
        /// </summary>
        /// <param name="subtitlesType"></param>
        private void SubtitlesReadingHasEnded(SubtitlesType subtitlesType)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];

            if (ThereIsSubtitles(subtitlesInfo.Subtitles))
            {
                subtitlesInfo.ProgressBar.Value = subtitlesInfo.ProgressBar.Maximum;
                subtitlesInfo.ProgressLabel.Text = $"100%";

                if (subtitlesType == SubtitlesType.Original)
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
                subtitlesInfo.ProgressBar.Visible = false;
                subtitlesInfo.ProgressLabel.Text = $"0%";
                subtitlesInfo.ActionLabel.Text = "Произошла ошибка во время чтения субтитров";
            }

            // !
            subtitlesInfo.ButtonOpenOrClose.Enabled =
                subtitlesInfo.OpenFromDownloadsButton.Enabled =
                    subtitlesInfo.OpenFromDefaultFolderButton.Enabled =
                    subtitlesInfo.OpenFromClipboardButton.Enabled =
                    subtitlesInfo.OpenIn1251Button.Enabled =
                    true;

            if (subtitlesInfo.ButtonTranslate != null)
                subtitlesInfo.ButtonTranslate.Enabled = true;

            SetTaskbarProgress();

            // TODO Ошибки?
        }

        private bool ThereIsSubtitles(Subtitle[] subtitles)
        {
            if (subtitles != null
                // && subtitles.Length > 0 // Вот здесь не знаю. Но, наверное, в моём случае, субтитры с каунтом 0 — все равно субтитры.
                // Если потока прям вообще нет — надо занулять
                )
                return true;

            return false;
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
            // Путь, по которому нужно создавать субтитры, не задан
            if (string.IsNullOrWhiteSpace(finalSubtitlesFilesPathBeginningRichTextBox.Text))
            {
                MessageBox.Show("Путь, по которому нужно создавать субтитры, не задан!", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

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
                    using (var filesAlreadyExistsForm = new FilesAlreadyExistsForm(originalSubtitlesPath, bilingualSubtitlesPath))
                    {
                        filesAlreadyExistsForm.ShowDialog();
                        if (filesAlreadyExistsForm.RewriteExistingFiles != true)
                            return;
                    }
                }


                //var result = MessageBox.Show($"Файлы\n\n• {originalSubtitlesPath}\n\nи\n\n• {bilingualSubtitlesPath}\n\nуже существуют! Перезаписать их?",
                //    String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                //if (result != DialogResult.OK)
                //    return;
                else if (originalSubtitlesFileExist)
                {
                    using (var filesAlreadyExistsForm = new FilesAlreadyExistsForm(originalSubtitlesPath))
                    {
                        filesAlreadyExistsForm.ShowDialog();
                        if (filesAlreadyExistsForm.RewriteExistingFiles != true)
                            return;
                    }

                    //var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуже существует! Перезаписать его?",
                    //    String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    //if (result != DialogResult.OK)
                    //    return;
                }
                else if (bilingualSubtitlesFileExists)
                {
                    using (var filesAlreadyExistsForm = new FilesAlreadyExistsForm(bilingualSubtitlesPath))
                    {
                        filesAlreadyExistsForm.ShowDialog();
                        if (filesAlreadyExistsForm.RewriteExistingFiles != true)
                            return;
                    }

                    //var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуже существует! Перезаписать его?",
                    //    String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    //if (result != DialogResult.OK)
                    //    return;
                }
            }
            else if (Settings.Default.CreateOriginalSubtitlesFile && !Settings.Default.CreateBilingualSubtitlesFile)
            {
                if (originalSubtitlesFileExist)
                {

                    using (var filesAlreadyExistsForm = new FilesAlreadyExistsForm(originalSubtitlesPath))
                    {
                        filesAlreadyExistsForm.ShowDialog();
                        if (filesAlreadyExistsForm.RewriteExistingFiles != true)
                            return;
                    }

                    //var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуже существует! Перезаписать его?",
                    //    String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    //if (result != DialogResult.OK)
                    //    return;
                }
            }
            else if (Settings.Default.CreateBilingualSubtitlesFile && !Settings.Default.CreateOriginalSubtitlesFile)
            {
                if (bilingualSubtitlesFileExists)
                {
                    using (var filesAlreadyExistsForm = new FilesAlreadyExistsForm(bilingualSubtitlesPath))
                    {
                        filesAlreadyExistsForm.ShowDialog();
                        if (filesAlreadyExistsForm.RewriteExistingFiles != true)
                            return;
                    }


                    //var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуже существует! Перезаписать его?",
                    //    String.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    //if (result != DialogResult.OK)
                    //    return;
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
                    using (var subtitlesSavedSuccessfullyForm = new SubtitlesSavedSuccessfullyForm(originalSubtitlesPath, bilingualSubtitlesPath))
                    {
                        subtitlesSavedSuccessfullyForm.ShowDialog();
                    }


                    //var result = MessageBox.Show($"Файлы\n\n• {originalSubtitlesPath}\n\nи\n\n• {bilingualSubtitlesPath}\n\nуспешно записаны!",
                    //    SUCCESS_MESSAGE_BOX_HEADER, MessageBoxButtons.OK, SUCCESS_MESSAGE_BOX_ICON);
                    //if (result != DialogResult.OK)
                    //    return;
                }
                else if (originalSubtitlesFileExist)
                {
                    using (var subtitlesSavedSuccessfullyForm = new SubtitlesSavedSuccessfullyForm(originalSubtitlesPath))
                    {
                        subtitlesSavedSuccessfullyForm.ShowDialog();
                    }

                    //var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуспешно записан!",
                    //    SUCCESS_MESSAGE_BOX_HEADER, MessageBoxButtons.OK, SUCCESS_MESSAGE_BOX_ICON);
                    //if (result != DialogResult.OK)
                    //    return;
                }
                else if (bilingualSubtitlesFileExists)
                {
                    using (var subtitlesSavedSuccessfullyForm = new SubtitlesSavedSuccessfullyForm(bilingualSubtitlesPath))
                    {
                        subtitlesSavedSuccessfullyForm.ShowDialog();
                    }

                    //var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуспешно записан!",
                    //    SUCCESS_MESSAGE_BOX_HEADER, MessageBoxButtons.OK, SUCCESS_MESSAGE_BOX_ICON);
                    //if (result != DialogResult.OK)
                    //    return;
                }
            }
            else if (Settings.Default.CreateOriginalSubtitlesFile && !Settings.Default.CreateBilingualSubtitlesFile)
            {
                if (originalSubtitlesFileExist)
                {
                    using (var subtitlesSavedSuccessfullyForm = new SubtitlesSavedSuccessfullyForm(originalSubtitlesPath))
                    {
                        subtitlesSavedSuccessfullyForm.ShowDialog();
                    }

                    //var result = MessageBox.Show($"Файл\n\n• {originalSubtitlesPath}\n\nуспешно записан!",
                    //    SUCCESS_MESSAGE_BOX_HEADER, MessageBoxButtons.OK, SUCCESS_MESSAGE_BOX_ICON);
                    //if (result != DialogResult.OK)
                    //    return;
                }
            }
            else if (Settings.Default.CreateBilingualSubtitlesFile && !Settings.Default.CreateOriginalSubtitlesFile)
            {
                if (bilingualSubtitlesFileExists)
                {
                    using (var subtitlesSavedSuccessfullyForm = new SubtitlesSavedSuccessfullyForm(bilingualSubtitlesPath))
                    {
                        subtitlesSavedSuccessfullyForm.ShowDialog();
                    }

                    //var result = MessageBox.Show($"Файл\n\n• {bilingualSubtitlesPath}\n\nуспешно записан!",
                    //    SUCCESS_MESSAGE_BOX_HEADER, MessageBoxButtons.OK, SUCCESS_MESSAGE_BOX_ICON);
                    //if (result != DialogResult.OK)
                    //    return;
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

                SaveSubtitlesColorsFromFormToPropertiesSettings();

                //if (senderButton == primarySubtitlesColorButton)
                //    Properties.Settings.Default.PrimarySubtitlesColor = primarySubtitlesColorButton.BackColor;
                //else if (senderButton == firstRussianSubtitlesColorButton)
                //    Properties.Settings.Default.FirstRussianSubtitlesColor = firstRussianSubtitlesColorButton.BackColor;
                //else if (senderButton == secondRussianSubtitlesColorButton)
                //    Properties.Settings.Default.SecondRussianSubtitlesColor = secondRussianSubtitlesColorButton.BackColor;
                //else if (senderButton == thirdRussianSubtitlesColorButton)
                //    Properties.Settings.Default.ThirdRussianSubtitlesColor = thirdRussianSubtitlesColorButton.BackColor;

                //Properties.Settings.Default.Save();
            }
        }

        private void SaveSubtitlesColorsFromFormToPropertiesSettings()
        {
            Properties.Settings.Default.PrimarySubtitlesColor = primarySubtitlesColorButton.BackColor;
            Properties.Settings.Default.FirstRussianSubtitlesColor = firstRussianSubtitlesColorButton.BackColor;
            Properties.Settings.Default.SecondRussianSubtitlesColor = secondRussianSubtitlesColorButton.BackColor;
            Properties.Settings.Default.ThirdRussianSubtitlesColor = thirdRussianSubtitlesColorButton.BackColor;

            Properties.Settings.Default.Save();
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
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.Original);

            //OpenFileAndReadSubtitlesFromFile(ref m_originalSubtitles,
            //    primarySubtitlesProgressBar, primarySubtitlesProgressLabel, primarySubtitlesActionLabel, primarySubtitlesTextBox,
            //    openPrimarySubtitlesButton);
        }

        private void openOrCloseFirstRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.FirstRussian);
            //OpenFileAndReadSubtitlesFromFile(ref m_firstRussianSubtitles,
            //    firstRussianSubtitlesProgressBar, firstRussianSubtitlesProgressLabel, firstRussianSubtitlesActionLabel,
            //    firstRussianSubtitlesTextBox, openFirstRussianSubtitlesButton, translateToFirstRussianSubtitlesButton);
        }

        private void openOrCloseSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.SecondRussian);

            //OpenFileAndReadSubtitlesFromFile(ref m_secondRussianSubtitles,
            //    secondRussianSubtitlesProgressBar, secondRussianSubtitlesProgressLabel, secondRussianSubtitlesActionLabel,
            //    secondRussianSubtitlesTextBox, openSecondRussianSubtitlesButton, translateToSecondRussianSubtitlesButton);
        }

        private void openOrCloseThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.ThirdRussian);

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
            using (var settingsForm = new SettingsForm(this))
            {
                settingsForm.ShowDialog(this);
                settingsForm.Dispose();

                if (settingsForm.SettingsWasSaved)
                    SetProgramAccordingToSettings();
            }
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
            OpenUrl("https://0xothik.wordpress.com/bilingual-subtitler#Help");

            //using var formAbout = new FormAbout();
            //formAbout.ShowDialog();
        }

        private void playVideoButton_Click(object sender, EventArgs e)
        {
            var videoFileName = finalSubtitlesFilesPathBeginningRichTextBox.Text +
                    GetVideoFileExtention();
            try
            {
                OpenFile(videoFileName);
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

            // Имя по умолчанию
            string defaultFileName;
            //+
            if (subtitlesInfo.FromClipboard == true)
            {
                defaultFileName = $"BilingualSubtitler.FromClipboard.{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}-{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}";
            }
            else
            {
                defaultFileName = string.IsNullOrWhiteSpace(subtitlesInfo.TrackLanguage) ? subtitlesInfo.FileNameWithoutExtention
                    : $"{subtitlesInfo.FileNameWithoutExtention}.Track{subtitlesInfo.TrackNumber}.Subtitles." +
                    $"{subtitlesInfo.TrackLanguage.ToUpper()}.BilingualSubtitlerExport";
            }

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

            //if (intoDownloads)
            //{
            //    if (File.Exists(resultingDocXFileName))
            //        MessageBox.Show($"Субтитры были сохранены в файл {resultingDocXFileName}", "Субтитры были сохранены успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}

            CheckIfFileExistAndShowSuccessMessageAboutSubtitlesSaved(resultingDocXFileName);

        }

        private void ExportSubtitlesToSrt(SubtitlesType subtitlesType)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];

            string formats = "Файл SubRipText |*.srt";

            // Имя по умолчанию
            string defaultFileName;
            //+
            if (subtitlesInfo.FromClipboard == true)
            {
                defaultFileName = $"BilingualSubtitler.FromClipboard.{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}-{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}";
            }
            else
            {
                defaultFileName = string.IsNullOrWhiteSpace(subtitlesInfo.TrackLanguage) ? subtitlesInfo.FileNameWithoutExtention
                    : $"{subtitlesInfo.FileNameWithoutExtention}.Track{subtitlesInfo.TrackNumber}.Subtitles." +
                    $"{subtitlesInfo.TrackLanguage.ToUpper()}.BilingualSubtitlerExport";
            }

            bool goodToGo = false;
            string resultingFileName = string.Empty;

            using var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = formats;
            saveFileDialog.FileName = defaultFileName;

            var result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                goodToGo = true;
                resultingFileName = saveFileDialog.FileName;
            }

            if (goodToGo == true)
            {
                var timeFormat = @"hh\:mm\:ss\,fff";

                var lines = new List<string>();
                for (int i = 0; i < subtitlesInfo.Subtitles.Length; i++)
                {
                    var subtitle = subtitlesInfo.Subtitles[i];

                    lines.Add((i + 1).ToString());
                    lines.Add($"{subtitle.Start.ToString(timeFormat)} --> {subtitle.End.ToString(timeFormat)}");
                    lines.Add(subtitle.Text);
                    lines.Add("");
                }

                File.WriteAllLines(resultingFileName, lines.ToArray());
            }

            CheckIfFileExistAndShowSuccessMessageAboutSubtitlesSaved(resultingFileName);
        }

        private void CheckIfFileExistAndShowSuccessMessageAboutSubtitlesSaved(string resultingFileName)
        {
            CheckIfFileExistAndShowSuccessMessage(resultingFileName);
        }

        private void CheckIfFileExistAndShowSuccessMessage(string resultingFileName)
        {
            if (File.Exists(resultingFileName))
            {
                using (var openSavedFileInDefaultAppForm = new OpenSavedFileInDefaultAppForm(resultingFileName))
                {
                    openSavedFileInDefaultAppForm.ShowDialog();

                    if (openSavedFileInDefaultAppForm.NeedToOpenInDefaultApp)
                    {
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = resultingFileName;
                        psi.UseShellExecute = true;
                        Process.Start(psi);
                    }
                }
            }
        }

        //private void CheckIfFileExistAndShowSuccessMessage(string resultingFileName, string message)
        //{
        //    var messageBoxHeader = SUCCESS_MESSAGE_BOX_HEADER;
        //    var messageBoxIcon = SUCCESS_MESSAGE_BOX_ICON;

        //    if (File.Exists(resultingFileName))
        //    {
        //        message += $":\n• {resultingFileName}";
        //        if (Properties.Settings.Default.AskToOpenSavedFileInDefaultApp)
        //        {
        //            message += "\n\n\nВернуться к окну Bilingual Subtitler, без работы с сохраненным файлом?\n(\"Нет\" — откроет сохраненный файл в программе по умолчанию)";
        //            var result = MessageBox.Show(message,
        //                $"{messageBoxHeader}. Вернуться к основному окну Bilingual Subtitler?",
        //                MessageBoxButtons.YesNo, messageBoxIcon, MessageBoxDefaultButton.Button1);

        //            if (result == DialogResult.No)
        //            {
        //                ProcessStartInfo psi = new ProcessStartInfo();
        //                psi.FileName = resultingFileName;
        //                psi.UseShellExecute = true;
        //                Process.Start(psi);
        //            }
        //        }
        //        else
        //            MessageBox.Show(message, messageBoxHeader, MessageBoxButtons.OK, messageBoxIcon);

        //    }
        //}

        private void firstRussianSubtitlesTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://translate.yandex.ru/doc");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://translate.google.com/#view=home&op=docs&sl=en&tl=ru");
        }

        private void OpenFile(string path)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(path)
            {
                UseShellExecute = true
            };
            p.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <remarks>https://stackoverflow.com/a/43232486/16721472</remarks>
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
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

        private void ResetSubtitlesAppearanceToDefaultButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Сбросить текущие настройки вида субтитров к значениям, заданным в окне настроек программы?", string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                subtitlesAppearanceSettingsControl.SetAccordingToPropertiesSettings();
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

                SaveSubtitlesColorsFromFormToPropertiesSettings();
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
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.Original, true);
        }

        private void primarySubtitlesExportAsDocxIntoDownloadsButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToDocx(SubtitlesType.Original, true);
        }

        private void firstRussianSubtitlesOpenFromDownloadsButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.FirstRussian, true);
        }

        private void secondRussianSubtitlesOpenFromDownloadsButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.SecondRussian, true);
        }

        private void thirdRussianSubtitlesOpenFromDownloadsButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.ThirdRussian, true);
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

        private void OpenAndReadSubtitlesFromDefaultFolder(SubtitlesType subtitlesType)
        {
            if (ThereIsSubtitles(m_subtitles[subtitlesType].Subtitles))
            {
                var result = MessageBox.Show("Убрать имеющиеся в этом потоке субтитры и считать новые?", string.Empty, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    CloseSubtitleStream(subtitlesType);
                }
            }

            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(subtitlesType, fromDefaultFolder: true);
        }

        private void openPrimarySubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromDefaultFolder(SubtitlesType.Original);
        }

        private void openFirstRussianSubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromDefaultFolder(SubtitlesType.FirstRussian);
        }

        private void openSecondRussianSubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromDefaultFolder(SubtitlesType.SecondRussian);
        }

        private void openThirdRussianSubtitlesFromDefaultFolderButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromDefaultFolder(SubtitlesType.ThirdRussian);
        }

        private void showLastSubtitleOfFirstRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            var originalSubtitles = m_subtitles[SubtitlesType.Original].Subtitles;
            var firstRussianSubtitles = m_subtitles[SubtitlesType.FirstRussian].Subtitles;
            var secondRussianSubtitles = m_subtitles[SubtitlesType.SecondRussian].Subtitles;
            var thirdRussianSubtitles = m_subtitles[SubtitlesType.ThirdRussian].Subtitles;

            var showSubtitlesForm = new ShowSubtitlesForm(originalSubtitles, firstRussianSubtitles, secondRussianSubtitles, thirdRussianSubtitles);
            showSubtitlesForm.ShowDialog();
            //ShowLastSubtitleOfSubtitles(SubtitlesType.FirstRussian);
        }

        private void showLastSubtitleOfSecondRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            ShowLastSubtitleOfSubtitles(SubtitlesType.SecondRussian);
        }

        private void showLastSubtitleOfThirdRussianSubtitlesButton_Click(object sender, EventArgs e)
        {
            ShowLastSubtitleOfSubtitles(SubtitlesType.ThirdRussian);
        }

        private void ShowLastSubtitleOfSubtitles(SubtitlesType subtitlesType)
        {
            var subtitlesInfo = m_subtitles[subtitlesType];
            var subtitles = subtitlesInfo.Subtitles;

            if (subtitles != null)
            {
                var message = "Первое слово из последних 4-х титров:\n";
                message += $"1-й титр с конца: {GetFirstWordOfSubtitleText(subtitles[subtitles.Length - 1])}\n";
                message += $"2-й титр с конца: {GetFirstWordOfSubtitleText(subtitles[subtitles.Length - 2])}\n";
                message += $"3-й титр с конца: {GetFirstWordOfSubtitleText(subtitles[subtitles.Length - 3])}\n";
                message += $"4-й титр с конца: {GetFirstWordOfSubtitleText(subtitles[subtitles.Length - 4])}\n";

                MessageBox.Show(message);
            }

        }

        private string GetFirstWordOfSubtitleText(Subtitle subtitle)
        {
            if ((subtitle == null) ||
                (String.IsNullOrWhiteSpace(subtitle.Text)))
                return string.Empty;

            return subtitle.Text.Split(' ')[0];

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.FirstRussian, fromClipboard: true);
            //var subtitlesType = SubtitlesType.FirstRussian;


            //var subtitlesWithInfo = m_subtitles[subtitlesType];


            //var readSubtitlesBackgroundWorker = new SubtitlesBackgroundWorker { WorkerReportsProgress = true };
            //readSubtitlesBackgroundWorker.DoWork += readSubtitlesBackgroundWorker_DoWork;
            //readSubtitlesBackgroundWorker.ProgressChanged += readSubtitlesBackgroundWorker_ProgressChanged;
            //readSubtitlesBackgroundWorker.RunWorkerCompleted += readSubtitlesBackgroundWorker_RunWorkerCompleted;

            //subtitlesWithInfo.ProgressBar.Value = subtitlesWithInfo.ProgressBar.Minimum;
            //subtitlesWithInfo.ProgressLabel.Text = $"0%";

            //subtitlesWithInfo.ButtonOpenOrClose.Enabled =
            //    subtitlesWithInfo.OpenFromDownloadsButton.Enabled =
            //        subtitlesWithInfo.OpenFromDefaultFolderButton.Enabled = false;
            //if (subtitlesWithInfo.ButtonTranslate != null)
            //    subtitlesWithInfo.ButtonTranslate.Enabled = false;
            //subtitlesWithInfo.ActionLabel.Text = SUBTITLES_ARE_OPENING;


            //var subtitlesInfo = m_subtitles[subtitlesType];

            //// Заполняеми информацию
            //subtitlesInfo.TrackLanguage = subtitlesInfo.TrackNumber = subtitlesInfo.TrackName = null;

            ////GUI
            //var outputTextBoxText = subtitlesInfo.FileNameWithoutExtention + subtitlesInfo.FileExtention;
            ////
            //BeginInvoke((Action)((() =>
            //{
            //    DoGUIActions(subtitlesType, outputTextBoxText);
            //})));


            //var text = Clipboard.GetText().Split("\r\n");
            //subtitlesInfo.Subtitles = ReadSrtMarkup(text);

        }

        private void primarySubtitlesExportAsSrtButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToSrt(SubtitlesType.Original);
        }

        private void originalSubtitlesOpenFromClipboardButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.Original, fromClipboard: true);
        }

        private void secondRussianSubtitlesOpenFromClipboardButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.SecondRussian, fromClipboard: true);
        }

        private void thirdRussianSubtitlesOpenFromClipboardButton_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(SubtitlesType.ThirdRussian, fromClipboard: true);
        }

        private void firstRussianSubtitlesExportAsSrtButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToSrt(SubtitlesType.FirstRussian);
        }

        private void secondRussianSubtitlesExportAsSrtButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToSrt(SubtitlesType.SecondRussian);
        }

        private void thirdRussianSubtitlesExportAsSrtButton_Click(object sender, EventArgs e)
        {
            ExportSubtitlesToSrt(SubtitlesType.ThirdRussian);
        }

        private void openStylesFromBilingualSubtitlerButton_Click(object sender, EventArgs e)
        {
            if (!redefineSubtitlesAppearanceSettingsCheckBox.Checked)
            {
                MessageBox.Show($"Для считывания стилей должна быть активна опция \"{redefineSubtitlesAppearanceSettingsCheckBox.Text}\"", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            string formats = "Субтитры, созданные через Bilingual Subtitler (.ass) |*.ass";

            using var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = formats;
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var lines = File.ReadAllLines(openFileDialog.FileName);

                var currentStringIndex = 0;
                // Header
                var newCurrentStringIndex = AdjustCaretToStylesSectionOfAssFile(lines);
                currentStringIndex = newCurrentStringIndex + 1;

                // Стили
                ReadStylesSectionAndThereAreTitlesOfOriginThereFromASSMarkedupDocumentWithBilingualSubtitles(lines, currentStringIndex);

                SaveSubtitlesColorsFromFormToPropertiesSettings();
            }
        }

        private void OpenAndReadSubtitlesIn1251(SubtitlesType subtitlesType)
        {
            if (ThereIsSubtitles(m_subtitles[subtitlesType].Subtitles))
            {
                var result = MessageBox.Show("Убрать имеющиеся в этом потоке субтитры и считать новые?", string.Empty, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    CloseSubtitleStream(subtitlesType);
                }
            }

            OpenAndReadSubtitlesFromSourceOrRemoveTheSubStream(subtitlesType, enc1251: true);
        }


        private void button3_Click_2(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesIn1251(SubtitlesType.Original);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesIn1251(SubtitlesType.FirstRussian);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesIn1251(SubtitlesType.SecondRussian);
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            OpenAndReadSubtitlesIn1251(SubtitlesType.ThirdRussian);
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            RemovePostfix(original: true);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            RemovePostfix(bilingual: true);
        }

        private void RemovePostfix(bool original = false, bool bilingual = false)
        {
            string subtitlesFileNameEnding = bilingual ? Properties.Settings.Default.BilingualSubtitlesFileNameEnding
                : Properties.Settings.Default.OriginalSubtitlesFileNameEnding;
            // Минус расширение
            var lastDotIndex = subtitlesFileNameEnding.LastIndexOf('.');
            var postfix = subtitlesFileNameEnding.Substring(0, lastDotIndex);


            // Ищем вхождения постфикса в строку
            // Берем последнее
            // Оставляем всю строку минус длина постфикса
            var newFileName = string.Empty;
            if (finalSubtitlesFilesPathBeginningRichTextBox.Text.EndsWith(postfix))
            {

                newFileName =
                    finalSubtitlesFilesPathBeginningRichTextBox.Text.Substring(0,
                    finalSubtitlesFilesPathBeginningRichTextBox.Text.Length - postfix.Length);
            }
            else
            {
                MessageBox.Show("Путь итоговых файлов субтитров / путь до файла видео (начальная часть) — не оканчивается на данный постфикс", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var dialogResult = MessageBox.Show($"Изменить текст окна \"🖴  Путь итоговых файлов субтитров / путь до файла видео (начальная часть)\" таким образом:\n\nБыло: {finalSubtitlesFilesPathBeginningRichTextBox.Text}\n\nСтанет: {newFileName}\n\n?", string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.OK)
            {
                finalSubtitlesFilesPathBeginningRichTextBox.Text = newFileName;
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            notifyIcon.Dispose();
        }

        private void subtitlesAppearanceSettingsControl_Load(object sender, EventArgs e)
        {
            subtitlesAppearanceSettingsControl.ResetSubtitlesAppearanceToDefaultButton.Text = "📖  Сбросить текущие настройки вида субтитров к сохраненным значениям";
            subtitlesAppearanceSettingsControl.ResetSubtitlesAppearanceToDefaultButton.Click += ResetSubtitlesAppearanceToDefaultButton_Click;
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
