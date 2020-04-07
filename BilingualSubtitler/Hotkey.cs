using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsInput.Native;

namespace BilingualSubtitler
{
    class Hotkey
    {
        public string KeyData;
        public int KeyCode;
        public VirtualKeyCode? ModifierKey;

        public Hotkey(string hotkeyString)
        {
            var hotkeyInfo = hotkeyString.Split('@');

            KeyData = hotkeyInfo[0];
            KeyCode = int.Parse(hotkeyInfo[1]);
        }

        public Hotkey(VirtualKeyCode hotkey, VirtualKeyCode? modifierKey = null)
        {
            KeyData = hotkey.ToString();
            KeyCode = (int) hotkey;
            ModifierKey = modifierKey;
        }

        public override string ToString()
        {
            return $"{KeyData}@{KeyCode}@{ModifierKey}";
        }
    }
}
