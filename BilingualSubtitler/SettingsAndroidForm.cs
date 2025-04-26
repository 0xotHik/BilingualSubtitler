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

        public SettingsAndroidForm(MainForm mainForm)
        {
            InitializeComponent();
            subtitlesAppearanceSettingsControl.ResetSubtitlesAppearanceToDefaultButton.Click += ResetSubtitlesAppearanceToDefaultButton_Click;

            // Проще отключить все контролы, и потом вернуть нужные, нежели отключать руками все ненужные
            foreach (var control in GetAllChildControls(subtitlesAppearanceSettingsControl))
            {
                control.Visible = false;
            }

            subtitlesAppearanceSettingsControl.Visible =
            subtitlesAppearanceSettingsControl.SubtitlesAppearanceSettingsGroupBox.Visible =
            subtitlesAppearanceSettingsControl.OriginalSubtitlesGroupBox.Visible =

            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.SecondRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.FourthRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesInOneLineCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesItalicCheckBox.Visible =
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesUnderlineCheckBox.Visible =

                        subtitlesAppearanceSettingsControl.FifthRussianSubtitlesGroupBox.Visible =
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesInOneLineCheckBox.Visible =
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
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.FirstRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.SecondRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.SecondRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.ThirdRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.ThirdRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.FourthRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesItalicCheckBox.Checked = currentSubsStrStyleSplitted.Length > 9 ? currentSubsStrStyleSplitted[9] == "1" : false;
            subtitlesAppearanceSettingsControl.FourthRussianSubtitlesUnderlineCheckBox.Checked = currentSubsStrStyleSplitted.Length > 10 ? currentSubsStrStyleSplitted[10] == "1" : false;
            //
            currentSubsStrStyleSplitted = Properties.SubtitlesAppearanceSettingsForAndroid.Default.FifthRussianSubtitlesAndroidStyleString.Split(";");
            subtitlesAppearanceSettingsControl.FifthRussianSubtitlesInOneLineCheckBox.Checked = currentSubsStrStyleSplitted[7] == "1";
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
            throw new NotImplementedException();
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

            Properties.SubtitlesAppearanceSettingsForAndroid.Default.Save();
        }

    }
}