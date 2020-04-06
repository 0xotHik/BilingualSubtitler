namespace BilingualSubtitler
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.richTextBoxLabelYouNeedToGetAPIKey = new System.Windows.Forms.RichTextBox();
            this.linkLabelGetAPIKey = new System.Windows.Forms.LinkLabel();
            this.richTextBoxForYandexApiKeyInSeparateForm = new System.Windows.Forms.RichTextBox();
            this.labelAPIInfo = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.linkLabelYandexAPIKeysList = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.hotkeysDataGridView = new System.Windows.Forms.DataGridView();
            this.keyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotkeysDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxLabelYouNeedToGetAPIKey
            // 
            this.richTextBoxLabelYouNeedToGetAPIKey.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxLabelYouNeedToGetAPIKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLabelYouNeedToGetAPIKey.Location = new System.Drawing.Point(16, 19);
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
            this.linkLabelGetAPIKey.Location = new System.Drawing.Point(13, 59);
            this.linkLabelGetAPIKey.Name = "linkLabelGetAPIKey";
            this.linkLabelGetAPIKey.Size = new System.Drawing.Size(222, 13);
            this.linkLabelGetAPIKey.TabIndex = 1;
            this.linkLabelGetAPIKey.TabStop = true;
            this.linkLabelGetAPIKey.Text = "Получить ключ к API Яндекс.Переводчика";
            this.linkLabelGetAPIKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGetAPIKey_LinkClicked);
            // 
            // richTextBoxForYandexApiKeyInSeparateForm
            // 
            this.richTextBoxForYandexApiKeyInSeparateForm.Location = new System.Drawing.Point(595, 19);
            this.richTextBoxForYandexApiKeyInSeparateForm.Name = "richTextBoxForYandexApiKeyInSeparateForm";
            this.richTextBoxForYandexApiKeyInSeparateForm.Size = new System.Drawing.Size(443, 108);
            this.richTextBoxForYandexApiKeyInSeparateForm.TabIndex = 2;
            this.richTextBoxForYandexApiKeyInSeparateForm.Text = "";
            // 
            // labelAPIInfo
            // 
            this.labelAPIInfo.AutoSize = true;
            this.labelAPIInfo.Location = new System.Drawing.Point(13, 88);
            this.labelAPIInfo.Name = "labelAPIInfo";
            this.labelAPIInfo.Size = new System.Drawing.Size(411, 52);
            this.labelAPIInfo.TabIndex = 3;
            this.labelAPIInfo.Text = resources.GetString("labelAPIInfo.Text");
            // 
            // buttonOk
            // 
            this.buttonOk.Image = global::BilingualSubtitler.Properties.Resources._16pxOkIcon;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(967, 623);
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
            this.buttonCancel.Location = new System.Drawing.Point(15, 623);
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
            this.linkLabelYandexAPIKeysList.Location = new System.Drawing.Point(255, 59);
            this.linkLabelYandexAPIKeysList.Name = "linkLabelYandexAPIKeysList";
            this.linkLabelYandexAPIKeysList.Size = new System.Drawing.Size(243, 13);
            this.linkLabelYandexAPIKeysList.TabIndex = 43;
            this.linkLabelYandexAPIKeysList.TabStop = true;
            this.linkLabelYandexAPIKeysList.Text = "Открыть список уже полученных вами ключей";
            this.linkLabelYandexAPIKeysList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelYandexAPIKeysList_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.richTextBoxLabelYouNeedToGetAPIKey);
            this.groupBox1.Controls.Add(this.linkLabelYandexAPIKeysList);
            this.groupBox1.Controls.Add(this.linkLabelGetAPIKey);
            this.groupBox1.Controls.Add(this.richTextBoxForYandexApiKeyInSeparateForm);
            this.groupBox1.Controls.Add(this.labelAPIInfo);
            this.groupBox1.Location = new System.Drawing.Point(15, 421);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1056, 145);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // hotkeysDataGridView
            // 
            this.hotkeysDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hotkeysDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.keyColumn});
            this.hotkeysDataGridView.Location = new System.Drawing.Point(16, 30);
            this.hotkeysDataGridView.Name = "hotkeysDataGridView";
            this.hotkeysDataGridView.Size = new System.Drawing.Size(199, 357);
            this.hotkeysDataGridView.TabIndex = 45;
            // 
            // keyColumn
            // 
            this.keyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.keyColumn.HeaderText = "Клавиши";
            this.keyColumn.Name = "keyColumn";
            this.keyColumn.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.hotkeysDataGridView);
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(571, 403);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(266, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(592, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(479, 403);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(18, 71);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(98, 187);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Оригинальные\nсубтитры";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Шрифт:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Русские:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1083, 670);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.Text = "Ключ для API Яндекса";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.YandexTranslatorAPIKeyForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotkeysDataGridView)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxLabelYouNeedToGetAPIKey;
        private System.Windows.Forms.LinkLabel linkLabelGetAPIKey;
        private System.Windows.Forms.RichTextBox richTextBoxForYandexApiKeyInSeparateForm;
        private System.Windows.Forms.Label labelAPIInfo;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.LinkLabel linkLabelYandexAPIKeysList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView hotkeysDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyColumn;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}