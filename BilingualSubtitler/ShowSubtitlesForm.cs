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
    public partial class ShowSubtitlesForm : Form
    {
        public ShowSubtitlesForm(
            Subtitle[] originalSubtitles,
            Subtitle[] firstRussianSubtitles,
            Subtitle[] secondRussianSubtitles,
            Subtitle[] thirdRussianSubtitles)
        {
            InitializeComponent();

            SetStyleForEachDataGridView(new List<DataGridView> {

            originalSubtitlesDataGridView,
            firstRussianSubtitlesDataGridView,
            secondRussianSubtitlesDataGridView,
            thirdRussianSubtitlesDataGridView
            });

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
            //+
            var textColumnForFirstRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();
            var textColumnForSecondRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();
            var textColumnForThirdRussianSubtitles = (DataGridViewColumn)textColumnForOriginalSubtitles.Clone();

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

            originalSubtitlesDataGridView.AllowUserToAddRows = false; 

            PrintSubtitles(originalSubtitles, originalSubtitlesDataGridView);
            PrintSubtitles(firstRussianSubtitles, firstRussianSubtitlesDataGridView);
            PrintSubtitles(secondRussianSubtitles, secondRussianSubtitlesDataGridView);
            PrintSubtitles(thirdRussianSubtitles, thirdRussianSubtitlesDataGridView);

            SetStyleForEachDataGridView(new List<DataGridView> {

            originalSubtitlesDataGridView,
            firstRussianSubtitlesDataGridView,
            secondRussianSubtitlesDataGridView,
            thirdRussianSubtitlesDataGridView
            });

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
                    dataGridView.Rows.Add(
                        $"{subtitles[i].Start.ToString(timeFormat)}\n-->\n{subtitles[i].End.ToString(timeFormat)}",
                        subtitles[i].Text);
                }
            }
        }


        private bool ThereIsSubtitles(Subtitle[] subtitlesArrayInQuestion)
        {
            if ((subtitlesArrayInQuestion != null) && (subtitlesArrayInQuestion.Length > 0))
                return true;

            return false;
        }
    }
}
