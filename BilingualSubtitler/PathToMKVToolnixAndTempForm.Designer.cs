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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialogMKVToolnix = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogTempSubs = new System.Windows.Forms.FolderBrowserDialog();
            this.linkLabelYandexAPIKeysList = new System.Windows.Forms.LinkLabel();
            this.pictureBoxYandexTranslator = new System.Windows.Forms.PictureBox();
            this.linkLabelGetYandexAPIKey = new System.Windows.Forms.LinkLabel();
            this.richTextBoxLabelEnterYandexAPIKey = new System.Windows.Forms.RichTextBox();
            this.richTextBoxForYandexAPIKey = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYandexTranslator)).BeginInit();
            this.SuspendLayout();
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
            // linkLabelYandexAPIKeysList
            // 
            this.linkLabelYandexAPIKeysList.AutoSize = true;
            this.linkLabelYandexAPIKeysList.Location = new System.Drawing.Point(311, 142);
            this.linkLabelYandexAPIKeysList.Name = "linkLabelYandexAPIKeysList";
            this.linkLabelYandexAPIKeysList.Size = new System.Drawing.Size(243, 13);
            this.linkLabelYandexAPIKeysList.TabIndex = 58;
            this.linkLabelYandexAPIKeysList.TabStop = true;
            this.linkLabelYandexAPIKeysList.Text = "Открыть список уже полученных вами ключей";
            // 
            // pictureBoxYandexTranslator
            // 
            this.pictureBoxYandexTranslator.Enabled = false;
            this.pictureBoxYandexTranslator.Image = global::BilingualSubtitler.Properties.Resources._25pxYandexTranslateIcon;
            this.pictureBoxYandexTranslator.Location = new System.Drawing.Point(5, 6);
            this.pictureBoxYandexTranslator.Name = "pictureBoxYandexTranslator";
            this.pictureBoxYandexTranslator.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxYandexTranslator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxYandexTranslator.TabIndex = 57;
            this.pictureBoxYandexTranslator.TabStop = false;
            // 
            // linkLabelGetYandexAPIKey
            // 
            this.linkLabelGetYandexAPIKey.AutoSize = true;
            this.linkLabelGetYandexAPIKey.Location = new System.Drawing.Point(5, 142);
            this.linkLabelGetYandexAPIKey.Name = "linkLabelGetYandexAPIKey";
            this.linkLabelGetYandexAPIKey.Size = new System.Drawing.Size(234, 13);
            this.linkLabelGetYandexAPIKey.TabIndex = 56;
            this.linkLabelGetYandexAPIKey.TabStop = true;
            this.linkLabelGetYandexAPIKey.Text = "Получить ключ для API Яндекс.Переводчика";
            // 
            // richTextBoxLabelEnterYandexAPIKey
            // 
            this.richTextBoxLabelEnterYandexAPIKey.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxLabelEnterYandexAPIKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLabelEnterYandexAPIKey.Location = new System.Drawing.Point(34, 12);
            this.richTextBoxLabelEnterYandexAPIKey.Name = "richTextBoxLabelEnterYandexAPIKey";
            this.richTextBoxLabelEnterYandexAPIKey.ReadOnly = true;
            this.richTextBoxLabelEnterYandexAPIKey.Size = new System.Drawing.Size(237, 21);
            this.richTextBoxLabelEnterYandexAPIKey.TabIndex = 55;
            this.richTextBoxLabelEnterYandexAPIKey.Text = "Введите ключ для API Яндекс.Переводчика";
            // 
            // richTextBoxForYandexAPIKey
            // 
            this.richTextBoxForYandexAPIKey.Location = new System.Drawing.Point(5, 39);
            this.richTextBoxForYandexAPIKey.Name = "richTextBoxForYandexAPIKey";
            this.richTextBoxForYandexAPIKey.Size = new System.Drawing.Size(540, 96);
            this.richTextBoxForYandexAPIKey.TabIndex = 54;
            this.richTextBoxForYandexAPIKey.Text = "";
            // 
            // PathToMKVToolnixAndTempForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(554, 248);
            this.Controls.Add(this.linkLabelYandexAPIKeysList);
            this.Controls.Add(this.pictureBoxYandexTranslator);
            this.Controls.Add(this.linkLabelGetYandexAPIKey);
            this.Controls.Add(this.richTextBoxLabelEnterYandexAPIKey);
            this.Controls.Add(this.richTextBoxForYandexAPIKey);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PathToMKVToolnixAndTempForm";
            this.Text = "PathToMKVToolnixAndTempForm";
            this.Load += new System.EventHandler(this.PathToMKVToolnixAndTempForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYandexTranslator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMKVToolnix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogTempSubs;
        private System.Windows.Forms.LinkLabel linkLabelYandexAPIKeysList;
        private System.Windows.Forms.PictureBox pictureBoxYandexTranslator;
        private System.Windows.Forms.LinkLabel linkLabelGetYandexAPIKey;
        private System.Windows.Forms.RichTextBox richTextBoxLabelEnterYandexAPIKey;
        private System.Windows.Forms.RichTextBox richTextBoxForYandexAPIKey;
    }
}