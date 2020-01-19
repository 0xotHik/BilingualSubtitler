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
            this.openPrimarySubtitlesButton = new System.Windows.Forms.Button();
            this.primarySubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.primarySubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.primarySubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.translateToPrimarySubtitlesButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.primarySubtitlesActionLabel = new System.Windows.Forms.Label();
            this.primarySubtitlesColorButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.firstRussianSubtitlesActionLabel = new System.Windows.Forms.Label();
            this.firstRussianSubtitlesColorButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.firstRussianSubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.openFirstRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.translateToFirstRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.firstRussianSubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.firstRussianSubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.secondRussianSubtitlesActionLabel = new System.Windows.Forms.Label();
            this.secondRussianSubtitlesColorButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.secondRussianSubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.openSecondRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.translateToSecondRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.secondRussianSubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.secondRussianSubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.thirdRussianSubtitlesActionLabel = new System.Windows.Forms.Label();
            this.thirdRussianSubtitlesColorButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.thirdRussianSubtitlesTextBox = new System.Windows.Forms.TextBox();
            this.openThirdRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.translateToThirdRussianSubtitlesButton = new System.Windows.Forms.Button();
            this.thirdRussianSubtitlesProgressLabel = new System.Windows.Forms.Label();
            this.thirdRussianSubtitlesProgressBar = new System.Windows.Forms.ProgressBar();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.videoStateComboBox = new System.Windows.Forms.ComboBox();
            this.subtitlesStateComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // openPrimarySubtitlesButton
            // 
            this.openPrimarySubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openPrimarySubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openPrimarySubtitlesButton.Name = "openPrimarySubtitlesButton";
            this.openPrimarySubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openPrimarySubtitlesButton.TabIndex = 0;
            this.openPrimarySubtitlesButton.Text = "Открыть из файла";
            this.openPrimarySubtitlesButton.UseVisualStyleBackColor = true;
            this.openPrimarySubtitlesButton.Click += new System.EventHandler(this.openPrimarySubtitlesButton_Click);
            // 
            // primarySubtitlesTextBox
            // 
            this.primarySubtitlesTextBox.Location = new System.Drawing.Point(6, 19);
            this.primarySubtitlesTextBox.Name = "primarySubtitlesTextBox";
            this.primarySubtitlesTextBox.ReadOnly = true;
            this.primarySubtitlesTextBox.Size = new System.Drawing.Size(329, 20);
            this.primarySubtitlesTextBox.TabIndex = 1;
            // 
            // primarySubtitlesProgressLabel
            // 
            this.primarySubtitlesProgressLabel.AutoSize = true;
            this.primarySubtitlesProgressLabel.Location = new System.Drawing.Point(297, 42);
            this.primarySubtitlesProgressLabel.Name = "primarySubtitlesProgressLabel";
            this.primarySubtitlesProgressLabel.Size = new System.Drawing.Size(35, 13);
            this.primarySubtitlesProgressLabel.TabIndex = 2;
            this.primarySubtitlesProgressLabel.Text = "label1";
            // 
            // primarySubtitlesProgressBar
            // 
            this.primarySubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.primarySubtitlesProgressBar.Name = "primarySubtitlesProgressBar";
            this.primarySubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.primarySubtitlesProgressBar.TabIndex = 3;
            // 
            // translateToPrimarySubtitlesButton
            // 
            this.translateToPrimarySubtitlesButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.translateToPrimarySubtitlesButton.Enabled = false;
            this.translateToPrimarySubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.translateToPrimarySubtitlesButton.Location = new System.Drawing.Point(410, 19);
            this.translateToPrimarySubtitlesButton.Name = "translateToPrimarySubtitlesButton";
            this.translateToPrimarySubtitlesButton.Size = new System.Drawing.Size(65, 49);
            this.translateToPrimarySubtitlesButton.TabIndex = 4;
            this.translateToPrimarySubtitlesButton.Text = "Перевести";
            this.translateToPrimarySubtitlesButton.UseVisualStyleBackColor = false;
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
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(557, 81);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // primarySubtitlesActionLabel
            // 
            this.primarySubtitlesActionLabel.AutoSize = true;
            this.primarySubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.primarySubtitlesActionLabel.Name = "primarySubtitlesActionLabel";
            this.primarySubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.primarySubtitlesActionLabel.TabIndex = 8;
            this.primarySubtitlesActionLabel.Text = "1";
            // 
            // primarySubtitlesColorButton
            // 
            this.primarySubtitlesColorButton.BackColor = System.Drawing.Color.White;
            this.primarySubtitlesColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.primarySubtitlesColorButton.Location = new System.Drawing.Point(481, 19);
            this.primarySubtitlesColorButton.Name = "primarySubtitlesColorButton";
            this.primarySubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.primarySubtitlesColorButton.TabIndex = 7;
            this.primarySubtitlesColorButton.UseVisualStyleBackColor = false;
            this.primarySubtitlesColorButton.Click += new System.EventHandler(this.colorPickingButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(478, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Цвет";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesActionLabel);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesColorButton);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesTextBox);
            this.groupBox2.Controls.Add(this.openFirstRussianSubtitlesButton);
            this.groupBox2.Controls.Add(this.translateToFirstRussianSubtitlesButton);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesProgressLabel);
            this.groupBox2.Controls.Add(this.firstRussianSubtitlesProgressBar);
            this.groupBox2.Location = new System.Drawing.Point(12, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(557, 81);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // firstRussianSubtitlesActionLabel
            // 
            this.firstRussianSubtitlesActionLabel.AutoSize = true;
            this.firstRussianSubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.firstRussianSubtitlesActionLabel.Name = "firstRussianSubtitlesActionLabel";
            this.firstRussianSubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.firstRussianSubtitlesActionLabel.TabIndex = 9;
            this.firstRussianSubtitlesActionLabel.Text = "1";
            // 
            // firstRussianSubtitlesColorButton
            // 
            this.firstRussianSubtitlesColorButton.BackColor = System.Drawing.Color.Silver;
            this.firstRussianSubtitlesColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.firstRussianSubtitlesColorButton.Location = new System.Drawing.Point(481, 19);
            this.firstRussianSubtitlesColorButton.Name = "firstRussianSubtitlesColorButton";
            this.firstRussianSubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.firstRussianSubtitlesColorButton.TabIndex = 8;
            this.firstRussianSubtitlesColorButton.UseVisualStyleBackColor = false;
            this.firstRussianSubtitlesColorButton.Click += new System.EventHandler(this.colorPickingButton_Click);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(478, 55);
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
            this.openFirstRussianSubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openFirstRussianSubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openFirstRussianSubtitlesButton.Name = "openFirstRussianSubtitlesButton";
            this.openFirstRussianSubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openFirstRussianSubtitlesButton.TabIndex = 0;
            this.openFirstRussianSubtitlesButton.Text = "Открыть из файла";
            this.openFirstRussianSubtitlesButton.UseVisualStyleBackColor = true;
            this.openFirstRussianSubtitlesButton.Click += new System.EventHandler(this.openFirstRussianSubtitlesButton_Click);
            // 
            // translateToFirstRussianSubtitlesButton
            // 
            this.translateToFirstRussianSubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.translateToFirstRussianSubtitlesButton.Location = new System.Drawing.Point(410, 19);
            this.translateToFirstRussianSubtitlesButton.Name = "translateToFirstRussianSubtitlesButton";
            this.translateToFirstRussianSubtitlesButton.Size = new System.Drawing.Size(65, 49);
            this.translateToFirstRussianSubtitlesButton.TabIndex = 4;
            this.translateToFirstRussianSubtitlesButton.Text = "Перевести";
            this.translateToFirstRussianSubtitlesButton.UseVisualStyleBackColor = true;
            this.translateToFirstRussianSubtitlesButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // firstRussianSubtitlesProgressLabel
            // 
            this.firstRussianSubtitlesProgressLabel.AutoSize = true;
            this.firstRussianSubtitlesProgressLabel.Location = new System.Drawing.Point(205, 42);
            this.firstRussianSubtitlesProgressLabel.Name = "firstRussianSubtitlesProgressLabel";
            this.firstRussianSubtitlesProgressLabel.Size = new System.Drawing.Size(127, 13);
            this.firstRussianSubtitlesProgressLabel.TabIndex = 2;
            this.firstRussianSubtitlesProgressLabel.Text = "firstRussianSubtitlesLabel";
            // 
            // firstRussianSubtitlesProgressBar
            // 
            this.firstRussianSubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.firstRussianSubtitlesProgressBar.Name = "firstRussianSubtitlesProgressBar";
            this.firstRussianSubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.firstRussianSubtitlesProgressBar.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.secondRussianSubtitlesActionLabel);
            this.groupBox3.Controls.Add(this.secondRussianSubtitlesColorButton);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.secondRussianSubtitlesTextBox);
            this.groupBox3.Controls.Add(this.openSecondRussianSubtitlesButton);
            this.groupBox3.Controls.Add(this.translateToSecondRussianSubtitlesButton);
            this.groupBox3.Controls.Add(this.secondRussianSubtitlesProgressLabel);
            this.groupBox3.Controls.Add(this.secondRussianSubtitlesProgressBar);
            this.groupBox3.Location = new System.Drawing.Point(12, 186);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(557, 81);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // secondRussianSubtitlesActionLabel
            // 
            this.secondRussianSubtitlesActionLabel.AutoSize = true;
            this.secondRussianSubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.secondRussianSubtitlesActionLabel.Name = "secondRussianSubtitlesActionLabel";
            this.secondRussianSubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.secondRussianSubtitlesActionLabel.TabIndex = 10;
            this.secondRussianSubtitlesActionLabel.Text = "1";
            // 
            // secondRussianSubtitlesColorButton
            // 
            this.secondRussianSubtitlesColorButton.BackColor = System.Drawing.Color.Cyan;
            this.secondRussianSubtitlesColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.secondRussianSubtitlesColorButton.Location = new System.Drawing.Point(481, 19);
            this.secondRussianSubtitlesColorButton.Name = "secondRussianSubtitlesColorButton";
            this.secondRussianSubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.secondRussianSubtitlesColorButton.TabIndex = 9;
            this.secondRussianSubtitlesColorButton.UseVisualStyleBackColor = false;
            this.secondRussianSubtitlesColorButton.Click += new System.EventHandler(this.colorPickingButton_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(478, 55);
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
            this.openSecondRussianSubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openSecondRussianSubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openSecondRussianSubtitlesButton.Name = "openSecondRussianSubtitlesButton";
            this.openSecondRussianSubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openSecondRussianSubtitlesButton.TabIndex = 0;
            this.openSecondRussianSubtitlesButton.Text = "Открыть из файла";
            this.openSecondRussianSubtitlesButton.UseVisualStyleBackColor = true;
            this.openSecondRussianSubtitlesButton.Click += new System.EventHandler(this.openSecondRussianSubtitlesButton_Click);
            // 
            // translateToSecondRussianSubtitlesButton
            // 
            this.translateToSecondRussianSubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.translateToSecondRussianSubtitlesButton.Location = new System.Drawing.Point(410, 19);
            this.translateToSecondRussianSubtitlesButton.Name = "translateToSecondRussianSubtitlesButton";
            this.translateToSecondRussianSubtitlesButton.Size = new System.Drawing.Size(65, 49);
            this.translateToSecondRussianSubtitlesButton.TabIndex = 4;
            this.translateToSecondRussianSubtitlesButton.Text = "Перевести";
            this.translateToSecondRussianSubtitlesButton.UseVisualStyleBackColor = true;
            this.translateToSecondRussianSubtitlesButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // secondRussianSubtitlesProgressLabel
            // 
            this.secondRussianSubtitlesProgressLabel.AutoSize = true;
            this.secondRussianSubtitlesProgressLabel.Location = new System.Drawing.Point(186, 42);
            this.secondRussianSubtitlesProgressLabel.Name = "secondRussianSubtitlesProgressLabel";
            this.secondRussianSubtitlesProgressLabel.Size = new System.Drawing.Size(146, 13);
            this.secondRussianSubtitlesProgressLabel.TabIndex = 2;
            this.secondRussianSubtitlesProgressLabel.Text = "secondRussianSubtitlesLabel";
            // 
            // secondRussianSubtitlesProgressBar
            // 
            this.secondRussianSubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.secondRussianSubtitlesProgressBar.Name = "secondRussianSubtitlesProgressBar";
            this.secondRussianSubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.secondRussianSubtitlesProgressBar.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.thirdRussianSubtitlesActionLabel);
            this.groupBox4.Controls.Add(this.thirdRussianSubtitlesColorButton);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.thirdRussianSubtitlesTextBox);
            this.groupBox4.Controls.Add(this.openThirdRussianSubtitlesButton);
            this.groupBox4.Controls.Add(this.translateToThirdRussianSubtitlesButton);
            this.groupBox4.Controls.Add(this.thirdRussianSubtitlesProgressLabel);
            this.groupBox4.Controls.Add(this.thirdRussianSubtitlesProgressBar);
            this.groupBox4.Location = new System.Drawing.Point(12, 273);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(557, 81);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "groupBox4";
            // 
            // thirdRussianSubtitlesActionLabel
            // 
            this.thirdRussianSubtitlesActionLabel.AutoSize = true;
            this.thirdRussianSubtitlesActionLabel.Location = new System.Drawing.Point(6, 42);
            this.thirdRussianSubtitlesActionLabel.Name = "thirdRussianSubtitlesActionLabel";
            this.thirdRussianSubtitlesActionLabel.Size = new System.Drawing.Size(13, 13);
            this.thirdRussianSubtitlesActionLabel.TabIndex = 11;
            this.thirdRussianSubtitlesActionLabel.Text = "1";
            // 
            // thirdRussianSubtitlesColorButton
            // 
            this.thirdRussianSubtitlesColorButton.BackColor = System.Drawing.Color.Gold;
            this.thirdRussianSubtitlesColorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.thirdRussianSubtitlesColorButton.Location = new System.Drawing.Point(481, 19);
            this.thirdRussianSubtitlesColorButton.Name = "thirdRussianSubtitlesColorButton";
            this.thirdRussianSubtitlesColorButton.Size = new System.Drawing.Size(63, 36);
            this.thirdRussianSubtitlesColorButton.TabIndex = 8;
            this.thirdRussianSubtitlesColorButton.UseVisualStyleBackColor = false;
            this.thirdRussianSubtitlesColorButton.Click += new System.EventHandler(this.colorPickingButton_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(478, 55);
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
            this.thirdRussianSubtitlesTextBox.Text = "thirdRussianSubtitlesTextBox";
            // 
            // openThirdRussianSubtitlesButton
            // 
            this.openThirdRussianSubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openThirdRussianSubtitlesButton.Location = new System.Drawing.Point(342, 19);
            this.openThirdRussianSubtitlesButton.Name = "openThirdRussianSubtitlesButton";
            this.openThirdRussianSubtitlesButton.Size = new System.Drawing.Size(62, 49);
            this.openThirdRussianSubtitlesButton.TabIndex = 0;
            this.openThirdRussianSubtitlesButton.Text = "Открыть из файла";
            this.openThirdRussianSubtitlesButton.UseVisualStyleBackColor = true;
            this.openThirdRussianSubtitlesButton.Click += new System.EventHandler(this.openThirdRussianSubtitlesButton_Click);
            // 
            // translateToThirdRussianSubtitlesButton
            // 
            this.translateToThirdRussianSubtitlesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.translateToThirdRussianSubtitlesButton.Location = new System.Drawing.Point(410, 19);
            this.translateToThirdRussianSubtitlesButton.Name = "translateToThirdRussianSubtitlesButton";
            this.translateToThirdRussianSubtitlesButton.Size = new System.Drawing.Size(65, 49);
            this.translateToThirdRussianSubtitlesButton.TabIndex = 4;
            this.translateToThirdRussianSubtitlesButton.Text = "Перевести";
            this.translateToThirdRussianSubtitlesButton.UseVisualStyleBackColor = true;
            this.translateToThirdRussianSubtitlesButton.Click += new System.EventHandler(this.button7_Click);
            // 
            // thirdRussianSubtitlesProgressLabel
            // 
            this.thirdRussianSubtitlesProgressLabel.AutoSize = true;
            this.thirdRussianSubtitlesProgressLabel.Location = new System.Drawing.Point(201, 42);
            this.thirdRussianSubtitlesProgressLabel.Name = "thirdRussianSubtitlesProgressLabel";
            this.thirdRussianSubtitlesProgressLabel.Size = new System.Drawing.Size(131, 13);
            this.thirdRussianSubtitlesProgressLabel.TabIndex = 2;
            this.thirdRussianSubtitlesProgressLabel.Text = "thirdRussianSubtitlesLabel";
            // 
            // thirdRussianSubtitlesProgressBar
            // 
            this.thirdRussianSubtitlesProgressBar.Location = new System.Drawing.Point(6, 58);
            this.thirdRussianSubtitlesProgressBar.Name = "thirdRussianSubtitlesProgressBar";
            this.thirdRussianSubtitlesProgressBar.Size = new System.Drawing.Size(329, 10);
            this.thirdRussianSubtitlesProgressBar.TabIndex = 3;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(354, 360);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(215, 46);
            this.button8.TabIndex = 8;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(18, 360);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(329, 20);
            this.textBox5.TabIndex = 7;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(18, 386);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(329, 20);
            this.textBox6.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "label9";
            // 
            // videoStateComboBox
            // 
            this.videoStateComboBox.FormattingEnabled = true;
            this.videoStateComboBox.Location = new System.Drawing.Point(119, 444);
            this.videoStateComboBox.Name = "videoStateComboBox";
            this.videoStateComboBox.Size = new System.Drawing.Size(121, 21);
            this.videoStateComboBox.TabIndex = 11;
            this.videoStateComboBox.SelectedValueChanged += new System.EventHandler(this.videoStateComboBox_SelectedValueChanged);
            // 
            // subtitlesStateComboBox
            // 
            this.subtitlesStateComboBox.FormattingEnabled = true;
            this.subtitlesStateComboBox.Location = new System.Drawing.Point(340, 443);
            this.subtitlesStateComboBox.Name = "subtitlesStateComboBox";
            this.subtitlesStateComboBox.Size = new System.Drawing.Size(121, 21);
            this.subtitlesStateComboBox.TabIndex = 12;
            this.subtitlesStateComboBox.SelectedValueChanged += new System.EventHandler(this.subtitlesStateComboBox_SelectedValueChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(531, 422);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 46);
            this.button1.TabIndex = 13;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(668, 502);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.subtitlesStateComboBox);
            this.Controls.Add(this.videoStateComboBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Bilingual Subtitler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button openPrimarySubtitlesButton;
        private System.Windows.Forms.TextBox primarySubtitlesTextBox;
        private System.Windows.Forms.Label primarySubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar primarySubtitlesProgressBar;
        private System.Windows.Forms.Button translateToPrimarySubtitlesButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox firstRussianSubtitlesTextBox;
        private System.Windows.Forms.Button openFirstRussianSubtitlesButton;
        private System.Windows.Forms.Button translateToFirstRussianSubtitlesButton;
        private System.Windows.Forms.Label firstRussianSubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar firstRussianSubtitlesProgressBar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox secondRussianSubtitlesTextBox;
        private System.Windows.Forms.Button openSecondRussianSubtitlesButton;
        private System.Windows.Forms.Button translateToSecondRussianSubtitlesButton;
        private System.Windows.Forms.Label secondRussianSubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar secondRussianSubtitlesProgressBar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox thirdRussianSubtitlesTextBox;
        private System.Windows.Forms.Button openThirdRussianSubtitlesButton;
        private System.Windows.Forms.Button translateToThirdRussianSubtitlesButton;
        private System.Windows.Forms.Label thirdRussianSubtitlesProgressLabel;
        private System.Windows.Forms.ProgressBar thirdRussianSubtitlesProgressBar;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button primarySubtitlesColorButton;
        private System.Windows.Forms.Button firstRussianSubtitlesColorButton;
        private System.Windows.Forms.Button secondRussianSubtitlesColorButton;
        private System.Windows.Forms.Button thirdRussianSubtitlesColorButton;
        private System.Windows.Forms.Label primarySubtitlesActionLabel;
        private System.Windows.Forms.Label firstRussianSubtitlesActionLabel;
        private System.Windows.Forms.Label secondRussianSubtitlesActionLabel;
        private System.Windows.Forms.Label thirdRussianSubtitlesActionLabel;
        private System.Windows.Forms.ComboBox videoStateComboBox;
        private System.Windows.Forms.ComboBox subtitlesStateComboBox;
        private System.Windows.Forms.Button button1;
    }
}

