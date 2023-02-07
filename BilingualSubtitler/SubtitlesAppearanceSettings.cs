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



            // В тэге — храним старое значение, перед изменением
            //
            OriginalSubtitlesMarginNumericUpDown.Tag = OriginalSubtitlesMarginNumericUpDown.Value;
            OriginalSubtitlesSizeNumericUpDown.Tag = OriginalSubtitlesSizeNumericUpDown.Value;
            OriginalSubtitlesOutlineNumericUpDown.Tag = OriginalSubtitlesOutlineNumericUpDown.Value;
            OriginalSubtitlesShadowNumericUpDown.Tag = OriginalSubtitlesShadowNumericUpDown.Value;
            OriginalSubtitlesTransparencyPercentageNumericUpDown.Tag = OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
            OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            //
            OriginalSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            OriginalSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            OriginalSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            OriginalSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            OriginalSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

            FirstRussianSubtitlesMarginNumericUpDown.Tag = FirstRussianSubtitlesMarginNumericUpDown.Value;
            FirstRussianSubtitlesSizeNumericUpDown.Tag = FirstRussianSubtitlesSizeNumericUpDown.Value;
            FirstRussianSubtitlesOutlineNumericUpDown.Tag = FirstRussianSubtitlesOutlineNumericUpDown.Value;
            FirstRussianSubtitlesShadowNumericUpDown.Tag = FirstRussianSubtitlesShadowNumericUpDown.Value;
            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            //
            FirstRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FirstRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FirstRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FirstRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

            SecondRussianSubtitlesMarginNumericUpDown.Tag = SecondRussianSubtitlesMarginNumericUpDown.Value;
            SecondRussianSubtitlesSizeNumericUpDown.Tag = SecondRussianSubtitlesSizeNumericUpDown.Value;
            SecondRussianSubtitlesOutlineNumericUpDown.Tag = SecondRussianSubtitlesOutlineNumericUpDown.Value;
            SecondRussianSubtitlesShadowNumericUpDown.Tag = SecondRussianSubtitlesShadowNumericUpDown.Value;
            SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
            SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            //
            SecondRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            SecondRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            SecondRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            SecondRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            SecondRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

            ThirdRussianSubtitlesMarginNumericUpDown.Tag = ThirdRussianSubtitlesMarginNumericUpDown.Value;
            ThirdRussianSubtitlesSizeNumericUpDown.Tag = ThirdRussianSubtitlesSizeNumericUpDown.Value;
            ThirdRussianSubtitlesOutlineNumericUpDown.Tag = ThirdRussianSubtitlesOutlineNumericUpDown.Value;
            ThirdRussianSubtitlesShadowNumericUpDown.Tag = ThirdRussianSubtitlesShadowNumericUpDown.Value;
            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
            ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            //
            ThirdRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            ThirdRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            ThirdRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            ThirdRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;


            SetAccordingToPropertiesSettings();
        }

        public void SetAccordingToPropertiesSettings()
        {
            changeMarginsToPairSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ChangeMarginsToPairSubtitles;
            changeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.СhangeOnTheSameDeltaValuesForAllSubtitles;
            setTheSameValuesForAllSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SetTheSameValuesForAllSubtitles;
            //
            marginCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.MarginCheckBoxChecked;
            sizeCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SizeCheckBoxChecked;
            outlineCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.OutlineCheckBoxChecked;
            shadowCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ShadowCheckBoxChecked;
            transparencyCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.TransparencyCheckBoxChecked;
            shadowTransparencyCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ShadowTransparencyCheckBoxChecked;

            var originalSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.OriginalSubtitlesStyleString.Split(';');
            foreach (var fontItem in OriginalSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == originalSubtitlesStyle[0])
                {
                    OriginalSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(OriginalSubtitlesFontComboBox.Text))
                OriginalSubtitlesFontComboBox.Text = originalSubtitlesStyle[0];
            OriginalSubtitlesMarginNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[1]);
            OriginalSubtitlesSizeNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[2]);
            OriginalSubtitlesOutlineNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[3]);
            OriginalSubtitlesShadowNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[4]);
            OriginalSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[5]);
            OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(originalSubtitlesStyle[6]);
            OriginalSubtitlesInOneLineCheckBox.Checked = originalSubtitlesStyle[7] == "1";

            var firstRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.FirstRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in FirstRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == firstRussianSubtitlesStyle[0])
                {
                    FirstRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(FirstRussianSubtitlesFontComboBox.Text))
                FirstRussianSubtitlesFontComboBox.Text = firstRussianSubtitlesStyle[0];
            FirstRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[1]);
            FirstRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[2]);
            FirstRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[3]);
            FirstRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[4]);
            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[5]);
            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(firstRussianSubtitlesStyle[6]);
            FirstRussianSubtitlesInOneLineCheckBox.Checked = firstRussianSubtitlesStyle[7] == "1";

            var secondRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.SecondRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in SecondRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == secondRussianSubtitlesStyle[0])
                {
                    SecondRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(SecondRussianSubtitlesFontComboBox.Text))
                SecondRussianSubtitlesFontComboBox.Text = secondRussianSubtitlesStyle[0];
            SecondRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[1]);
            SecondRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[2]);
            SecondRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[3]);
            SecondRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[4]);
            SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[5]);
            SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(secondRussianSubtitlesStyle[6]);
            SecondRussianSubtitlesInOneLineCheckBox.Checked = secondRussianSubtitlesStyle[7] == "1";

            var thirdRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.ThirdRussianSubtitlesStyleString.Split(';');
            foreach (var fontItem in ThirdRussianSubtitlesFontComboBox.Items)
            {
                if ((string)fontItem == thirdRussianSubtitlesStyle[0])
                {
                    ThirdRussianSubtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(ThirdRussianSubtitlesFontComboBox.Text))
                ThirdRussianSubtitlesFontComboBox.Text = thirdRussianSubtitlesStyle[0];
            ThirdRussianSubtitlesMarginNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[1]);
            ThirdRussianSubtitlesSizeNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[2]);
            ThirdRussianSubtitlesOutlineNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[3]);
            ThirdRussianSubtitlesShadowNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[4]);
            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[5]);
            ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(thirdRussianSubtitlesStyle[6]);
            ThirdRussianSubtitlesInOneLineCheckBox.Checked = thirdRussianSubtitlesStyle[7] == "1";

            SetControlAccordingToCheckBoxes();
        }

        private void NumericUpDownValueChanged(object sender, EventArgs e)
        {
            var numericUpDownSender = (NumericUpDown)sender;
            //var oldValue = (decimal)numericUpDownSender.Tag;

            var newValue = numericUpDownSender.Value;
            numericUpDownSender.Tag = newValue;
        }

        private void changeRussianSubtitlesStylesAccordingToOriginalCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // TODO v11
            //SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled =
            ////
            ////firstRussianSubtitlesFontComboBox.Enabled = 
            //FirstRussianSubtitlesMarginNumericUpDown.Enabled =
            //    FirstRussianSubtitlesShadowNumericUpDown.Enabled =
            //        FirstRussianSubtitlesOutlineNumericUpDown.Enabled = FirstRussianSubtitlesSizeNumericUpDown.Enabled =
            //                // firstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
            //                // Вторые
            //                SecondRussianSubtitlesFontComboBox.Enabled =
            //                    SecondRussianSubtitlesMarginNumericUpDown.Enabled =
            //                        SecondRussianSubtitlesShadowNumericUpDown.Enabled =
            //                            SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
            //                                SecondRussianSubtitlesSizeNumericUpDown.Enabled =
            //                                    SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
            //                                    SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
            //                                        // Третьи
            //                                        ThirdRussianSubtitlesFontComboBox.Enabled =
            //                                            ThirdRussianSubtitlesMarginNumericUpDown.Enabled =
            //                                                ThirdRussianSubtitlesShadowNumericUpDown.Enabled =
            //                                                    ThirdRussianSubtitlesOutlineNumericUpDown.Enabled =
            //                                                        ThirdRussianSubtitlesSizeNumericUpDown.Enabled =
            //                                                            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown
            //                                                                    .Enabled =
            //                                                                    ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown
            //                                                                    .Enabled =
            //                                                                //
            //                                                                !ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;

            //SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled = ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;
        }

        private void firstRussianSubtitlesFontComboBox_TextChanged(object sender, EventArgs e)
        {
            // TODO v11
            //if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            //{
            //    SecondRussianSubtitlesFontComboBox.Text =
            //        ThirdRussianSubtitlesFontComboBox.Text =
            //            FirstRussianSubtitlesFontComboBox.Text;
            //}
        }

        private void ChangeMargin()
        {
            //    if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
            //    {
            //        if (SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled && SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked)
            //        {
            //            var firstRussianSubtitlesMargin = OriginalSubtitlesMarginNumericUpDown.Value -
            //                                              (2 * OriginalSubtitlesSizeNumericUpDown.Value +
            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
            //            FirstRussianSubtitlesMarginNumericUpDown.Value =
            //                firstRussianSubtitlesMargin < 0 ? 0 : firstRussianSubtitlesMargin;

            //            ThirdRussianSubtitlesMarginNumericUpDown.Value = 290 - (2 * OriginalSubtitlesSizeNumericUpDown.Value +
            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
            //            SecondRussianSubtitlesMarginNumericUpDown.Value = ThirdRussianSubtitlesMarginNumericUpDown.Value -
            //                (2 * OriginalSubtitlesSizeNumericUpDown.Value +
            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
            //        }
            //        else
            //        {
            //            FirstRussianSubtitlesMarginNumericUpDown.Value =
            //                OriginalSubtitlesMarginNumericUpDown.Value +
            //                (2 * OriginalSubtitlesSizeNumericUpDown.Value +
            //                 OriginalSubtitlesSizeNumericUpDown.Value / 10);
            //            SecondRussianSubtitlesMarginNumericUpDown.Value = OriginalSubtitlesMarginNumericUpDown.Value +
            //                                                              (2 * OriginalSubtitlesSizeNumericUpDown.Value +
            //                                                               OriginalSubtitlesSizeNumericUpDown.Value / 10) * 2;

            //            var thirdRussianSubtitlesMargin = OriginalSubtitlesMarginNumericUpDown.Value -
            //                                              (2 * OriginalSubtitlesSizeNumericUpDown.Value +
            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
            //            ThirdRussianSubtitlesMarginNumericUpDown.Value =
            //                thirdRussianSubtitlesMargin < 0 ? 0 : thirdRussianSubtitlesMargin;
            //        }

            //    }
        }

        private void SetValueToNumericUpDownFromCodeSafe(NumericUpDown targetNumericUpDown, decimal value)
        {
            // Руками надо чекать, да, падало
            if (value >= targetNumericUpDown.Minimum && value <= targetNumericUpDown.Maximum)
            {
                targetNumericUpDown.Value = value;
            }
            else if (value < targetNumericUpDown.Minimum)
                targetNumericUpDown.Value = targetNumericUpDown.Minimum;
            else if (value > targetNumericUpDown.Maximum)
                targetNumericUpDown.Value = targetNumericUpDown.Maximum;
        }

        private void AddValueToNumericUpDownSafe(NumericUpDown targetNumericUpDown, decimal value)
        {
            SetValueToNumericUpDownFromCodeSafe(targetNumericUpDown, 
                (decimal)targetNumericUpDown.Tag // В тэге у нас должно лежать текущее, до прибавления, значение
                + value);
        }

        private void originalSubtitlesMarginNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EnabledAndChecked(marginCheckBox))
            {
                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesMarginNumericUpDown.Value =
                        SecondRussianSubtitlesMarginNumericUpDown.Value =
                        ThirdRussianSubtitlesMarginNumericUpDown.Value =
                        OriginalSubtitlesMarginNumericUpDown.Value;
                }
                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesMarginNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesMarginNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesMarginNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesMarginNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesMarginNumericUpDown, delta);
                }
            }

            // Если включен "Перемещать попарно" — двигаем еще 1-е русские
            if (EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(FirstRussianSubtitlesMarginNumericUpDown, OriginalSubtitlesMarginNumericUpDown.Value + (2 * OriginalSubtitlesSizeNumericUpDown.Value) + 2);

                // И 3-и русские
                SetValueToNumericUpDownFromCodeSafe(ThirdRussianSubtitlesMarginNumericUpDown, SecondRussianSubtitlesMarginNumericUpDown.Value + (2 * SecondRussianSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }

        private void originalSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EnabledAndChecked(sizeCheckBox))
            {
                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesSizeNumericUpDown.Value =
                        SecondRussianSubtitlesSizeNumericUpDown.Value =
                        ThirdRussianSubtitlesSizeNumericUpDown.Value =
                        OriginalSubtitlesSizeNumericUpDown.Value;
                }
                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesSizeNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesSizeNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesSizeNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesSizeNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesSizeNumericUpDown, delta);
                }
            }

            // Если включен "Перемещать попарно" — двигаем еще 1-е русские
            if (EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(FirstRussianSubtitlesMarginNumericUpDown, OriginalSubtitlesMarginNumericUpDown.Value + (2 * OriginalSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }

        private void originalSubtitlesOutlineNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

            if (EnabledAndChecked(outlineCheckBox))
            {
                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesOutlineNumericUpDown.Value =
                        SecondRussianSubtitlesOutlineNumericUpDown.Value =
                        ThirdRussianSubtitlesOutlineNumericUpDown.Value =
                        OriginalSubtitlesOutlineNumericUpDown.Value;
                }
                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesOutlineNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesOutlineNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesOutlineNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesOutlineNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesOutlineNumericUpDown, delta);
                }
            }

        }

        private void originalSubtitlesShadowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EnabledAndChecked(shadowCheckBox))
            {
                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesShadowNumericUpDown.Value =
                        SecondRussianSubtitlesShadowNumericUpDown.Value =
                        ThirdRussianSubtitlesShadowNumericUpDown.Value =
                        OriginalSubtitlesShadowNumericUpDown.Value;
                }
                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesShadowNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesShadowNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesShadowNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesShadowNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesShadowNumericUpDown, delta);
                }
            }
        }

        private void originalSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EnabledAndChecked(transparencyCheckBox))
            {
                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                        SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                        ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                        OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
                }
                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesTransparencyPercentageNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
                }
            }
        }

        private void firstRussianSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
        }

        private void FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
        }

        private void changeOnTheSameDeltaValuesForAllSubtitlesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //    var changeOnTheSameDeltaValuesForAllSubtitles = ((RadioButton)sender).Checked;

            //    setTheSameValuesForAllSubtitlesRadioButton.Checked = !changeOnTheSameDeltaValuesForAllSubtitles;
            //    marginCheckBox.Enabled = changeOnTheSameDeltaValuesForAllSubtitles;

        }

        private void setTheSameValuesForAllSubtitlesRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //var setTheSameValuesForAllSubtitles = ((RadioButton)sender).Checked;

            //changeOnTheSameDeltaValuesForAllSubtitlesRadioButton.Checked = !setTheSameValuesForAllSubtitles;
            //marginCheckBox.Enabled = !setTheSameValuesForAllSubtitles;
        }

        private void changeOnTheSameDeltaValuesForAllSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var changeOnTheSameDeltaValuesForAllSubtitles = ((CheckBox)sender).Checked;

            if (changeOnTheSameDeltaValuesForAllSubtitles)
            {
                setTheSameValuesForAllSubtitlesCheckBox.Checked = false;
                marginCheckBox.Enabled = true;
            }

            SetControlAccordingToCheckBoxes();
        }

        private void setTheSameValuesForAllSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var setTheSameValuesForAllSubtitles = ((CheckBox)sender).Checked;

            if (setTheSameValuesForAllSubtitles)
            {
                changeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked = false;
                marginCheckBox.Enabled = false;
            }

            SetControlAccordingToCheckBoxes();
        }



        private void SetControlAccordingToCheckBoxes()
        {
            var oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked =
                changeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked || setTheSameValuesForAllSubtitlesCheckBox.Checked;

            marginCheckBox.Enabled =
               sizeCheckBox.Enabled =
               outlineCheckBox.Enabled =
               shadowCheckBox.Enabled =
               transparencyCheckBox.Enabled =
               shadowTransparencyCheckBox.Enabled =
               oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked;

            changeMarginsToPairSubtitlesCheckBox.Enabled = !(EnabledAndChecked(marginCheckBox) && EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox));

            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
                ThirdRussianSubtitlesMarginNumericUpDown.Enabled =
                !(
                (oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(marginCheckBox))
                || EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox));

            SecondRussianSubtitlesMarginNumericUpDown.Enabled =
                !(
                (oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(marginCheckBox)
                && !EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox)));

            FirstRussianSubtitlesSizeNumericUpDown.Enabled =
            SecondRussianSubtitlesSizeNumericUpDown.Enabled =
            ThirdRussianSubtitlesSizeNumericUpDown.Enabled =
            !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(sizeCheckBox));

            FirstRussianSubtitlesOutlineNumericUpDown.Enabled =
              SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
              ThirdRussianSubtitlesOutlineNumericUpDown.Enabled =
              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(outlineCheckBox));

            FirstRussianSubtitlesShadowNumericUpDown.Enabled =
              SecondRussianSubtitlesShadowNumericUpDown.Enabled =
              ThirdRussianSubtitlesShadowNumericUpDown.Enabled =
              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(shadowCheckBox));


            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(transparencyCheckBox));

            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
              SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
              ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
          !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(shadowTransparencyCheckBox));




        }

        private void SetSubtitlesAppearanceSettingsAccordingToStoredValues()
        {
            // TODO v11
            //subtitlesAppearanceSettingsControl.ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked =
            //   Properties.Settings.Default.ChangeRussianSubtitlesStylesAccordingToOriginal;
            //subtitlesAppearanceSettingsControl.SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreen;
            //subtitlesAppearanceSettingsControl.SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled = Properties.SubtitlesAppearanceSettings.Default.SecondAndThirdRussianSubtitlesAtTopOfTheScreenEnabled;


        }

        private void changeMarginsToPairSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccordingToCheckBoxes();
        }

        private void SecondRussianSubtitlesMarginNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Если включен "Перемещать попарно" — двигаем еще 3-и русские
            if (EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(ThirdRussianSubtitlesMarginNumericUpDown, SecondRussianSubtitlesMarginNumericUpDown.Value + (2 * SecondRussianSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }

        private void SecondRussianSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Если включен "Перемещать попарно" — двигаем еще 3-и русские
            if (EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(ThirdRussianSubtitlesMarginNumericUpDown, SecondRussianSubtitlesMarginNumericUpDown.Value + (2 * SecondRussianSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }

        private void sizeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccordingToCheckBoxes();
        }

        private bool EnabledAndChecked(CheckBox checkBox)
        {
            return checkBox.Checked && checkBox.Enabled;
        }

        private void outlineCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccordingToCheckBoxes();
        }

        private void shadowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccordingToCheckBoxes();
        }

        private void transparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccordingToCheckBoxes();
        }

        private void shadowTransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccordingToCheckBoxes();
        }

        private void marginCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            SetControlAccordingToCheckBoxes();
        }

        private void OriginalSubtitlesShadowTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EnabledAndChecked(shadowTransparencyCheckBox))
            {
                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                        SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                        ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                        OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                }
                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                }
            }
        }
    }
}
