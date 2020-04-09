using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsInput.Native;

namespace BilingualSubtitler
{
    public class Hotkey
    {
        public string KeyValue;
        public int KeyCode;
        public VirtualKeyCode? ModifierKey = null;

        public Hotkey(string hotkeyString)
        {
            var hotkeyInfo = hotkeyString.Split('@');

            KeyValue = hotkeyInfo[0];
            KeyCode = int.Parse(hotkeyInfo[1]);

            if ((hotkeyInfo.Length == 3) && (!string.IsNullOrWhiteSpace(hotkeyInfo[2])))
            {
                switch (hotkeyInfo[2])
                {
                    case "SHIFT":
                        {
                            ModifierKey = VirtualKeyCode.SHIFT;
                            break;
                        }
                    case "MENU": // Alt
                        {
                            ModifierKey = VirtualKeyCode.MENU;
                            break;
                        }
                    case "CONTROL":
                        {
                            ModifierKey = VirtualKeyCode.CONTROL;
                            break;
                        }
                }


            }

        }

        public Hotkey(VirtualKeyCode hotkey, VirtualKeyCode? modifierKey = null)
        {
            KeyValue = hotkey.ToString();
            KeyCode = (int)hotkey;
            ModifierKey = modifierKey;
        }

        public Hotkey(string keyValue, int keyCode, VirtualKeyCode? modifierKey = null)
        {
            KeyValue = keyValue;
            KeyCode = (int)keyCode;
            ModifierKey = modifierKey;
        }

        public override string ToString()
        {
            return $"{KeyValue}@{KeyCode}@{ModifierKey}";
        }
    }
}
