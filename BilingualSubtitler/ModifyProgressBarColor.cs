using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace BilingualSubtitler
{
    //Модифицирует прогресс-бар для придания ему других цветов.
    //Использование:
    //ModifyProgressBarColor.SetState(progressBar, 2);
    //1 — зеленый
    //2 — красный
    //3 — желтый
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
        public static void SetState(this ProgressBar pBar, string state)
        {
            int stateInt = 0;
            switch (state)
            {
                case "green":
                    stateInt = 1;
                    break;
                case "red":
                    stateInt = 2;
                    break;
                case "yellow":
                    stateInt = 3;
                    break;
            }
            SendMessage(pBar.Handle, 1040, (IntPtr)stateInt, IntPtr.Zero);
        }
    }
}
