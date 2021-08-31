using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nikse.SubtitleEdit.Core.ContainerFormats.Matroska;

namespace BilingualSubtitler
{
    public partial class TrackToExtractFromMKVForm : Form
    {
        private List<Button> buttons;
        private string mkvtollnixOutput;
        private Color previousButtonColor;

        public int SelectedTrackNumber;
        public Tuple<string, string, string> SelectedTrackIdLangTitle;

        public TrackToExtractFromMKVForm(List<MatroskaTrackInfo> tracks)
        {
            InitializeComponent();

            buttons = new List<Button> { buttonOk, buttonCancel };

            //foreach (var btn in buttons)
            //{
            //    btn.FlatAppearance.BorderSize = 0;
            //    btn.FlatStyle = FlatStyle.Flat;
            //}

            dataGridViewSubTracks.RowHeadersVisible = false;
            dataGridViewSubTracks.Columns[0].Width = "99".Length * 20;
            dataGridViewSubTracks.Columns[1].Width = "99".Length * 60;
            dataGridViewSubTracks.Columns[2].Width = (dataGridViewSubTracks.Width - dataGridViewSubTracks.Columns[0].Width - dataGridViewSubTracks.Columns[1].Width);
            dataGridViewSubTracks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewSubTracks.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewSubTracks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewSubTracks.CellDoubleClick += DataGridViewSubTracks_CellDoubleClick;

            ////Синий фон у кнопок при наведении курсора
            //foreach (var btn in buttons)
            //{
            //    btn.MouseEnter += btn_MouseEnter;
            //    btn.MouseLeave += btn_MouseLeave;
            //}

            dataGridViewSubTracks.DefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;

            for (int i = 0; i < tracks.Count; i++)
            {
                var track = tracks[i];

                //Пишем всё в датаГрид
                dataGridViewSubTracks.Rows.Add(track.TrackNumber, track.Language, track.Name);
                //dataGridViewSubTracks.Rows[i].Cells[0].Value = track.TrackNumber;
                //dataGridViewSubTracks.Rows[i].Cells[1].Value = track.Language;
                //dataGridViewSubTracks.Rows[i].Cells[2].Value = track.Name;

                dataGridViewSubTracks.Rows[i].Tag = track.TrackNumber;
            }

            //TopMost = true; // 
        }


        /// <summary>
        /// A workaround for showing a form on the foreground and with focus,
        /// even if it is run by a process other than the main one.
        /// Зачем — почему-то вечно создавалась позади МейнФормы
        /// </summary>
        public DialogResult ShowDialogInForeground()
        {
            var form = this;

            //it's an hack, thanks to http://stackoverflow.com/a/1463479/505893
            form.WindowState = FormWindowState.Minimized;
            form.Shown += delegate (Object sender, EventArgs e)
            {
                ((Form)sender).WindowState = FormWindowState.Normal;
            };
            return form.ShowDialog();
        }

        //public TrackToExtractFromMKVForm(string _mkvtoolnixOutput)
        //{
        //    mkvtollnixOutput = _mkvtoolnixOutput;
        //    InitializeComponent();

        //    buttons = new List<Button>();
        //    buttons.Add(buttonOk);
        //    buttons.Add(buttonCancel);

        //    foreach (var btn in buttons)
        //    {
        //        btn.FlatAppearance.BorderSize = 0;
        //        btn.FlatStyle = FlatStyle.Flat;
        //    }

        //    dataGridViewSubTracks.RowHeadersVisible = false;
        //    dataGridViewSubTracks.Columns[0].Width = "99".Length * 20;
        //    dataGridViewSubTracks.Columns[1].Width = "99".Length * 60;
        //    dataGridViewSubTracks.Columns[2].Width = (dataGridViewSubTracks.Width - dataGridViewSubTracks.Columns[0].Width - dataGridViewSubTracks.Columns[1].Width);
        //    dataGridViewSubTracks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        //    dataGridViewSubTracks.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        //    dataGridViewSubTracks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        //    //Синий фон у кнопок при наведении курсора
        //    foreach (var btn in buttons)
        //    {
        //        btn.MouseEnter += btn_MouseEnter;
        //        btn.MouseLeave += btn_MouseLeave;
        //    }

        //    dataGridViewSubTracks.DefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;

        //    SeparateSubtitleTracks();
        //}

        private void TrackToExtractFromMKVForm_Load(object sender, EventArgs e)
        {

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
            FromClosingWithSuccess();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DataGridViewSubTracks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridViewSubTracks.Rows[e.RowIndex].Selected = true;
            FromClosingWithSuccess();
        }

        private void FromClosingWithSuccess()
        {
            SelectedTrackNumber = (int)dataGridViewSubTracks.Rows[dataGridViewSubTracks.CurrentRow.Index].Tag;

            var cells = dataGridViewSubTracks.Rows[dataGridViewSubTracks.CurrentRow.Index].Cells;
            SelectedTrackIdLangTitle = new Tuple<string, string, string>($"{cells[0].Value}", $"{cells[1].Value}", $"{cells[2].Value ?? string.Empty}");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
