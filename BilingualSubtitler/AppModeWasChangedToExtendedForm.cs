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
    public partial class AppModeWasChangedToExtendedForm : Form
    {
        public int? SettedRussianSubtitlesStreamToSetConsolasTo = null;
        public AppModeWasChangedToExtendedForm()
        {
            InitializeComponent();

            button2.Focus();
            button2.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettedRussianSubtitlesStreamToSetConsolasTo = int.Parse(numericUpDown1.Value.ToString());
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettedRussianSubtitlesStreamToSetConsolasTo = null;
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
