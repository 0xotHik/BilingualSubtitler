using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PathToMKVToolnixAndTempForm_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
