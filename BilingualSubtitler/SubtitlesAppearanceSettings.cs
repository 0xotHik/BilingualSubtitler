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
            // OG
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

            // 1 переведенные
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

            // 2
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

            // 3
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

            // 4
            FourthRussianSubtitlesMarginNumericUpDown.Tag = FourthRussianSubtitlesMarginNumericUpDown.Value;
            FourthRussianSubtitlesSizeNumericUpDown.Tag = FourthRussianSubtitlesSizeNumericUpDown.Value;
            FourthRussianSubtitlesOutlineNumericUpDown.Tag = FourthRussianSubtitlesOutlineNumericUpDown.Value;
            FourthRussianSubtitlesShadowNumericUpDown.Tag = FourthRussianSubtitlesShadowNumericUpDown.Value;
            FourthRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = FourthRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
            FourthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = FourthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            //
            FourthRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FourthRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FourthRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FourthRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FourthRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FourthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;

            // 5
            FifthRussianSubtitlesMarginNumericUpDown.Tag = FifthRussianSubtitlesMarginNumericUpDown.Value;
            FifthRussianSubtitlesSizeNumericUpDown.Tag = FifthRussianSubtitlesSizeNumericUpDown.Value;
            FifthRussianSubtitlesOutlineNumericUpDown.Tag = FifthRussianSubtitlesOutlineNumericUpDown.Value;
            FifthRussianSubtitlesShadowNumericUpDown.Tag = FifthRussianSubtitlesShadowNumericUpDown.Value;
            FifthRussianSubtitlesTransparencyPercentageNumericUpDown.Tag = FifthRussianSubtitlesTransparencyPercentageNumericUpDown.Value;
            FifthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Tag = FifthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
            //
            FifthRussianSubtitlesMarginNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FifthRussianSubtitlesSizeNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FifthRussianSubtitlesOutlineNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FifthRussianSubtitlesShadowNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FifthRussianSubtitlesTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;
            FifthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.ValueChanged += NumericUpDownValueChanged;


            SetAccordingToPropertiesSettings();
        }

        public void SetAccordingToPropertiesSettings()
        {
            ChangeMarginsToPairSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ChangeMarginsToPairSubtitles;
            ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.СhangeOnTheSameDeltaValuesForAllSubtitles;
            SetTheSameValuesForAllSubtitlesCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SetTheSameValuesForAllSubtitles;
            //
            marginCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.MarginCheckBoxChecked;
            sizeCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.SizeCheckBoxChecked;
            outlineCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.OutlineCheckBoxChecked;
            shadowCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ShadowCheckBoxChecked;
            transparencyCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.TransparencyCheckBoxChecked;
            shadowTransparencyCheckBox.Checked = Properties.SubtitlesAppearanceSettings.Default.ShadowTransparencyCheckBoxChecked;

            var originalSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.OriginalSubtitlesStyleString.Split(';');
            SetControlsValuesAccordingToStyleString(originalSubtitlesStyle, OriginalSubtitlesFontComboBox,
            OriginalSubtitlesMarginNumericUpDown,
            OriginalSubtitlesSizeNumericUpDown,
            OriginalSubtitlesOutlineNumericUpDown,
            OriginalSubtitlesShadowNumericUpDown,
            OriginalSubtitlesTransparencyPercentageNumericUpDown,
            OriginalSubtitlesShadowTransparencyPercentageNumericUpDown,
            OriginalSubtitlesInOneLineCheckBox,
            OriginalSubtitlesBoldCheckBox,
            OriginalSubtitlesItalicCheckBox,
            OriginalSubtitlesUnderlineCheckBox,
            OriginalSubtitlesStrikeoutCheckBox);

            var firstRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.FirstRussianSubtitlesStyleString.Split(';');
            SetControlsValuesAccordingToStyleString(firstRussianSubtitlesStyle, FirstRussianSubtitlesFontComboBox,
            FirstRussianSubtitlesMarginNumericUpDown,
            FirstRussianSubtitlesSizeNumericUpDown,
            FirstRussianSubtitlesOutlineNumericUpDown,
            FirstRussianSubtitlesShadowNumericUpDown,
            FirstRussianSubtitlesTransparencyPercentageNumericUpDown,
            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown,
            FirstRussianSubtitlesInOneLineCheckBox,
            FirstRussianSubtitlesBoldCheckBox,
            FirstRussianSubtitlesItalicCheckBox,
            FirstRussianSubtitlesUnderlineCheckBox,
            FirstRussianSubtitlesStrikeoutCheckBox);

            var secondRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.SecondRussianSubtitlesStyleString.Split(';');
            SetControlsValuesAccordingToStyleString(secondRussianSubtitlesStyle, SecondRussianSubtitlesFontComboBox,
            SecondRussianSubtitlesMarginNumericUpDown,
            SecondRussianSubtitlesSizeNumericUpDown,
            SecondRussianSubtitlesOutlineNumericUpDown,
            SecondRussianSubtitlesShadowNumericUpDown,
            SecondRussianSubtitlesTransparencyPercentageNumericUpDown,
            SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown,
            SecondRussianSubtitlesInOneLineCheckBox,
            SecondRussianSubtitlesBoldCheckBox,
            SecondRussianSubtitlesItalicCheckBox,
            SecondRussianSubtitlesUnderlineCheckBox,
            SecondRussianSubtitlesStrikeoutCheckBox);

            var thirdRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.ThirdRussianSubtitlesStyleString.Split(';');
            SetControlsValuesAccordingToStyleString(thirdRussianSubtitlesStyle, ThirdRussianSubtitlesFontComboBox,
            ThirdRussianSubtitlesMarginNumericUpDown,
            ThirdRussianSubtitlesSizeNumericUpDown,
            ThirdRussianSubtitlesOutlineNumericUpDown,
            ThirdRussianSubtitlesShadowNumericUpDown,
            ThirdRussianSubtitlesTransparencyPercentageNumericUpDown,
            ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown,
            ThirdRussianSubtitlesInOneLineCheckBox,
            ThirdRussianSubtitlesBoldCheckBox,
            ThirdRussianSubtitlesItalicCheckBox,
            ThirdRussianSubtitlesUnderlineCheckBox,
            ThirdRussianSubtitlesStrikeoutCheckBox);

            var fourthRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.FourthRussianSubtitlesStyleString.Split(';');
            SetControlsValuesAccordingToStyleString(fourthRussianSubtitlesStyle, FourthRussianSubtitlesFontComboBox,
            FourthRussianSubtitlesMarginNumericUpDown,
            FourthRussianSubtitlesSizeNumericUpDown,
            FourthRussianSubtitlesOutlineNumericUpDown,
            FourthRussianSubtitlesShadowNumericUpDown,
            FourthRussianSubtitlesTransparencyPercentageNumericUpDown,
            FourthRussianSubtitlesShadowTransparencyPercentageNumericUpDown,
            FourthRussianSubtitlesInOneLineCheckBox,
            FourthRussianSubtitlesBoldCheckBox,
            FourthRussianSubtitlesItalicCheckBox,
            FourthRussianSubtitlesUnderlineCheckBox,
            FourthRussianSubtitlesStrikeoutCheckBox);

            var fifthRussianSubtitlesStyle = Properties.SubtitlesAppearanceSettings.Default.FifthRussianSubtitlesStyleString.Split(';');
            SetControlsValuesAccordingToStyleString(fifthRussianSubtitlesStyle, FifthRussianSubtitlesFontComboBox,
            FifthRussianSubtitlesMarginNumericUpDown,
            FifthRussianSubtitlesSizeNumericUpDown,
            FifthRussianSubtitlesOutlineNumericUpDown,
            FifthRussianSubtitlesShadowNumericUpDown,
            FifthRussianSubtitlesTransparencyPercentageNumericUpDown,
            FifthRussianSubtitlesShadowTransparencyPercentageNumericUpDown,
            FifthRussianSubtitlesInOneLineCheckBox,
            FifthRussianSubtitlesBoldCheckBox,
            FifthRussianSubtitlesItalicCheckBox,
            FifthRussianSubtitlesUnderlineCheckBox,
            FifthRussianSubtitlesStrikeoutCheckBox);

            SetControlAccordingToCheckBoxes();
        }

        private void SetControlsValuesAccordingToStyleString(string[] subtitlesStyle,
            ComboBox subtitlesFontComboBox,
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
            CheckBox strikeoutCheckBox
            )
        {
            foreach (var fontItem in subtitlesFontComboBox.Items)
            {
                if ((string)fontItem == subtitlesStyle[0])
                {
                    subtitlesFontComboBox.SelectedItem = fontItem;
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(subtitlesFontComboBox.Text))
                subtitlesFontComboBox.Text = subtitlesStyle[0];
            subtitlesMarginNumericUpDown.Value = decimal.Parse(subtitlesStyle[1]);
            subtitlesSizeNumericUpDown.Value = decimal.Parse(subtitlesStyle[2]);
            subtitlesOutlineNumericUpDown.Value = decimal.Parse(subtitlesStyle[3]);
            subtitlesShadowNumericUpDown.Value = decimal.Parse(subtitlesStyle[4]);
            subtitlesTransparencyPercentageNumericUpDown.Value = decimal.Parse(subtitlesStyle[5]);
            subtitlesShadowTransparencyPercentageNumericUpDown.Value = decimal.Parse(subtitlesStyle[6]);
            subtitlesInOneLineCheckBox.Checked = subtitlesStyle[7] == "1";

            boldCheckBox.Checked = subtitlesStyle.Length > 8 ? subtitlesStyle[8] == "1" : false;
            italicCheckBox.Checked = subtitlesStyle.Length > 9 ? subtitlesStyle[9] == "1" : false;
            underlineCheckBox.Checked = subtitlesStyle.Length > 10 ? subtitlesStyle[10] == "1" : false;
            strikeoutCheckBox.Checked = subtitlesStyle.Length > 11 ? subtitlesStyle[11] == "1" : false;


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
                if (EnabledAndChecked(SetTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesMarginNumericUpDown.Value =
                        SecondRussianSubtitlesMarginNumericUpDown.Value =
                        ThirdRussianSubtitlesMarginNumericUpDown.Value =
                        FourthRussianSubtitlesMarginNumericUpDown.Value =
                                                FifthRussianSubtitlesMarginNumericUpDown.Value =

                        OriginalSubtitlesMarginNumericUpDown.Value;
                }
                else if (EnabledAndChecked(ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesMarginNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesMarginNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesMarginNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesMarginNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesMarginNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FourthRussianSubtitlesMarginNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FifthRussianSubtitlesMarginNumericUpDown, delta);
                }
            }

            // Если включен "Перемещать попарно" — двигаем еще 1-е русские
            if (EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox))
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
                if (EnabledAndChecked(SetTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesSizeNumericUpDown.Value =
                        SecondRussianSubtitlesSizeNumericUpDown.Value =
                        ThirdRussianSubtitlesSizeNumericUpDown.Value =
                        FourthRussianSubtitlesSizeNumericUpDown.Value =
                        FifthRussianSubtitlesSizeNumericUpDown.Value =

                        OriginalSubtitlesSizeNumericUpDown.Value;
                }
                else if (EnabledAndChecked(ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesSizeNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesSizeNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesSizeNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesSizeNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesSizeNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FourthRussianSubtitlesSizeNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FifthRussianSubtitlesSizeNumericUpDown, delta);
                }
            }

            // Если включен "Перемещать попарно" — двигаем еще 1-е русские
            if (EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(FirstRussianSubtitlesMarginNumericUpDown, OriginalSubtitlesMarginNumericUpDown.Value + (2 * OriginalSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }

        private void originalSubtitlesOutlineNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

            if (EnabledAndChecked(outlineCheckBox))
            {
                if (EnabledAndChecked(SetTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesOutlineNumericUpDown.Value =
                        SecondRussianSubtitlesOutlineNumericUpDown.Value =
                        ThirdRussianSubtitlesOutlineNumericUpDown.Value =
                        FourthRussianSubtitlesOutlineNumericUpDown.Value =
                        FifthRussianSubtitlesOutlineNumericUpDown.Value =

                        OriginalSubtitlesOutlineNumericUpDown.Value;
                }
                else if (EnabledAndChecked(ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesOutlineNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesOutlineNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesOutlineNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesOutlineNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesOutlineNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FourthRussianSubtitlesOutlineNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FifthRussianSubtitlesOutlineNumericUpDown, delta);
                }
            }

        }

        private void originalSubtitlesShadowNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EnabledAndChecked(shadowCheckBox))
            {
                if (EnabledAndChecked(SetTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesShadowNumericUpDown.Value =
                        SecondRussianSubtitlesShadowNumericUpDown.Value =
                        ThirdRussianSubtitlesShadowNumericUpDown.Value =
                        FourthRussianSubtitlesShadowNumericUpDown.Value =
                        FifthRussianSubtitlesShadowNumericUpDown.Value =

                        OriginalSubtitlesShadowNumericUpDown.Value;
                }
                else if (EnabledAndChecked(ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesShadowNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesShadowNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesShadowNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesShadowNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesShadowNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FourthRussianSubtitlesShadowNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FifthRussianSubtitlesShadowNumericUpDown, delta);
                }
            }
        }

        private void originalSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (EnabledAndChecked(transparencyCheckBox))
            {
                if (EnabledAndChecked(SetTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                        SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                        ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                        FourthRussianSubtitlesTransparencyPercentageNumericUpDown.Value =
                        FifthRussianSubtitlesTransparencyPercentageNumericUpDown.Value =

                        OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
                }
                else if (EnabledAndChecked(ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesTransparencyPercentageNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesTransparencyPercentageNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FourthRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FifthRussianSubtitlesTransparencyPercentageNumericUpDown, delta);
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
                SetTheSameValuesForAllSubtitlesCheckBox.Checked = false;
                marginCheckBox.Enabled = true;
            }

            SetControlAccordingToCheckBoxes();
        }

        private void setTheSameValuesForAllSubtitlesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var setTheSameValuesForAllSubtitles = ((CheckBox)sender).Checked;

            if (setTheSameValuesForAllSubtitles)
            {
                ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked = false;
                marginCheckBox.Enabled = false;
            }

            SetControlAccordingToCheckBoxes();
        }



        private void SetControlAccordingToCheckBoxes()
        {
            var oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked =
                ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox.Checked || SetTheSameValuesForAllSubtitlesCheckBox.Checked;

            marginCheckBox.Enabled =
               sizeCheckBox.Enabled =
               outlineCheckBox.Enabled =
               shadowCheckBox.Enabled =
               transparencyCheckBox.Enabled =
               shadowTransparencyCheckBox.Enabled =
               oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked;

            ChangeMarginsToPairSubtitlesCheckBox.Enabled = !(EnabledAndChecked(marginCheckBox) && EnabledAndChecked(SetTheSameValuesForAllSubtitlesCheckBox));

            FirstRussianSubtitlesMarginNumericUpDown.Enabled =
                ThirdRussianSubtitlesMarginNumericUpDown.Enabled =
                    FifthRussianSubtitlesMarginNumericUpDown.Enabled =
                !(
                (oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(marginCheckBox))
                || EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox));

            SecondRussianSubtitlesMarginNumericUpDown.Enabled =
                FourthRussianSubtitlesMarginNumericUpDown.Enabled =
                !(
                (oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(marginCheckBox)
                && !EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox)));

            FirstRussianSubtitlesSizeNumericUpDown.Enabled =
            SecondRussianSubtitlesSizeNumericUpDown.Enabled =
            ThirdRussianSubtitlesSizeNumericUpDown.Enabled =
            FourthRussianSubtitlesSizeNumericUpDown.Enabled =
            FifthRussianSubtitlesSizeNumericUpDown.Enabled =
            !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(sizeCheckBox));

            FirstRussianSubtitlesOutlineNumericUpDown.Enabled =
              SecondRussianSubtitlesOutlineNumericUpDown.Enabled =
              ThirdRussianSubtitlesOutlineNumericUpDown.Enabled =
              FourthRussianSubtitlesOutlineNumericUpDown.Enabled =
              FifthRussianSubtitlesOutlineNumericUpDown.Enabled =
              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(outlineCheckBox));

            FirstRussianSubtitlesShadowNumericUpDown.Enabled =
              SecondRussianSubtitlesShadowNumericUpDown.Enabled =
              ThirdRussianSubtitlesShadowNumericUpDown.Enabled =
              FourthRussianSubtitlesShadowNumericUpDown.Enabled =
              FifthRussianSubtitlesShadowNumericUpDown.Enabled =
              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(shadowCheckBox));


            FirstRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              SecondRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              ThirdRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              FourthRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              FifthRussianSubtitlesTransparencyPercentageNumericUpDown.Enabled =
              !(oneOfCheckBoxesAboutSettingsValuesForAllSubtitlesIsChecked && EnabledAndChecked(transparencyCheckBox));

            FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
              SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
              ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
              FourthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
              FifthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Enabled =
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
            if (EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(ThirdRussianSubtitlesMarginNumericUpDown, SecondRussianSubtitlesMarginNumericUpDown.Value + (2 * SecondRussianSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }

        private void SecondRussianSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Если включен "Перемещать попарно" — двигаем еще 3-и русские
            if (EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox))
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
                if (EnabledAndChecked(SetTheSameValuesForAllSubtitlesCheckBox))
                {
                    FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                        SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                        ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                        FourthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =
                        FifthRussianSubtitlesShadowTransparencyPercentageNumericUpDown.Value =

                        OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                }
                else if (EnabledAndChecked(ChangeOnTheSameDeltaValuesForAllSubtitlesCheckBox))
                {
                    // В тэге NumericUpDown лежит старое значение. Надеюсь.
                    var oldValue = (decimal)OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Tag;
                    var newValue = OriginalSubtitlesShadowTransparencyPercentageNumericUpDown.Value;
                    var delta = newValue - oldValue;

                    AddValueToNumericUpDownSafe(FirstRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(SecondRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(ThirdRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FourthRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                    AddValueToNumericUpDownSafe(FifthRussianSubtitlesShadowTransparencyPercentageNumericUpDown, delta);
                }
            }
        }

        private void ThirdRussianSubtitlesTransparencyPercentageNumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void OriginalSubtitlesFontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FourthRussianSubtitlesMarginNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Если включен "Перемещать попарно" — двигаем еще 5-е русские
            if (EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(FifthRussianSubtitlesMarginNumericUpDown, FourthRussianSubtitlesMarginNumericUpDown.Value + (2 * FourthRussianSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }

        private void FourthRussianSubtitlesSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            // Если включен "Перемещать попарно" — двигаем еще 5-е русские
            if (EnabledAndChecked(ChangeMarginsToPairSubtitlesCheckBox))
            {
                SetValueToNumericUpDownFromCodeSafe(FifthRussianSubtitlesMarginNumericUpDown, FourthRussianSubtitlesMarginNumericUpDown.Value + (2 * FourthRussianSubtitlesSizeNumericUpDown.Value) + 2);
            }
        }
    }
}
