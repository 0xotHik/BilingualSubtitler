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
    public partial class SubtitlesSavedSuccessfullyForm : Form
    {
        public SubtitlesSavedSuccessfullyForm(string savedFileName, string bilingualSubtitlesSavedFileName = null)
        {
            InitializeComponent();

            fileOrFilesLabel.Text = bilingualSubtitlesSavedFileName == null ? "Субтитры были сохранены в файл:" 
                : "Субтитры были сохранены в файлы:";

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
            okButton.Top = bottomOfTheText + 25;
            this.ClientSize = new System.Drawing.Size(this.Width, okButton.Bottom + 10);

            this.CenterToParent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openFileInDefaultAppButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
