using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BilingualSubtitler
{
    class Hotkey
    {
        public string KeyData;
        public int KeyCode;
        public ModifierKeys ModifierKey;

        public Hotkey(string hotkeyString)
        {
            var hotkeyInfo = hotkeyString.Split('@');

            KeyData = hotkeyInfo[0];
            KeyCode = int.Parse(hotkeyInfo[1]);
        }
    }
}
