using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using BilingualSubtitler.Properties;
using YandexLinguistics.NET;

namespace BilingualSubtitler
{
    public partial class TranslationInProgressForm : Form
    {
        private string[] originalSubs;
        private bool flagIsTranslated = false;
        private int linesInOriginalSubs = 0;
        private MainForm form1;
        private List<Button> buttons;

        private string yandexTranslatorAPIKey =
            "trnsl.1.1.20150627T135643Z.b79b9a9564333355.08ab2bf3ca2a0e1d1bacd63175b724f0ff966559";

        private string currentColor = "ffff00";

        public TranslationInProgressForm(object parent_form)
        {
            form1 = parent_form as MainForm;
            InitializeComponent();
        }

        public TranslationInProgressForm(object parent_form, string[] _originalSubs)
        {
            form1 = parent_form as MainForm;
            InitializeComponent();
            originalSubs = _originalSubs;
        }

        private void TranslationInProgressForm_Load(object sender, EventArgs e)
        {
            buttons = new List<Button>();

            buttons.Add(buttonColorDialog);
            buttons.Add(button1);
            buttons.Add(buttonBackToMainForm);

            foreach (var btn in buttons)
            {
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
            }

        }

        public string YandexTranslator(string text)
        {
            string[] strings = text.Split(new Char[] { '\n' });
            string output = "";
            var translator =
                new Translator(yandexTranslatorAPIKey);

            progressBar1.Maximum = strings.Length;

            foreach (var str in strings)
            {
                if (str.Length != 0)
                {
                    try
                    {
                        //labelCurrentLine.Text = str;

                        var translation = translator.Translate(str, new LangPair(Lang.En, Lang.Ru), null, false);
                        output += translation.Text + '\n';
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Строка " + str +
                                        "была обработана неверно. \n Вместо перевода будет записан оригинальный текст. \n " +
                                        "Код ошибки: " + ex.Message);
                        output += str + '\n';
                    }

                    progressBar1.Value++;

                }
                else
                {
                    flagIsTranslated = true;
                    button1.Text = "Закрыть";
                    progressBar1.Value = progressBar1.Maximum;
                    FlashWindow.Flash(this);
                }
            }

            //Console.WriteLine(translation.Text + '\n' + translation.LangPair + '\n' + translation.Detected.Lang);

            return output;
        }


        private string ReadSubs()
        {
            try
            {
                string originalText = "";

                for (int i = 0; i != originalSubs.Length; i++)
                {
                    if ((originalSubs[i].Contains("-->")) && (i != originalSubs.Length - 1))
                    {
                        linesInOriginalSubs++;
                        i++;
                        int level = 0;
                        while (originalSubs[i].Length != 0)
                        {
                            {
                                originalText += originalSubs[i] + '\n';
                                if (i != originalSubs.Length - 1)
                                {
                                    i++;
                                    level++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                }

                return originalText;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return "";
            }

        }

        private void ConstructRusSub(string translatedText)
        {
            linesInOriginalSubs++; //Т.к. цикл обсчета русских субтитров не учитывает последний блок субтитров

            foreach (var btn in buttons)
            {
                //btn.Image = Resources._48pxGREYBackIcon;
            }

            this.Refresh();
            Thread.Sleep(500);

            try
            {
                for (int i = 0; i != originalSubs.Length; i++)
                {
                    if (((originalSubs[i].Contains("-->")) && (i != originalSubs.Length - 1)) &&
                        (translatedText.Length != 0))
                    {
                        //originalSubs[i - 1] = linesInOriginalSubs++.ToString();

                        i++;
                        while (originalSubs[i].Length != 0)
                        {
                            {
                                originalSubs[i] = "<font color=" + '"' + '#' + currentColor + '"' + ">" +
                                                  translatedText.Substring(0, translatedText.IndexOf('\n')) + "</font>";
                                translatedText = translatedText.Remove(0, translatedText.IndexOf('\n') + 1);
                                if ((i != originalSubs.Length - 1) && (translatedText.Length != 0))
                                {
                                    i++;
                                    //level++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!flagIsTranslated)
                ConstructRusSub(YandexTranslator(ReadSubs())); //Создаем русский подстрочник
            else 
                this.Close();

        }

        private void buttonColorDialog_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                currentColor = String.Format("{0:X2}{1:X2}{2:X2}", colorDialog.Color.R, colorDialog.Color.G,
                    colorDialog.Color.B);
            }
        }

        private void buttonBackToMainForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
