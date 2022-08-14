using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilingualSubtitler
{

    public partial class SettingsForm : Form
    {
        private Dictionary<string, ProcessPriorityClass> m_processPriorityNamesAndValues;
        private Dictionary<ProcessPriorityClass, string> m_processPriorityValuesAndNames;

        private List<Button> m_buttons;
        private Color m_previousButtonColor;
        private bool m_flagKeyIsInvalid = false;

        public SettingsForm(MainForm mainForm)
        {
            InitializeComponent();

            try
            {
                SetFormAccordingToSettings();
            }
            catch (Exception e)
            {
                throw new BilingualSubtitlerPropertiesLoadingException(e);
            }


            m_processPriorityNamesAndValues = new Dictionary<string, ProcessPriorityClass>
            {
                { "Низкий", ProcessPriorityClass.Idle},
                { "Ниже среднего", ProcessPriorityClass.BelowNormal},
                { "Обычный", ProcessPriorityClass.Normal},
                { "Выше среднего", ProcessPriorityClass.AboveNormal},
                { "Высокий", ProcessPriorityClass.High},
                { "Реального времени", ProcessPriorityClass.RealTime},
            };

            m_processPriorityValuesAndNames = new Dictionary<ProcessPriorityClass, string>(m_processPriorityNamesAndValues.Count);
            //
            foreach (var key in m_processPriorityNamesAndValues.Keys)
                m_processPriorityValuesAndNames.Add(m_processPriorityNamesAndValues[key], key);

            // Лэйбл текущего приоритета процесса
            using (var p = Process.GetCurrentProcess())
            {
                currentProcessPriorityTextBox.Text = m_processPriorityValuesAndNames[p.PriorityClass];
            }

            // Заполняем КомбоБокс
            int indexForCurrentProcessPriority = 0;
            foreach (var prioprityName in m_processPriorityNamesAndValues.Keys)
            {
                var index = targetProcessPriorityTextBox.Items.Add(prioprityName);

                if (prioprityName == currentProcessPriorityTextBox.Text)
                    indexForCurrentProcessPriority = index;
            }
            targetProcessPriorityTextBox.SelectedIndex = indexForCurrentProcessPriority;

            // Версии
            var currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            currentAppVersionLabel.Text = currentVersion.ToString();
            // Проверка обновлений
            var checkUpdatesBgW = new BackgroundWorker();
            checkUpdatesBgW.DoWork += mainForm.CheckUpdatesBgW_DoWork;
            checkUpdatesBgW.RunWorkerCompleted += CheckUpdatesBgW_RunWorkerCompleted;
            lastAppVersionLabel.Text = "Идет проверка...";
            checkUpdatesBgW.RunWorkerAsync();
        }

        private void CheckUpdatesBgW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is Tuple<string, Octokit.Release>)
            {
                var latestVersionOnGitHub = ((Tuple<string, Octokit.Release>)e.Result).Item1;
                var latestReleaseOnGitHub = ((Tuple<string, Octokit.Release>)e.Result).Item2;

                lastAppVersionLabel.Text = latestVersionOnGitHub;
            }
            else if (e.Result is Exception)
            {
                var exception = (Exception)e.Result;
                lastAppVersionLabel.Text = "Не удалось получить информацию\nо новых версиях :( (Подробности — по клику)";
                lastAppVersionLabel.Tag = exception;
            }


        }

        public void SetFormAccordingToSettings()
        {
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

            //m_flagKeyIsInvalid = true;

            foreach (var hotkeyString in Properties.Settings.Default.Hotkeys)
            {
                AddKeyToHotkeysDataGridView(hotkeyString);
            }

            videoPlayerPauseButtonTextBox.Text =
                new Hotkey(Properties.Settings.Default.VideoPlayerPauseButtonString).KeyValue;
            videoPlayerPauseButtonTextBox.Tag = Properties.Settings.Default.VideoPlayerPauseButtonString;
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Text =
                new Hotkey(Properties.Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString).KeyValue;
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Tag =
                Properties.Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString;
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Text =
                new Hotkey(Properties.Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString).KeyValue;
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Tag =
                Properties.Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString;

            // Файлы субтитров
            originalSubtitlesPathEndingTextBox.Text = Properties.Settings.Default.OriginalSubtitlesFileNameEnding;
            bilingualSubtitlesPathEndingTextBox.Text = Properties.Settings.Default.BilingualSubtitlesFileNameEnding;
            //
            CreateOriginalSubtitlesFileCheckBox.Checked = Properties.Settings.Default.CreateOriginalSubtitlesFile;
            CreateBilingualSubtitlesFileCheckBox.Checked = Properties.Settings.Default.CreateBilingualSubtitlesFile;

            videoplayerProcessNameTextBox.Text = Properties.Settings.Default.VideoPlayerProcessName;

            richTextBoxForYandexApiKeyInSeparateForm.Text = Properties.Settings.Default.YandexTranslatorAPIKey;
            gotTheYandexTranslatorAPIKeyCheckBox.Checked = Properties.Settings.Default.YandexTranslatorAPIEnabled;

            checkUpdatesOnAppStartCheckBox.Checked = Properties.Settings.Default.CheckUpdates;

            if (Properties.Settings.Default.AdvancedMode)
                advancedModeRadioButton.Checked = true;
            else
                notAdvancedModeRadioButton.Checked = true;

            downloadsFolderPathRichTextBox.Text = Properties.Settings.Default.DownloadsFolder;
            defaultFolderPathRichTextBox.Text = Properties.Settings.Default.FolderToOpenFilesByDefaultFrom;

            SetFormAccordingToSubtitlesAppearanceSettings();
        }

        private void SetFormAccordingToSubtitlesAppearanceSettings()
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

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //m_buttons = new List<Button>();
            //m_buttons.Add(buttonOk);
            //m_buttons.Add(buttonCancel);

            //foreach (var btn in m_buttons)
            //{
            //    btn.FlatAppearance.BorderSize = 0;
            //    btn.FlatStyle = FlatStyle.Flat;
            //}

            //if (m_flagKeyIsInvalid)
            //    richTextBoxLabelYouNeedToGetAPIKey.Text = "Введенный ключ неверен. " +
            //                                              "Для использования данного приложения необходимо ввести валидный ключ к API сервиса "
            //                                              + '"' + "Яндекс.Переводчик" + '"' + ".";

            //richTextBoxForYandexApiKeyInSeparateForm.Text = Properties.Settings.Default.YandexTranslatorAPIKey;

            ////Синий фон у кнопок при наведении курсора
            //foreach (var btn in m_buttons)
            //{
            //    btn.MouseEnter += new EventHandler(btn_MouseEnter);
            //    btn.MouseLeave += new EventHandler(btn_MouseLeave);
            //}

            //richTextBoxForYandexApiKeyInSeparateForm.Select();

        }

        private void AddKeyToHotkeysDataGridView(string hotkeyString)
        {
            var hotkey = new Hotkey(hotkeyString);

            var rowIndex = hotkeysDataGridView.Rows.Add(hotkey.KeyValue);
            hotkeysDataGridView.Rows[rowIndex].Tag = hotkeyString;
        }

        private void AddKeyToHotkeysDataGridView(Hotkey hotkey)
        {
            var rowIndex = hotkeysDataGridView.Rows.Add(hotkey.KeyValue);
            hotkeysDataGridView.Rows[rowIndex].Tag = hotkey.ToString();
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            m_previousButtonColor = ((Button)sender).BackColor;
            ((Button)sender).BackColor = Color.Gold;

        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = m_previousButtonColor;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            bool processPriorityChangedSuccessfully;
            try
            {
                using (var p = Process.GetCurrentProcess())
                {
                    p.PriorityClass = m_processPriorityNamesAndValues[targetProcessPriorityTextBox.Text];

                }
                processPriorityChangedSuccessfully = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Смена приоритета процесса не удалась.\n\n\nОшибка:{ex}", "Смена приоритета процесса не удалась",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                processPriorityChangedSuccessfully = false;
            }
            //
            if (processPriorityChangedSuccessfully)
                Properties.Settings.Default.ProcessPriority = m_processPriorityNamesAndValues[targetProcessPriorityTextBox.Text];


            //Вычищаем символы переноса строки и пробелы из ключа
            int carrySymbols = richTextBoxForYandexApiKeyInSeparateForm.Text.Split('\n').Length - 1;
            int spaceSymbols = richTextBoxForYandexApiKeyInSeparateForm.Text.Split(' ').Length - 1;

            Properties.Settings.Default.Hotkeys = new StringCollection();
            foreach (DataGridViewRow hotkeyRow in hotkeysDataGridView.Rows)
            {
                if (hotkeyRow.Tag != null)
                    Properties.Settings.Default.Hotkeys.Add((string)hotkeyRow.Tag);
            }

            Properties.Settings.Default.VideoPlayerPauseButtonString = (string)videoPlayerPauseButtonTextBox.Tag;
            Properties.Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString =
                (string)videoPlayerChangeToBilingualSubtitlesButtonTextBox.Tag;
            Properties.Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString =
                (string)videoPlayerChangeToOriginalSubtitlesButtonTextBox.Tag;
            Properties.Settings.Default.CreateOriginalSubtitlesFile = CreateOriginalSubtitlesFileCheckBox.Checked;
            Properties.Settings.Default.CreateBilingualSubtitlesFile = CreateBilingualSubtitlesFileCheckBox.Checked;
            Properties.Settings.Default.OriginalSubtitlesFileNameEnding = originalSubtitlesPathEndingTextBox.Text;
            Properties.Settings.Default.BilingualSubtitlesFileNameEnding = bilingualSubtitlesPathEndingTextBox.Text;
            Properties.Settings.Default.ChangeRussianSubtitlesStylesAccordingToOriginal = changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;
            Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreen = secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked;
            Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreenEnabled = secondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled;

            Properties.Settings.Default.YandexTranslatorAPIKey =
                richTextBoxForYandexApiKeyInSeparateForm.Text.Substring(0,
                    richTextBoxForYandexApiKeyInSeparateForm.Text.Length - spaceSymbols - carrySymbols);

            Properties.Settings.Default.VideoPlayerProcessName = videoplayerProcessNameTextBox.Text;

            Properties.SubtitlesAppearanceSettings.Default.OriginalSubtitlesStyleString = $"{originalSubtitlesFontComboBox.Text};" +
                                                                       $"{originalSubtitlesMarginNumericUpDown.Text};" +
                                                                       $"{originalSubtitlesSizeNumericUpDown.Value};" +
                                                                       $"{originalSubtitlesOutlineNumericUpDown.Value};" +
                                                                       $"{originalSubtitlesShadowNumericUpDown.Value};" +
                                                                       $"{originalSubtitlesTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{originalSubtitlesShadowTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{(originalSubtitlesInOneLineCheckBox.Checked ? 1 : 0)}";

            Properties.SubtitlesAppearanceSettings.Default.FirstRussianSubtitlesStyleString = $"{firstRussianSubtitlesFontComboBox.Text};" +
                                                                       $"{firstRussianSubtitlesMarginNumericUpDown.Text};" +
                                                                       $"{firstRussianSubtitlesSizeNumericUpDown.Value};" +
                                                                       $"{firstRussianSubtitlesOutlineNumericUpDown.Value};" +
                                                                       $"{firstRussianSubtitlesShadowNumericUpDown.Value};" +
                                                                       $"{firstRussianSubtitlesTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{firstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{(firstRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)}";

            Properties.SubtitlesAppearanceSettings.Default.SecondRussianSubtitlesStyleString = $"{secondRussianSubtitlesFontComboBox.Text};" +
                                                                       $"{secondRussianSubtitlesMarginNumericUpDown.Text};" +
                                                                       $"{secondRussianSubtitlesSizeNumericUpDown.Value};" +
                                                                       $"{secondRussianSubtitlesOutlineNumericUpDown.Value};" +
                                                                       $"{secondRussianSubtitlesShadowNumericUpDown.Value};" +
                                                                       $"{secondRussianSubtitlesTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{secondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{(secondRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)}";

            Properties.SubtitlesAppearanceSettings.Default.ThirdRussianSubtitlesStyleString = $"{thirdRussianSubtitlesFontComboBox.Text};" +
                                                                       $"{thirdRussianSubtitlesMarginNumericUpDown.Text};" +
                                                                       $"{thirdRussianSubtitlesSizeNumericUpDown.Value};" +
                                                                       $"{thirdRussianSubtitlesOutlineNumericUpDown.Value};" +
                                                                       $"{thirdRussianSubtitlesShadowNumericUpDown.Value};" +
                                                                       $"{thirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{thirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value};" +
                                                                       $"{(thirdRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)}";

            Properties.Settings.Default.YandexTranslatorAPIEnabled = gotTheYandexTranslatorAPIKeyCheckBox.Checked;

            Properties.Settings.Default.CheckUpdates = checkUpdatesOnAppStartCheckBox.Checked;

            Properties.Settings.Default.AdvancedMode = advancedModeRadioButton.Checked;

            Properties.Settings.Default.DownloadsFolder = downloadsFolderPathRichTextBox.Text;
            Properties.Settings.Default.FolderToOpenFilesByDefaultFrom = defaultFolderPathRichTextBox.Text;

            Properties.Settings.Default.Save();
            Properties.SubtitlesAppearanceSettings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void linkLabelGetAPIKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.linkLabelGetAPIKey.LinkVisited = true;
            //System.Diagnostics.Process.Start("https://tech.yandex.ru/keys/get/?service=trnsl");
        }



        private void YandexTranslatorAPIKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void linkLabelYandexAPIKeysList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelYandexAPIKeysList.LinkVisited = true;
            MainForm.OpenUrl("https://tech.yandex.ru/keys/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hotkeySettingForm = new HotkeySettingForm(true, true);
            var dialogResult = hotkeySettingForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                // TODO Если кнопка уже установлена

                AddKeyToHotkeysDataGridView(hotkeySettingForm.SettedHotkey);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены? Все настройки будут сброшены к настройкам по умолчанию!", "",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.Reset();
                Properties.Settings.Default.Save();

                Properties.SubtitlesAppearanceSettings.Default.Reset();
                Properties.SubtitlesAppearanceSettings.Default.Save();

                Close();
            }
        }

        private void videoplayerPauseHotkeySetButton_Click(object sender, EventArgs e)
        {
            var hotkeySettingForm = new HotkeySettingForm(true);
            var dialogResult = hotkeySettingForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                videoPlayerPauseButtonTextBox.Text = hotkeySettingForm.SettedHotkey.KeyValue;
                videoPlayerPauseButtonTextBox.Tag = hotkeySettingForm.SettedHotkey.ToString();
            }

            hotkeySettingForm.Dispose();
        }

        private void videoplayerNextSubtitlesHotkeySetButton_Click(object sender, EventArgs e)
        {
            var hotkeySettingForm = new HotkeySettingForm();
            var dialogResult = hotkeySettingForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                videoPlayerChangeToBilingualSubtitlesButtonTextBox.Text = hotkeySettingForm.SettedHotkey.KeyValue;
                videoPlayerChangeToBilingualSubtitlesButtonTextBox.Tag = hotkeySettingForm.SettedHotkey.ToString();
            }

            hotkeySettingForm.Dispose();
        }

        private void videoPlayerChangeToOriginalSubtitlesHotkeySetButton_Click(object sender, EventArgs e)
        {
            var hotkeySettingForm = new HotkeySettingForm();
            var dialogResult = hotkeySettingForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                videoPlayerChangeToOriginalSubtitlesButtonTextBox.Text = hotkeySettingForm.SettedHotkey.KeyValue;
                videoPlayerChangeToOriginalSubtitlesButtonTextBox.Tag = hotkeySettingForm.SettedHotkey.ToString();
            }

            hotkeySettingForm.Dispose();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            ClearSelectedRows(hotkeysDataGridView);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Clear(hotkeysDataGridView);
        }

        public void Clear(DataGridView dataGridView)
        {
            while (dataGridView.Rows.Count > 1)
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                    dataGridView.Rows.Remove(dataGridView.Rows[i]);
        }


        public void ClearSelectedRows(DataGridView dataGridView)
        {
            //for (int i = dataGridView.Rows.Count - 1; i > 0; i--)
            //{
            //    var row = dataGridView.Rows[i];
            //    if (row.Selected)
            //        dataGridView.Rows.Remove(row);
            //}

            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                var row = dataGridView.Rows[i];
                if (row.Selected)
                {
                    dataGridView.Rows.Remove(row);
                    //i--;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string formats = "Исполняемые файлы |*.exe";

            openFileDialog.Filter = formats;
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                var videoPlayerExecutable = new FileInfo(openFileDialog.FileName);
                videoplayerProcessNameTextBox.Text = videoPlayerExecutable.Name.Substring(0,
                    videoPlayerExecutable.Name.Length - videoPlayerExecutable.Extension.Length);
            }

            openFileDialog.Dispose();
        }

        private void originalSubtitlesFontComboBox_TextChanged(object sender, EventArgs e)
        {
            //if (changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            //{
            //    firstRussianSubtitlesFontComboBox.Text = secondRussianSubtitlesFontComboBox.Text =
            //        thirdRussianSubtitlesFontComboBox.Text =
            //            originalSubtitlesFontComboBox.Text;
            //}
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

        private void originalSubtitlesTransparencyNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

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

        private void button9_Click(object sender, EventArgs e)
        {
            using var hotkeysToAuthorsExtendedDefaultsForm = new HotkeysToAuthorsExtendedDefaultsForm();
            var result = hotkeysToAuthorsExtendedDefaultsForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.Hotkeys = new StringCollection
                {
                    "Left@37@",
                    "Up@38@",
                    "Down@40@",
                    "Right@39@",
                    "NumPad0@96@",
                    "Decimal@110@",
                    "Return@13@",
                    "Add@107@",
                    "Subtract@109@",
                    "Multiply@106@",
                    "Divide@111@",
                    "F3@114@",
                    "Space@32@"
                };


                Clear(hotkeysDataGridView);
                foreach (var hotkeyString in Properties.Settings.Default.Hotkeys)
                {
                    AddKeyToHotkeysDataGridView(hotkeyString);
                }
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

        private void currentProcessPriorityTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void lastAppVersionLabel_Click(object sender, EventArgs e)
        {
            var exception = (Exception)((System.Windows.Forms.Label)sender).Tag;

            MessageBox.Show($"Не удалось получить информацию о новых версиях.\nОшибка:{exception}", "Не удалось получить информацию о новых версиях",
               MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Properties.SubtitlesAppearanceSettings.Default.Reset();
            Properties.SubtitlesAppearanceSettings.Default.Save();

            SetFormAccordingToSubtitlesAppearanceSettings();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://github.com/0xotHik/BilingualSubtitler/releases");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://t.me/bilingualsubtitler");
        }

        private void notAdvancedModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            advancedModeRadioButton.Checked = !((RadioButton)sender).Checked;
        }

        private void advancedModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            notAdvancedModeRadioButton.Checked = !((RadioButton)sender).Checked;
        }

        private void downloadsFolderPathSetButton_Click(object sender, EventArgs e)
        {
            var openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.SelectedPath = downloadsFolderPathRichTextBox.Text;
            var dialogResult = openFolderDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
                downloadsFolderPathRichTextBox.Text = openFolderDialog.SelectedPath;
        }

        private void defaultFolderPathSetButton_Click(object sender, EventArgs e)
        {
            var openFolderDialog = new FolderBrowserDialog();
            openFolderDialog.SelectedPath = defaultFolderPathRichTextBox.Text;
            var dialogResult = openFolderDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
                defaultFolderPathRichTextBox.Text = openFolderDialog.SelectedPath;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            using var formAbout = new FormAbout();
            formAbout.ShowDialog();
        }





        //public partial class SettingsForm : Form
        //{
        //    [DllImport("user32.dll")]
        //    static extern bool SetKeyboardState(byte[] lpKeyState);
        //    [DllImport("user32.dll")]
        //    static extern bool GetKeyboardState(byte[] lpKeyState);

        //    public SettingsForm()
        //    {
        //        InitializeComponent();
        //    }

        //    private void KeySettingForm_KeyPress(object sender, KeyPressEventArgs e)
        //    {
        //        var shift = new byte[256];
        //        GetKeyboardState(shift);

        //        File.WriteAllBytes("C:\\Users\\jenek\\source\\repos\\0xotHik\\" +
        //                           "BilingualSubtitler\\BilingualSubtitler\\bin\\Debug\\shift.dat", shift);

        //    }

        //    private void KeySettingForm_KeyDown(object sender, KeyEventArgs e)
        //    {
        //        var shift = new byte[256];
        //        GetKeyboardState(shift);

        //        File.WriteAllBytes("C:\\Users\\jenek\\source\\repos\\0xotHik\\" +
        //                           "BilingualSubtitler\\BilingualSubtitler\\bin\\Debug\\shiftDown.dat", shift);
        //    }
        //}
    }
}
