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
    public partial class TrackToExtractFromMKVForm : Form
    {
        private List<Button> buttons; 
        private string mkvtollnixOutput;
        public string selectedID;
        private Color previousButtonColor;

        public TrackToExtractFromMKVForm(string _mkvtoolnixOutput)
        {
            mkvtollnixOutput = _mkvtoolnixOutput;
            InitializeComponent();
        }

        private void TrackToExtractFromMKVForm_Load(object sender, EventArgs e)
        {
            buttons = new List<Button>();
            buttons.Add(buttonOk);
            buttons.Add(buttonCancel);

            foreach (var btn in buttons)
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
            }

            dataGridViewSubTracks.RowHeadersVisible = false;
            dataGridViewSubTracks.Columns[0].Width = "99".Length * 20;
            dataGridViewSubTracks.Columns[1].Width = "99".Length * 60;
            dataGridViewSubTracks.Columns[2].Width = (dataGridViewSubTracks.Width - dataGridViewSubTracks.Columns[0].Width - dataGridViewSubTracks.Columns[1].Width);
            dataGridViewSubTracks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewSubTracks.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewSubTracks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //Синий фон у кнопок при наведении курсора
            foreach (var btn in buttons)
            {
                btn.MouseEnter += new EventHandler(btn_MouseEnter);
                btn.MouseLeave += new EventHandler(btn_MouseLeave);
            }

            this.dataGridViewSubTracks.DefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;

            SeparateSubtitleTracks();
        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            previousButtonColor = ((Button)sender).BackColor;
            ((Button)sender).BackColor = SystemColors.GradientInactiveCaption;

        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = previousButtonColor;
        }

        private void SeparateSubtitleTracks()
        {
            string[] tracks = mkvtollnixOutput.Split('\n');
            int strings = 0;
            for (int i = 0; i < tracks.Length; i++)
            {
                if (tracks[i].Contains("subtitles (SubRip/SRT)"))
                {
                    dataGridViewSubTracks.Rows.Add();
                    AddValues(tracks[i], strings);
                    strings++;

                }
            }
        }

        private void AddValues(string track, int strings)
        {
            try
            {
                //Выделяем ID
                string ID = track.Substring("Track ID".Length, (track.IndexOf(':') - "Track ID".Length));
                //Вырезаем строку до указания языка
                track = track.Substring(track.IndexOf("language:"));
                //Выделяем язык
                string lang = track.Substring("language:".Length, (track.IndexOf(' ') - "language:".Length));
                //Тоже самое в случае существования делаем с названием трека
                string trackName = "";
                if (track.Contains("track_name:"))
                {
                    track = track.Substring(track.IndexOf("track_name:"));

                    trackName = track.Substring("track_name:".Length, (track.IndexOf(' ') - "track_name:".Length));
                }
                //Пишем всё в датаГрид
                dataGridViewSubTracks.Rows[strings].Cells[0].Value = ID;
                dataGridViewSubTracks.Rows[strings].Cells[1].Value = lang;
                dataGridViewSubTracks.Rows[strings].Cells[2].Value = trackName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            selectedID = dataGridViewSubTracks.Rows[dataGridViewSubTracks.CurrentRow.Index].Cells[0].Value.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
