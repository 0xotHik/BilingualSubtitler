using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace BilingualSubtitler
{
    enum ChangeTimingTo
    {
        Plus,
        Minus
    }

    public partial class ShowSubtitlesForm : Form
    {
        Dictionary<SubtitlesType, SubtitlesAndInfo> m_subtitlesAndInfos;

        private List<DataGridView> m_dataGridViews;
        private Subtitle[] m_originalSubtitles;
        private Subtitle[] m_firstRussianSubtitles;
        private Subtitle[] m_secondRussianSubtitles;
        private Subtitle[] m_thirdRussianSubtitles;
        private Subtitle[] m_fourthRussianSubtitles;
        private Subtitle[] m_fifthRussianSubtitles;

        public ShowSubtitlesForm(Dictionary<SubtitlesType, SubtitlesAndInfo> subtitlesAndInfos)
        {
            InitializeComponent();

            m_subtitlesAndInfos = subtitlesAndInfos;

            m_originalSubtitles = subtitlesAndInfos[SubtitlesType.Original].Subtitles;
            m_firstRussianSubtitles = subtitlesAndInfos[SubtitlesType.FirstRussian].Subtitles;
            m_secondRussianSubtitles = subtitlesAndInfos[SubtitlesType.SecondRussian].Subtitles;
            m_thirdRussianSubtitles = subtitlesAndInfos[SubtitlesType.ThirdRussian].Subtitles;
            m_fourthRussianSubtitles = subtitlesAndInfos[SubtitlesType.FourthRussian].Subtitles;
            m_fifthRussianSubtitles = subtitlesAndInfos[SubtitlesType.FifthRussian].Subtitles;

            m_dataGridViews = (new List<DataGridView> {

            originalSubtitlesDataGridView,
            firstRussianSubtitlesDataGridView,
            secondRussianSubtitlesDataGridView,
            thirdRussianSubtitlesDataGridView,
            fourthRussianSubtitlesDataGridView,
            fifthRussianSubtitlesDataGridView
            });

            showSubtitlesOnlyFirstWordsCheckBox.Checked = Properties.Settings.Default.ShowSubtitlesOnlyFirstWords;
            showSubtitlesOnlyFirstWordsCountNumericUpDown.Value = Properties.Settings.Default.ShowSubtitlesOnlyFirstWordsCount;
            //
            showSubtitlesOnlyFirstWordsCountNumericUpDown.Enabled = showSubtitlesOnlyFirstWordsCheckBox.Checked;
            // TODO Сделать бы просто единый SetFormAccordingToSettings
            //+
            showSubtitlesOnlyFirstWordsCountNumericUpDown.ValueChanged += showSubtitlesOnlyFirstWordsCountNumericUpDown_ValueChanged;
            showSubtitlesOnlyFirstWordsCheckBox.CheckedChanged += showSubtitlesOnlyFirstWordsCheckBox_CheckedChanged;


            SetStyleForEachDataGridView(m_dataGridViews);

            var timingColumnForOriginalSubtitles = new DataGridViewColumn();
            timingColumnForOriginalSubtitles.HeaderText = "Тайминг"; //текст в шапке
            timingColumnForOriginalSubtitles.Width = TextRenderer.MeasureText("00:00:59,804", originalSubtitlesDataGridView.DefaultCellStyle.Font).Width + 10;
            //
            //
            timingColumnForOriginalSubtitles.ReadOnly = true;
            timingColumnForOriginalSubtitles.CellTemplate = new DataGridViewTextBoxCell();


            var textColumnForOriginalSubtitles = new DataGridViewColumn();
            textColumnForOriginalSubtitles.HeaderText = "Текст";
            textColumnForOriginalSubtitles.CellTemplate = new DataGridViewTextBoxCell();
            textColumnForOriginalSubtitles.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            var timingColumnForFirstRussianSubtitles = (DataGridViewColumn)timingColumnForOriginalSubtitles.Clone();
            var timingColumnForSecondRussianSubtitles = (DataGridViewColumn)timingColumnForOriginalSubtitles.Clone();
            var timingColumnForThirdRussianSubtitles = (DataGridViewColumn)timingColumnForOriginalSubtitles.Clone();
            var timingColumnForFourthRussianSubtitles = (DataGridViewColumn)timingColumnForOriginalSubtitles.Clone();
            var timingColumnForFifthRussianSubtitles = (DataGridViewColumn)timingColumnForOriginalSubtitles.Clone();

            //+
            var textColumnForFirstRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();
            var textColumnForSecondRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();
            var textColumnForThirdRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();
            var textColumnForFourthRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();
            var textColumnForFifthRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();


            // Столбцы
            originalSubtitlesDataGridView.Columns.Add(timingColumnForOriginalSubtitles);
            originalSubtitlesDataGridView.Columns.Add(textColumnForOriginalSubtitles);
            //
            firstRussianSubtitlesDataGridView.Columns.Add(timingColumnForFirstRussianSubtitles);
            firstRussianSubtitlesDataGridView.Columns.Add(textColumnForFirstRussianSubtitles);
            //
            secondRussianSubtitlesDataGridView.Columns.Add(timingColumnForSecondRussianSubtitles);
            secondRussianSubtitlesDataGridView.Columns.Add(textColumnForSecondRussianSubtitles);
            //
            thirdRussianSubtitlesDataGridView.Columns.Add(timingColumnForThirdRussianSubtitles);
            thirdRussianSubtitlesDataGridView.Columns.Add(textColumnForThirdRussianSubtitles);
            //
            fourthRussianSubtitlesDataGridView.Columns.Add(timingColumnForFourthRussianSubtitles);
            fourthRussianSubtitlesDataGridView.Columns.Add(textColumnForFourthRussianSubtitles);
            //
            fifthRussianSubtitlesDataGridView.Columns.Add(timingColumnForFifthRussianSubtitles);
            fifthRussianSubtitlesDataGridView.Columns.Add(textColumnForFifthRussianSubtitles);

            originalSubtitlesDataGridView.AllowUserToAddRows = false;

            PrintSubtitles(m_originalSubtitles, originalSubtitlesDataGridView);
            PrintSubtitles(m_firstRussianSubtitles, firstRussianSubtitlesDataGridView);
            PrintSubtitles(m_secondRussianSubtitles, secondRussianSubtitlesDataGridView);
            PrintSubtitles(m_thirdRussianSubtitles, thirdRussianSubtitlesDataGridView);
            PrintSubtitles(m_fourthRussianSubtitles, fourthRussianSubtitlesDataGridView);
            PrintSubtitles(m_fifthRussianSubtitles, fifthRussianSubtitlesDataGridView);

            SetStyleForEachDataGridView(m_dataGridViews);

            ShowOnlyFirstWordsOrAllOfTheWordsFromSubtitles();

            timingDeltaNumericUpDown.Value = Properties.Settings.Default.ChangeSubtitlesTimingDelta;

            SetSubtitlesTimelineTrack(SubtitlesType.Original, 0);
            SetSubtitlesTimelineTrack(SubtitlesType.FirstRussian, 1);
            SetSubtitlesTimelineTrack(SubtitlesType.SecondRussian, 2);
            SetSubtitlesTimelineTrack(SubtitlesType.ThirdRussian, 3);
            SetSubtitlesTimelineTrack(SubtitlesType.FourthRussian, 4);
            SetSubtitlesTimelineTrack(SubtitlesType.FifthRussian, 5);
            //
            RefreshTimelineKeepView();
            subtitleTimelineControl.Focus();

        }

        private void SetSubtitlesTimelineTrack(SubtitlesType type, int idOnTimeline)
        {
            var currentSubtitles = m_subtitlesAndInfos[type];
            if (ThereAreSubtitles(currentSubtitles.Subtitles))
            {
                subtitleTimelineControl.Tracks[idOnTimeline] = new()
                {
                    Subtitles = currentSubtitles.Subtitles,
                    Color = currentSubtitles.ColorPickingButton.BackColor
                };
            }
        }

        private void SetStyleForEachDataGridView(List<DataGridView> dataGridViews)
        {
            foreach (var dataGridView in dataGridViews)
            {
                dataGridView.AllowUserToAddRows = false;
                dataGridView.RowHeadersVisible = false;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                dataGridView.RowTemplate.Height = TextRenderer.MeasureText("00:00:59,804\n-->\n00:00:59,804", dataGridView.DefaultCellStyle.Font).Width;

                //dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                //foreach (DataGridViewColumn column in dataGridView.Columns)
                //{
                //    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                //    column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                //}
            }
        }

        private void PrintSubtitles(Subtitle[] subtitles, DataGridView dataGridView)
        {
            var timeFormat = @"hh\:mm\:ss\,fff";
            // 1. Сохраняем позицию и выделение
            int firstDisplayed = -1;
            int selectedIndex = -1;

            if (dataGridView.FirstDisplayedScrollingRowIndex >= 0)
                firstDisplayed = dataGridView.FirstDisplayedScrollingRowIndex;

            if (dataGridView.CurrentCell != null)
                selectedIndex = dataGridView.CurrentCell.RowIndex;

            // 2. Отключаем временно автообновление
            dataGridView.SuspendLayout();

            dataGridView.Rows.Clear();

            if (subtitles != null)
            {
                for (int i = 0; i < subtitles.Length; i++)
                {
                    //Добавляем строку, указывая значения колонок по очереди слева направо
                    var index = dataGridView.Rows.Add(
                        $"{subtitles[i].Start.ToString(timeFormat)}\n-->\n{subtitles[i].End.ToString(timeFormat)}",
                        subtitles[i].Text);

                    // Для того, чтобы было попроще с "Показывать n слов от субтитра" — сложим в тэг текст
                    dataGridView.Rows[index].Tag = subtitles[i].Text;
                }
            }

            // 4. Восстанавливаем позицию
            if (firstDisplayed >= 0 && firstDisplayed < dataGridView.RowCount)
                dataGridView.FirstDisplayedScrollingRowIndex = firstDisplayed;

            if (selectedIndex >= 0 && selectedIndex < dataGridView.RowCount)
                dataGridView.CurrentCell = dataGridView.Rows[selectedIndex].Cells[0];

            // 5. Возобновляем прорисовку
            dataGridView.ResumeLayout();
        }


        private bool ThereAreSubtitles(Subtitle[] subtitlesArrayInQuestion)
        {
            if ((subtitlesArrayInQuestion != null) && (subtitlesArrayInQuestion.Length > 0))
                return true;

            return false;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            // e.NewValue — похоже, в процентах координата верха vScroll'а

            foreach (var dataGridView in m_dataGridViews)
            {
                var newFirstDisplayedScrollingRowIndex = (dataGridView.RowCount * e.NewValue) / 100;
                if (dataGridView.RowCount > newFirstDisplayedScrollingRowIndex)
                    dataGridView.FirstDisplayedScrollingRowIndex = newFirstDisplayedScrollingRowIndex;

                // OutOfRangeEx ↓
                //dataGridView.FirstDisplayedScrollingRowIndex = dataGridView.RowCount;
            }
        }

        private void showSubtitlesOnlyFirstWordsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowSubtitlesOnlyFirstWords = showSubtitlesOnlyFirstWordsCheckBox.Checked;
            Properties.Settings.Default.Save();

            ShowOnlyFirstWordsOrAllOfTheWordsFromSubtitles();
        }

        private void ShowOnlyFirstWordsOrAllOfTheWordsFromSubtitles()
        {
            if (showSubtitlesOnlyFirstWordsCheckBox.Checked)
            {
                ShowOnlyThisMuchFirstWords(int.Parse(showSubtitlesOnlyFirstWordsCountNumericUpDown.Value.ToString()));

                showSubtitlesOnlyFirstWordsCountNumericUpDown.Enabled = true;
            }
            else
            {
                foreach (var dataGridView in m_dataGridViews)
                {
                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        var row = dataGridView.Rows[i];
                        var text = (string)row.Tag;
                        ((DataGridViewTextBoxCell)dataGridView[1, i]).Value = text;
                    }
                }

                showSubtitlesOnlyFirstWordsCountNumericUpDown.Enabled = false;
            }
        }

        private void showSubtitlesOnlyFirstWordsCountNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var intWordsCount = int.Parse(showSubtitlesOnlyFirstWordsCountNumericUpDown.Value.ToString()); // Хотелось сделать побыстрей

            Properties.Settings.Default.ShowSubtitlesOnlyFirstWordsCount = intWordsCount;
            Properties.Settings.Default.Save();

            ShowOnlyThisMuchFirstWords(intWordsCount);
        }

        private void ShowOnlyThisMuchFirstWords(int count)
        {
            foreach (var dataGridView in m_dataGridViews)
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    var row = dataGridView.Rows[i];
                    var text = ((string)row.Tag).Replace("\n", string.Empty).Replace("\r\n", string.Empty);
                    var words = text.Split(' ');
                    var newText = string.Empty;

                    for (int j = 0;
                        j < words.Length && j < count;
                        j++)
                    {
                        newText += $"{words[j]} ";
                    }

                    ((DataGridViewTextBoxCell)dataGridView[1, i]).Value = newText;
                }
            }
        }

        private void ChangeTiming(Subtitle[] subtitles, DataGridView dataGridView, TimeSpan delta, ChangeTimingTo direction)
        {
            foreach (var subtitle in subtitles)
            {
                if (direction == ChangeTimingTo.Plus)
                {
                    subtitle.Start += delta;
                    subtitle.End += delta;
                }
                else
                {
                    subtitle.Start -= delta;
                    subtitle.End -= delta;
                }
            }

            PrintSubtitles(subtitles, dataGridView);
            RefreshTimelineKeepView();
        }

        private void RefreshTimelineKeepView()
        {
            var subtitlesAndInfos = m_subtitlesAndInfos;

            var currentSubtitles = subtitlesAndInfos[SubtitlesType.Original];
            subtitleTimelineControl.Tracks[0].Subtitles = currentSubtitles.Subtitles;
            currentSubtitles = subtitlesAndInfos[SubtitlesType.FirstRussian];
            subtitleTimelineControl.Tracks[1].Subtitles = currentSubtitles.Subtitles;
            currentSubtitles = subtitlesAndInfos[SubtitlesType.SecondRussian];
            subtitleTimelineControl.Tracks[2].Subtitles = currentSubtitles.Subtitles;
            currentSubtitles = subtitlesAndInfos[SubtitlesType.ThirdRussian];
            subtitleTimelineControl.Tracks[3].Subtitles = currentSubtitles.Subtitles;
            currentSubtitles = subtitlesAndInfos[SubtitlesType.FourthRussian];
            subtitleTimelineControl.Tracks[4].Subtitles = currentSubtitles.Subtitles;
            currentSubtitles = subtitlesAndInfos[SubtitlesType.FifthRussian];
            subtitleTimelineControl.Tracks[5].Subtitles = currentSubtitles.Subtitles;

            subtitleTimelineControl.RefreshTimelineKeepView();
        }

        private void originalSubtitlesPlusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_originalSubtitles;
            var currentSubtitlesDataGridView = originalSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Plus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void originalSubtitlesMinusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_originalSubtitles;
            var currentSubtitlesDataGridView = originalSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Minus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void firstRussianSubtitlesPlusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_firstRussianSubtitles;
            var currentSubtitlesDataGridView = firstRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Plus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void firstRussianSubtitlesMinusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_firstRussianSubtitles;
            var currentSubtitlesDataGridView = firstRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Minus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void secondRussianSubtitlesPlusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_secondRussianSubtitles;
            var currentSubtitlesDataGridView = secondRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Plus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void secondRussianSubtitlesMinusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_secondRussianSubtitles;
            var currentSubtitlesDataGridView = secondRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Minus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void thirdRussianSubtitlesPlusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_thirdRussianSubtitles;
            var currentSubtitlesDataGridView = thirdRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Plus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void thirdRussianSubtitlesMinusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_thirdRussianSubtitles;
            var currentSubtitlesDataGridView = thirdRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Minus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void fourthRussianSubtitlesPlusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_fourthRussianSubtitles;
            var currentSubtitlesDataGridView = fourthRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Plus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void fourthRussianSubtitlesMinusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_fourthRussianSubtitles;
            var currentSubtitlesDataGridView = fourthRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Minus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void fifthRussianSubtitlesPlusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_fifthRussianSubtitles;
            var currentSubtitlesDataGridView = fifthRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Plus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void fifthRussianSubtitlesMinusTimeButton_Click(object sender, EventArgs e)
        {
            var currentSubtitles = m_fifthRussianSubtitles;
            var currentSubtitlesDataGridView = fifthRussianSubtitlesDataGridView;
            var timespan = TimeSpan.FromMilliseconds(Convert.ToInt64(timingDeltaNumericUpDown.Value));
            var direction = ChangeTimingTo.Minus;

            ChangeTiming(currentSubtitles, currentSubtitlesDataGridView, timespan, direction);
        }

        private void ShowSubtitlesForm_Load(object sender, EventArgs e)
        {

        }

        private void timingDeltaNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ChangeSubtitlesTimingDelta = Convert.ToUInt64(timingDeltaNumericUpDown.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
