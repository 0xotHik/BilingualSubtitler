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
            richTextBoxForYandexApiKeyInSeparateForm = new System.Windows.Forms.RichTextBox();
            buttonOk = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            linkLabelYandexAPIKeysList = new System.Windows.Forms.LinkLabel();
            yandexTranslatorGroupBox = new System.Windows.Forms.GroupBox();
            gotTheYandexTranslatorAPIKeyCheckBox = new System.Windows.Forms.CheckBox();
            hotkeysDataGridView = new System.Windows.Forms.DataGridView();
            keyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            groupBox2 = new System.Windows.Forms.GroupBox();
            button9 = new System.Windows.Forms.Button();
            button8 = new System.Windows.Forms.Button();
            button7 = new System.Windows.Forms.Button();
            button1 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            groupBox5 = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            CreateBilingualSubtitlesFileCheckBox = new System.Windows.Forms.CheckBox();
            CreateOriginalSubtitlesFileCheckBox = new System.Windows.Forms.CheckBox();
            bilingualSubtitlesPathEndingTextBox = new System.Windows.Forms.TextBox();
            originalSubtitlesPathEndingTextBox = new System.Windows.Forms.TextBox();
            groupBox6 = new System.Windows.Forms.GroupBox();
            button6 = new System.Windows.Forms.Button();
            label10 = new System.Windows.Forms.Label();
            videoplayerProcessNameTextBox = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            button4 = new System.Windows.Forms.Button();
            videoPlayerChangeToOriginalSubtitlesButtonTextBox = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            button3 = new System.Windows.Forms.Button();
            videoPlayerChangeToBilingualSubtitlesButtonTextBox = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            button2 = new System.Windows.Forms.Button();
            videoPlayerPauseButtonTextBox = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            processPriorityGroupBox = new System.Windows.Forms.GroupBox();
            targetProcessPriorityTextBox = new System.Windows.Forms.ComboBox();
            currentProcessPriorityTextBox = new System.Windows.Forms.TextBox();
            label38 = new System.Windows.Forms.Label();
            label37 = new System.Windows.Forms.Label();
            checkUpdatesGroupBox = new System.Windows.Forms.GroupBox();
            linkLabel2 = new System.Windows.Forms.LinkLabel();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            lastAppVersionLabel = new System.Windows.Forms.Label();
            currentAppVersionLabel = new System.Windows.Forms.Label();
            label39 = new System.Windows.Forms.Label();
            checkUpdatesOnAppStartCheckBox = new System.Windows.Forms.CheckBox();
            label40 = new System.Windows.Forms.Label();
            groupBox12 = new System.Windows.Forms.GroupBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            advancedModeRadioButton = new System.Windows.Forms.RadioButton();
            notAdvancedModeRadioButton = new System.Windows.Forms.RadioButton();
            downloadsDirectoryGroupBox = new System.Windows.Forms.GroupBox();
            downloadsFolderPathSetButton = new System.Windows.Forms.Button();
            downloadsFolderPathRichTextBox = new System.Windows.Forms.RichTextBox();
            defaultFolderPathSetButton = new System.Windows.Forms.Button();
            label42 = new System.Windows.Forms.Label();
            defaultFolderPathRichTextBox = new System.Windows.Forms.RichTextBox();
            button11 = new System.Windows.Forms.Button();
            subtitlesAppearanceSettingsControl = new SubtitlesAppearanceSettings();
            defaultDirectoryGroupBox = new System.Windows.Forms.GroupBox();
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox = new System.Windows.Forms.CheckBox();
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox = new System.Windows.Forms.CheckBox();
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox = new System.Windows.Forms.CheckBox();
            removeAnCheckBox = new System.Windows.Forms.CheckBox();
            yandexTranslatorGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)hotkeysDataGridView).BeginInit();
            groupBox2.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            processPriorityGroupBox.SuspendLayout();
            checkUpdatesGroupBox.SuspendLayout();
            groupBox12.SuspendLayout();
            downloadsDirectoryGroupBox.SuspendLayout();
            defaultDirectoryGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // richTextBoxForYandexApiKeyInSeparateForm
            // 
            richTextBoxForYandexApiKeyInSeparateForm.Location = new System.Drawing.Point(340, 22);
            richTextBoxForYandexApiKeyInSeparateForm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            richTextBoxForYandexApiKeyInSeparateForm.Name = "richTextBoxForYandexApiKeyInSeparateForm";
            richTextBoxForYandexApiKeyInSeparateForm.Size = new System.Drawing.Size(871, 50);
            richTextBoxForYandexApiKeyInSeparateForm.TabIndex = 2;
            richTextBoxForYandexApiKeyInSeparateForm.Text = "";
            // 
            // buttonOk
            // 
            buttonOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOk.Image = Properties.Resources._16pxOkIcon;
            buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonOk.Location = new System.Drawing.Point(1138, 942);
            buttonOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(111, 40);
            buttonOk.TabIndex = 5;
            buttonOk.Text = "Применить";
            buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            buttonCancel.Image = Properties.Resources._16pxCancelIconAnother;
            buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCancel.Location = new System.Drawing.Point(564, 942);
            buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(86, 40);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Отмена";
            buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // linkLabelYandexAPIKeysList
            // 
            linkLabelYandexAPIKeysList.AutoSize = true;
            linkLabelYandexAPIKeysList.Location = new System.Drawing.Point(15, 57);
            linkLabelYandexAPIKeysList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabelYandexAPIKeysList.Name = "linkLabelYandexAPIKeysList";
            linkLabelYandexAPIKeysList.Size = new System.Drawing.Size(269, 15);
            linkLabelYandexAPIKeysList.TabIndex = 43;
            linkLabelYandexAPIKeysList.TabStop = true;
            linkLabelYandexAPIKeysList.Text = "Открыть список уже полученных вами ключей";
            linkLabelYandexAPIKeysList.LinkClicked += linkLabelYandexAPIKeysList_LinkClicked;
            // 
            // yandexTranslatorGroupBox
            // 
            yandexTranslatorGroupBox.Controls.Add(gotTheYandexTranslatorAPIKeyCheckBox);
            yandexTranslatorGroupBox.Controls.Add(linkLabelYandexAPIKeysList);
            yandexTranslatorGroupBox.Controls.Add(richTextBoxForYandexApiKeyInSeparateForm);
            yandexTranslatorGroupBox.Location = new System.Drawing.Point(18, 847);
            yandexTranslatorGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            yandexTranslatorGroupBox.Name = "yandexTranslatorGroupBox";
            yandexTranslatorGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            yandexTranslatorGroupBox.Size = new System.Drawing.Size(1232, 87);
            yandexTranslatorGroupBox.TabIndex = 44;
            yandexTranslatorGroupBox.TabStop = false;
            yandexTranslatorGroupBox.Text = "Ключ Яндекс.Переводчика";
            // 
            // gotTheYandexTranslatorAPIKeyCheckBox
            // 
            gotTheYandexTranslatorAPIKeyCheckBox.AutoSize = true;
            gotTheYandexTranslatorAPIKeyCheckBox.Checked = true;
            gotTheYandexTranslatorAPIKeyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            gotTheYandexTranslatorAPIKeyCheckBox.Location = new System.Drawing.Point(19, 35);
            gotTheYandexTranslatorAPIKeyCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            gotTheYandexTranslatorAPIKeyCheckBox.Name = "gotTheYandexTranslatorAPIKeyCheckBox";
            gotTheYandexTranslatorAPIKeyCheckBox.Size = new System.Drawing.Size(283, 19);
            gotTheYandexTranslatorAPIKeyCheckBox.TabIndex = 97;
            gotTheYandexTranslatorAPIKeyCheckBox.Text = "У меня есть ключ для API Яндекс.Переводчика";
            gotTheYandexTranslatorAPIKeyCheckBox.UseVisualStyleBackColor = true;
            // 
            // hotkeysDataGridView
            // 
            hotkeysDataGridView.BackgroundColor = System.Drawing.Color.DarkGoldenrod;
            hotkeysDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            hotkeysDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { keyColumn });
            hotkeysDataGridView.Location = new System.Drawing.Point(19, 55);
            hotkeysDataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            hotkeysDataGridView.MultiSelect = false;
            hotkeysDataGridView.Name = "hotkeysDataGridView";
            hotkeysDataGridView.RowHeadersVisible = false;
            hotkeysDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            hotkeysDataGridView.Size = new System.Drawing.Size(169, 366);
            hotkeysDataGridView.TabIndex = 45;
            // 
            // keyColumn
            // 
            keyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            keyColumn.HeaderText = "Клавиши";
            keyColumn.Name = "keyColumn";
            keyColumn.ReadOnly = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button9);
            groupBox2.Controls.Add(button8);
            groupBox2.Controls.Add(button7);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(hotkeysDataGridView);
            groupBox2.Location = new System.Drawing.Point(380, 14);
            groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox2.Size = new System.Drawing.Size(210, 565);
            groupBox2.TabIndex = 46;
            groupBox2.TabStop = false;
            groupBox2.Text = "Горячие клавиши программы";
            // 
            // button9
            // 
            button9.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button9.Location = new System.Drawing.Point(19, 489);
            button9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(169, 70);
            button9.TabIndex = 49;
            button9.Text = "⌨️  Задать авторский \r\nрасширенный\r\nнабор горячих клавиш...";
            button9.UseVisualStyleBackColor = false;
            button9.Click += button9_Click;
            // 
            // button8
            // 
            button8.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button8.Location = new System.Drawing.Point(19, 456);
            button8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(169, 27);
            button8.TabIndex = 48;
            button8.Text = "x Очистить все";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // button7
            // 
            button7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button7.Location = new System.Drawing.Point(19, 428);
            button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(169, 27);
            button7.TabIndex = 47;
            button7.Text = "- Убрать выбранную";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button1.Location = new System.Drawing.Point(19, 22);
            button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(169, 27);
            button1.TabIndex = 46;
            button1.Text = "+ Задать новую";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button5.Location = new System.Drawing.Point(750, 942);
            button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(273, 40);
            button5.TabIndex = 48;
            button5.Text = "📖  Сбросить все настройки программы\r\n к значениям по умолчанию";
            button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(CreateBilingualSubtitlesFileCheckBox);
            groupBox5.Controls.Add(CreateOriginalSubtitlesFileCheckBox);
            groupBox5.Controls.Add(bilingualSubtitlesPathEndingTextBox);
            groupBox5.Controls.Add(originalSubtitlesPathEndingTextBox);
            groupBox5.Location = new System.Drawing.Point(18, 411);
            groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox5.Size = new System.Drawing.Size(348, 145);
            groupBox5.TabIndex = 69;
            groupBox5.TabStop = false;
            groupBox5.Text = "Создавать файлы:";
            groupBox5.Enter += groupBox5_Enter;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(107, 78);
            label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(127, 15);
            label4.TabIndex = 60;
            label4.Text = "с окончанием имени:";
            // 
            // CreateBilingualSubtitlesFileCheckBox
            // 
            CreateBilingualSubtitlesFileCheckBox.AutoSize = true;
            CreateBilingualSubtitlesFileCheckBox.Checked = true;
            CreateBilingualSubtitlesFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            CreateBilingualSubtitlesFileCheckBox.Location = new System.Drawing.Point(196, 27);
            CreateBilingualSubtitlesFileCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CreateBilingualSubtitlesFileCheckBox.Name = "CreateBilingualSubtitlesFileCheckBox";
            CreateBilingualSubtitlesFileCheckBox.Size = new System.Drawing.Size(93, 34);
            CreateBilingualSubtitlesFileCheckBox.TabIndex = 59;
            CreateBilingualSubtitlesFileCheckBox.Text = "двуязычных\r\nсубтитров";
            CreateBilingualSubtitlesFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // CreateOriginalSubtitlesFileCheckBox
            // 
            CreateOriginalSubtitlesFileCheckBox.AutoSize = true;
            CreateOriginalSubtitlesFileCheckBox.Checked = true;
            CreateOriginalSubtitlesFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            CreateOriginalSubtitlesFileCheckBox.Location = new System.Drawing.Point(63, 27);
            CreateOriginalSubtitlesFileCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            CreateOriginalSubtitlesFileCheckBox.Name = "CreateOriginalSubtitlesFileCheckBox";
            CreateOriginalSubtitlesFileCheckBox.Size = new System.Drawing.Size(107, 34);
            CreateOriginalSubtitlesFileCheckBox.TabIndex = 58;
            CreateOriginalSubtitlesFileCheckBox.Text = "оригинальных\r\nсубтитров";
            CreateOriginalSubtitlesFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // bilingualSubtitlesPathEndingTextBox
            // 
            bilingualSubtitlesPathEndingTextBox.Location = new System.Drawing.Point(196, 107);
            bilingualSubtitlesPathEndingTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            bilingualSubtitlesPathEndingTextBox.Name = "bilingualSubtitlesPathEndingTextBox";
            bilingualSubtitlesPathEndingTextBox.Size = new System.Drawing.Size(116, 23);
            bilingualSubtitlesPathEndingTextBox.TabIndex = 56;
            // 
            // originalSubtitlesPathEndingTextBox
            // 
            originalSubtitlesPathEndingTextBox.Location = new System.Drawing.Point(55, 107);
            originalSubtitlesPathEndingTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            originalSubtitlesPathEndingTextBox.Name = "originalSubtitlesPathEndingTextBox";
            originalSubtitlesPathEndingTextBox.Size = new System.Drawing.Size(116, 23);
            originalSubtitlesPathEndingTextBox.TabIndex = 55;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(button6);
            groupBox6.Controls.Add(label10);
            groupBox6.Controls.Add(videoplayerProcessNameTextBox);
            groupBox6.Controls.Add(label9);
            groupBox6.Controls.Add(button4);
            groupBox6.Controls.Add(videoPlayerChangeToOriginalSubtitlesButtonTextBox);
            groupBox6.Controls.Add(label8);
            groupBox6.Controls.Add(button3);
            groupBox6.Controls.Add(videoPlayerChangeToBilingualSubtitlesButtonTextBox);
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(button2);
            groupBox6.Controls.Add(videoPlayerPauseButtonTextBox);
            groupBox6.Controls.Add(label6);
            groupBox6.Controls.Add(label5);
            groupBox6.Location = new System.Drawing.Point(18, 123);
            groupBox6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox6.Size = new System.Drawing.Size(348, 284);
            groupBox6.TabIndex = 70;
            groupBox6.TabStop = false;
            groupBox6.Text = "Настройки видеоплеера";
            // 
            // button6
            // 
            button6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button6.Location = new System.Drawing.Point(196, 227);
            button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(134, 45);
            button6.TabIndex = 81;
            button6.Text = "🎥   Выбрать \r\nисполняемый файл";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(54, 208);
            label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(191, 15);
            label10.TabIndex = 80;
            label10.Text = "Название процесса видеоплеера:";
            // 
            // videoplayerProcessNameTextBox
            // 
            videoplayerProcessNameTextBox.Location = new System.Drawing.Point(57, 239);
            videoplayerProcessNameTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            videoplayerProcessNameTextBox.Name = "videoplayerProcessNameTextBox";
            videoplayerProcessNameTextBox.Size = new System.Drawing.Size(114, 23);
            videoplayerProcessNameTextBox.TabIndex = 79;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(28, 171);
            label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(81, 15);
            label9.TabIndex = 78;
            label9.Text = "предыдущие:";
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button4.Location = new System.Drawing.Point(245, 165);
            button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(85, 27);
            button4.TabIndex = 77;
            button4.Text = "⌨️  Задать";
            button4.UseVisualStyleBackColor = false;
            button4.Click += videoPlayerChangeToOriginalSubtitlesHotkeySetButton_Click;
            // 
            // videoPlayerChangeToOriginalSubtitlesButtonTextBox
            // 
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Location = new System.Drawing.Point(124, 167);
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Name = "videoPlayerChangeToOriginalSubtitlesButtonTextBox";
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.ReadOnly = true;
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Size = new System.Drawing.Size(114, 23);
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.TabIndex = 76;
            videoPlayerChangeToOriginalSubtitlesButtonTextBox.Text = ".eng.ass";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(28, 137);
            label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(75, 15);
            label8.TabIndex = 75;
            label8.Text = "следующие:";
            // 
            // button3
            // 
            button3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button3.Location = new System.Drawing.Point(245, 132);
            button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(85, 27);
            button3.TabIndex = 74;
            button3.Text = "⌨️  Задать";
            button3.UseVisualStyleBackColor = false;
            button3.Click += videoplayerNextSubtitlesHotkeySetButton_Click;
            // 
            // videoPlayerChangeToBilingualSubtitlesButtonTextBox
            // 
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Location = new System.Drawing.Point(124, 134);
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Name = "videoPlayerChangeToBilingualSubtitlesButtonTextBox";
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.ReadOnly = true;
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Size = new System.Drawing.Size(114, 23);
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.TabIndex = 73;
            videoPlayerChangeToBilingualSubtitlesButtonTextBox.Text = ".eng.ass";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(28, 108);
            label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(124, 15);
            label7.TabIndex = 72;
            label7.Text = "Смены субтитров на:";
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button2.Location = new System.Drawing.Point(245, 65);
            button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(85, 27);
            button2.TabIndex = 71;
            button2.Text = "⌨️  Задать";
            button2.UseVisualStyleBackColor = false;
            button2.Click += videoplayerPauseHotkeySetButton_Click;
            // 
            // videoPlayerPauseButtonTextBox
            // 
            videoPlayerPauseButtonTextBox.Location = new System.Drawing.Point(124, 67);
            videoPlayerPauseButtonTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            videoPlayerPauseButtonTextBox.Name = "videoPlayerPauseButtonTextBox";
            videoPlayerPauseButtonTextBox.ReadOnly = true;
            videoPlayerPauseButtonTextBox.Size = new System.Drawing.Size(114, 23);
            videoPlayerPauseButtonTextBox.TabIndex = 70;
            videoPlayerPauseButtonTextBox.Text = ".eng.ass";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(28, 70);
            label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(45, 15);
            label6.TabIndex = 69;
            label6.Text = "Паузы:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(54, 31);
            label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(192, 15);
            label5.TabIndex = 68;
            label5.Text = "Горячие клавиши в видеоплеере:";
            // 
            // processPriorityGroupBox
            // 
            processPriorityGroupBox.Controls.Add(targetProcessPriorityTextBox);
            processPriorityGroupBox.Controls.Add(currentProcessPriorityTextBox);
            processPriorityGroupBox.Controls.Add(label38);
            processPriorityGroupBox.Controls.Add(label37);
            processPriorityGroupBox.Location = new System.Drawing.Point(18, 670);
            processPriorityGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            processPriorityGroupBox.Name = "processPriorityGroupBox";
            processPriorityGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            processPriorityGroupBox.Size = new System.Drawing.Size(348, 74);
            processPriorityGroupBox.TabIndex = 98;
            processPriorityGroupBox.TabStop = false;
            processPriorityGroupBox.Text = "Приоритет процесса Bilingual Subtitler";
            // 
            // targetProcessPriorityTextBox
            // 
            targetProcessPriorityTextBox.FormattingEnabled = true;
            targetProcessPriorityTextBox.Location = new System.Drawing.Point(177, 44);
            targetProcessPriorityTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            targetProcessPriorityTextBox.Name = "targetProcessPriorityTextBox";
            targetProcessPriorityTextBox.Size = new System.Drawing.Size(150, 23);
            targetProcessPriorityTextBox.TabIndex = 96;
            targetProcessPriorityTextBox.Text = "Реального времени";
            // 
            // currentProcessPriorityTextBox
            // 
            currentProcessPriorityTextBox.Location = new System.Drawing.Point(19, 45);
            currentProcessPriorityTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            currentProcessPriorityTextBox.Name = "currentProcessPriorityTextBox";
            currentProcessPriorityTextBox.ReadOnly = true;
            currentProcessPriorityTextBox.Size = new System.Drawing.Size(136, 23);
            currentProcessPriorityTextBox.TabIndex = 82;
            currentProcessPriorityTextBox.Text = "Реального времени";
            currentProcessPriorityTextBox.TextChanged += currentProcessPriorityTextBox_TextChanged;
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Location = new System.Drawing.Point(174, 21);
            label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label38.Name = "label38";
            label38.Size = new System.Drawing.Size(71, 15);
            label38.TabIndex = 4;
            label38.Text = "Желаемый:";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Location = new System.Drawing.Point(15, 21);
            label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label37.Name = "label37";
            label37.Size = new System.Drawing.Size(59, 15);
            label37.TabIndex = 3;
            label37.Text = "Текущий:";
            // 
            // checkUpdatesGroupBox
            // 
            checkUpdatesGroupBox.Controls.Add(linkLabel2);
            checkUpdatesGroupBox.Controls.Add(linkLabel1);
            checkUpdatesGroupBox.Controls.Add(lastAppVersionLabel);
            checkUpdatesGroupBox.Controls.Add(currentAppVersionLabel);
            checkUpdatesGroupBox.Controls.Add(label39);
            checkUpdatesGroupBox.Controls.Add(checkUpdatesOnAppStartCheckBox);
            checkUpdatesGroupBox.Controls.Add(label40);
            checkUpdatesGroupBox.Location = new System.Drawing.Point(382, 670);
            checkUpdatesGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkUpdatesGroupBox.Name = "checkUpdatesGroupBox";
            checkUpdatesGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkUpdatesGroupBox.Size = new System.Drawing.Size(868, 74);
            checkUpdatesGroupBox.TabIndex = 99;
            checkUpdatesGroupBox.TabStop = false;
            checkUpdatesGroupBox.Text = "Обновления программы";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Location = new System.Drawing.Point(4, 49);
            linkLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new System.Drawing.Size(141, 15);
            linkLabel2.TabIndex = 103;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Канал новостей проекта";
            linkLabel2.LinkClicked += linkLabel2_LinkClicked;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new System.Drawing.Point(182, 49);
            linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(177, 15);
            linkLabel1.TabIndex = 102;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Страница релизов программы";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // lastAppVersionLabel
            // 
            lastAppVersionLabel.Location = new System.Drawing.Point(530, 41);
            lastAppVersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lastAppVersionLabel.Name = "lastAppVersionLabel";
            lastAppVersionLabel.Size = new System.Drawing.Size(328, 30);
            lastAppVersionLabel.TabIndex = 101;
            lastAppVersionLabel.Text = "Не удалось получить информацию о новых версиях :( \r\n(Подробности — по клику)";
            lastAppVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lastAppVersionLabel.Click += lastAppVersionLabel_Click;
            // 
            // currentAppVersionLabel
            // 
            currentAppVersionLabel.Location = new System.Drawing.Point(362, 49);
            currentAppVersionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            currentAppVersionLabel.Name = "currentAppVersionLabel";
            currentAppVersionLabel.Size = new System.Drawing.Size(169, 15);
            currentAppVersionLabel.TabIndex = 100;
            currentAppVersionLabel.Text = "Текущая версия:";
            currentAppVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Location = new System.Drawing.Point(639, 23);
            label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label39.Name = "label39";
            label39.Size = new System.Drawing.Size(111, 15);
            label39.TabIndex = 99;
            label39.Text = "Последняя версия:";
            label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkUpdatesOnAppStartCheckBox
            // 
            checkUpdatesOnAppStartCheckBox.AutoSize = true;
            checkUpdatesOnAppStartCheckBox.Checked = true;
            checkUpdatesOnAppStartCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            checkUpdatesOnAppStartCheckBox.Location = new System.Drawing.Point(7, 21);
            checkUpdatesOnAppStartCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkUpdatesOnAppStartCheckBox.Name = "checkUpdatesOnAppStartCheckBox";
            checkUpdatesOnAppStartCheckBox.Size = new System.Drawing.Size(286, 19);
            checkUpdatesOnAppStartCheckBox.TabIndex = 98;
            checkUpdatesOnAppStartCheckBox.Text = "Проверять обновления при старте программы";
            checkUpdatesOnAppStartCheckBox.UseVisualStyleBackColor = true;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Location = new System.Drawing.Point(397, 23);
            label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label40.Name = "label40";
            label40.Size = new System.Drawing.Size(98, 15);
            label40.TabIndex = 3;
            label40.Text = "Текущая версия:";
            label40.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox12
            // 
            groupBox12.Controls.Add(checkBox1);
            groupBox12.Controls.Add(advancedModeRadioButton);
            groupBox12.Controls.Add(notAdvancedModeRadioButton);
            groupBox12.Location = new System.Drawing.Point(18, 14);
            groupBox12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox12.Name = "groupBox12";
            groupBox12.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            groupBox12.Size = new System.Drawing.Size(348, 103);
            groupBox12.TabIndex = 82;
            groupBox12.TabStop = false;
            groupBox12.Text = "Главное окно Bilingual Subtitler";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(28, 74);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(316, 19);
            checkBox1.TabIndex = 62;
            checkBox1.Text = "Включить 4-й и 5-й потоки переведенных субтитров";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // advancedModeRadioButton
            // 
            advancedModeRadioButton.AutoSize = true;
            advancedModeRadioButton.Location = new System.Drawing.Point(15, 49);
            advancedModeRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            advancedModeRadioButton.Name = "advancedModeRadioButton";
            advancedModeRadioButton.Size = new System.Drawing.Size(146, 19);
            advancedModeRadioButton.TabIndex = 61;
            advancedModeRadioButton.TabStop = true;
            advancedModeRadioButton.Text = "Расширенный режим";
            advancedModeRadioButton.UseVisualStyleBackColor = true;
            advancedModeRadioButton.CheckedChanged += advancedModeRadioButton_CheckedChanged;
            advancedModeRadioButton.Click += advancedModeRadioButton_Click;
            // 
            // notAdvancedModeRadioButton
            // 
            notAdvancedModeRadioButton.AutoSize = true;
            notAdvancedModeRadioButton.Location = new System.Drawing.Point(15, 21);
            notAdvancedModeRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            notAdvancedModeRadioButton.Name = "notAdvancedModeRadioButton";
            notAdvancedModeRadioButton.Size = new System.Drawing.Size(143, 19);
            notAdvancedModeRadioButton.TabIndex = 60;
            notAdvancedModeRadioButton.TabStop = true;
            notAdvancedModeRadioButton.Text = "Облегченный режим";
            notAdvancedModeRadioButton.UseVisualStyleBackColor = true;
            notAdvancedModeRadioButton.CheckedChanged += notAdvancedModeRadioButton_CheckedChanged;
            // 
            // downloadsDirectoryGroupBox
            // 
            downloadsDirectoryGroupBox.Controls.Add(downloadsFolderPathSetButton);
            downloadsDirectoryGroupBox.Controls.Add(downloadsFolderPathRichTextBox);
            downloadsDirectoryGroupBox.Location = new System.Drawing.Point(18, 752);
            downloadsDirectoryGroupBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            downloadsDirectoryGroupBox.Name = "downloadsDirectoryGroupBox";
            downloadsDirectoryGroupBox.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            downloadsDirectoryGroupBox.Size = new System.Drawing.Size(589, 89);
            downloadsDirectoryGroupBox.TabIndex = 98;
            downloadsDirectoryGroupBox.TabStop = false;
            downloadsDirectoryGroupBox.Text = "Путь к папке \"Загрузки\":";
            // 
            // downloadsFolderPathSetButton
            // 
            downloadsFolderPathSetButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            downloadsFolderPathSetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            downloadsFolderPathSetButton.Location = new System.Drawing.Point(510, 21);
            downloadsFolderPathSetButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            downloadsFolderPathSetButton.Name = "downloadsFolderPathSetButton";
            downloadsFolderPathSetButton.Size = new System.Drawing.Size(71, 57);
            downloadsFolderPathSetButton.TabIndex = 98;
            downloadsFolderPathSetButton.Text = "📁\r\nВыбрать\r\nпапку";
            downloadsFolderPathSetButton.UseVisualStyleBackColor = false;
            downloadsFolderPathSetButton.Click += downloadsFolderPathSetButton_Click;
            // 
            // downloadsFolderPathRichTextBox
            // 
            downloadsFolderPathRichTextBox.Location = new System.Drawing.Point(15, 21);
            downloadsFolderPathRichTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            downloadsFolderPathRichTextBox.Name = "downloadsFolderPathRichTextBox";
            downloadsFolderPathRichTextBox.Size = new System.Drawing.Size(481, 57);
            downloadsFolderPathRichTextBox.TabIndex = 2;
            downloadsFolderPathRichTextBox.Text = "";
            // 
            // defaultFolderPathSetButton
            // 
            defaultFolderPathSetButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            defaultFolderPathSetButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            defaultFolderPathSetButton.Location = new System.Drawing.Point(510, 21);
            defaultFolderPathSetButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            defaultFolderPathSetButton.Name = "defaultFolderPathSetButton";
            defaultFolderPathSetButton.Size = new System.Drawing.Size(71, 57);
            defaultFolderPathSetButton.TabIndex = 99;
            defaultFolderPathSetButton.Text = "📁\r\nВыбрать\r\nпапку";
            defaultFolderPathSetButton.UseVisualStyleBackColor = false;
            defaultFolderPathSetButton.Click += defaultFolderPathSetButton_Click;
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Location = new System.Drawing.Point(-130, 41);
            label42.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label42.Name = "label42";
            label42.Size = new System.Drawing.Size(0, 15);
            label42.TabIndex = 6;
            // 
            // defaultFolderPathRichTextBox
            // 
            defaultFolderPathRichTextBox.Location = new System.Drawing.Point(15, 21);
            defaultFolderPathRichTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            defaultFolderPathRichTextBox.Name = "defaultFolderPathRichTextBox";
            defaultFolderPathRichTextBox.Size = new System.Drawing.Size(481, 57);
            defaultFolderPathRichTextBox.TabIndex = 5;
            defaultFolderPathRichTextBox.Text = "";
            // 
            // button11
            // 
            button11.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button11.Location = new System.Drawing.Point(18, 942);
            button11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button11.Name = "button11";
            button11.Size = new System.Drawing.Size(273, 40);
            button11.TabIndex = 100;
            button11.Text = "ℹ️ О программе";
            button11.UseVisualStyleBackColor = false;
            button11.Click += button11_Click;
            // 
            // subtitlesAppearanceSettingsControl
            // 
            subtitlesAppearanceSettingsControl.AutoScroll = true;
            subtitlesAppearanceSettingsControl.AutoScrollMinSize = new System.Drawing.Size(934, 541);
            subtitlesAppearanceSettingsControl.Location = new System.Drawing.Point(595, 14);
            subtitlesAppearanceSettingsControl.Name = "subtitlesAppearanceSettingsControl";
            subtitlesAppearanceSettingsControl.Size = new System.Drawing.Size(654, 626);
            subtitlesAppearanceSettingsControl.TabIndex = 101;
            subtitlesAppearanceSettingsControl.Load += subtitlesAppearanceSettingsControl_Load;
            // 
            // defaultDirectoryGroupBox
            // 
            defaultDirectoryGroupBox.Controls.Add(defaultFolderPathSetButton);
            defaultDirectoryGroupBox.Controls.Add(defaultFolderPathRichTextBox);
            defaultDirectoryGroupBox.Controls.Add(label42);
            defaultDirectoryGroupBox.Location = new System.Drawing.Point(661, 752);
            defaultDirectoryGroupBox.Name = "defaultDirectoryGroupBox";
            defaultDirectoryGroupBox.Size = new System.Drawing.Size(589, 89);
            defaultDirectoryGroupBox.TabIndex = 103;
            defaultDirectoryGroupBox.TabStop = false;
            defaultDirectoryGroupBox.Text = "Путь к папке для открытия файлов по умолчанию:";
            // 
            // fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox
            // 
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.AutoSize = true;
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.Checked = true;
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.Location = new System.Drawing.Point(18, 562);
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.Name = "fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox";
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.Size = new System.Drawing.Size(315, 64);
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.TabIndex = 104;
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.Text = "При считывании исходных субтитров — \r\nисправлять ошибку разметки, \r\nпри которой запятая, точка, вопросительный или \r\nвосклицательный знаки выносятся на новую строку";
            fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox
            // 
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox.AutoSize = true;
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox.Location = new System.Drawing.Point(18, 628);
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox.Name = "ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox";
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox.Size = new System.Drawing.Size(570, 19);
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox.TabIndex = 105;
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox.Text = "Считывать из и записывать в итоговые файлы субтитров названия потоков субтитров-исходников";
            ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox.UseVisualStyleBackColor = true;
            // 
            // notifyAboutSuccessfullySavedSubtitlesFileCheckBox
            // 
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox.AutoSize = true;
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox.Location = new System.Drawing.Point(350, 585);
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox.Name = "notifyAboutSuccessfullySavedSubtitlesFileCheckBox";
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox.Size = new System.Drawing.Size(240, 34);
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox.TabIndex = 106;
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox.Text = "Отображать сообщение об успешной \r\nзаписи субтитров в файл";
            notifyAboutSuccessfullySavedSubtitlesFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // removeAnCheckBox
            // 
            removeAnCheckBox.AutoSize = true;
            removeAnCheckBox.Location = new System.Drawing.Point(18, 653);
            removeAnCheckBox.Name = "removeAnCheckBox";
            removeAnCheckBox.Size = new System.Drawing.Size(134, 19);
            removeAnCheckBox.TabIndex = 107;
            removeAnCheckBox.Text = "removeAnCheckBox";
            removeAnCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            ClientSize = new System.Drawing.Size(1264, 991);
            Controls.Add(removeAnCheckBox);
            Controls.Add(notifyAboutSuccessfullySavedSubtitlesFileCheckBox);
            Controls.Add(ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox);
            Controls.Add(fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox);
            Controls.Add(defaultDirectoryGroupBox);
            Controls.Add(subtitlesAppearanceSettingsControl);
            Controls.Add(button11);
            Controls.Add(downloadsDirectoryGroupBox);
            Controls.Add(groupBox12);
            Controls.Add(checkUpdatesGroupBox);
            Controls.Add(processPriorityGroupBox);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(button5);
            Controls.Add(groupBox2);
            Controls.Add(yandexTranslatorGroupBox);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "SettingsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Настройки программы";
            FormClosing += YandexTranslatorAPIKeyForm_FormClosing;
            Load += SettingsForm_Load;
            yandexTranslatorGroupBox.ResumeLayout(false);
            yandexTranslatorGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)hotkeysDataGridView).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            processPriorityGroupBox.ResumeLayout(false);
            processPriorityGroupBox.PerformLayout();
            checkUpdatesGroupBox.ResumeLayout(false);
            checkUpdatesGroupBox.PerformLayout();
            groupBox12.ResumeLayout(false);
            groupBox12.PerformLayout();
            downloadsDirectoryGroupBox.ResumeLayout(false);
            defaultDirectoryGroupBox.ResumeLayout(false);
            defaultDirectoryGroupBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxForYandexApiKeyInSeparateForm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.LinkLabel linkLabelYandexAPIKeysList;
        private System.Windows.Forms.GroupBox yandexTranslatorGroupBox;
        private System.Windows.Forms.DataGridView hotkeysDataGridView;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn keyColumn;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CreateBilingualSubtitlesFileCheckBox;
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
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.CheckBox gotTheYandexTranslatorAPIKeyCheckBox;
        private System.Windows.Forms.GroupBox processPriorityGroupBox;
        private System.Windows.Forms.ComboBox targetProcessPriorityTextBox;
        private System.Windows.Forms.TextBox currentProcessPriorityTextBox;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.GroupBox checkUpdatesGroupBox;
        private System.Windows.Forms.Label lastAppVersionLabel;
        private System.Windows.Forms.Label currentAppVersionLabel;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.CheckBox checkUpdatesOnAppStartCheckBox;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.RadioButton advancedModeRadioButton;
        private System.Windows.Forms.RadioButton notAdvancedModeRadioButton;
        private System.Windows.Forms.GroupBox downloadsDirectoryGroupBox;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.RichTextBox defaultFolderPathRichTextBox;
        private System.Windows.Forms.RichTextBox downloadsFolderPathRichTextBox;
        private System.Windows.Forms.Button defaultFolderPathSetButton;
        private System.Windows.Forms.Button downloadsFolderPathSetButton;
        private System.Windows.Forms.Button button11;
        private SubtitlesAppearanceSettings subtitlesAppearanceSettingsControl;
        private System.Windows.Forms.GroupBox defaultDirectoryGroupBox;
        private System.Windows.Forms.CheckBox fixDotOrCommaAsTheFisrtCharOfNewLIneCheckBox;
        private System.Windows.Forms.CheckBox ReadAndWriteTitlesOfOriginIntoFinalFilesCheckBox;
        private System.Windows.Forms.CheckBox notifyAboutSuccessfullySavedSubtitlesFileCheckBox;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox removeAnCheckBox;
    }
}