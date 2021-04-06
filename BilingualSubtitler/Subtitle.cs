using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace BilingualSubtitler
{
    public class Subtitle
    {
        public TimeSpan Start { get; private set; }
        public TimeSpan End { get; private set; }

        /// <summary>
        /// Здесь должен лежать текст в обычном виде, чтобы нормально переводился через Яндекс.Переводчик
        /// </summary>
        public string Text { get; set; }

        public Subtitle(string timing, string text)
        {
            Text = RemoveOneLetterTagsAndUnifyNewStrings(text);

            // 00:00:42,292-- > 00:00:43,377
            string[] timingStringComponents = null;
            if (timing.Contains(" --> "))
                timingStringComponents = timing.Split(new string[] { " --> " }, StringSplitOptions.RemoveEmptyEntries);
            else // Гугл Транслэйт
            {
                timingStringComponents = timing.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                timingStringComponents[0] = timingStringComponents[0].Replace(" ", "");
                timingStringComponents[1] = timingStringComponents[1].Replace(" ", "");
            }

            Start = TimeSpan.Parse(timingStringComponents[0], CultureInfo.GetCultureInfo("ru-ru"));
            End = TimeSpan.Parse(timingStringComponents[1], CultureInfo.GetCultureInfo("ru-ru"));
        }

        public Subtitle(long start, long end, string text)
        {
            Text = RemoveOneLetterTagsAndUnifyNewStrings(text);

            Start = TimeSpan.FromMilliseconds(start);
            End = TimeSpan.FromMilliseconds(end);
        }

        public Subtitle(TimeSpan start, TimeSpan end, string text)
        {
            Text = RemoveOneLetterTagsAndUnifyNewStrings(text);

            Start = start;
            End = end;
        }

        public Subtitle(string startTiming, string endTiming, string text)
        {
            Text = RemoveOneLetterTagsAndUnifyNewStrings(text);

            Start = TimeSpan.Parse(startTiming, CultureInfo.GetCultureInfo("ru-ru"));
            End = TimeSpan.Parse(endTiming, CultureInfo.GetCultureInfo("ru-ru")); ;
        }

        private string RemoveOneLetterTagsAndUnifyNewStrings(string originalText)
        {
            // Перенос
            if (originalText.Contains("\r\n"))
                originalText = originalText.Replace("\r\n", "\n");
            else
            if (originalText.Contains("\\N"))
                originalText = originalText.Replace("\\N", "\n");
            //subtitle.Text = subtitle.Text.Replace("\n", "\\N");

            return Regex.Replace(originalText, "[<].{1,2}[>]", "");
        }

        //public Subtitle(int _id, string _timing, Color _color, string _text)
        //{
        //    ID = _id;
        //    Timing = _timing;
        //    color = _color;
        //    Text = _text;
        //}
    }
    public class SubtitlesAndInfo
    {
        public Subtitle[] Subtitles;

        public SubtitlesBackgroundWorker BackgroundWorker
        {
            get;
            private set;
        }

        public ProgressBar ProgressBar { get; private set; }
        public Label ProgressLabel { get; private set; }
        public Button ButtonOpenOrClose { get; private set; }
        public Button ButtonTranslate { get; private set; }
        public Button ButtonTranslateWordByWord { get; private set; }

        public Label ActionLabel { get; private set; }
        public TextBox OutputTextBox { get; private set; }
        public Button ColorPickingButton;

        public Button CloseSubtitleStreamConfimationButton;
        public Button CloseSubtitleStreamCancellationButton;

        public string TrackNumber;
        public string TrackLanguage;
        public string TrackName;
        public string FileNameWithoutExtention { get; set; }
        public string FileExtention { get; set; }
        
        public SubtitlesAndInfo(ProgressBar progressBar, Label progressLabel, Button buttonOpen, Button buttonTranslate, Button buttonTranslateWordByWord,
           Label actionLabel, TextBox outputTextBox, Button colorPickingButton)
        {
            ProgressBar = progressBar;
            ProgressLabel = progressLabel;
            ButtonOpenOrClose = buttonOpen;
            ButtonTranslate = buttonTranslate;
            ButtonTranslateWordByWord = buttonTranslateWordByWord;
            ActionLabel = actionLabel;
            OutputTextBox = outputTextBox;
            ColorPickingButton = colorPickingButton;
        }

        public void SetBackgroundWorker(SubtitlesBackgroundWorker backgroundWorker, SubtitlesType subtitlesType)
        {
            BackgroundWorker = backgroundWorker;
            BackgroundWorker.SubtitlesType = subtitlesType;
        }

        public void SetOriginalFile(string filePath, bool fromMKV)
        {
            var fileName = new FileInfo(filePath).Name;
            var fileExt = new FileInfo(filePath).Extension;

            OutputTextBox.Text = fileName;

            FileNameWithoutExtention = fileName.Substring(0, fileName.Length - fileExt.Length);
            FileExtention = fileExt;

            if (!fromMKV)
                TrackLanguage = TrackNumber = TrackName = null;
        }
    }

    

    public class SubtitlesStyle
    {
        public string Font;
        public int MarginV;
        public int Size;
        public int OutlineSize;
        public int ShadowSize;
        public int TransparencyPercentage;
        public int ShadowTransparencyPercentage;
        public Color Color;


        public SubtitlesStyle (string subtitlesStyleString)
        {
        // Format: Name, Fontname, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding
        //Style: 3_sub_stream,Times New Roman,20,&H6600D7FF,&H6600FFFF,&H66000000,&H7F000000,0,0,0,0,100,100,0,0,1,2,3,2,10,10,248,1

        var components = subtitlesStyleString.Split(',');
            Font = components[1];
            Size = int.Parse(components[2]);

            //var transparencyPercentage = styleComponents[5];
            //var transparency = ((int)(float.Parse(transparencyPercentage) / 100f * 255f)).ToString("X2");
            //var shadowTransparencyPercentage = styleComponents[6];
            //var shadowTransparency = ((int)(float.Parse(shadowTransparencyPercentage) / 100f * 255f)).ToString("X2");
            TransparencyPercentage = ToDec(components[3].Substring(2, 2)) * 100 / 255;
            Color = Color.FromArgb(
                (ToDec(components[3].Substring(8, 2))),
                (ToDec(components[3].Substring(6, 2))),
                (ToDec(components[3].Substring(4, 2))));
            // В ASS цвет пишется в BGR, не забываем

            ShadowTransparencyPercentage = (int)((float)ToDec(components[6].Substring(2, 2)) * 100f / 255f);

            OutlineSize = int.Parse(components[16]);
            ShadowSize = int.Parse(components[17]);
            MarginV = int.Parse(components[21]);
        }

        public int ToDec(string hexValue)
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }

    }
}
