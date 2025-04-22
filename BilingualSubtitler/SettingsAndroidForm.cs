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

            //subtitlesAppearanceSettingsControl.OriginalSubtitlesGroupBox.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesMarginNumericUpDown.Visible =

            //subtitlesAppearanceSettingsControl.OriginalSubtitlesSizeNumericUpDown.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesOutlineNumericUpDown.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowNumericUpDown.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesTransparencyPercentageNumericUpDown.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesInOneLineCheckBox.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesBoldCheckBox.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesStrikeoutCheckBox.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesItalicCheckBox.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesUnderlineCheckBox.Visible =

            //subtitlesAppearanceSettingsControl.OriginalSubtitlesFontLabel.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesMarginLabel.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesSizeLabel.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesOutlineLabel.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowLabel.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesTransparencyPercentageLabel.Visible =
            //subtitlesAppearanceSettingsControl.OriginalSubtitlesShadowTransparencyPercentageLabel.Visible =

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
        }

        private void ResetSubtitlesAppearanceToDefaultButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            Properties.SubtitlesAppearanceSettingsForAndroid.Default.OriginalSubtitlesAndroidStyleString = GetSubtitlesStyleString(subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox,
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

        private string GetSubtitlesStyleString(ComboBox subtitlesFontComboBox,
            NumericUpDown subtitlesMarginNumericUpDown,
            NumericUpDown subtitlesSizeNumericUpDown,
            NumericUpDown subtitlesOutlineNumericUpDown,
            NumericUpDown subtitlesShadowNumericUpDown,
            NumericUpDown subtitlesTransparencyPercentageNumericUpDown,
            NumericUpDown subtitlesShadowTransparencyPercentageNumericUpDown,
            CheckBox subtitlesInOneLineCheckBox,
            CheckBox boldCheckBox,
            CheckBox italicCheckBox,
            CheckBox underlineCheckBox,
            CheckBox strikeoutCheckBox)
        {
            return $"{subtitlesFontComboBox.Text};" +
                                                           $"{subtitlesMarginNumericUpDown.Text};" +
                                                           $"{subtitlesSizeNumericUpDown.Value};" +
                                                           $"{subtitlesOutlineNumericUpDown.Value};" +
                                                           $"{subtitlesShadowNumericUpDown.Value};" +
                                                           $"{subtitlesTransparencyPercentageNumericUpDown.Value};" +
                                                           $"{subtitlesShadowTransparencyPercentageNumericUpDown.Value};" +
                                                           $"{(subtitlesInOneLineCheckBox.Checked ? 1 : 0)};" +
                                                           $"{(boldCheckBox.Checked ? 1 : 0)};" +
                                                           $"{(italicCheckBox.Checked ? 1 : 0)};" +
                                                           $"{(underlineCheckBox.Checked ? 1 : 0)};" +
                                                           $"{(strikeoutCheckBox.Checked ? 1 : 0)};";
        }
    }
}