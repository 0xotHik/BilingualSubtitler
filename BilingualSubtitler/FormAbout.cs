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
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            labelTextBox.Text = $"Bilingual Subtitler {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        }

        private void linkLabel0xothiksHut_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://0xothik.wordpress.com");
        }

        private void linkLabelHabrahabr_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://habrahabr.ru/post/204372/"); 
        }

        private void linkLabelStackOverflowKvanTTT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("http://stackoverflow.com/users/1046374/kvanttt"); 
        }

        private void linkLabelLinkedInKvanTTT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://ru.linkedin.com/in/kvanttt/en"); 
        }

        private void linkLabelGitHubKvanTTT_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://github.com/KvanTTT/"); 
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://0xothik.wordpress.com/bilingual-subtitler/"); 
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://0xothik.wordpress.com/2016/02/16/about-bilingual-subtitler/"); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            MainForm.OpenUrl("https://0xothik.wordpress.com/bilingual-subtitler#thnksFrThBlnglSbttls");
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MainForm.OpenUrl("https://t.me/bilingualsubtitler");
        }
    }
}
