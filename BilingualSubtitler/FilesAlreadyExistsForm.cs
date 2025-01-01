using Nikse.SubtitleEdit.Core.AudioToText;
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
    public partial class FilesAlreadyExistsForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool RewriteExistingFiles { get; private set; }

        public FilesAlreadyExistsForm(string savedFileName, string bilingualSubtitlesSavedFileName = null)
        {
            InitializeComponent();

            RewriteExistingFiles = false;

            fileOrFilesLabel.Text = bilingualSubtitlesSavedFileName == null ? "Файл уже существует:" 
                : "Файлы уже существуют:";

            fileNameLabel.MaximumSize = new Size(this.ClientSize.Width - 30, 0);
            fileNameLabel.AutoSize = true;

            fileNameLabel.Text = savedFileName;

            var bottomOfTheText = fileNameLabel.Bottom;

            if (bilingualSubtitlesSavedFileName != null)
            {
                var bilingualFileNameLabel = new Label();
                bilingualFileNameLabel.MaximumSize = fileNameLabel.MaximumSize;
                bilingualFileNameLabel.AutoSize = fileNameLabel.AutoSize;
                bilingualFileNameLabel.Location = new Point(fileNameLabel.Left, bottomOfTheText + 10);
                bilingualFileNameLabel.Parent = fileNameLabel.Parent;
                fileNameLabel.Parent.Controls.Add(bilingualFileNameLabel);

                var bilingualFileDotLabel = new Label();
                bilingualFileDotLabel.Size = dotLabel.Size;
                bilingualFileDotLabel.Location = new Point(dotLabel.Left, bottomOfTheText + 10);
                bilingualFileDotLabel.Text = dotLabel.Text;
                bilingualFileDotLabel.Parent = dotLabel.Parent;
                dotLabel.Parent.Controls.Add(bilingualFileDotLabel);

                bilingualFileNameLabel.Text = bilingualSubtitlesSavedFileName;

                bilingualFileNameLabel.Show();
                bilingualFileDotLabel.Show();

                bottomOfTheText = bilingualFileNameLabel.Bottom;
            }

            // Перестановки
            rewriteLabel.Top= bottomOfTheText + 10;
            bottomOfTheText = rewriteLabel.Bottom;
            noButton.Top = yesButton.Top = bottomOfTheText + 25;
            this.ClientSize = new System.Drawing.Size(this.Width, noButton.Bottom + 10);

            yesButton.Select();
            this.CenterToParent();

            System.Media.SystemSounds.Exclamation.Play();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            RewriteExistingFiles = true;

            this.Close();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            RewriteExistingFiles = false;

            this.Close();
        }
    }
}
