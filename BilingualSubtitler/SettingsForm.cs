using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilingualSubtitler
{
    public partial class SettingsForm : Form
    {
        private List<Button> m_buttons;
        private Color m_previousButtonColor;
        private bool m_flagKeyIsInvalid = false;

        public SettingsForm()
        {
            InitializeComponent();

            //m_flagKeyIsInvalid = true;

            foreach (var hotkeyString in Properties.Settings.Default.Hotkeys)
            {
                AddKeyToHotkeysDataGridView(hotkeyString);
            }

            videoPlayerPauseButtonTextBox.Text =
                new Hotkey(Properties.Settings.Default.VideoPlayerPauseButtonString).KeyValue;
            videoPlayerPauseButtonTextBox.Tag = Properties.Settings.Default.VideoPlayerPauseButtonString;
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Text =
                new Hotkey(Properties.Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString).ToString();
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Tag =
                Properties.Settings.Default.VideoPlayerChangeToBilingualSubtitlesHotkeyString;
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Text =
                new Hotkey(Properties.Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString).ToString();
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Tag =
                Properties.Settings.Default.VideoPlayerChangeToOriginalSubtitlesHotkeyString;

            originalSubtitlesPathEndingTextBox.Text = Properties.Settings.Default.OriginalSubtitlesFileNameEnding;
            bilingualSubtitlesPathEndingTextBox.Text = Properties.Settings.Default.BilingualSubtitlesFileNameEnding;

            CreateOriginalSubtitlesFileCheckBox.Checked = Properties.Settings.Default.CreateOriginalSubtitlesFile;

            videoplayerProcessNameTextBox.Text = Properties.Settings.Default.VideoPlayerProcessName;

            changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked =
                Properties.Settings.Default.ChangeRussianSubtitlesStylesAccordingToOriginal;

        }

        private void changeRussianSubtitlesStylesAccordingToOriginalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            firstRussianSubtitlesFontComboBox.Enabled = firstRussianSubtitlesMarginNumericUpDown.Enabled =
                firstRussianSubtitlesShadowNumericUpDown.Enabled =
                    firstRussianSubtitlesOutlineNumericUpDown.Enabled = firstRussianSubtitlesSizeNumericUpDown.Enabled =
                        firstRussianSubtitlesTransparencyNumericUpDown.Enabled =
                            // Вторые
                            secondRussianSubtitlesFontComboBox.Enabled =
                                secondRussianSubtitlesMarginNumericUpDown.Enabled =
                                    secondRussianSubtitlesShadowNumericUpDown.Enabled =
                                        secondRussianSubtitlesOutlineNumericUpDown.Enabled =
                                            secondRussianSubtitlesSizeNumericUpDown.Enabled =
                                                secondRussianSubtitlesTransparencyNumericUpDown.Enabled =
                                                    // Третьи
                                                    thirdRussianSubtitlesFontComboBox.Enabled =
                                                        thirdRussianSubtitlesMarginNumericUpDown.Enabled =
                                                            thirdRussianSubtitlesShadowNumericUpDown.Enabled =
                                                                thirdRussianSubtitlesOutlineNumericUpDown.Enabled =
                                                                    thirdRussianSubtitlesSizeNumericUpDown.Enabled =
                                                                        thirdRussianSubtitlesTransparencyNumericUpDown
                                                                                .Enabled =
                                                                            //
                                                                            !changeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            m_buttons = new List<Button>();
            m_buttons.Add(buttonOk);
            m_buttons.Add(buttonCancel);

            foreach (var btn in m_buttons)
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
            }

            if (m_flagKeyIsInvalid)
                richTextBoxLabelYouNeedToGetAPIKey.Text = "Введенный ключ неверен. " +
                                                          "Для использования данного приложения необходимо ввести валидный ключ к API сервиса "
                                                          + '"' + "Яндекс.Переводчик" + '"' + ".";

            richTextBoxForYandexApiKeyInSeparateForm.Text = Properties.Settings.Default.YandexTranslatorAPIKey;

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
            Properties.Settings.Default.OriginalSubtitlesFileNameEnding = originalSubtitlesPathEndingTextBox.Text;
            Properties.Settings.Default.BilingualSubtitlesFileNameEnding = bilingualSubtitlesPathEndingTextBox.Text;

            Properties.Settings.Default.YandexTranslatorAPIKey =
                richTextBoxForYandexApiKeyInSeparateForm.Text.Substring(0,
                    richTextBoxForYandexApiKeyInSeparateForm.Text.Length - spaceSymbols - carrySymbols);

            Properties.Settings.Default.OriginalSubtitlesStyleString = $"{originalSubtitlesFontComboBox};" +
                                                                       $"{originalSubtitlesMarginNumericUpDown};" +
                                                                       $"{originalSubtitlesSizeNumericUpDown};" +
                                                                       $"{originalSubtitlesOutlineNumericUpDown};" +
                                                                       $"{originalSubtitlesShadowNumericUpDown};" +
                                                                       $"{originalSubtitlesTransparencyNumericUpDown}";

            Properties.Settings.Default.FirstRussianSubtitlesStyleString = $"{firstRussianSubtitlesFontComboBox};" +
                                                                       $"{firstRussianSubtitlesMarginNumericUpDown};" +
                                                                       $"{firstRussianSubtitlesSizeNumericUpDown};" +
                                                                       $"{firstRussianSubtitlesOutlineNumericUpDown};" +
                                                                       $"{firstRussianSubtitlesShadowNumericUpDown};" +
                                                                       $"{firstRussianSubtitlesTransparencyNumericUpDown}";

            Properties.Settings.Default.SecondRussianSubtitlesStyleString = $"{secondRussianSubtitlesFontComboBox};" +
                                                                       $"{secondRussianSubtitlesMarginNumericUpDown};" +
                                                                       $"{secondRussianSubtitlesSizeNumericUpDown};" +
                                                                       $"{secondRussianSubtitlesOutlineNumericUpDown};" +
                                                                       $"{secondRussianSubtitlesShadowNumericUpDown};" +
                                                                       $"{secondRussianSubtitlesTransparencyNumericUpDown}";

            Properties.Settings.Default.ThirdRussianSubtitlesStyleString = $"{thirdRussianSubtitlesFontComboBox};" +
                                                                       $"{thirdRussianSubtitlesMarginNumericUpDown};" +
                                                                       $"{thirdRussianSubtitlesSizeNumericUpDown};" +
                                                                       $"{thirdRussianSubtitlesOutlineNumericUpDown};" +
                                                                       $"{thirdRussianSubtitlesShadowNumericUpDown};" +
                                                                       $"{thirdRussianSubtitlesTransparencyNumericUpDown}";

            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void linkLabelGetAPIKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelGetAPIKey.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tech.yandex.ru/keys/get/?service=trnsl");
        }



        private void YandexTranslatorAPIKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void linkLabelYandexAPIKeysList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelYandexAPIKeysList.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tech.yandex.ru/keys/");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var hotkeySettingForm = new HotkeySettingForm();
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
                Close();
            }
        }

        private void videoplayerPauseHotkeySetButton_Click(object sender, EventArgs e)
        {
            var hotkeySettingForm = new HotkeySettingForm();
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
            firstRussianSubtitlesFontComboBox.Text = secondRussianSubtitlesFontComboBox.Text =
                thirdRussianSubtitlesFontComboBox.Text =
                    secondRussianSubtitlesFontComboBox.Text;
        }

        private void originalSubtitlesMarginNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            firstRussianSubtitlesMarginNumericUpDown.Value =
                secondRussianSubtitlesMarginNumericUpDown.Value =
                thirdRussianSubtitlesMarginNumericUpDown.Value =
                    originalSubtitlesMarginNumericUpDown.Value;

        }

        private void originalSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            firstRussianSubtitlesSizeNumericUpDown.Value =
                            secondRussianSubtitlesSizeNumericUpDown.Value =
                            thirdRussianSubtitlesSizeNumericUpDown.Value =
                                originalSubtitlesSizeNumericUpDown.Value;
        }

        private void originalSubtitlesOutlineNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            firstRussianSubtitlesOutlineNumericUpDown.Value =
                            secondRussianSubtitlesOutlineNumericUpDown.Value =
                            thirdRussianSubtitlesOutlineNumericUpDown.Value =
                                originalSubtitlesOutlineNumericUpDown.Value;
        }

        private void originalSubtitlesShadowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            firstRussianSubtitlesShadowNumericUpDown.Value =
                            secondRussianSubtitlesShadowNumericUpDown.Value =
                            thirdRussianSubtitlesShadowNumericUpDown.Value =
                                originalSubtitlesShadowNumericUpDown.Value;
        }

        private void originalSubtitlesTransparencyNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            firstRussianSubtitlesTransparencyNumericUpDown.Value =
                            secondRussianSubtitlesTransparencyNumericUpDown.Value =
                            thirdRussianSubtitlesTransparencyNumericUpDown.Value =
                                originalSubtitlesTransparencyNumericUpDown.Value;
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
