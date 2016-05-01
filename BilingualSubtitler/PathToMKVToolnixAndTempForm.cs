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
    public partial class PathToMKVToolnixAndTempForm : Form
    {
        private List<Button> buttons; 
        public PathToMKVToolnixAndTempForm()
        {
            InitializeComponent();
        }

        private void PathToMKVToolnixAndTempForm_Load(object sender, EventArgs e)
        {
            buttons = new List<Button>();
            buttons.Add(buttonOk);
            buttons.Add(buttonCancel);

            foreach (var btn in buttons)
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
            }

            labelReason.Text = "";
            if ((Properties.Settings.Default.PathToMKVToolnixFolder == "") ||
                Properties.Settings.Default.PathTempSubs == "")
                labelReason.Text = "Пожалуйста, укажите папки, необходимые для работы программы.";
            else
            {
                if (System.IO.File.Exists(Properties.Settings.Default.PathToMKVToolnixFolder + "mkvmerge.exe"))
                    labelReason.Text += "В указанной вами папке с mkvtoolnix не содержится файла mkvmerge.exe";
                if (System.IO.File.Exists(Properties.Settings.Default.PathToMKVToolnixFolder + "mkvextract.exe"))
                    labelReason.Text += "В указанной вами папке с mkvtoolnix не содержится файла mkvextract.exe";
                textBoxPathToMKVToolnix.Text = Properties.Settings.Default.PathToMKVToolnixFolder;
                textBoxPathToTemp.Text = Properties.Settings.Default.PathTempSubs;

            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if ((textBoxPathToMKVToolnix.Text == "") ||
                (textBoxPathToTemp.Text == ""))
                MessageBox.Show("Пожалуйста, укажите папки, необходимые для работы программы",
                    "Пожалуйста, укажите папки", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
            {

                if (textBoxPathToMKVToolnix.Text.Substring(textBoxPathToMKVToolnix.Text.Length - 1, 1) != @"\")
                    textBoxPathToMKVToolnix.Text += @"\";
                if (textBoxPathToTemp.Text.Substring(textBoxPathToTemp.Text.Length - 1, 1) != @"\")
                    textBoxPathToTemp.Text += @"\";

                if (!System.IO.File.Exists(textBoxPathToMKVToolnix.Text + "mkvmerge.exe"))
                {
                    MessageBox.Show("В указанной вами папке с mkvtoolnix не содержится файла mkvmerge.exe",
                        "Отсутствует файл mkvmerge.exe",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (!System.IO.File.Exists(textBoxPathToMKVToolnix.Text + "mkvextract.exe"))
                {
                    MessageBox.Show("В указанной вами папке с mkvtoolnix не содержится файла mkvextract.exe",
                        "Отсутствует файл mkvextract.exe",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    Properties.Settings.Default.PathToMKVToolnixFolder = textBoxPathToMKVToolnix.Text;
                    Properties.Settings.Default.PathTempSubs = textBoxPathToTemp.Text;
                    Properties.Settings.Default.Save();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonBrowsePathToMKVToolnix_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogMKVToolnix.ShowDialog() == DialogResult.OK)
            {
                textBoxPathToMKVToolnix.Text = folderBrowserDialogMKVToolnix.SelectedPath;
                if (textBoxPathToMKVToolnix.Text.Substring(textBoxPathToMKVToolnix.Text.Length - 1, 1) != @"\")
                    textBoxPathToMKVToolnix.Text += @"\";

                if (!System.IO.File.Exists(textBoxPathToMKVToolnix.Text + "mkvmerge.exe"))
                {
                    MessageBox.Show("В указанной вами папке с mkvtoolnix не содержится файла mkvmerge.exe",
                        "Отсутствует файл mkvmerge.exe",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                if (!System.IO.File.Exists(textBoxPathToMKVToolnix.Text + "mkvextract.exe"))
                {
                    MessageBox.Show("В указанной вами папке с mkvtoolnix не содержится файла mkvextract.exe",
                        "Отсутствует файл mkvextract.exe",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void buttonBrowsePathToTemp_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialogTempSubs.ShowDialog() == DialogResult.OK)
            {
                textBoxPathToTemp.Text = folderBrowserDialogTempSubs.SelectedPath;
                if (textBoxPathToTemp.Text.Substring(textBoxPathToTemp.Text.Length - 1, 1) != @"\")
                    textBoxPathToTemp.Text += @"\";
            }
        }


    }
}
