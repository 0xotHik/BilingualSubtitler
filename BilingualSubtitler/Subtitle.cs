using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilingualSubtitler
{
    class Subtitle
    {
        private int id;
        private string timing;
        private Color color;
        private string text;

        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Timing
        {
            get
            {
                return timing;
            }
            set
            {
                timing = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
            }
        }

        public Subtitle(int _id, string _timing, string _text)
        {
            id = _id;
            timing = _timing;
            text = _text;
        }

        public Subtitle(int _id, string _timing, Color _color, string _text)
        {
            id = _id;
            timing = _timing;
            color = _color;
            text = _text;
        }
    }
}
