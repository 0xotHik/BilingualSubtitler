using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace BilingualSubtitler
{
    public class Subtitle
    {
        public TimeSpan Start { get; private set; }
        public TimeSpan End { get; private set; }

        public string Text { get; set; }

        public Subtitle(string timing, string text)
        {
            Text = RemoveTags(text);

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

            Start = TimeSpan.Parse(timingStringComponents[0]);
            End = TimeSpan.Parse(timingStringComponents[1]);
        }

        public Subtitle(long start, long end, string text)
        {
            Text = RemoveTags(text);

            Start = TimeSpan.FromMilliseconds(start);
            End = TimeSpan.FromMilliseconds(end);
        }

        public Subtitle(TimeSpan start, TimeSpan end, string text)
        {
            Text = RemoveTags(text);

            Start = start;
            End = end;
        }

        private string RemoveTags(string originalText)
        {
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
}
