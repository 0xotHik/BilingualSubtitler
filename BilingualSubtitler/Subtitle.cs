﻿using System;
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

    public class SubtitlesStyle
    {
        public string Font;
        public int Align;
        public int Size;
        public int OutlineSize;
        public int ShadowSize;
        public int TransparencyPercentage;
        public int ShadowTransparencyPercentage;
        public Color Color;


        public SubtitlesStyle (string subtitlesStyleString)
        {
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
                int.Parse((components[3].Substring(4, 2))),
                int.Parse((components[3].Substring(6, 2))),
                int.Parse((components[3].Substring(8, 2))));


        }

        public int ToDec(string hexValue)
        {
            return int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
        }

    }
}
