using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilingualSubtitler
{
    class Subtitle
    {
        private Color color;

        public int ID { get; set; }

        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }

        public string Text { get; set; }

        public Subtitle(int id, string timing, string text)
        {
            ID = id;
            Text = text;

            // 00:00:42,292-- > 00:00:43,377
            var timingStringComponents = timing.Split(new string[] { " --> "} , StringSplitOptions.RemoveEmptyEntries);
            Start = DateTime.ParseExact(timingStringComponents[0], "HH:mm:ss,fff", CultureInfo.InvariantCulture);
            End = DateTime.ParseExact(timingStringComponents[1], "HH:mm:ss,fff", CultureInfo.InvariantCulture);
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
