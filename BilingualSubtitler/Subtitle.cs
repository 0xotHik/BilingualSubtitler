using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BilingualSubtitler
{
    class Subtitle
    {
        public TimeSpan Start { get; private set; }
        public TimeSpan End { get; private set; }

        public string Text { get; set; }

        public Subtitle(string timing, string text)
        {
            Text = ParseText(text);

            // 00:00:42,292-- > 00:00:43,377
            var timingStringComponents = timing.Split(new string[] { " --> "} , StringSplitOptions.RemoveEmptyEntries);
            Start = TimeSpan.Parse(timingStringComponents[0]);
            End = TimeSpan.Parse(timingStringComponents[1]);
        }

        public Subtitle(long start, long end, string text)
        {
            Text = ParseText(text);

            Start = TimeSpan.FromMilliseconds(start);
            End = TimeSpan.FromMilliseconds(end);
        }

        private string ParseText(string originalText)
        {
            return Regex.Replace(originalText, "<*>", "");
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
