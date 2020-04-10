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
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.CreateOriginalSubtitlesFileCheckBox = new System.Windows.Forms.CheckBox();
            this.bilingualSubtitlesPathEndingTextBox = new System.Windows.Forms.TextBox();
            this.originalSubtitlesPathEndingTextBox = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.videoplayerProcessNameTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.videoPlayerChangeToOriginalSubtitlesButtonTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.videoPlayerChangeToBilingualSubtitlesButtonTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.videoPlayerPauseButtonTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hotkeysDataGridView)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxLabelYouNeedToGetAPIKey
            // 
            this.richTextBoxLabelYouNeedToGetAPIKey.BackColor = System.Drawing.SystemColors.ControlLightLight;
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
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Image = global::BilingualSubtitler.Properties.Resources._16pxOkIcon;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(958, 623);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(95, 35);
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "Сохранить";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.hotkeysDataGridView.BackgroundColor = System.Drawing.Color.DarkGoldenrod;
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
            this.groupBox2.Size = new System.Drawing.Size(262, 403);
            this.groupBox2.TabIndex = 46;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(147, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(23, 23);
            this.button1.TabIndex = 46;
            this.button1.Text = "+";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox3);
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
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(30, 367);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(151, 17);
            this.checkBox3.TabIndex = 68;
            this.checkBox3.Text = "менять соответствующе";
            this.checkBox3.UseVisualStyleBackColor = true;
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
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(443, 623);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(222, 35);
            this.button5.TabIndex = 48;
            this.button5.Text = "Вернуться к настройкам по умолчанию";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.checkBox2);
            this.groupBox5.Controls.Add(this.CreateOriginalSubtitlesFileCheckBox);
            this.groupBox5.Controls.Add(this.bilingualSubtitlesPathEndingTextBox);
            this.groupBox5.Controls.Add(this.originalSubtitlesPathEndingTextBox);
            this.groupBox5.Location = new System.Drawing.Point(288, 278);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(298, 137);
            this.groupBox5.TabIndex = 69;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Создавать файлы:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 13);
            this.label4.TabIndex = 60;
            this.label4.Text = "с окончанием имени:";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(168, 23);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(87, 30);
            this.checkBox2.TabIndex = 59;
            this.checkBox2.Text = "двуязычных\r\nсубтитров";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // CreateOriginalSubtitlesFileCheckBox
            // 
            this.CreateOriginalSubtitlesFileCheckBox.AutoSize = true;
            this.CreateOriginalSubtitlesFileCheckBox.Checked = true;
            this.CreateOriginalSubtitlesFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CreateOriginalSubtitlesFileCheckBox.Location = new System.Drawing.Point(54, 23);
            this.CreateOriginalSubtitlesFileCheckBox.Name = "CreateOriginalSubtitlesFileCheckBox";
            this.CreateOriginalSubtitlesFileCheckBox.Size = new System.Drawing.Size(98, 30);
            this.CreateOriginalSubtitlesFileCheckBox.TabIndex = 58;
            this.CreateOriginalSubtitlesFileCheckBox.Text = "оригинальных\r\nсубтитров";
            this.CreateOriginalSubtitlesFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // bilingualSubtitlesPathEndingTextBox
            // 
            this.bilingualSubtitlesPathEndingTextBox.Location = new System.Drawing.Point(168, 93);
            this.bilingualSubtitlesPathEndingTextBox.Name = "bilingualSubtitlesPathEndingTextBox";
            this.bilingualSubtitlesPathEndingTextBox.Size = new System.Drawing.Size(100, 20);
            this.bilingualSubtitlesPathEndingTextBox.TabIndex = 56;
            // 
            // originalSubtitlesPathEndingTextBox
            // 
            this.originalSubtitlesPathEndingTextBox.Location = new System.Drawing.Point(54, 93);
            this.originalSubtitlesPathEndingTextBox.Name = "originalSubtitlesPathEndingTextBox";
            this.originalSubtitlesPathEndingTextBox.Size = new System.Drawing.Size(100, 20);
            this.originalSubtitlesPathEndingTextBox.TabIndex = 55;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.videoplayerProcessNameTextBox);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.button4);
            this.groupBox6.Controls.Add(this.videoPlayerChangeToOriginalSubtitlesButtonTextBox);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Controls.Add(this.videoPlayerChangeToBilingualSubtitlesButtonTextBox);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.button2);
            this.groupBox6.Controls.Add(this.videoPlayerPauseButtonTextBox);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Location = new System.Drawing.Point(288, 13);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(298, 242);
            this.groupBox6.TabIndex = 70;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Настройки видеоплеера";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(46, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(180, 13);
            this.label10.TabIndex = 80;
            this.label10.Text = "Название процесса видеоплеера:";
            // 
            // videoplayerProcessNameTextBox
            // 
            this.videoplayerProcessNameTextBox.Location = new System.Drawing.Point(90, 199);
            this.videoplayerProcessNameTextBox.Name = "videoplayerProcessNameTextBox";
            this.videoplayerProcessNameTextBox.Size = new System.Drawing.Size(98, 20);
            this.videoplayerProcessNameTextBox.TabIndex = 79;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(24, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 78;
            this.label9.Text = "предыдущие:";
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(219, 143);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(64, 23);
            this.button4.TabIndex = 77;
            this.button4.Text = "Задать";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.videoPlayerChangeToOriginalSubtitlesHotkeySetButton_Click);
            // 
            // videoPlayerChangeToOriginalSubtitlesButtonTextBox
            // 
            this.videoPlayerChangeToOriginalSubtitlesButtonTextBox.Location = new System.Drawing.Point(106, 145);
            this.videoPlayerChangeToOriginalSubtitlesButtonTextBox.Name = "videoPlayerChangeToOriginalSubtitlesButtonTextBox";
            this.videoPlayerChangeToOriginalSubtitlesButtonTextBox.Size = new System.Drawing.Size(98, 20);
            this.videoPlayerChangeToOriginalSubtitlesButtonTextBox.TabIndex = 76;
            this.videoPlayerChangeToOriginalSubtitlesButtonTextBox.Text = ".eng.ass";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(24, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 75;
            this.label8.Text = "следующие:";
            // 
            // button3
            // 
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(219, 114);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(64, 23);
            this.button3.TabIndex = 74;
            this.button3.Text = "Задать";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.videoplayerNextSubtitlesHotkeySetButton_Click);
            // 
            // videoPlayerChangeToBilingualSubtitlesButtonTextBox
            // 
            this.videoPlayerChangeToBilingualSubtitlesButtonTextBox.Location = new System.Drawing.Point(106, 116);
            this.videoPlayerChangeToBilingualSubtitlesButtonTextBox.Name = "videoPlayerChangeToBilingualSubtitlesButtonTextBox";
            this.videoPlayerChangeToBilingualSubtitlesButtonTextBox.Size = new System.Drawing.Size(98, 20);
            this.videoPlayerChangeToBilingualSubtitlesButtonTextBox.TabIndex = 73;
            this.videoPlayerChangeToBilingualSubtitlesButtonTextBox.Text = ".eng.ass";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 72;
            this.label7.Text = "Смены субтитров на:";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(219, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 23);
            this.button2.TabIndex = 71;
            this.button2.Text = "Задать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.videoplayerPauseHotkeySetButton_Click);
            // 
            // videoPlayerPauseButtonTextBox
            // 
            this.videoPlayerPauseButtonTextBox.Location = new System.Drawing.Point(106, 58);
            this.videoPlayerPauseButtonTextBox.Name = "videoPlayerPauseButtonTextBox";
            this.videoPlayerPauseButtonTextBox.Size = new System.Drawing.Size(98, 20);
            this.videoPlayerPauseButtonTextBox.TabIndex = 70;
            this.videoPlayerPauseButtonTextBox.Text = ".eng.ass";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 69;
            this.label6.Text = "Паузы:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(176, 13);
            this.label5.TabIndex = 68;
            this.label5.Text = "Горячие клавиши в видеоплеере:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1083, 670);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button5);
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
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
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
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox CreateOriginalSubtitlesFileCheckBox;
        private System.Windows.Forms.TextBox bilingualSubtitlesPathEndingTextBox;
        private System.Windows.Forms.TextBox originalSubtitlesPathEndingTextBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox videoplayerProcessNameTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox videoPlayerChangeToOriginalSubtitlesButtonTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox videoPlayerChangeToBilingualSubtitlesButtonTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox videoPlayerPauseButtonTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}