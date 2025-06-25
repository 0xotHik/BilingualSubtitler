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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BilingualSubtitler.MainForm;

namespace BilingualSubtitler
{

    public partial class SettingsAndroidForm : Form
    {
        private int m_inititalCheckUpdatesGroupBoxLeft;
        private int m_inititalDownloadsDirectoryGroupBox;

        private Dictionary<string, ProcessPriorityClass> m_processPriorityNamesAndValues;
        private Dictionary<ProcessPriorityClass, string> m_processPriorityValuesAndNames;

        private List<Button> m_buttons;
        private Color m_previousButtonColor;
        private bool m_flagKeyIsInvalid = false;

        private FontFamily[] m_installedFontFamilies;

        public bool SettingsWasSaved = false;

        public SettingsAndroidForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;
            subtitlesAppearanceSettingsControl.ResetSubtitlesAppearanceToDefaultButton.Click += ResetSubtitlesAppearanceToDefaultButton_Click;

            // Проще отключить все контролы, и потом вернуть нужные, нежели отключать руками все ненужные
            foreach (var control in GetAllChildControls(subtitlesAppearanceSettingsControl))
            {
                control.Visible = false;
            }

            // originalSubtitlesPathEndingTextBox.Text = Properties.Settings.Default.OriginalSubtitlesFileNameEnding;
            srtPackPathEndingTextBox.Text = Properties.SettingsForAndroid.Default.AndroidSrtPackOrSeparateStreamsFileNameEnding;

            subtitlesAppearanceSettingsControl.Visible =
            subtitlesAppearanceSettingsControl.SubtitlesAppearanceSettingsGroupBox.Visible =
            subtitlesAppearanceSettingsControl.OriginalSubtitlesGroupBox.Visible =

            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesBoldCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.SecondRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesBoldCheckBox.Visible =
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesBoldCheckBox.Visible =
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.FourthRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesBoldCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.FifthRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesBoldCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesUnderlineCheckBox.Visible =

            subtitlesAppearanceSettingsControl.ResetSubtitlesAppearanceToDefaultButton.Visible =

            okButton.Visible = cancelButton.Visible =

            true;

            foreach (var control in GetAllChildControls(subtitlesAppearanceSettingsControl.OriginalSubtitlesGroupBox))
            {
                control.Visible = true;
            }

            SetFormAccordingToSubtitlesAppearanceSettings();

            if (Properties.SettingsForAndroid.Default.AndroidCreateSrtPack)
                createSrtPackRadioButton.Checked = true;
            else
                createSrtSeparateSteamsRadioButton.Checked = true;
        }

        private void SetFormAccordingToSubtitlesAppearanceSettings()
        {
            SubtitlesAppearanceSettings.SetControlsValuesAccordingToStyleStringSplitted(Properties.SubtitlesAppearanceSettingsForAndroid.Default.OriginalSubtitlesAndroidStyleString.Split(";"),
                    subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesMarginNumericUpDown,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesSizeNumericUpDown,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesOutlineNumericUpDown,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowNumericUpDown,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesTransparencyPercentageNumericUpDown,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowTransparencyPercentageNumericUpDown,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesInOneLineCheckBox,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesBoldCheckBox,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesItalicCheckBox,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesUnderlineCheckBox,
                subtitlesAppearanceSettingsControl.OriginalSubtitlesStrikeoutCheckBox);

            var currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.FirstRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesBoldCheckBox.Checked = currentSubsStrStyleSplitted.Length > 8 ? currentSubsStrStyleSplitted[8] == "1" : false;
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.SecondRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesBoldCheckBox.Checked = currentSubsStrStyleSplitted.Length > 8 ? currentSubsStrStyleSplitted[8] == "1" : false;
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.ThirdRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesBoldCheckBox.Checked = currentSubsStrStyleSplitted.Length > 8 ? currentSubsStrStyleSplitted[8] == "1" : false;
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.FourthRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesBoldCheckBox.Checked = currentSubsStrStyleSplitted.Length > 8 ? currentSubsStrStyleSplitted[8] == "1" : false;
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.FifthRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesBoldCheckBox.Checked = currentSubsStrStyleSplitted.Length > 8 ? currentSubsStrStyleSplitted[8] == "1" : false;
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;

        }

        private void ResetSubtitlesAppearanceToDefaultButton_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Сбросить настройки вида субтитров для Android к значениям по умолчанию?", string.Empty, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                Properties.SubtitlesAppearanceSettingsForAndroid.Default.Reset();
                Properties.SubtitlesAppearanceSettingsForAndroid.Default.Save();

