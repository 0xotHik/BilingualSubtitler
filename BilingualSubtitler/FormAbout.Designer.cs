namespace BilingualSubtitler
{
    partial class FormAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            pictureBox1 = new System.Windows.Forms.PictureBox();
            label1 = new System.Windows.Forms.Label();
            labelVersion = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            linkLabel0xothiksHut = new System.Windows.Forms.LinkLabel();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            buttonOk = new System.Windows.Forms.Button();
            pictureBox2 = new System.Windows.Forms.PictureBox();
            labelTextBox = new System.Windows.Forms.TextBox();
            linkLabel2 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo;
            pictureBox1.Location = new System.Drawing.Point(21, 14);
            pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(275, 239);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            label1.Location = new System.Drawing.Point(303, -21);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(312, 75);
            label1.TabIndex = 1;
            label1.Text = "Bilingual Subtitler";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            labelVersion.AutoSize = true;
            labelVersion.Location = new System.Drawing.Point(303, 55);
            labelVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new System.Drawing.Size(49, 15);
            labelVersion.TabIndex = 2;
            labelVersion.Text = "Версия ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(303, 155);
            label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(269, 15);
            label3.TabIndex = 4;
            label3.Text = "Автор программы — Евгений 0xotHik Помелов";
            // 
            // linkLabel0xothiksHut
            // 
            linkLabel0xothiksHut.AutoSize = true;
            linkLabel0xothiksHut.Location = new System.Drawing.Point(303, 182);
            linkLabel0xothiksHut.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel0xothiksHut.Name = "linkLabel0xothiksHut";
            linkLabel0xothiksHut.Size = new System.Drawing.Size(125, 15);
            linkLabel0xothiksHut.TabIndex = 33;
            linkLabel0xothiksHut.TabStop = true;
            linkLabel0xothiksHut.Text = "Посетить сайт автора";
            linkLabel0xothiksHut.LinkClicked += linkLabel0xothiksHut_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new System.Drawing.Point(303, 84);
            linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(210, 60);
            linkLabel1.TabIndex = 40;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Сайт программы:\r\nинструкция по применению, \r\nсписок использованных библиотек, \r\nописание работы программы";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // buttonOk
            // 
            buttonOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOk.Location = new System.Drawing.Point(497, 226);
            buttonOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(88, 27);
            buttonOk.TabIndex = 42;
            buttonOk.Text = "Ok";
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += buttonOk_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            pictureBox2.Image = (System.Drawing.Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new System.Drawing.Point(307, 226);
            pictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new System.Drawing.Size(168, 24);
            pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 43;
            pictureBox2.TabStop = false;
            pictureBox2.Click += pictureBox2_Click;
            // 
            // labelTextBox
            // 
            labelTextBox.Location = new System.Drawing.Point(358, 52);
            labelTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            labelTextBox.Name = "labelTextBox";
            labelTextBox.ReadOnly = true;
            labelTextBox.Size = new System.Drawing.Size(226, 23);
            labelTextBox.TabIndex = 56;
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new System.Drawing.Point(303, 197);
            linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new System.Drawing.Size(141, 15);
            linkLabel2.TabIndex = 104;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Канал новостей проекта";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked_1;
            // 
            // FormAbout
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(603, 262);
            Controls.Add(linkLabel2);
            Controls.Add(labelTextBox);
            Controls.Add(pictureBox2);
            Controls.Add(buttonOk);
            Controls.Add(linkLabel1);
            Controls.Add(linkLabel0xothiksHut);
            Controls.Add(label3);
            Controls.Add(labelVersion);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Cursor = System.Windows.Forms.Cursors.Default;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormAbout";
            Text = "О программе";
            Load += FormAbout_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel0xothiksHut;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox labelTextBox;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}