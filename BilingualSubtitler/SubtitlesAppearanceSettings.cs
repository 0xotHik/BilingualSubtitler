using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilingualSubtitler
{
    public partial class SubtitlesAppearanceSettings : UserControl
    {
        public SubtitlesAppearanceSettings()
        {
            InitializeComponent();
        }

        private void changeRussianSubtitlesStylesAccordingToOriginalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled =
            //
            //firstRussianSubtitlesFontComboBox.Enabled = 
            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
                FirstRussianSubtitlesShadowNumericUpDown.Enabled =
                    FirstRussianSubtitlesOutlineNumericUpDown.Enabled = FirstRussianSubtitlesSizeNumericUpDown.Enabled =
                            // firstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
                            // Вторые
                            SecondRussianSubtitlesFontComboBox.Enabled =
                                SecondRussianSubtitlesMarginNumericUpDown.Enabled =
                                    SecondRussianSubtitlesShadowNumericUpDown.Enabled =
                                        SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
                                            SecondRussianSubtitlesSizeNumericUpDown.Enabled =
                                                SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
                                                SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
                                                    // Третьи
                                                    ThirdRussianSubtitlesFontComboBox.Enabled =
                                                        ThirdRussianSubtitlesMarginNumericUpDown.Enabled =
                                                            ThirdRussianSubtitlesShadowNumericUpDown.Enabled =
                                                                ThirdRussianSubtitlesOutlineNumericUpDown.Enabled =
                                                                    ThirdRussianSubtitlesSizeNumericUpDown.Enabled =
                                                                        ThirdRussianSubtitlesTransparencyPercentageNumericUpDown
                                                                                .Enabled =
                                                                                ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown
                                                                                .Enabled =
                                                                            //
                                                                            !ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;

            SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled = ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;
        }

        private void firstRussianSubtitlesFontComboBox_TextChanged(object sender, EventArgs e)
        {
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                SecondRussianSubtitlesFontComboBox.Text =
                    ThirdRussianSubtitlesFontComboBox.Text =
                        FirstRussianSubtitlesFontComboBox.Text;
            }
        }

        private void ChangeMargin()
        {
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                if (SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled && SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked)
                {
                    var firstRussianSubtitlesMargin = OriginalSubtitlesMarginNumericUpDown.Value -
                                                      (2 * OriginalSubtitlesSizeNumericUpDown.Value +
                                                       OriginalSubtitlesSizeNumericUpDown.Value / 10);
                    FirstRussianSubtitlesMarginNumericUpDown.Value =
                        firstRussianSubtitlesMargin < 0 ? 0 : firstRussianSubtitlesMargin;

                    ThirdRussianSubtitlesMarginNumericUpDown.Value = 290 - (2 * OriginalSubtitlesSizeNumericUpDown.Value +
                                                       OriginalSubtitlesSizeNumericUpDown.Value / 10);
                    SecondRussianSubtitlesMarginNumericUpDown.Value = ThirdRussianSubtitlesMarginNumericUpDown.Value -
                        (2 * OriginalSubtitlesSizeNumericUpDown.Value +
                                                       OriginalSubtitlesSizeNumericUpDown.Value / 10);
                }
                else
                {
                    FirstRussianSubtitlesMarginNumericUpDown.Value =
                        OriginalSubtitlesMarginNumericUpDown.Value +
                        (2 * OriginalSubtitlesSizeNumericUpDown.Value +
                         OriginalSubtitlesSizeNumericUpDown.Value / 10);
                    SecondRussianSubtitlesMarginNumericUpDown.Value = OriginalSubtitlesMarginNumericUpDown.Value +
                                                                      (2 * OriginalSubtitlesSizeNumericUpDown.Value +
                                                                       OriginalSubtitlesSizeNumericUpDown.Value / 10) * 2;

                    var thirdRussianSubtitlesMargin = OriginalSubtitlesMarginNumericUpDown.Value -
                                                      (2 * OriginalSubtitlesSizeNumericUpDown.Value +
                                                       OriginalSubtitlesSizeNumericUpDown.Value / 10);
                    ThirdRussianSubtitlesMarginNumericUpDown.Value =
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
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                FirstRussianSubtitlesSizeNumericUpDown.Value =
                    SecondRussianSubtitlesSizeNumericUpDown.Value =
                        ThirdRussianSubtitlesSizeNumericUpDown.Value =
                            OriginalSubtitlesSizeNumericUpDown.Value;
            }

            ChangeMargin();
        }

        private void originalSubtitlesOutlineNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                FirstRussianSubtitlesOutlineNumericUpDown.Value =
                    SecondRussianSubtitlesOutlineNumericUpDown.Value =
                       ThirdRussianSubtitlesOutlineNumericUpDown.Value =
                           OriginalSubtitlesOutlineNumericUpDown.Value;
            }
        }

        private void originalSubtitlesShadowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                FirstRussianSubtitlesShadowNumericUpDown.Value =
                    SecondRussianSubtitlesShadowNumericUpDown.Value =
                       ThirdRussianSubtitlesShadowNumericUpDown.Value =
                           OriginalSubtitlesShadowNumericUpDown.Value;
            }
        }

        private void originalSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                    FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
            }
        }

        private void firstRussianSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                    FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            }
        }

        private void FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            {
                SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                    FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            }
        }
    }
}
