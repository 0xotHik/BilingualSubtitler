namespace BilingualSubtitler
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.videoStateComboBox = new System.Windows.Forms.ComboBox();
            this.subtitlesStateComboBox = new System.Windows.Forms.ComboBox();
            this.settingsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.selectVideoFileToGetPathForSubtitlesButton = new System.Windows.Forms.Button();
            this.bilingualSubtitlesFileNameEndingLabel = new System.Windows.Forms.Label();
            this.originalSubtitlesFileNameEndingLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.finalSubtitlesFilesPathBeginningRichTextBox = new System.Windows.Forms.RichTextBox();
            this.bilingualSubtitlesFileNameEnding = new System.Windows.Forms.TextBox();
            this.originalSubtitlesFileNameEnding = new System.Windows.Forms.TextBox();
            this.createOriginalAndBilingualSubtitlesFilesButton = new System.Windows.Forms.Button();
            this.showSecondRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.hideSecondRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.hideThirdRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.showThirdRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.translateToFirstRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.translateWordByWordToFirstRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.firstRussianSubtitlesActionLabel = new System.Windows.Forms.Label();
            this.firstRussianSubtitlesColorButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.firstRussianSubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.openFirstRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.firstRussianSubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.firstRussianSubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.primarySubtitlesActionLabel = new System.Windows.Forms.Label();
            this.primarySubtitlesColorButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.primarySubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.openPrimarySubtitlesButton = new System.Windows.Forms.Button();
            this.translateToPrimarySubtitlesButton = new System.Windows.Forms.Button();
            this.primarySubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.primarySubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.secondRussianSubtitlesGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.translateWordByWordToSecondRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.translateToSecondRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.secondRussianSubtitlesActionLabel = new System.Windows.Forms.Label();
            this.secondRussianSubtitlesColorButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.secondRussianSubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.openSecondRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.secondRussianSubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.secondRussianSubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.thirdRussianSubtitlesGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.translateWordByWordToThirdRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.translateToThirdRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.thirdRussianSubtitlesActionLabel = new System.Windows.Forms.Label();
            this.thirdRussianSubtitlesColorButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.thirdRussianSubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.openThirdRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.thirdRussianSubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.thirdRussianSubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.secondRussianSubtitlesGroupBox.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.thirdRussianSubtitlesGroupBox.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoStateComboBox
            // 
            this.videoStateComboBox.FormattingEnabled = true;
            this.videoStateComboBox.Location = new System.Drawing.Point(20, 41);
            this.videoStateComboBox.Name = "videoStateComboBox";
            this.videoStateComboBox.Size = new System.Drawing.Size(121, 21);
            this.videoStateComboBox.TabIndex = 11;
            this.videoStateComboBox.SelectedValueChanged += new System.EventHandler(this.videoStateComboBox_SelectedValueChanged);
            // 
            // subtitlesStateComboBox
            // 
            this.subtitlesStateComboBox.FormattingEnabled = true;
            this.subtitlesStateComboBox.Location = new System.Drawing.Point(175, 41);
            this.subtitlesStateComboBox.Name = "subtitlesStateComboBox";
            this.subtitlesStateComboBox.Size = new System.Drawing.Size(182, 21);
            this.subtitlesStateComboBox.TabIndex = 12;
            this.subtitlesStateComboBox.SelectedValueChanged += new System.EventHandler(this.subtitlesStateComboBox_SelectedValueChanged);
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.settingsButton.Location = new System.Drawing.Point(24, 567);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(675, 37);
            this.settingsButton.TabIndex = 13;
            this.settingsButton.Text = "⚙️ Настройки программы";
            this.settingsButton.UseVisualStyleBackColor = false;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "c";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.subtitlesStateComboBox);
            this.groupBox5.Controls.Add(this.videoStateComboBox);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Location = new System.Drawing.Point(24, 622);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(381, 79);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "🎥 Сейчас видео:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.showSecondRussianSubtitlesButton);
            this.groupBox3.Controls.Add(this.hideSecondRussianSubtitlesButton);
            this.groupBox3.Controls.Add(this.hideThirdRussianSubtitlesButton);
            this.groupBox3.Controls.Add(this.showThirdRussianSubtitlesButton);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.secondRussianSubtitlesGroupBox);
            this.groupBox3.Controls.Add(this.thirdRussianSubtitlesGroupBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(693, 552);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Создание субтитров";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.selectVideoFileToGetPathForSubtitlesButton);
            this.groupBox4.Controls.Add(this.bilingualSubtitlesFileNameEndingLabel);
            this.groupBox4.Controls.Add(this.originalSubtitlesFileNameEndingLabel);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.finalSubtitlesFilesPathBeginningRichTextBox);
            this.groupBox4.Controls.Add(this.bilingualSubtitlesFileNameEnding);
            this.groupBox4.Controls.Add(this.originalSubtitlesFileNameEnding);
            this.groupBox4.Controls.Add(this.createOriginalAndBilingualSubtitlesFilesButton);
            this.groupBox4.Location = new System.Drawing.Point(6, 370);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(681, 176);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Итоговые файлы субтитров";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(241, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "для установки пути файлов субтитров до него\r\n";
            // 
            // selectVideoFileToGetPathForSubtitlesButton
            // 
            this.selectVideoFileToGetPathForSubtitlesButton.AutoSize = true;
            this.selectVideoFileToGetPathForSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.selectVideoFileToGetPathForSubtitlesButton.Location = new System.Drawing.Point(12, 123);
            this.selectVideoFileToGetPathForSubtitlesButton.Name = "selectVideoFileToGetPathForSubtitlesButton";
            this.selectVideoFileToGetPathForSubtitlesButton.Size = new System.Drawing.Size(317, 25);
            this.selectVideoFileToGetPathForSubtitlesButton.TabIndex = 43;
            this.selectVideoFileToGetPathForSubtitlesButton.Text = "🎞️ Выбрать файл видео, с которым планируется просмотр";
            this.selectVideoFileToGetPathForSubtitlesButton.UseVisualStyleBackColor = false;
            this.selectVideoFileToGetPathForSubtitlesButton.Click += new System.EventHandler(this.selectVideoFileToGetPathForSubtitlesButton_Click);
            // 
            // bilingualSubtitlesFileNameEndingLabel
            // 
            this.bilingualSubtitlesFileNameEndingLabel.AutoSize = true;
            this.bilingualSubtitlesFileNameEndingLabel.Location = new System.Drawing.Point(352, 75);
            this.bilingualSubtitlesFileNameEndingLabel.Name = "bilingualSubtitlesFileNameEndingLabel";
            this.bilingualSubtitlesFileNameEndingLabel.Size = new System.Drawing.Size(154, 13);
            this.bilingualSubtitlesFileNameEndingLabel.TabIndex = 46;
            this.bilingualSubtitlesFileNameEndingLabel.Text = "файл двуязычных субтитров:";
            // 
            // originalSubtitlesFileNameEndingLabel
            // 
            this.originalSubtitlesFileNameEndingLabel.AutoSize = true;
            this.originalSubtitlesFileNameEndingLabel.Location = new System.Drawing.Point(347, 20);
            this.originalSubtitlesFileNameEndingLabel.Name = "originalSubtitlesFileNameEndingLabel";
            this.originalSubtitlesFileNameEndingLabel.Size = new System.Drawing.Size(165, 13);
            this.originalSubtitlesFileNameEndingLabel.TabIndex = 45;
            this.originalSubtitlesFileNameEndingLabel.Text = "файл оригинальных субтитров:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(332, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "+";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "🖴  Путь итоговых файлов:";
            // 
            // finalSubtitlesFilesPathBeginningRichTextBox
            // 
            this.finalSubtitlesFilesPathBeginningRichTextBox.Location = new System.Drawing.Point(9, 43);
            this.finalSubtitlesFilesPathBeginningRichTextBox.Name = "finalSubtitlesFilesPathBeginningRichTextBox";
            this.finalSubtitlesFilesPathBeginningRichTextBox.Size = new System.Drawing.Size(320, 74);
            this.finalSubtitlesFilesPathBeginningRichTextBox.TabIndex = 42;
            this.finalSubtitlesFilesPathBeginningRichTextBox.Text = "";
            // 
            // bilingualSubtitlesFileNameEnding
            // 
            this.bilingualSubtitlesFileNameEnding.Location = new System.Drawing.Point(364, 96);
            this.bilingualSubtitlesFileNameEnding.Name = "bilingualSubtitlesFileNameEnding";
            this.bilingualSubtitlesFileNameEnding.ReadOnly = true;
            this.bilingualSubtitlesFileNameEnding.Size = new System.Drawing.Size(130, 20);
            this.bilingualSubtitlesFileNameEnding.TabIndex = 40;
            this.bilingualSubtitlesFileNameEnding.Text = ".ruseng.ass";
            // 
            // originalSubtitlesFileNameEnding
            // 
            this.originalSubtitlesFileNameEnding.Location = new System.Drawing.Point(365, 41);
            this.originalSubtitlesFileNameEnding.Name = "originalSubtitlesFileNameEnding";
            this.originalSubtitlesFileNameEnding.ReadOnly = true;
            this.originalSubtitlesFileNameEnding.Size = new System.Drawing.Size(129, 20);
            this.originalSubtitlesFileNameEnding.TabIndex = 37;
            this.originalSubtitlesFileNameEnding.Text = ".eng.ass";
            // 
            // createOriginalAndBilingualSubtitlesFilesButton
            // 
            this.createOriginalAndBilingualSubtitlesFilesButton.BackColor = System.Drawing.Color.Gold;
            this.createOriginalAndBilingualSubtitlesFilesButton.Location = new System.Drawing.Point(519, 20);
            this.createOriginalAndBilingualSubtitlesFilesButton.Name = "createOriginalAndBilingualSubtitlesFilesButton";
            this.createOriginalAndBilingualSubtitlesFilesButton.Size = new System.Drawing.Size(156, 97);
            this.createOriginalAndBilingualSubtitlesFilesButton.TabIndex = 38;
            this.createOriginalAndBilingualSubtitlesFilesButton.Text = "💾\r\nСоздать\r\nсубтитры";
            this.createOriginalAndBilingualSubtitlesFilesButton.UseVisualStyleBackColor = false;
            this.createOriginalAndBilingualSubtitlesFilesButton.Click += new System.EventHandler(this.createOriginalAndBilingualSubtitlesFilesButton_Click);
            // 
            // showSecondRussianSubtitlesButton
            // 
            this.showSecondRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.showSecondRussianSubtitlesButton.Location = new System.Drawing.Point(6, 196);
            this.showSecondRussianSubtitlesButton.Name = "showSecondRussianSubtitlesButton";
            this.showSecondRussianSubtitlesButton.Size = new System.Drawing.Size(175, 23);
            this.showSecondRussianSubtitlesButton.TabIndex = 37;
            this.showSecondRussianSubtitlesButton.Text = "+ 2-й поток русских субтитров";
            this.showSecondRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.showSecondRussianSubtitlesButton.Click += new System.EventHandler(this.showSecondRussianSubtitlesButton_Click);
            // 
            // hideSecondRussianSubtitlesButton
            // 
            this.hideSecondRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.hideSecondRussianSubtitlesButton.Location = new System.Drawing.Point(645, 196);
            this.hideSecondRussianSubtitlesButton.Name = "hideSecondRussianSubtitlesButton";
            this.hideSecondRussianSubtitlesButton.Size = new System.Drawing.Size(18, 18);
            this.hideSecondRussianSubtitlesButton.TabIndex = 39;
            this.hideSecondRussianSubtitlesButton.Text = "-";
            this.hideSecondRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.hideSecondRussianSubtitlesButton.Click += new System.EventHandler(this.hideSecondRussianSubtitlesButton_Click);
            // 
            // hideThirdRussianSubtitlesButton
            // 
            this.hideThirdRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.hideThirdRussianSubtitlesButton.Location = new System.Drawing.Point(645, 283);
            this.hideThirdRussianSubtitlesButton.Name = "hideThirdRussianSubtitlesButton";
            this.hideThirdRussianSubtitlesButton.Size = new System.Drawing.Size(18, 18);
            this.hideThirdRussianSubtitlesButton.TabIndex = 40;
            this.hideThirdRussianSubtitlesButton.Text = "-";
            this.hideThirdRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.hideThirdRussianSubtitlesButton.Click += new System.EventHandler(this.hideThirdRussianSubtitlesButton_Click);
            // 
            // showThirdRussianSubtitlesButton
            // 
            this.showThirdRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.showThirdRussianSubtitlesButton.Location = new System.Drawing.Point(6, 283);
            this.showThirdRussianSubtitlesButton.Name = "showThirdRussianSubtitlesButton";
            this.showThirdRussianSubtitlesButton.Size = new System.Drawing.Size(175, 23);
            this.showThirdRussianSubtitlesButton.TabIndex = 38;
            this.showThirdRussianSubtitlesButton.Text = "+ 3-й поток русских субтитров";
            this.showThirdRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.showThirdRussianSubtitlesButton.Click += new System.EventHandler(this.showThirdRussianSubtitlesButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesActionLabel);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesColorButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesTextBox);
            this.groupBox2.Controls.Add(this.openFirstRussianSubtitlesButton);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesProgressLabel);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesProgressBar);
            this.groupBox2.Location = new System.Drawing.Point(6, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(681, 81);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "1-й поток русских субтитров";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.translateToFirstRussianSubtitlesButton);
            this.groupBox6.Controls.Add(this.translateWordByWordToFirstRussianSubtitlesButton);
            this.groupBox6.Location = new System.Drawing.Point(408, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(184, 70);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Яндекс.Переводчик";
            // 
            // translateToFirstRussianSubtitlesButton
            // 
            this.translateToFirstRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.translateToFirstRussianSubtitlesButton.Image = global::BilingualSubtitler.Properties.Resources._smallTranslateToRus;
            this.translateToFirstRussianSubtitlesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.translateToFirstRussianSubtitlesButton.Location = new System.Drawing.Point(6, 15);
            this.translateToFirstRussianSubtitlesButton.Name = "translateToFirstRussianSubtitlesButton";
            this.translateToFirstRussianSubtitlesButton.Size = new System.Drawing.Size(125, 49);
            this.translateToFirstRussianSubtitlesButton.TabIndex = 4;
            this.translateToFirstRussianSubtitlesButton.Text = "Перевести\r\nоригинальные\r\nсубтитры";
            this.translateToFirstRussianSubtitlesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.translateToFirstRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.translateToFirstRussianSubtitlesButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // translateWordByWordToFirstRussianSubtitlesButton
            // 
            this.translateWordByWordToFirstRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.translateWordByWordToFirstRussianSubtitlesButton.Location = new System.Drawing.Point(129, 15);
            this.translateWordByWordToFirstRussianSubtitlesButton.Name = "translateWordByWordToFirstRussianSubtitlesButton";
            this.translateWordByWordToFirstRussianSubtitlesButton.Size = new System.Drawing.Size(44, 49);
            this.translateWordByWordToFirstRussianSubtitlesButton.TabIndex = 10;
            this.translateWordByWordToFirstRussianSubtitlesButton.Text = "По-\r\nслов-\r\nно";
            this.translateWordByWordToFirstRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.translateWordByWordToFirstRussianSubtitlesButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // firstRussianSubtitlesActionLabel
            // 
            this.firstRussianSubtitlesActionLabel.AutoSize = true;
            this.firstRussianSubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.firstRussianSubtitlesActionLabel.Name = "firstRussianSubtitlesActionLabel";
            this.firstRussianSubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.firstRussianSubtitlesActionLabel.TabIndex = 9;
            this.firstRussianSubtitlesActionLabel.Text = "1";
            this.firstRussianSubtitlesActionLabel.Visible = false;
            // 
            // firstRussianSubtitlesColorButton
            // 
            this.firstRussianSubtitlesColorButton.BackColor = System.Drawing.Color.Silver;
            this.firstRussianSubtitlesColorButton.Location = new System.Drawing.Point(600, 19);
            this.firstRussianSubtitlesColorButton.Name = "firstRussianSubtitlesColorButton";
            this.firstRussianSubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.firstRussianSubtitlesColorButton.TabIndex = 8;
            this.firstRussianSubtitlesColorButton.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(598, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Цвет";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // firstRussianSubtitlesTextBox
            // 
            this.firstRussianSubtitlesTextBox.Location = new System.Drawing.Point(6, 19);
            this.firstRussianSubtitlesTextBox.Name = "firstRussianSubtitlesTextBox";
            this.firstRussianSubtitlesTextBox.ReadOnly = true;
            this.firstRussianSubtitlesTextBox.Size = new System.Drawing.Size(329, 20);
            this.firstRussianSubtitlesTextBox.TabIndex = 1;
            // 
            // openFirstRussianSubtitlesButton
            // 
            this.openFirstRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.openFirstRussianSubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openFirstRussianSubtitlesButton.Name = "openFirstRussianSubtitlesButton";
            this.openFirstRussianSubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openFirstRussianSubtitlesButton.TabIndex = 0;
            this.openFirstRussianSubtitlesButton.Text = "📁 Открыть из файла";
            this.openFirstRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.openFirstRussianSubtitlesButton.Click += new System.EventHandler(this.openFirstRussianSubtitlesButton_Click);
            // 
            // firstRussianSubtitlesProgressLabel
            // 
            this.firstRussianSubtitlesProgressLabel.AutoSize = true;
            this.firstRussianSubtitlesProgressLabel.Location = new System.Drawing.Point(205, 42);
            this.firstRussianSubtitlesProgressLabel.Name = "firstRussianSubtitlesProgressLabel";
            this.firstRussianSubtitlesProgressLabel.Size = new System.Drawing.Size(127, 13);
            this.firstRussianSubtitlesProgressLabel.TabIndex = 2;
            this.firstRussianSubtitlesProgressLabel.Text = "firstRussianSubtitlesLabel";
            this.firstRussianSubtitlesProgressLabel.Visible = false;
            // 
            // firstRussianSubtitlesProgressBar
            // 
            this.firstRussianSubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.firstRussianSubtitlesProgressBar.Name = "firstRussianSubtitlesProgressBar";
            this.firstRussianSubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.firstRussianSubtitlesProgressBar.TabIndex = 3;
            this.firstRussianSubtitlesProgressBar.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.primarySubtitlesActionLabel);
            this.groupBox1.Controls.Add(this.primarySubtitlesColorButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.primarySubtitlesTextBox);
            this.groupBox1.Controls.Add(this.openPrimarySubtitlesButton);
            this.groupBox1.Controls.Add(this.translateToPrimarySubtitlesButton);
            this.groupBox1.Controls.Add(this.primarySubtitlesProgressLabel);
            this.groupBox1.Controls.Add(this.primarySubtitlesProgressBar);
            this.groupBox1.Location = new System.Drawing.Point(6, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(681, 81);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Оригинальные субтитры";
            // 
            // primarySubtitlesActionLabel
            // 
            this.primarySubtitlesActionLabel.AutoSize = true;
            this.primarySubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.primarySubtitlesActionLabel.Name = "primarySubtitlesActionLabel";
            this.primarySubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.primarySubtitlesActionLabel.TabIndex = 8;
            this.primarySubtitlesActionLabel.Text = "1";
            this.primarySubtitlesActionLabel.Visible = false;
            // 
            // primarySubtitlesColorButton
            // 
            this.primarySubtitlesColorButton.BackColor = System.Drawing.Color.White;
            this.primarySubtitlesColorButton.Location = new System.Drawing.Point(600, 19);
            this.primarySubtitlesColorButton.Name = "primarySubtitlesColorButton";
            this.primarySubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.primarySubtitlesColorButton.TabIndex = 7;
            this.primarySubtitlesColorButton.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(598, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Цвет";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // primarySubtitlesTextBox
            // 
            this.primarySubtitlesTextBox.Location = new System.Drawing.Point(6, 19);
            this.primarySubtitlesTextBox.Name = "primarySubtitlesTextBox";
            this.primarySubtitlesTextBox.ReadOnly = true;
            this.primarySubtitlesTextBox.Size = new System.Drawing.Size(329, 20);
            this.primarySubtitlesTextBox.TabIndex = 1;
            // 
            // openPrimarySubtitlesButton
            // 
            this.openPrimarySubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.openPrimarySubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openPrimarySubtitlesButton.Name = "openPrimarySubtitlesButton";
            this.openPrimarySubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openPrimarySubtitlesButton.TabIndex = 0;
            this.openPrimarySubtitlesButton.Text = "📁 Открыть из файла";
            this.openPrimarySubtitlesButton.UseVisualStyleBackColor = false;
            this.openPrimarySubtitlesButton.Click += new System.EventHandler(this.openPrimarySubtitlesButton_Click);
            // 
            // translateToPrimarySubtitlesButton
            // 
            this.translateToPrimarySubtitlesButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.translateToPrimarySubtitlesButton.Enabled = false;
            this.translateToPrimarySubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.translateToPrimarySubtitlesButton.Location = new System.Drawing.Point(416, 19);
            this.translateToPrimarySubtitlesButton.Name = "translateToPrimarySubtitlesButton";
            this.translateToPrimarySubtitlesButton.Size = new System.Drawing.Size(157, 49);
            this.translateToPrimarySubtitlesButton.TabIndex = 4;
            this.translateToPrimarySubtitlesButton.Text = "Перевести";
            this.translateToPrimarySubtitlesButton.UseVisualStyleBackColor = false;
            this.translateToPrimarySubtitlesButton.Visible = false;
            // 
            // primarySubtitlesProgressLabel
            // 
            this.primarySubtitlesProgressLabel.AutoSize = true;
            this.primarySubtitlesProgressLabel.Location = new System.Drawing.Point(297, 42);
            this.primarySubtitlesProgressLabel.Name = "primarySubtitlesProgressLabel";
            this.primarySubtitlesProgressLabel.Size = new System.Drawing.Size(35, 13);
            this.primarySubtitlesProgressLabel.TabIndex = 2;
            this.primarySubtitlesProgressLabel.Text = "label1";
            this.primarySubtitlesProgressLabel.Visible = false;
            // 
            // primarySubtitlesProgressBar
            // 
            this.primarySubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.primarySubtitlesProgressBar.Name = "primarySubtitlesProgressBar";
            this.primarySubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.primarySubtitlesProgressBar.TabIndex = 3;
            this.primarySubtitlesProgressBar.Visible = false;
            // 
            // secondRussianSubtitlesGroupBox
            // 
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.groupBox7);
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.secondRussianSubtitlesActionLabel);
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.secondRussianSubtitlesColorButton);
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.label5);
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.secondRussianSubtitlesTextBox);
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.openSecondRussianSubtitlesButton);
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.secondRussianSubtitlesProgressLabel);
            this.secondRussianSubtitlesGroupBox.Controls.Add(this.secondRussianSubtitlesProgressBar);
            this.secondRussianSubtitlesGroupBox.Location = new System.Drawing.Point(6, 196);
            this.secondRussianSubtitlesGroupBox.Name = "secondRussianSubtitlesGroupBox";
            this.secondRussianSubtitlesGroupBox.Size = new System.Drawing.Size(681, 81);
            this.secondRussianSubtitlesGroupBox.TabIndex = 27;
            this.secondRussianSubtitlesGroupBox.TabStop = false;
            this.secondRussianSubtitlesGroupBox.Text = "2-й поток русских субтитров";
            this.secondRussianSubtitlesGroupBox.Visible = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.translateWordByWordToSecondRussianSubtitlesButton);
            this.groupBox7.Controls.Add(this.translateToSecondRussianSubtitlesButton);
            this.groupBox7.Location = new System.Drawing.Point(408, 8);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(184, 70);
            this.groupBox7.TabIndex = 14;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Яндекс.Переводчик";
            // 
            // translateWordByWordToSecondRussianSubtitlesButton
            // 
            this.translateWordByWordToSecondRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.translateWordByWordToSecondRussianSubtitlesButton.Location = new System.Drawing.Point(129, 15);
            this.translateWordByWordToSecondRussianSubtitlesButton.Name = "translateWordByWordToSecondRussianSubtitlesButton";
            this.translateWordByWordToSecondRussianSubtitlesButton.Size = new System.Drawing.Size(44, 49);
            this.translateWordByWordToSecondRussianSubtitlesButton.TabIndex = 13;
            this.translateWordByWordToSecondRussianSubtitlesButton.Text = "По-\r\nслов-\r\nно";
            this.translateWordByWordToSecondRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.translateWordByWordToSecondRussianSubtitlesButton.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // translateToSecondRussianSubtitlesButton
            // 
            this.translateToSecondRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.translateToSecondRussianSubtitlesButton.Image = ((System.Drawing.Image)(resources.GetObject("translateToSecondRussianSubtitlesButton.Image")));
            this.translateToSecondRussianSubtitlesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.translateToSecondRussianSubtitlesButton.Location = new System.Drawing.Point(6, 15);
            this.translateToSecondRussianSubtitlesButton.Name = "translateToSecondRussianSubtitlesButton";
            this.translateToSecondRussianSubtitlesButton.Size = new System.Drawing.Size(125, 49);
            this.translateToSecondRussianSubtitlesButton.TabIndex = 4;
            this.translateToSecondRussianSubtitlesButton.Text = "Перевести\r\nоригинальные\r\nсубтитры";
            this.translateToSecondRussianSubtitlesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.translateToSecondRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.translateToSecondRussianSubtitlesButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // secondRussianSubtitlesActionLabel
            // 
            this.secondRussianSubtitlesActionLabel.AutoSize = true;
            this.secondRussianSubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.secondRussianSubtitlesActionLabel.Name = "secondRussianSubtitlesActionLabel";
            this.secondRussianSubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.secondRussianSubtitlesActionLabel.TabIndex = 10;
            this.secondRussianSubtitlesActionLabel.Text = "1";
            this.secondRussianSubtitlesActionLabel.Visible = false;
            // 
            // secondRussianSubtitlesColorButton
            // 
            this.secondRussianSubtitlesColorButton.BackColor = System.Drawing.Color.Cyan;
            this.secondRussianSubtitlesColorButton.Location = new System.Drawing.Point(600, 19);
            this.secondRussianSubtitlesColorButton.Name = "secondRussianSubtitlesColorButton";
            this.secondRussianSubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.secondRussianSubtitlesColorButton.TabIndex = 9;
            this.secondRussianSubtitlesColorButton.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(598, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Цвет";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // secondRussianSubtitlesTextBox
            // 
            this.secondRussianSubtitlesTextBox.Location = new System.Drawing.Point(6, 19);
            this.secondRussianSubtitlesTextBox.Name = "secondRussianSubtitlesTextBox";
            this.secondRussianSubtitlesTextBox.ReadOnly = true;
            this.secondRussianSubtitlesTextBox.Size = new System.Drawing.Size(329, 20);
            this.secondRussianSubtitlesTextBox.TabIndex = 1;
            // 
            // openSecondRussianSubtitlesButton
            // 
            this.openSecondRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.openSecondRussianSubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openSecondRussianSubtitlesButton.Name = "openSecondRussianSubtitlesButton";
            this.openSecondRussianSubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openSecondRussianSubtitlesButton.TabIndex = 0;
            this.openSecondRussianSubtitlesButton.Text = "📁 Открыть из файла";
            this.openSecondRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.openSecondRussianSubtitlesButton.Click += new System.EventHandler(this.openSecondRussianSubtitlesButton_Click);
            // 
            // secondRussianSubtitlesProgressLabel
            // 
            this.secondRussianSubtitlesProgressLabel.AutoSize = true;
            this.secondRussianSubtitlesProgressLabel.Location = new System.Drawing.Point(186, 42);
            this.secondRussianSubtitlesProgressLabel.Name = "secondRussianSubtitlesProgressLabel";
            this.secondRussianSubtitlesProgressLabel.Size = new System.Drawing.Size(146, 13);
            this.secondRussianSubtitlesProgressLabel.TabIndex = 2;
            this.secondRussianSubtitlesProgressLabel.Text = "secondRussianSubtitlesLabel";
            this.secondRussianSubtitlesProgressLabel.Visible = false;
            // 
            // secondRussianSubtitlesProgressBar
            // 
            this.secondRussianSubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.secondRussianSubtitlesProgressBar.Name = "secondRussianSubtitlesProgressBar";
            this.secondRussianSubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.secondRussianSubtitlesProgressBar.TabIndex = 3;
            this.secondRussianSubtitlesProgressBar.Visible = false;
            // 
            // thirdRussianSubtitlesGroupBox
            // 
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.groupBox8);
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.thirdRussianSubtitlesActionLabel);
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.thirdRussianSubtitlesColorButton);
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.label7);
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.thirdRussianSubtitlesTextBox);
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.openThirdRussianSubtitlesButton);
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.thirdRussianSubtitlesProgressLabel);
            this.thirdRussianSubtitlesGroupBox.Controls.Add(this.thirdRussianSubtitlesProgressBar);
            this.thirdRussianSubtitlesGroupBox.Location = new System.Drawing.Point(6, 283);
            this.thirdRussianSubtitlesGroupBox.Name = "thirdRussianSubtitlesGroupBox";
            this.thirdRussianSubtitlesGroupBox.Size = new System.Drawing.Size(681, 81);
            this.thirdRussianSubtitlesGroupBox.TabIndex = 24;
            this.thirdRussianSubtitlesGroupBox.TabStop = false;
            this.thirdRussianSubtitlesGroupBox.Text = "3-й поток русских субтитров";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.translateWordByWordToThirdRussianSubtitlesButton);
            this.groupBox8.Controls.Add(this.translateToThirdRussianSubtitlesButton);
            this.groupBox8.Location = new System.Drawing.Point(408, 8);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(184, 70);
            this.groupBox8.TabIndex = 15;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Яндекс.Переводчик";
            // 
            // translateWordByWordToThirdRussianSubtitlesButton
            // 
            this.translateWordByWordToThirdRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.translateWordByWordToThirdRussianSubtitlesButton.Location = new System.Drawing.Point(129, 15);
            this.translateWordByWordToThirdRussianSubtitlesButton.Name = "translateWordByWordToThirdRussianSubtitlesButton";
            this.translateWordByWordToThirdRussianSubtitlesButton.Size = new System.Drawing.Size(44, 49);
            this.translateWordByWordToThirdRussianSubtitlesButton.TabIndex = 16;
            this.translateWordByWordToThirdRussianSubtitlesButton.Text = "По-\r\nслов-\r\nно";
            this.translateWordByWordToThirdRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.translateWordByWordToThirdRussianSubtitlesButton.Click += new System.EventHandler(this.button8_Click);
            // 
            // translateToThirdRussianSubtitlesButton
            // 
            this.translateToThirdRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.translateToThirdRussianSubtitlesButton.Image = ((System.Drawing.Image)(resources.GetObject("translateToThirdRussianSubtitlesButton.Image")));
            this.translateToThirdRussianSubtitlesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.translateToThirdRussianSubtitlesButton.Location = new System.Drawing.Point(6, 15);
            this.translateToThirdRussianSubtitlesButton.Name = "translateToThirdRussianSubtitlesButton";
            this.translateToThirdRussianSubtitlesButton.Size = new System.Drawing.Size(125, 49);
            this.translateToThirdRussianSubtitlesButton.TabIndex = 4;
            this.translateToThirdRussianSubtitlesButton.Text = "Перевести\r\nоригинальные\r\nсубтитры";
            this.translateToThirdRussianSubtitlesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.translateToThirdRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.translateToThirdRussianSubtitlesButton.Click += new System.EventHandler(this.button7_Click);
            // 
            // thirdRussianSubtitlesActionLabel
            // 
            this.thirdRussianSubtitlesActionLabel.AutoSize = true;
            this.thirdRussianSubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.thirdRussianSubtitlesActionLabel.Name = "thirdRussianSubtitlesActionLabel";
            this.thirdRussianSubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.thirdRussianSubtitlesActionLabel.TabIndex = 11;
            this.thirdRussianSubtitlesActionLabel.Text = "1";
            this.thirdRussianSubtitlesActionLabel.Visible = false;
            // 
            // thirdRussianSubtitlesColorButton
            // 
            this.thirdRussianSubtitlesColorButton.BackColor = System.Drawing.Color.Gold;
            this.thirdRussianSubtitlesColorButton.Location = new System.Drawing.Point(600, 19);
            this.thirdRussianSubtitlesColorButton.Name = "thirdRussianSubtitlesColorButton";
            this.thirdRussianSubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.thirdRussianSubtitlesColorButton.TabIndex = 8;
            this.thirdRussianSubtitlesColorButton.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(598, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Цвет";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thirdRussianSubtitlesTextBox
            // 
            this.thirdRussianSubtitlesTextBox.Location = new System.Drawing.Point(6, 19);
            this.thirdRussianSubtitlesTextBox.Name = "thirdRussianSubtitlesTextBox";
            this.thirdRussianSubtitlesTextBox.ReadOnly = true;
            this.thirdRussianSubtitlesTextBox.Size = new System.Drawing.Size(329, 20);
            this.thirdRussianSubtitlesTextBox.TabIndex = 1;
            // 
            // openThirdRussianSubtitlesButton
            // 
            this.openThirdRussianSubtitlesButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.openThirdRussianSubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openThirdRussianSubtitlesButton.Name = "openThirdRussianSubtitlesButton";
            this.openThirdRussianSubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openThirdRussianSubtitlesButton.TabIndex = 0;
            this.openThirdRussianSubtitlesButton.Text = "📁 Открыть из файла";
            this.openThirdRussianSubtitlesButton.UseVisualStyleBackColor = false;
            this.openThirdRussianSubtitlesButton.Click += new System.EventHandler(this.openThirdRussianSubtitlesButton_Click);
            // 
            // thirdRussianSubtitlesProgressLabel
            // 
            this.thirdRussianSubtitlesProgressLabel.AutoSize = true;
            this.thirdRussianSubtitlesProgressLabel.Location = new System.Drawing.Point(201, 42);
            this.thirdRussianSubtitlesProgressLabel.Name = "thirdRussianSubtitlesProgressLabel";
            this.thirdRussianSubtitlesProgressLabel.Size = new System.Drawing.Size(131, 13);
            this.thirdRussianSubtitlesProgressLabel.TabIndex = 2;
            this.thirdRussianSubtitlesProgressLabel.Text = "thirdRussianSubtitlesLabel";
            this.thirdRussianSubtitlesProgressLabel.Visible = false;
            // 
            // thirdRussianSubtitlesProgressBar
            // 
            this.thirdRussianSubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.thirdRussianSubtitlesProgressBar.Name = "thirdRussianSubtitlesProgressBar";
            this.thirdRussianSubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.thirdRussianSubtitlesProgressBar.TabIndex = 3;
            this.thirdRussianSubtitlesProgressBar.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(750, 714);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.settingsButton);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Bilingual Subtitler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.secondRussianSubtitlesGroupBox.ResumeLayout(false);
            this.secondRussianSubtitlesGroupBox.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.thirdRussianSubtitlesGroupBox.ResumeLayout(false);
            this.thirdRussianSubtitlesGroupBox.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox videoStateComboBox;
        private System.Windows.Forms.ComboBox subtitlesStateComboBox;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button showSecondRussianSubtitlesButton;
        private System.Windows.Forms.Button hideSecondRussianSubtitlesButton;
        private System.Windows.Forms.Button hideThirdRussianSubtitlesButton;
        private System.Windows.Forms.Button showThirdRussianSubtitlesButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button translateToFirstRussianSubtitlesButton;
        private System.Windows.Forms.Button translateWordByWordToFirstRussianSubtitlesButton;
        private System.Windows.Forms.Label firstRussianSubtitlesActionLabel;
        private System.Windows.Forms.Button firstRussianSubtitlesColorButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstRussianSubtitlesTextBox;
        private System.Windows.Forms.Button openFirstRussianSubtitlesButton;
        private System.Windows.Forms.Label firstRussianSubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar firstRussianSubtitlesProgressBar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label primarySubtitlesActionLabel;
        private System.Windows.Forms.Button primarySubtitlesColorButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox primarySubtitlesTextBox;
        private System.Windows.Forms.Button openPrimarySubtitlesButton;
        private System.Windows.Forms.Button translateToPrimarySubtitlesButton;
        private System.Windows.Forms.Label primarySubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar primarySubtitlesProgressBar;
        private System.Windows.Forms.GroupBox secondRussianSubtitlesGroupBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button translateWordByWordToSecondRussianSubtitlesButton;
        private System.Windows.Forms.Button translateToSecondRussianSubtitlesButton;
        private System.Windows.Forms.Label secondRussianSubtitlesActionLabel;
        private System.Windows.Forms.Button secondRussianSubtitlesColorButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox secondRussianSubtitlesTextBox;
        private System.Windows.Forms.Button openSecondRussianSubtitlesButton;
        private System.Windows.Forms.Label secondRussianSubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar secondRussianSubtitlesProgressBar;
        private System.Windows.Forms.GroupBox thirdRussianSubtitlesGroupBox;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button translateWordByWordToThirdRussianSubtitlesButton;
        private System.Windows.Forms.Button translateToThirdRussianSubtitlesButton;
        private System.Windows.Forms.Label thirdRussianSubtitlesActionLabel;
        private System.Windows.Forms.Button thirdRussianSubtitlesColorButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox thirdRussianSubtitlesTextBox;
        private System.Windows.Forms.Button openThirdRussianSubtitlesButton;
        private System.Windows.Forms.Label thirdRussianSubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar thirdRussianSubtitlesProgressBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button selectVideoFileToGetPathForSubtitlesButton;
        private System.Windows.Forms.Label bilingualSubtitlesFileNameEndingLabel;
        private System.Windows.Forms.Label originalSubtitlesFileNameEndingLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox finalSubtitlesFilesPathBeginningRichTextBox;
        private System.Windows.Forms.TextBox bilingualSubtitlesFileNameEnding;
        private System.Windows.Forms.TextBox originalSubtitlesFileNameEnding;
        private System.Windows.Forms.Button createOriginalAndBilingualSubtitlesFilesButton;
    }
}

