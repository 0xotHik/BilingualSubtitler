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

namespace BilingualSubtitler
{
    public partial class ShowSubtitlesForm : Form
    {
        private List<DataGridView> m_dataGridViews;

        public ShowSubtitlesForm(
            Subtitle[] originalSubtitles,
            Subtitle[] firstRussianSubtitles,
            Subtitle[] secondRussianSubtitles,
            Subtitle[] thirdRussianSubtitles,
            Subtitle[] fourthRussianSubtitles,
            Subtitle[] fifthRussianSubtitles)
        {
            InitializeComponent();

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
            timingColumnForOriginalSubtitles.Width = TextRenderer.MeasureText("00:00:59,804", originalSubtitlesDataGridView.DefaultCellStyle.Font).Width+10;
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

            PrintSubtitles(originalSubtitles, originalSubtitlesDataGridView);
            PrintSubtitles(firstRussianSubtitles, firstRussianSubtitlesDataGridView);
            PrintSubtitles(secondRussianSubtitles, secondRussianSubtitlesDataGridView);
            PrintSubtitles(thirdRussianSubtitles, thirdRussianSubtitlesDataGridView);
            PrintSubtitles(fourthRussianSubtitles, fourthRussianSubtitlesDataGridView);
            PrintSubtitles(fifthRussianSubtitles, fifthRussianSubtitlesDataGridView);

            SetStyleForEachDataGridView(m_dataGridViews);

            ShowOnlyFirstWordsOrAllOfTheWordsFromSubtitles();

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
        }


        private bool ThereIsSubtitles(Subtitle[] subtitlesArrayInQuestion)
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
                if (dataGridView.RowCount> newFirstDisplayedScrollingRowIndex)
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
    }
}
