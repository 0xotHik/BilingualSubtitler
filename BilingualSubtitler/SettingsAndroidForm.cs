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

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void YandexTranslatorAPIKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void subtitlesAppearanceSettingsControl_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
            Properties.SubtitlesAppearanceSettings.Default.OriginalSubtitlesStyleString = GetSubtitlesStyleString(subtitlesAppearanceSettingsControl.OriginalSubtitlesFontComboBox,
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