namespace BilingualSubtitler
{
    partial class HotkeysToAuthorsExtendedDefaultsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeysToAuthorsExtendedDefaultsForm));
            this.labelInfo = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(355, 18);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(192, 26);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "Авторская расширенная раскладка:\r\n\r\n";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clearButton.Location = new System.Drawing.Point(12, 517);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(97, 43);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Отмена";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.okButton.Location = new System.Drawing.Point(913, 517);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(47, 43);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BilingualSubtitler.Properties.Resources.hotkeys_authors_default;
            this.pictureBox1.Location = new System.Drawing.Point(15, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(945, 325);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(143, 411);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(616, 78);
            this.label1.TabIndex = 4;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(109, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(685, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "- Пробел, Num0, Decimal (.), Add (+), Substract (-), Divide (/), Up (↑), Down (↓)" +
    ", Left (→), Right (←), Return (Enter), F3";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(659, 476);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(65, 13);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "на сайте →";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // HotkeysToAuthorsExtendedDefaultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(972, 572);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.labelInfo);
            this.Name = "HotkeysToAuthorsExtendedDefaultsForm";
            this.Text = "KeySettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}