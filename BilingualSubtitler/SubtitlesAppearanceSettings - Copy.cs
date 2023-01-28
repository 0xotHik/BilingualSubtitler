//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace BilingualSubtitler
//{
//    public partial class SubtitlesAppearanceSettings : UserControl
//    {
//        public SubtitlesAppearanceSettings()
//        {
//            InitializeComponent();

//            SetAccordingToPropertiesSettings();

//            // В тэге — храним старое значение, перед изменением
//            //
//            OriginalSubtitlesMarginNumericUpDown.Tag = OriginalSubtitlesMarginNumericUpDown.Value;
//            OriginalSubtitlesSizeNumericUpDown.Tag = OriginalSubtitlesSizeNumericUpDown.Value;
//            OriginalSubtitlesOutlineNumericUpDown.Tag = OriginalSubtitlesOutlineNumericUpDown.Value;
//            OriginalSubtitlesShadowNumericUpDown.Tag = OriginalSubtitlesShadowNumericUpDown.Value;
//            OriginalSubtitlesTransparencyPercentageNumericUpDown.Tag = OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
//            OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
//            //
//            OriginalSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            OriginalSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            OriginalSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            OriginalSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            OriginalSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

//            FirstRussianSubtitlesMarginNumericUpDown.Tag = FirstRussianSubtitlesMarginNumericUpDown.Value;
//            FirstRussianSubtitlesSizeNumericUpDown.Tag = FirstRussianSubtitlesSizeNumericUpDown.Value;
//            FirstRussianSubtitlesOutlineNumericUpDown.Tag = FirstRussianSubtitlesOutlineNumericUpDown.Value;
//            FirstRussianSubtitlesShadowNumericUpDown.Tag = FirstRussianSubtitlesShadowNumericUpDown.Value;
//            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
//            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
//            //
//            FirstRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            FirstRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            FirstRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            FirstRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

//            SecondRussianSubtitlesMarginNumericUpDown.Tag = SecondRussianSubtitlesMarginNumericUpDown.Value;
//            SecondRussianSubtitlesSizeNumericUpDown.Tag = SecondRussianSubtitlesSizeNumericUpDown.Value;
//            SecondRussianSubtitlesOutlineNumericUpDown.Tag = SecondRussianSubtitlesOutlineNumericUpDown.Value;
//            SecondRussianSubtitlesShadowNumericUpDown.Tag = SecondRussianSubtitlesShadowNumericUpDown.Value;
//            SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
//            SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
//            //
//            SecondRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            SecondRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            SecondRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            SecondRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            SecondRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

//            ThirdRussianSubtitlesMarginNumericUpDown.Tag = ThirdRussianSubtitlesMarginNumericUpDown.Value;
//            ThirdRussianSubtitlesSizeNumericUpDown.Tag = ThirdRussianSubtitlesSizeNumericUpDown.Value;
//            ThirdRussianSubtitlesOutlineNumericUpDown.Tag = ThirdRussianSubtitlesOutlineNumericUpDown.Value;
//            ThirdRussianSubtitlesShadowNumericUpDown.Tag = ThirdRussianSubtitlesShadowNumericUpDown.Value;
//            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
//            ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
//            //
//            ThirdRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            ThirdRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            ThirdRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            ThirdRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
//            ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

//        }

//        public void SetAccordingToPropertiesSettings()
//        {
//            changeMarginsToPairSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ChangeMarginsToPairSubtitles;
//            changeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.СhangeOnTheSameDeltaValuesForAllSubtitles;
//            setTheSameValuesForAllSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SetTheSameValuesForAllSubtitlesCheckBox;

//            marginCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.MarginCheckBoxChecked;
//            sizeCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SizeCheckBoxChecked;
//            outlineCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.OutlineCheckBoxChecked;
//            shadowCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ShadowCheckBoxChecked;
//            transparencyCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.TransparencyCheckBoxChecked;
//            shadowTransparencyCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ShadowTransparencyCheckBoxChecked;
//        }

