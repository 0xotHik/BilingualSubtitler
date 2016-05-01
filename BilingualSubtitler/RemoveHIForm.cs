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

    public partial class RemoveHIForm : Form
    {
        private Subtitle[] originalSubs;
        private bool squreBacketIsInSubtitles;
        private bool roundBracketIsInSubtitles;
        private bool curlyBracketIsInSubtitles;
        private bool rolesIsInSubtitles;

        public RemoveHIForm(object[] subs)
        {
            InitializeComponent();
            originalSubs = (Subtitle[]) subs;
        }

        private void CheckSubs()
        {
            foreach (var sub in originalSubs)
            {
                //if (sub.Text)
            }
        }
    }
}
