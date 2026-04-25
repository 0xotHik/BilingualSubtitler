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
        private bool m_audio;
        private List<Button> buttons;
        private string mkvtollnixOutput;
        private Color previousButtonColor;

        public int SelectedTrackNumber;
        public Tuple<string, string, string> SelectedTrackNumberLangAndTitle;

        public TrackToExtractFromMKVForm(List<MatroskaTrackInfo> tracks, bool audio = false)
        {
            InitializeComponent();

            if (!audio)
                ArgsRichTextBox.Hide();

            m_audio = audio;

            buttons = new List<Button> { buttonOk, buttonCancel };

            mkvTracksDGW.RowHeadersVisible = false;
            //
            mkvTracksDGW.Columns[0].Width = "99".Length * 20;
            mkvTracksDGW.Columns[1].Width = "99".Length * 60;
            //dataGridViewSubTracks.Columns[3].Width = "99".Length * 60;
            mkvTracksDGW.Columns[2].Width = (mkvTracksDGW.Width
                - mkvTracksDGW.Columns[0].Width
                - mkvTracksDGW.Columns[1].Width
                //- dataGridViewSubTracks.Columns[3].Width
                );
            //
            mkvTracksDGW.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            mkvTracksDGW.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            mkvTracksDGW.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            mkvTracksDGW.CellDoubleClick += DataGridViewSubTracks_CellDoubleClick;

            mkvTracksDGW.DefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;

            for (int i = 0; i < tracks.Count; i++)
            {
                var track = tracks[i];

                //Пишем в датаГрид
                mkvTracksDGW.Rows.Add(track.TrackNumber, track.Language, track.Name);

                if (audio)
                {
                    if (track.IsAudio == false)
                        mkvTracksDGW.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(181, 196, 208);
                }
                else
                {
                    if (track.CodecId != "S_TEXT/UTF8")
                        mkvTracksDGW.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(181, 196, 208);
                }

                //dataGridViewSubTracks.Rows[i].Cells[0].Value = track.TrackNumber;
                //dataGridViewSubTracks.Rows[i].Cells[1].Value = track.Language;
                //dataGridViewSubTracks.Rows[i].Cells[2].Value = track.Name;

                mkvTracksDGW.Rows[i].Tag = new Tuple<int, string>(track.TrackNumber, track.CodecId);
            }
        }


        /// <summary>
        /// A workaround for showing a form on the foreground and with focus,
        /// even if it is run by a process other than the main one.
        /// Зачем — почему-то вечно создавалась позади МейнФормы
        /// UPD 2023.01.06 — всё еще проблемы с этим
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
                    mkvTracksDGW.Rows.Add();
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
                mkvTracksDGW.Rows[strings].Cells[0].Value = ID;
                mkvTracksDGW.Rows[strings].Cells[1].Value = lang;
                mkvTracksDGW.Rows[strings].Cells[2].Value = trackName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            FormClosingWithCodecCheckAndSuccess();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void DataGridViewSubTracks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            mkvTracksDGW.Rows[e.RowIndex].Selected = true;
            FormClosingWithCodecCheckAndSuccess();
        }

        private void FormClosingWithCodecCheckAndSuccess()
        {
            Tuple<int, string> selectedRowTagContent = ((Tuple<int, string>)mkvTracksDGW.Rows[mkvTracksDGW.CurrentRow.Index].Tag);
            SelectedTrackNumber = selectedRowTagContent.Item1;
            var selectedTrackCodecId = selectedRowTagContent.Item2;

            // TODO
            if (m_audio) // TODO TEMP audio
            {
                var dialogResult = MessageBox.Show(ArgsRichTextBox.Text, String.Empty, buttons: MessageBoxButtons.OKCancel, icon: MessageBoxIcon.Question);
                if (dialogResult != DialogResult.OK)
                    return;
            }
            else
            {
                var desiredSubtitlesType = "S_TEXT/UTF8";
                if (selectedTrackCodecId != desiredSubtitlesType)
                {
                    var result = MessageBox.Show($"Тип данных субтитров — {selectedTrackCodecId} — отличается от совместимого с Bilingual Subtitler — {desiredSubtitlesType}.\nВсё равно продолжить с данными субтитрами?\n\n\n\n(Вы можете извлечь данные субтитры и сохранить их в формате SubRipText (srt) в стороннем приложении — например, Subtitle Edit)", string.Empty, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);

                    if (result != DialogResult.Yes)
                        return;
                }
            }

            var cells = mkvTracksDGW.Rows[mkvTracksDGW.CurrentRow.Index].Cells;
            SelectedTrackNumberLangAndTitle = new Tuple<string, string, string>($"{cells[0].Value}", $"{cells[1].Value}", $"{cells[2].Value ?? string.Empty}");

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
