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
    public partial class AppModeWasChangedToAdvancedForm : Form
    {
        public int? SettedRussianSubtitlesStreamToSetConsolasTo = null;
        public int? SettedRussianSubtitlesStreamToSetUndelineTo = null;

        public AppModeWasChangedToAdvancedForm()
        {
            InitializeComponent();

            button2.Focus();
            button2.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettedRussianSubtitlesStreamToSetConsolasTo = int.Parse(numericUpDown1.Value.ToString());
            SettedRussianSubtitlesStreamToSetUndelineTo = int.Parse(numericUpDown2.Value.ToString());

            Properties.Settings.Default.SecondRussianSubtitlesIsVisible = true;
            Properties.Settings.Default.ThirdRussianSubtitlesIsVisible = true;
            //
            Properties.Settings.Default.Save();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SettedRussianSubtitlesStreamToSetConsolasTo = null;
            SettedRussianSubtitlesStreamToSetUndelineTo = null;
            this.Close();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
