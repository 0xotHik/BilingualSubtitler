using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BilingualSubtitler
{
    public partial class KeySettingForm : Form
    {
        [DllImport("user32.dll")]
        static extern bool SetKeyboardState(byte[] lpKeyState);
        [DllImport("user32.dll")]
        static extern bool GetKeyboardState(byte[] lpKeyState);

        public KeySettingForm()
        {
            InitializeComponent();
        }

        private void KeySettingForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var shift = new byte[256];
            GetKeyboardState(shift);

            File.WriteAllBytes("C:\\Users\\jenek\\source\\repos\\0xotHik\\" +
                               "BilingualSubtitler\\BilingualSubtitler\\bin\\Debug\\shift.dat", shift);

        }

        private void KeySettingForm_KeyDown(object sender, KeyEventArgs e)
        {
            var shift = new byte[256];
            GetKeyboardState(shift);

            File.WriteAllBytes("C:\\Users\\jenek\\source\\repos\\0xotHik\\" +
                               "BilingualSubtitler\\BilingualSubtitler\\bin\\Debug\\shiftDown.dat", shift);
        }
    }
}
