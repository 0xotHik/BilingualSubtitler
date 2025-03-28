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
    public partial class SaveFileReportSuccessAskToOpenInDefaultAppForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool NeedToOpenInDefaultApp { get; private set; }

        public SaveFileReportSuccessAskToOpenInDefaultAppForm(string savedFileName, 
            string bilingualSubtitlesSavedFileName = null,
            bool showOpenTranslatorButton = false)
        {
            InitializeComponent();

            openTranslatorButton.Visible = showOpenTranslatorButton;

            fileNameLabel.MaximumSize = new Size(this.ClientSize.Width - 10, 0);
            fileNameLabel.AutoSize = true;

            fileNameLabel.Text = $"• {savedFileName}";

            // Перестановки
            openFileInDefaultAppButton.Top = okButton.Top = fileNameLabel.Bottom + 50;
            this.ClientSize = new System.Drawing.Size(this.Width, openFileInDefaultAppButton.Bottom + 10);

            this.CenterToParent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            NeedToOpenInDefaultApp = false;

            this.Close();
        }

        private void openFileInDefaultAppButton_Click(object sender, EventArgs e)
        {
            NeedToOpenInDefaultApp = true;

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm.OpenUrl("https://translate.yandex.ru/doc");
        }
    }
}