//        private void NumericUpDownValueChanged(object sender, EventArgs e)
//        {
//            var numericUpDownSender = (NumericUpDown)sender;
//            //var oldValue = (decimal)numericUpDownSender.Tag;

//            var newValue = numericUpDownSender.Value;
//            numericUpDownSender.Tag = newValue;
//        }

//        private void changeRussianSubtitlesStylesAccordingToOriginalCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            // TODO v11
//            //SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled =
//            ////
//            ////firstRussianSubtitlesFontComboBox.Enabled = 
//            //FirstRussianSubtitlesMarginNumericUpDown.Enabled =
//            //    FirstRussianSubtitlesShadowNumericUpDown.Enabled =
//            //        FirstRussianSubtitlesOutlineNumericUpDown.Enabled = FirstRussianSubtitlesSizeNumericUpDown.Enabled =
//            //                // firstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//            //                // Вторые
//            //                SecondRussianSubtitlesFontComboBox.Enabled =
//            //                    SecondRussianSubtitlesMarginNumericUpDown.Enabled =
//            //                        SecondRussianSubtitlesShadowNumericUpDown.Enabled =
//            //                            SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
//            //                                SecondRussianSubtitlesSizeNumericUpDown.Enabled =
//            //                                    SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//            //                                    SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//            //                                        // Третьи
//            //                                        ThirdRussianSubtitlesFontComboBox.Enabled =
//            //                                            ThirdRussianSubtitlesMarginNumericUpDown.Enabled =
//            //                                                ThirdRussianSubtitlesShadowNumericUpDown.Enabled =
//            //                                                    ThirdRussianSubtitlesOutlineNumericUpDown.Enabled =
//            //                                                        ThirdRussianSubtitlesSizeNumericUpDown.Enabled =
//            //                                                            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown
//            //                                                                    .Enabled =
//            //                                                                    ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown
//            //                                                                    .Enabled =
//            //                                                                //
//            //                                                                !ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;

//            //SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled = ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked;
//        }

//        private void firstRussianSubtitlesFontComboBox_TextChanged(object sender, EventArgs e)
//        {
//            // TODO v11
//            //if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
//            //{
//            //    SecondRussianSubtitlesFontComboBox.Text =
//            //        ThirdRussianSubtitlesFontComboBox.Text =
//            //            FirstRussianSubtitlesFontComboBox.Text;
//            //}
//        }

//        private void ChangeMargin()
//        {
//            //    if (ChangeRussianSubtitlesStylesAccordingToOriginalCheckBox.Checked)
//            //    {
//            //        if (SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Enabled && SecondAndThirdRussianSubtitlesAtTheTopOfScreenCheckBox.Checked)
//            //        {
//            //            var firstRussianSubtitlesMargin = OriginalSubtitlesMarginNumericUpDown.Value -
//            //                                              (2 * OriginalSubtitlesSizeNumericUpDown.Value +
//            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
//            //            FirstRussianSubtitlesMarginNumericUpDown.Value =
//            //                firstRussianSubtitlesMargin < 0 ? 0 : firstRussianSubtitlesMargin;

//            //            ThirdRussianSubtitlesMarginNumericUpDown.Value = 290 - (2 * OriginalSubtitlesSizeNumericUpDown.Value +
//            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
//            //            SecondRussianSubtitlesMarginNumericUpDown.Value = ThirdRussianSubtitlesMarginNumericUpDown.Value -
//            //                (2 * OriginalSubtitlesSizeNumericUpDown.Value +
//            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
//            //        }
//            //        else
//            //        {
//            //            FirstRussianSubtitlesMarginNumericUpDown.Value =
//            //                OriginalSubtitlesMarginNumericUpDown.Value +
//            //                (2 * OriginalSubtitlesSizeNumericUpDown.Value +
//            //                 OriginalSubtitlesSizeNumericUpDown.Value / 10);
//            //            SecondRussianSubtitlesMarginNumericUpDown.Value = OriginalSubtitlesMarginNumericUpDown.Value +
//            //                                                              (2 * OriginalSubtitlesSizeNumericUpDown.Value +
//            //                                                               OriginalSubtitlesSizeNumericUpDown.Value / 10) * 2;

