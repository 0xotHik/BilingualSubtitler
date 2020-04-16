using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;

namespace BilingualSubtitler
{
    public partial class HotkeysToAuthorsExtendedDefaultsForm : Form
    {


        public HotkeysToAuthorsExtendedDefaultsForm(bool onlyKeyWithoutModifiers = false)
        {
            InitializeComponent();
            }

        
        private void clearButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }


    }
}
