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
    public partial class YandexTranslatorAPIKeyForm : Form
    {
        private List<Button> buttons; 
        private DialogResult dr;
        private Color previousButtonColor;
        private bool flagKeyIsInvalid = false;
        public YandexTranslatorAPIKeyForm()
        {
            InitializeComponent();
        }

        public YandexTranslatorAPIKeyForm(bool keyIsInvalid)
        {
            flagKeyIsInvalid = true;
            InitializeComponent();
        }

        private void YandexTranslatorAPIKeyForm_Load(object sender, EventArgs e)
        {
            buttons = new List<Button>();
            buttons.Add(buttonOk);
            buttons.Add(buttonCancel);

            foreach (var btn in buttons)
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
            }

            if (flagKeyIsInvalid)
                richTextBoxLabelYouNeedToGetAPIKey.Text = "Введенный ключ неверен. Для использования данного приложения необходимо ввести валидный ключ к API сервиса " + '"' + "Яндекс.Переводчик" + '"' + ".";

            richTextBoxForYandexApiKeyInSeparateForm.Text = Properties.Settings.Default.YandexTranslatorAPIKey;

            //Синий фон у кнопок при наведении курсора
            foreach (var btn in buttons)
            {
                btn.MouseEnter += new EventHandler(btn_MouseEnter);
                btn.MouseLeave += new EventHandler(btn_MouseLeave);
            }

            richTextBoxForYandexApiKeyInSeparateForm.Select();

        }

        private void btn_MouseEnter(object sender, EventArgs e)
        {
            previousButtonColor = ((Button)sender).BackColor;
            ((Button)sender).BackColor = SystemColors.GradientInactiveCaption;

        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = previousButtonColor;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //Вычищаем символы переноса строки и пробелы из ключа
            int carrySymbols = richTextBoxForYandexApiKeyInSeparateForm.Text.Split('\n').Length - 1;
            int spaceSymbols = richTextBoxForYandexApiKeyInSeparateForm.Text.Split(' ').Length - 1;

            Properties.Settings.Default.YandexTranslatorAPIKey = richTextBoxForYandexApiKeyInSeparateForm.Text.Substring(0,richTextBoxForYandexApiKeyInSeparateForm.Text.Length-spaceSymbols-carrySymbols);
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void linkLabelGetAPIKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelGetAPIKey.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tech.yandex.ru/keys/get/?service=trnsl");
        }



        private void YandexTranslatorAPIKeyForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void buttonOk_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabelYandexAPIKeysList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.linkLabelYandexAPIKeysList.LinkVisited = true;
            System.Diagnostics.Process.Start("https://tech.yandex.ru/keys/");
        }


    }
}