//            //            var thirdRussianSubtitlesMargin = OriginalSubtitlesMarginNumericUpDown.Value -
//            //                                              (2 * OriginalSubtitlesSizeNumericUpDown.Value +
//            //                                               OriginalSubtitlesSizeNumericUpDown.Value / 10);
//            //            ThirdRussianSubtitlesMarginNumericUpDown.Value =
//            //                thirdRussianSubtitlesMargin < 0 ? 0 : thirdRussianSubtitlesMargin;
//            //        }

//            //    }
//        }

//        private void originalSubtitlesMarginNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//            if (EnabledAndChecked(marginCheckBox))
//            {
//                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
//                {
//                    FirstRussianSubtitlesMarginNumericUpDown.Value =
//                        SecondRussianSubtitlesMarginNumericUpDown.Value =
//                        ThirdRussianSubtitlesMarginNumericUpDown.Value =
//                        OriginalSubtitlesMarginNumericUpDown.Value;
//                }
//                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
//                {
//                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
//                    var oldValue = (decimal)OriginalSubtitlesMarginNumericUpDown.Tag;
//                    var newValue = OriginalSubtitlesMarginNumericUpDown.Value;
//                    var delta = newValue - oldValue;

//                    FirstRussianSubtitlesMarginNumericUpDown.Value = (decimal)FirstRussianSubtitlesMarginNumericUpDown.Tag + delta;
//                    SecondRussianSubtitlesMarginNumericUpDown.Value = (decimal)SecondRussianSubtitlesMarginNumericUpDown.Tag + delta;
//                    ThirdRussianSubtitlesMarginNumericUpDown.Value = (decimal)ThirdRussianSubtitlesMarginNumericUpDown.Tag + delta;
//                }
//            }

//            // Если включен "Перемещать попарно" — двигаем еще 1-е русские
//            if (changeMarginsToPairSubtitlesCheckBox.Checked)
//            {
//                FirstRussianSubtitlesMarginNumericUpDown.Value = OriginalSubtitlesMarginNumericUpDown.Value + (2 * OriginalSubtitlesSizeNumericUpDown.Value) + 2;
//            }
//        }

//        private void originalSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//            if (EnabledAndChecked(sizeCheckBox))
//            {
//                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
//                {
//                    FirstRussianSubtitlesSizeNumericUpDown.Value =
//                        SecondRussianSubtitlesSizeNumericUpDown.Value =
//                        ThirdRussianSubtitlesSizeNumericUpDown.Value =
//                        OriginalSubtitlesSizeNumericUpDown.Value;
//                }
//                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
//                {
//                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
//                    var oldValue = (decimal)OriginalSubtitlesSizeNumericUpDown.Tag;
//                    var newValue = OriginalSubtitlesSizeNumericUpDown.Value;
//                    var delta = newValue - oldValue;

//                    FirstRussianSubtitlesSizeNumericUpDown.Value = (decimal)FirstRussianSubtitlesSizeNumericUpDown.Tag + delta;
//                    SecondRussianSubtitlesSizeNumericUpDown.Value = (decimal)SecondRussianSubtitlesSizeNumericUpDown.Tag + delta;
//                    ThirdRussianSubtitlesSizeNumericUpDown.Value = (decimal)ThirdRussianSubtitlesSizeNumericUpDown.Tag + delta;
//                }
//            }

//            // Если включен "Перемещать попарно" — двигаем еще 1-е русские
//            if (changeMarginsToPairSubtitlesCheckBox.Checked)
//            {
//                FirstRussianSubtitlesMarginNumericUpDown.Value = OriginalSubtitlesMarginNumericUpDown.Value + (2 * OriginalSubtitlesSizeNumericUpDown.Value) + 2;
//            }
//        }

//        private void originalSubtitlesOutlineNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {

//            if (EnabledAndChecked(outlineCheckBox))
//            {
//                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
//                {
//                    FirstRussianSubtitlesOutlineNumericUpDown.Value =
//                        SecondRussianSubtitlesOutlineNumericUpDown.Value =
//                        ThirdRussianSubtitlesOutlineNumericUpDown.Value =
//                        OriginalSubtitlesOutlineNumericUpDown.Value;
//                }
//                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
//                {
//                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
//                    var oldValue = (decimal)OriginalSubtitlesOutlineNumericUpDown.Tag;
//                    var newValue = OriginalSubtitlesOutlineNumericUpDown.Value;
//                    var delta = newValue - oldValue;

//                    FirstRussianSubtitlesOutlineNumericUpDown.Value = (decimal)FirstRussianSubtitlesOutlineNumericUpDown.Tag + delta;
//                    SecondRussianSubtitlesOutlineNumericUpDown.Value = (decimal)SecondRussianSubtitlesOutlineNumericUpDown.Tag + delta;
//                    ThirdRussianSubtitlesOutlineNumericUpDown.Value = (decimal)ThirdRussianSubtitlesOutlineNumericUpDown.Tag + delta;
//                }
//            }

//        }

//        private void originalSubtitlesShadowNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//            if (EnabledAndChecked(shadowCheckBox))
//            {
//                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
//                {
//                    FirstRussianSubtitlesShadowNumericUpDown.Value =
//                        SecondRussianSubtitlesShadowNumericUpDown.Value =
//                        ThirdRussianSubtitlesShadowNumericUpDown.Value =
//                        OriginalSubtitlesShadowNumericUpDown.Value;
//                }
//                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
//                {
//                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
//                    var oldValue = (decimal)OriginalSubtitlesShadowNumericUpDown.Tag;
//                    var newValue = OriginalSubtitlesShadowNumericUpDown.Value;
//                    var delta = newValue - oldValue;

//                    FirstRussianSubtitlesShadowNumericUpDown.Value = (decimal)FirstRussianSubtitlesShadowNumericUpDown.Tag + delta;
//                    SecondRussianSubtitlesShadowNumericUpDown.Value = (decimal)SecondRussianSubtitlesShadowNumericUpDown.Tag + delta;
//                    ThirdRussianSubtitlesShadowNumericUpDown.Value = (decimal)ThirdRussianSubtitlesShadowNumericUpDown.Tag + delta;
//                }
//            }
//        }

//        private void originalSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//            if (EnabledAndChecked(transparencyCheckBox))
//            {
//                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
//                {
//                    FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
//                        SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
//                        ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
//                        OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
//                }
//                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
//                {
//                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
//                    var oldValue = (decimal)OriginalSubtitlesTransparencyPercentageNumericUpDown.Tag;
//                    var newValue = OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
//                    var delta = newValue - oldValue;

//                    FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value = (decimal)FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Tag + delta;
//                    SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value = (decimal)SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Tag + delta;
//                    ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value = (decimal)ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Tag + delta;
//                }
//            }
//        }

//        private void firstRussianSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//        }

//        private void FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//        }

//        private void changeOnTheSameDeltaValuesForAllSubtitlesRadioButton_CheckedChanged(object sender, EventArgs e)
//        {
//            //    var changeOnTheSameDeltaValuesForAllSubtitles = ((RadioButton)sender).Checked;

//            //    setTheSameValuesForAllSubtitlesRadioButton.Checked = !changeOnTheSameDeltaValuesForAllSubtitles;
//            //    marginCheckBox.Enabled = changeOnTheSameDeltaValuesForAllSubtitles;

//        }

