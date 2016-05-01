namespace BilingualSubtitler
{
    partial class YandexTranslatorAPIKeyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YandexTranslatorAPIKeyForm));
            this.richTextBoxLabelYouNeedToGetAPIKey = new System.Windows.Forms.RichTextBox();
            this.linkLabelGetAPIKey = new System.Windows.Forms.LinkLabel();
            this.richTextBoxForYandexApiKeyInSeparateForm = new System.Windows.Forms.RichTextBox();
            this.labelAPIInfo = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.linkLabelYandexAPIKeysList = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // richTextBoxLabelYouNeedToGetAPIKey
            // 
            this.richTextBoxLabelYouNeedToGetAPIKey.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxLabelYouNeedToGetAPIKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLabelYouNeedToGetAPIKey.Location = new System.Drawing.Point(13, 13);
            this.richTextBoxLabelYouNeedToGetAPIKey.Name = "richTextBoxLabelYouNeedToGetAPIKey";
            this.richTextBoxLabelYouNeedToGetAPIKey.ReadOnly = true;
            this.richTextBoxLabelYouNeedToGetAPIKey.Size = new System.Drawing.Size(413, 37);
            this.richTextBoxLabelYouNeedToGetAPIKey.TabIndex = 0;
            this.richTextBoxLabelYouNeedToGetAPIKey.Text = "Для использования данного приложения необходимо получить и ввести ключ к API серв" +
    "иса \"Яндекс.Переводчик\".";
            // 
            // linkLabelGetAPIKey
            // 
            this.linkLabelGetAPIKey.AutoSize = true;
            this.linkLabelGetAPIKey.Location = new System.Drawing.Point(10, 53);
            this.linkLabelGetAPIKey.Name = "linkLabelGetAPIKey";
            this.linkLabelGetAPIKey.Size = new System.Drawing.Size(222, 13);
            this.linkLabelGetAPIKey.TabIndex = 1;
            this.linkLabelGetAPIKey.TabStop = true;
            this.linkLabelGetAPIKey.Text = "Получить ключ к API Яндекс.Переводчика";
            this.linkLabelGetAPIKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGetAPIKey_LinkClicked);
            // 
            // richTextBoxForYandexApiKeyInSeparateForm
            // 
            this.richTextBoxForYandexApiKeyInSeparateForm.Location = new System.Drawing.Point(13, 78);
            this.richTextBoxForYandexApiKeyInSeparateForm.Name = "richTextBoxForYandexApiKeyInSeparateForm";
            this.richTextBoxForYandexApiKeyInSeparateForm.Size = new System.Drawing.Size(413, 71);
            this.richTextBoxForYandexApiKeyInSeparateForm.TabIndex = 2;
            this.richTextBoxForYandexApiKeyInSeparateForm.Text = "";
            // 
            // labelAPIInfo
            // 
            this.labelAPIInfo.AutoSize = true;
            this.labelAPIInfo.Location = new System.Drawing.Point(12, 170);
            this.labelAPIInfo.Name = "labelAPIInfo";
            this.labelAPIInfo.Size = new System.Drawing.Size(411, 52);
            this.labelAPIInfo.TabIndex = 3;
            this.labelAPIInfo.Text = resources.GetString("labelAPIInfo.Text");
            // 
            // buttonOk
            // 
            this.buttonOk.Image = global::BilingualSubtitler.Properties.Resources._16pxOkIcon;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(337, 233);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(86, 35);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Image = global::BilingualSubtitler.Properties.Resources._16pxCancelIconAnother;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(15, 233);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(74, 35);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // linkLabelYandexAPIKeysList
            // 
            this.linkLabelYandexAPIKeysList.AutoSize = true;
            this.linkLabelYandexAPIKeysList.Location = new System.Drawing.Point(12, 152);
            this.linkLabelYandexAPIKeysList.Name = "linkLabelYandexAPIKeysList";
            this.linkLabelYandexAPIKeysList.Size = new System.Drawing.Size(243, 13);
            this.linkLabelYandexAPIKeysList.TabIndex = 43;
            this.linkLabelYandexAPIKeysList.TabStop = true;
            this.linkLabelYandexAPIKeysList.Text = "Открыть список уже полученных вами ключей";
            this.linkLabelYandexAPIKeysList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelYandexAPIKeysList_LinkClicked);
            // 
            // YandexTranslatorAPIKeyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(430, 273);
            this.Controls.Add(this.linkLabelYandexAPIKeysList);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.labelAPIInfo);
            this.Controls.Add(this.richTextBoxForYandexApiKeyInSeparateForm);
            this.Controls.Add(this.linkLabelGetAPIKey);
            this.Controls.Add(this.richTextBoxLabelYouNeedToGetAPIKey);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "YandexTranslatorAPIKeyForm";
            this.Text = "Ключ для API Яндекса";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.YandexTranslatorAPIKeyForm_FormClosing);
            this.Load += new System.EventHandler(this.YandexTranslatorAPIKeyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLabelYouNeedToGetAPIKey;
        private System.Windows.Forms.LinkLabel linkLabelGetAPIKey;
        private System.Windows.Forms.RichTextBox richTextBoxForYandexApiKeyInSeparateForm;
        private System.Windows.Forms.Label labelAPIInfo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.LinkLabel linkLabelYandexAPIKeysList;
    }
}