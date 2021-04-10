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
    public partial class FileToUseFromZipForm : Form
    {
        private List<Button> buttons; 
        private Color previousButtonColor;

        public string SelectedFileName;

        public FileToUseFromZipForm(List<string> filesNames)
        {
            InitializeComponent();

            buttons = new List<Button> {buttonOk, buttonCancel};

            dataGridViewFilesInAcrhive.RowHeadersVisible = false;
            dataGridViewFilesInAcrhive.Columns[1].Width = "Расширения".Length * 
                10 // Магическое число, на глаз
                ;
            //dataGridViewFilesInAcrhive.Columns[0].Width = dataGridViewFilesInAcrhive.Width - dataGridViewFilesInAcrhive.Columns[1].Width;
            dataGridViewFilesInAcrhive.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewFilesInAcrhive.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridViewFilesInAcrhive.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dataGridViewFilesInAcrhive.DefaultCellStyle.ForeColor = SystemColors.ActiveCaptionText;

            for (int i=0; i < filesNames.Count; i++)
            {
                var fileName = filesNames[i];
                var indexOfLastDot = fileName.LastIndexOf('.');
                var fileNameWithoutExt = fileName.Substring(0, indexOfLastDot);
                var ext = fileName.Substring(indexOfLastDot, fileName.Length - fileNameWithoutExt.Length);

                //Пишем всё в датаГрид
                dataGridViewFilesInAcrhive.Rows.Add(fileNameWithoutExt, ext);
                dataGridViewFilesInAcrhive.Rows[i].Tag = fileName;
            }
        }

  
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

        

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SelectedFileName = (string)dataGridViewFilesInAcrhive.Rows[dataGridViewFilesInAcrhive.CurrentRow.Index].Tag;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