//        private void setTheSameValuesForAllSubtitlesRadioButton_CheckedChanged(object sender, EventArgs e)
//        {
//            //var setTheSameValuesForAllSubtitles = ((RadioButton)sender).Checked;

//            //changeOnTheSameDeltaValuesForAllSubtitlesRadioButton.Checked = !setTheSameValuesForAllSubtitles;
//            //marginCheckBox.Enabled = !setTheSameValuesForAllSubtitles;
//        }

//        private void changeOnTheSameDeltaValuesForAllSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            var changeOnTheSameDeltaValuesForAllSubtitles = ((CheckBox)sender).Checked;

//            if (changeOnTheSameDeltaValuesForAllSubtitles)
//            {
//                setTheSameValuesForAllSubtitlesCheckBox.Checked = false;
//                marginCheckBox.Enabled = true;
//            }
//            else if (setTheSameValuesForAllSubtitlesCheckBox.Checked == false)
//                NoCheckBoxAboutValuesForAllSubtitlesIsChecked();
//        }

//        private void setTheSameValuesForAllSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            var setTheSameValuesForAllSubtitles = ((CheckBox)sender).Checked;

//            if (setTheSameValuesForAllSubtitles)
//            {
//                changeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked = false;
//                marginCheckBox.Enabled = false;
//            }
//            else if (changeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked == false)
//                NoCheckBoxAboutValuesForAllSubtitlesIsChecked();
//        }

//        private void NoCheckBoxAboutValuesForAllSubtitlesIsChecked()
//        {
//            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
//                SecondRussianSubtitlesMarginNumericUpDown.Enabled =
//                ThirdRussianSubtitlesMarginNumericUpDown.Enabled =
//                FirstRussianSubtitlesSizeNumericUpDown.Enabled =
//                SecondRussianSubtitlesSizeNumericUpDown.Enabled =
//                ThirdRussianSubtitlesSizeNumericUpDown.Enabled =
//                FirstRussianSubtitlesOutlineNumericUpDown.Enabled =
//                SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
//                ThirdRussianSubtitlesOutlineNumericUpDown.Enabled =
//                FirstRussianSubtitlesShadowNumericUpDown.Enabled =
//                SecondRussianSubtitlesShadowNumericUpDown.Enabled =
//                ThirdRussianSubtitlesShadowNumericUpDown.Enabled =
//                FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//                SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//                ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//                SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//                ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//            true;

//            marginCheckBox.Enabled =
//                sizeCheckBox.Enabled =
//                outlineCheckBox.Enabled =
//                shadowCheckBox.Enabled =
//                transparencyCheckBox.Enabled =
//                shadowTransparencyCheckBox.Enabled =
//                false;

//            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
//        ThirdRussianSubtitlesMarginNumericUpDown.Enabled
//        = !changeMarginsToPairSubtitlesCheckBox.Checked;
//        }

//        private void SetControlAccordingToCheckBoxes()
//        {
//            var oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked =
//                changeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked || setTheSameValuesForAllSubtitlesCheckBox.Checked;

//            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
//                ThirdRussianSubtitlesMarginNumericUpDown.Enabled =
//                !((oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && marginCheckBox.Checked)
//                || changeMarginsToPairSubtitlesCheckBox.Checked);

//              SecondRussianSubtitlesMarginNumericUpDown.Enabled =
//              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && marginCheckBox.Checked);

//              FirstRussianSubtitlesSizeNumericUpDown.Enabled =
//              SecondRussianSubtitlesSizeNumericUpDown.Enabled =
//              ThirdRussianSubtitlesSizeNumericUpDown.Enabled =
//              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && sizeCheckBox.Checked);

//            FirstRussianSubtitlesOutlineNumericUpDown.Enabled =
//              SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
//              ThirdRussianSubtitlesOutlineNumericUpDown.Enabled =
//              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && outlineCheckBox.Checked);

