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
    public partial class HotkeySettingForm : Form
    {
        private const string DEFAULT_TEXT = "Нажмите клавишу / сочетание клавиш";

        private bool m_onlyKeyWithoutModifiers;

        public Hotkey SettedHotkey = null;

        public HotkeySettingForm(bool onlyKeyWithoutModifiers = false)
        {
            InitializeComponent();

            m_onlyKeyWithoutModifiers = onlyKeyWithoutModifiers;

            labelInfo.Text = DEFAULT_TEXT;
            this.KeyUp += KeySettingForm_KeyUpKeySetting;
            this.Select();
        }

        private void KeySettingForm_KeyUpKeySetting(object sender, KeyEventArgs e)
        {
            this.KeyUp -= KeySettingForm_KeyUpKeySetting;

            switch (e.Modifiers)
            {
                case Keys.None:
                    {
                        SetHotkey(e.KeyData, e.KeyValue);
                        break;
                    }
                case Keys.Shift:
                    {
                        if (m_onlyKeyWithoutModifiers)
                        {
                            labelInfo.Text =
                                "На данный момент для данной горячей клавиши не поддерживаются клавиши-модификаторы.\n" +
                                "Если у вас есть в этом потребность — пожалуйста, напишите автору программы.";
                            clearButton.Visible = true;
                        }
                        else
                            SetHotkey(e.KeyData, e.KeyValue, VirtualKeyCode.SHIFT);
                        break;
                    }
                case Keys.Alt:
                    {
                        if (m_onlyKeyWithoutModifiers)
                        {
                            labelInfo.Text =
                                "На данный момент для данной горячей клавиши не поддерживаются клавиши-модификаторы.\n" +
                                "Если у вас есть в этом потребность — пожалуйста, напишите автору программы.";
                            clearButton.Visible = true;
                        }
                        else
                            SetHotkey(e.KeyData, e.KeyValue, VirtualKeyCode.MENU);
                        break;
                    }
                case Keys.Control:
                    {
                        if (m_onlyKeyWithoutModifiers)
                        {
                            labelInfo.Text =
                                "На данный момент для данной горячей клавиши не поддерживаются клавиши-модификаторы.\n" +
                                "Если у вас есть в этом потребность — пожалуйста, напишите автору программы.";
                            clearButton.Visible = true;
                        }
                        else
                            SetHotkey(e.KeyData, e.KeyValue, VirtualKeyCode.CONTROL);
                        break;
                    }
                default:
                    {
                        labelInfo.Text =
                            "На данный момент поддерживается только одна клавиша-модификатор.\n" +
                            "Если у вас есть потребность в нескольких — пожалуйста, напишите автору программы.";
                        clearButton.Visible = true;
                        break;
                    }
            }

        }

        private void SetHotkey(Keys keyData, int keyValue, VirtualKeyCode? modifierKey = null)
        {
            var hotkey = modifierKey == null ? new Hotkey(keyData.ToString(), keyValue)
                : new Hotkey(keyData.ToString(), keyValue, modifierKey);
            SettedHotkey = hotkey;
            labelInfo.Text = hotkey.ToString();

            clearButton.Visible = okButton.Visible = true;
            okButton.Select();
            //this.KeyUp += KeySettingForm_KeyUpFormClosing;
        }



        private void clearButton_Click(object sender, EventArgs e)
        {
            clearButton.Visible = okButton.Visible = false;
            SettedHotkey = null;
            labelInfo.Text = DEFAULT_TEXT;
            //this.KeyUp -= KeySettingForm_KeyUpFormClosing;
            this.KeyUp += KeySettingForm_KeyUpKeySetting;

            this.Select();
            this.Focus();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void KeySettingForm_KeyUpFormClosing(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter) && (labelInfo.Text != DEFAULT_TEXT))
                okButton_Click(null, null);
        }
    }
}
