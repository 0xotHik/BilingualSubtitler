namespace BilingualSubtitler
{
    partial class PathToMKVToolnixAndTempForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PathToMKVToolnixAndTempForm));
            this.linkLabelDownloadMKVToolnix = new System.Windows.Forms.LinkLabel();
            this.labelPathToTemp = new System.Windows.Forms.Label();
            this.buttonBrowsePathToTemp = new System.Windows.Forms.Button();
            this.textBoxPathToMKVToolnix = new System.Windows.Forms.TextBox();
            this.textBoxPathToTemp = new System.Windows.Forms.TextBox();
            this.labelPathToMKVToolnix = new System.Windows.Forms.Label();
            this.labelReason = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonBrowsePathToMKVToolnix = new System.Windows.Forms.Button();
            this.pictureBoxMKVExtractLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxMKVToolnix = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialogMKVToolnix = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogTempSubs = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVExtractLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVToolnix)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabelDownloadMKVToolnix
            // 
            this.linkLabelDownloadMKVToolnix.AutoSize = true;
            this.linkLabelDownloadMKVToolnix.Location = new System.Drawing.Point(7, 102);
            this.linkLabelDownloadMKVToolnix.Name = "linkLabelDownloadMKVToolnix";
            this.linkLabelDownloadMKVToolnix.Size = new System.Drawing.Size(108, 13);
            this.linkLabelDownloadMKVToolnix.TabIndex = 51;
            this.linkLabelDownloadMKVToolnix.TabStop = true;
            this.linkLabelDownloadMKVToolnix.Text = "Скачать MKVToolnix";
            // 
            // labelPathToTemp
            // 
            this.labelPathToTemp.AutoSize = true;
            this.labelPathToTemp.Location = new System.Drawing.Point(41, 130);
            this.labelPathToTemp.Name = "labelPathToTemp";
            this.labelPathToTemp.Size = new System.Drawing.Size(343, 13);
            this.labelPathToTemp.TabIndex = 50;
            this.labelPathToTemp.Text = "Выберите папку, в которой будут содержаться временные файлы";
            // 
            // buttonBrowsePathToTemp
            // 
            this.buttonBrowsePathToTemp.Image = global::BilingualSubtitler.Properties.Resources._20pxOpenIcon;
            this.buttonBrowsePathToTemp.Location = new System.Drawing.Point(524, 160);
            this.buttonBrowsePathToTemp.Name = "buttonBrowsePathToTemp";
            this.buttonBrowsePathToTemp.Size = new System.Drawing.Size(20, 20);
            this.buttonBrowsePathToTemp.TabIndex = 49;
            this.buttonBrowsePathToTemp.UseVisualStyleBackColor = true;
            this.buttonBrowsePathToTemp.Click += new System.EventHandler(this.buttonBrowsePathToTemp_Click);
            // 
            // textBoxPathToMKVToolnix
            // 
            this.textBoxPathToMKVToolnix.Location = new System.Drawing.Point(10, 79);
            this.textBoxPathToMKVToolnix.Name = "textBoxPathToMKVToolnix";
            this.textBoxPathToMKVToolnix.Size = new System.Drawing.Size(508, 20);
            this.textBoxPathToMKVToolnix.TabIndex = 48;
            // 
            // textBoxPathToTemp
            // 
            this.textBoxPathToTemp.Location = new System.Drawing.Point(10, 161);
            this.textBoxPathToTemp.Name = "textBoxPathToTemp";
            this.textBoxPathToTemp.Size = new System.Drawing.Size(508, 20);
            this.textBoxPathToTemp.TabIndex = 46;
            // 
            // labelPathToMKVToolnix
            // 
            this.labelPathToMKVToolnix.AutoSize = true;
            this.labelPathToMKVToolnix.Location = new System.Drawing.Point(41, 46);
            this.labelPathToMKVToolnix.Name = "labelPathToMKVToolnix";
            this.labelPathToMKVToolnix.Size = new System.Drawing.Size(366, 26);
            this.labelPathToMKVToolnix.TabIndex = 45;
            this.labelPathToMKVToolnix.Text = "Выберите папку, в которой установлен mkvtoolnix \r\n(в папке должны содержаться при" +
    "ложения \"mkvmerge\" и \"mkvextract\")";
            // 
            // labelReason
            // 
            this.labelReason.AutoSize = true;
            this.labelReason.Location = new System.Drawing.Point(9, 13);
            this.labelReason.Name = "labelReason";
            this.labelReason.Size = new System.Drawing.Size(35, 13);
            this.labelReason.TabIndex = 54;
            this.labelReason.Text = "label1";
            // 
            // buttonOk
            // 
            this.buttonOk.Image = global::BilingualSubtitler.Properties.Resources._16pxOkIcon;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(458, 198);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(86, 35);
            this.buttonOk.TabIndex = 53;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = global::BilingualSubtitler.Properties.Resources._16pxCancelIconAnother;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(9, 198);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(74, 35);
            this.buttonCancel.TabIndex = 52;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonBrowsePathToMKVToolnix
            // 
            this.buttonBrowsePathToMKVToolnix.Image = global::BilingualSubtitler.Properties.Resources._20pxOpenIcon;
            this.buttonBrowsePathToMKVToolnix.Location = new System.Drawing.Point(524, 79);
            this.buttonBrowsePathToMKVToolnix.Name = "buttonBrowsePathToMKVToolnix";
            this.buttonBrowsePathToMKVToolnix.Size = new System.Drawing.Size(20, 20);
            this.buttonBrowsePathToMKVToolnix.TabIndex = 47;
            this.buttonBrowsePathToMKVToolnix.UseVisualStyleBackColor = true;
            this.buttonBrowsePathToMKVToolnix.Click += new System.EventHandler(this.buttonBrowsePathToMKVToolnix_Click);
            // 
            // pictureBoxMKVExtractLogo
            // 
            this.pictureBoxMKVExtractLogo.Image = global::BilingualSubtitler.Properties.Resources._25pxMkvExtractLogo;
            this.pictureBoxMKVExtractLogo.Location = new System.Drawing.Point(10, 130);
            this.pictureBoxMKVExtractLogo.Name = "pictureBoxMKVExtractLogo";
            this.pictureBoxMKVExtractLogo.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxMKVExtractLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMKVExtractLogo.TabIndex = 44;
            this.pictureBoxMKVExtractLogo.TabStop = false;
            // 
            // pictureBoxMKVToolnix
            // 
            this.pictureBoxMKVToolnix.Image = global::BilingualSubtitler.Properties.Resources._25pxMkvtoolnixLogo;
            this.pictureBoxMKVToolnix.Location = new System.Drawing.Point(10, 46);
            this.pictureBoxMKVToolnix.Name = "pictureBoxMKVToolnix";
            this.pictureBoxMKVToolnix.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxMKVToolnix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMKVToolnix.TabIndex = 43;
            this.pictureBoxMKVToolnix.TabStop = false;
            // 
            // PathToMKVToolnixAndTempForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(554, 248);
            this.Controls.Add(this.labelReason);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.linkLabelDownloadMKVToolnix);
            this.Controls.Add(this.buttonBrowsePathToMKVToolnix);
            this.Controls.Add(this.labelPathToTemp);
            this.Controls.Add(this.buttonBrowsePathToTemp);
            this.Controls.Add(this.textBoxPathToMKVToolnix);
            this.Controls.Add(this.textBoxPathToTemp);
            this.Controls.Add(this.labelPathToMKVToolnix);
            this.Controls.Add(this.pictureBoxMKVExtractLogo);
            this.Controls.Add(this.pictureBoxMKVToolnix);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PathToMKVToolnixAndTempForm";
            this.Text = "PathToMKVToolnixAndTempForm";
            this.Load += new System.EventHandler(this.PathToMKVToolnixAndTempForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVExtractLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVToolnix)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelDownloadMKVToolnix;
        private System.Windows.Forms.Button buttonBrowsePathToMKVToolnix;
        private System.Windows.Forms.Label labelPathToTemp;
        private System.Windows.Forms.Button buttonBrowsePathToTemp;
        private System.Windows.Forms.TextBox textBoxPathToMKVToolnix;
        private System.Windows.Forms.TextBox textBoxPathToTemp;
        private System.Windows.Forms.Label labelPathToMKVToolnix;
        private System.Windows.Forms.PictureBox pictureBoxMKVExtractLogo;
        private System.Windows.Forms.PictureBox pictureBoxMKVToolnix;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelReason;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMKVToolnix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogTempSubs;
    }
}