//            FirstRussianSubtitlesShadowNumericUpDown.Enabled =
//              SecondRussianSubtitlesShadowNumericUpDown.Enabled =
//              ThirdRussianSubtitlesShadowNumericUpDown.Enabled =
//              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && shadowCheckBox.Checked);


//            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//              SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//              ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && transparencyCheckBox.Checked);

//            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//              SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//              ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//          !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && shadowTransparencyCheckBox.Checked);
//        }

//        private void changeMarginsToPairSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
//                ThirdRussianSubtitlesMarginNumericUpDown.Enabled
//                = !changeMarginsToPairSubtitlesCheckBox.Checked;

//        }

//        private void SecondRussianSubtitlesMarginNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//            // Если включен "Перемещать попарно" — двигаем еще 3-и русские
//            if (EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox))
//            {
//                ThirdRussianSubtitlesMarginNumericUpDown.Value = SecondRussianSubtitlesMarginNumericUpDown.Value + (2 * SecondRussianSubtitlesSizeNumericUpDown.Value) + 2;
//            }
//        }

//        private void SecondRussianSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//            // Если включен "Перемещать попарно" — двигаем еще 3-и русские
//            if (EnabledAndChecked(changeMarginsToPairSubtitlesCheckBox))
//            {
//                ThirdRussianSubtitlesMarginNumericUpDown.Value = SecondRussianSubtitlesMarginNumericUpDown.Value + (2 * SecondRussianSubtitlesSizeNumericUpDown.Value) + 2;
//            }
//        }

//        private void sizeCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            FirstRussianSubtitlesSizeNumericUpDown.Enabled =
//                SecondRussianSubtitlesSizeNumericUpDown.Enabled =
//                ThirdRussianSubtitlesSizeNumericUpDown.Enabled
//                    = !EnabledAndChecked(sizeCheckBox);
//        }

//        private bool EnabledAndChecked(CheckBox checkBox)
//        {
//            return checkBox.Checked && checkBox.Enabled;
//        }

//        private void outlineCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            FirstRussianSubtitlesOutlineNumericUpDown.Enabled =
//                SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
//                ThirdRussianSubtitlesOutlineNumericUpDown.Enabled
//                    = !EnabledAndChecked(outlineCheckBox);
//        }

//        private void shadowCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            FirstRussianSubtitlesShadowNumericUpDown.Enabled =
//                SecondRussianSubtitlesShadowNumericUpDown.Enabled =
//                ThirdRussianSubtitlesShadowNumericUpDown.Enabled
//                    = !EnabledAndChecked(shadowCheckBox);
//        }

//        private void transparencyCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//                SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
//                ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled
//                    = !EnabledAndChecked(transparencyCheckBox);
//        }

//        private void shadowTransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//                SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
//                ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled
//                    = !EnabledAndChecked(shadowTransparencyCheckBox);
//        }

//        private void marginCheckBox_CheckedChanged(object sender, EventArgs e)
//        {
//            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
//                SecondRussianSubtitlesMarginNumericUpDown.Enabled =
//                ThirdRussianSubtitlesMarginNumericUpDown.Enabled
//                    = !EnabledAndChecked(marginCheckBox);

//            changeMarginsToPairSubtitlesCheckBox.Enabled = !(EnabledAndChecked(marginCheckBox) && EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox));
//        }

//        private void OriginalSubtitlesShadowTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
//        {
//            if (EnabledAndChecked(shadowTransparencyCheckBox))
//            {
//                if (EnabledAndChecked(setTheSameValuesForAllSubtitlesCheckBox))
//                {
//                    FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
//                        SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
//                        ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
//                        OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
//                }
//                else if (EnabledAndChecked(changeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
//                {
//                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
//                    var oldValue = (decimal)OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Tag;
//                    var newValue = OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
//                    var delta = newValue - oldValue;

//                    FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = (decimal)FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag + delta;
//                    SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = (decimal)SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag + delta;
//                    ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value = (decimal)ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag + delta;
//                }
//            }
//        }
//    }
//}