                SetFormAccordingToSubtitlesAppearanceSettings();
            }


        }

        public static List<Control> GetAllChildControlsViaReflection(Control control)
        {
            List<Control> result = new List<Control>();

            // Используем рефлексию для получения свойства "Controls"
            PropertyInfo controlsProp = control.GetType().GetProperty("Controls");
            if (controlsProp != null)
            {
                object controlsObj = controlsProp.GetValue(control);
                if (controlsObj is ControlCollection controls)
                {
                    foreach (Control child in controls)
                    {
                        result.Add(child);
                        result.AddRange(GetAllChildControlsViaReflection(child)); // Рекурсивно получаем все дочерние контролы
                    }
                }
            }

            return result;
        }

        public static List<Control> GetAllChildControls(Control parent)
        {
            List<Control> result = new List<Control>();
            foreach (Control c in parent.Controls)
            {
                result.Add(c);
                result.AddRange(GetAllChildControls(c));
            }
            return result;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
        }

        private void YandexTranslatorAPIKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void subtitlesAppearanceSettingsControl_Load(object sender, EventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.OriginalSubtitlesAndroidStyleString = SettingsForm.GetSubtitlesStyleString(subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesMarginNumericUpDown,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesSizeNumericUpDown,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesOutlineNumericUpDown,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowNumericUpDown,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesTransparencyPercentageNumericUpDown,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowTransparencyPercentageNumericUpDown,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesInOneLineCheckBox,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesBoldCheckBox,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesItalicCheckBox,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesUnderlineCheckBox,
                                                                       subtitlesAppearanceSettingsControl.OriginalSubtitlesStrikeoutCheckBox);

            // 1
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.FirstRussianSubtitlesAndroidStyleString = "null;null;null;null;null;null;null;" +
                                                           $"{(subtitlesAppearanceSettingsControl.FirstRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)};" + "null;" +
                                                           $"{(subtitlesAppearanceSettingsControl.FirstRussianSubtitlesBoldCheckBox.Checked ? 1 : 0)};" +
                                                           $"{(subtitlesAppearanceSettingsControl.FirstRussianSubtitlesItalicCheckBox.Checked ? 1 : 0)};" +
                                                           $"{(subtitlesAppearanceSettingsControl.FirstRussianSubtitlesUnderlineCheckBox.Checked ? 1 : 0)};" + "null;";
            // 2
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.SecondRussianSubtitlesAndroidStyleString = "null;null;null;null;null;null;null;" +
                                               $"{(subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)};" + "null;" +
                                               $"{(subtitlesAppearanceSettingsControl.SecondRussianSubtitlesBoldCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.SecondRussianSubtitlesItalicCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.SecondRussianSubtitlesUnderlineCheckBox.Checked ? 1 : 0)};" + "null;";
            // 3
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.ThirdRussianSubtitlesAndroidStyleString = "null;null;null;null;null;null;null;" +
                                               $"{(subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)};" + "null;" +
                                               $"{(subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesBoldCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesItalicCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesUnderlineCheckBox.Checked ? 1 : 0)};" + "null;";
            // 4
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.FourthRussianSubtitlesAndroidStyleString = "null;null;null;null;null;null;null;" +
                                               $"{(subtitlesAppearanceSettingsControl.FourthRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)};" + "null;" +
                                               $"{(subtitlesAppearanceSettingsControl.FourthRussianSubtitlesBoldCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.FourthRussianSubtitlesItalicCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.FourthRussianSubtitlesUnderlineCheckBox.Checked ? 1 : 0)};" + "null;";
            // 5
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.FifthRussianSubtitlesAndroidStyleString = "null;null;null;null;null;null;null;" +
                                               $"{(subtitlesAppearanceSettingsControl.FifthRussianSubtitlesInOneLineCheckBox.Checked ? 1 : 0)};" + "null;" +
                                               $"{(subtitlesAppearanceSettingsControl.FifthRussianSubtitlesBoldCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.FifthRussianSubtitlesItalicCheckBox.Checked ? 1 : 0)};" +
                                               $"{(subtitlesAppearanceSettingsControl.FifthRussianSubtitlesUnderlineCheckBox.Checked ? 1 : 0)};" + "null;";
            //
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.Save();

            Properties.SettingsForAndroid.Default.AndroidCreateSrtPack = createSrtPackRadioButton.Checked;
            //
            // Properties.Settings.Default.OriginalSubtitlesFileNameEnding = originalSubtitlesPathEndingTextBox.Text;
            Properties.SettingsForAndroid.Default.AndroidSrtPackOrSeparateStreamsFileNameEnding = srtPackPathEndingTextBox.Text;
            Properties.SettingsForAndroid.Default.Save();
            //
            SettingsWasSaved = true;

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void createSrtPackRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            createSrtSeparateSteamsRadioButton.Checked = !((RadioButton)sender).Checked;
        }

        private void createSrtSeparateSteamsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            createSrtPackRadioButton.Checked = !((RadioButton)sender).Checked;
        }

        private void SettingsAndroidForm_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены? Все настройки для Android будут сброшены к значениям по умолчанию!", "",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {
                Properties.SettingsForAndroid.Default.Reset();
                Properties.SubtitlesAppearanceSettingsForAndroid.Default.Reset();
                //
                Properties.SettingsForAndroid.Default.Save();
                Properties.SubtitlesAppearanceSettingsForAndroid.Default.Save();

                DialogResult = DialogResult.OK;
                SettingsWasSaved = true;

                Close();
            }
        }
    }
}