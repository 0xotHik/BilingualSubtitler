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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIming = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrigSub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RusSub = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSubtitlesCreation = new System.Windows.Forms.Panel();
            this.checkBoxRemoveStylesFromSecondarySubs = new System.Windows.Forms.CheckBox();
            this.checkBoxSecondarySubsInOneLine = new System.Windows.Forms.CheckBox();
            this.labelCurrentPrimarySubtitlesColor = new System.Windows.Forms.Label();
            this.buttonCurrentPrimarySubtitlesColor = new System.Windows.Forms.Button();
            this.buttonCurrentSecondarySubtitlesColor = new System.Windows.Forms.Button();
            this.labelCurrentSecondarySubtitlesColor = new System.Windows.Forms.Label();
            this.backgroundWorkerTranslation = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerWriteSubsToDataGrid = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.panelSettings = new System.Windows.Forms.Panel();
            this.linkLabelYandexAPIKeysList = new System.Windows.Forms.LinkLabel();
            this.linkLabelDownloadMKVToolnix = new System.Windows.Forms.LinkLabel();
            this.labelPathToTemp = new System.Windows.Forms.Label();
            this.textBoxPathToMKVToolnix = new System.Windows.Forms.TextBox();
            this.textBoxPathToTemp = new System.Windows.Forms.TextBox();
            this.labelPathToMKVToolnix = new System.Windows.Forms.Label();
            this.linkLabelGetYandexAPIKey = new System.Windows.Forms.LinkLabel();
            this.richTextBoxLabelEnterYandexAPIKey = new System.Windows.Forms.RichTextBox();
            this.richTextBoxForYandexAPIKey = new System.Windows.Forms.RichTextBox();
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel = new System.Windows.Forms.Button();
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel = new System.Windows.Forms.Button();
            this.openFileDialogMKV = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogMKVToolnixFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.labelCurrentProcess = new System.Windows.Forms.Label();
            this.labelProcessPercenage = new System.Windows.Forms.Label();
            this.folderBrowserDialogMKVToolnix = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialogTempSubs = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonHelpOnSettingsPanel = new System.Windows.Forms.Button();
            this.buttonBrowsePathToMKVToolnix = new System.Windows.Forms.Button();
            this.buttonBrowsePathToTemp = new System.Windows.Forms.Button();
            this.pictureBoxMKVExtractLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxMKVToolnix = new System.Windows.Forms.PictureBox();
            this.buttonWriteBilingualSubtitles = new System.Windows.Forms.Button();
            this.pictureBoxYandexTranslator = new System.Windows.Forms.PictureBox();
            this.buttonResetToDefault = new System.Windows.Forms.Button();
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonRemoveHI = new System.Windows.Forms.Button();
            this.buttonWriteBilingualSubtitlesAfterTranlation = new System.Windows.Forms.Button();
            this.buttonBackToMainForm = new System.Windows.Forms.Button();
            this.buttonTranslate = new System.Windows.Forms.Button();
            this.buttonOpenMKV = new System.Windows.Forms.Button();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonSubtitlesCreationPanel = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelMain.SuspendLayout();
            this.panelSubtitlesCreation.SuspendLayout();
            this.panelSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVExtractLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVToolnix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYandexTranslator)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(475, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Visible = false;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.TIming,
            this.OrigSub,
            this.RusSub});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGridView1.Location = new System.Drawing.Point(13, 116);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Size = new System.Drawing.Size(542, 348);
            this.dataGridView1.TabIndex = 9;
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "№";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // TIming
            // 
            this.TIming.Frozen = true;
            this.TIming.HeaderText = "Тайминг";
            this.TIming.Name = "TIming";
            this.TIming.ReadOnly = true;
            // 
            // OrigSub
            // 
            this.OrigSub.Frozen = true;
            this.OrigSub.HeaderText = "Оригинальный текст";
            this.OrigSub.Name = "OrigSub";
            this.OrigSub.ReadOnly = true;
            // 
            // RusSub
            // 
            this.RusSub.Frozen = true;
            this.RusSub.HeaderText = "Перевод";
            this.RusSub.Name = "RusSub";
            this.RusSub.ReadOnly = true;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonOpenMKV);
            this.panelMain.Controls.Add(this.buttonOpenFile);
            this.panelMain.Controls.Add(this.buttonAbout);
            this.panelMain.Controls.Add(this.buttonSubtitlesCreationPanel);
            this.panelMain.Controls.Add(this.buttonHelp);
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Controls.Add(this.buttonSettings);
            this.panelMain.Location = new System.Drawing.Point(3, 3);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(564, 107);
            this.panelMain.TabIndex = 12;
            // 
            // panelSubtitlesCreation
            // 
            this.panelSubtitlesCreation.Controls.Add(this.buttonRemoveHI);
            this.panelSubtitlesCreation.Controls.Add(this.checkBoxRemoveStylesFromSecondarySubs);
            this.panelSubtitlesCreation.Controls.Add(this.checkBoxSecondarySubsInOneLine);
            this.panelSubtitlesCreation.Controls.Add(this.buttonWriteBilingualSubtitlesAfterTranlation);
            this.panelSubtitlesCreation.Controls.Add(this.labelCurrentPrimarySubtitlesColor);
            this.panelSubtitlesCreation.Controls.Add(this.buttonCurrentPrimarySubtitlesColor);
            this.panelSubtitlesCreation.Controls.Add(this.buttonCurrentSecondarySubtitlesColor);
            this.panelSubtitlesCreation.Controls.Add(this.buttonBackToMainForm);
            this.panelSubtitlesCreation.Controls.Add(this.buttonTranslate);
            this.panelSubtitlesCreation.Controls.Add(this.labelCurrentSecondarySubtitlesColor);
            this.panelSubtitlesCreation.Location = new System.Drawing.Point(6, 211);
            this.panelSubtitlesCreation.Name = "panelSubtitlesCreation";
            this.panelSubtitlesCreation.Size = new System.Drawing.Size(564, 107);
            this.panelSubtitlesCreation.TabIndex = 13;
            // 
            // checkBoxRemoveStylesFromSecondarySubs
            // 
            this.checkBoxRemoveStylesFromSecondarySubs.AutoSize = true;
            this.checkBoxRemoveStylesFromSecondarySubs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxRemoveStylesFromSecondarySubs.Location = new System.Drawing.Point(153, 85);
            this.checkBoxRemoveStylesFromSecondarySubs.Name = "checkBoxRemoveStylesFromSecondarySubs";
            this.checkBoxRemoveStylesFromSecondarySubs.Size = new System.Drawing.Size(211, 17);
            this.checkBoxRemoveStylesFromSecondarySubs.TabIndex = 19;
            this.checkBoxRemoveStylesFromSecondarySubs.Text = "Удалить стили вторичных субтитров";
            this.checkBoxRemoveStylesFromSecondarySubs.UseVisualStyleBackColor = true;
            this.checkBoxRemoveStylesFromSecondarySubs.CheckedChanged += new System.EventHandler(this.checkBoxRemoveStylesFromSecondarySubs_CheckedChanged);
            // 
            // checkBoxSecondarySubsInOneLine
            // 
            this.checkBoxSecondarySubsInOneLine.AutoSize = true;
            this.checkBoxSecondarySubsInOneLine.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSecondarySubsInOneLine.Location = new System.Drawing.Point(153, 56);
            this.checkBoxSecondarySubsInOneLine.Name = "checkBoxSecondarySubsInOneLine";
            this.checkBoxSecondarySubsInOneLine.Size = new System.Drawing.Size(197, 30);
            this.checkBoxSecondarySubsInOneLine.TabIndex = 18;
            this.checkBoxSecondarySubsInOneLine.Text = "Разместить вторичные субтитры \r\nв одну строку";
            this.checkBoxSecondarySubsInOneLine.UseVisualStyleBackColor = true;
            this.checkBoxSecondarySubsInOneLine.CheckedChanged += new System.EventHandler(this.checkBoxSecondarySubsInOneLine_CheckedChanged);
            // 
            // labelCurrentPrimarySubtitlesColor
            // 
            this.labelCurrentPrimarySubtitlesColor.AutoSize = true;
            this.labelCurrentPrimarySubtitlesColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentPrimarySubtitlesColor.Location = new System.Drawing.Point(173, 9);
            this.labelCurrentPrimarySubtitlesColor.Name = "labelCurrentPrimarySubtitlesColor";
            this.labelCurrentPrimarySubtitlesColor.Size = new System.Drawing.Size(189, 13);
            this.labelCurrentPrimarySubtitlesColor.TabIndex = 13;
            this.labelCurrentPrimarySubtitlesColor.Text = "Текущий цвет первичных субтитров";
            // 
            // buttonCurrentPrimarySubtitlesColor
            // 
            this.buttonCurrentPrimarySubtitlesColor.FlatAppearance.BorderSize = 0;
            this.buttonCurrentPrimarySubtitlesColor.Location = new System.Drawing.Point(153, 6);
            this.buttonCurrentPrimarySubtitlesColor.Name = "buttonCurrentPrimarySubtitlesColor";
            this.buttonCurrentPrimarySubtitlesColor.Size = new System.Drawing.Size(18, 18);
            this.buttonCurrentPrimarySubtitlesColor.TabIndex = 12;
            this.buttonCurrentPrimarySubtitlesColor.UseVisualStyleBackColor = true;
            this.buttonCurrentPrimarySubtitlesColor.Click += new System.EventHandler(this.buttonCurrentPrimarySubtitlesColor_Click);
            // 
            // buttonCurrentSecondarySubtitlesColor
            // 
            this.buttonCurrentSecondarySubtitlesColor.FlatAppearance.BorderSize = 0;
            this.buttonCurrentSecondarySubtitlesColor.Location = new System.Drawing.Point(153, 30);
            this.buttonCurrentSecondarySubtitlesColor.Name = "buttonCurrentSecondarySubtitlesColor";
            this.buttonCurrentSecondarySubtitlesColor.Size = new System.Drawing.Size(18, 18);
            this.buttonCurrentSecondarySubtitlesColor.TabIndex = 11;
            this.buttonCurrentSecondarySubtitlesColor.UseVisualStyleBackColor = true;
            this.buttonCurrentSecondarySubtitlesColor.Click += new System.EventHandler(this.buttonCurrentSecondarySubtitlesColor_Click);
            // 
            // labelCurrentSecondarySubtitlesColor
            // 
            this.labelCurrentSecondarySubtitlesColor.AutoSize = true;
            this.labelCurrentSecondarySubtitlesColor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentSecondarySubtitlesColor.Location = new System.Drawing.Point(173, 33);
            this.labelCurrentSecondarySubtitlesColor.Name = "labelCurrentSecondarySubtitlesColor";
            this.labelCurrentSecondarySubtitlesColor.Size = new System.Drawing.Size(188, 13);
            this.labelCurrentSecondarySubtitlesColor.TabIndex = 10;
            this.labelCurrentSecondarySubtitlesColor.Text = "Текущий цвет вторичных субтитров";
            // 
            // backgroundWorkerTranslation
            // 
            this.backgroundWorkerTranslation.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerTranslation_DoWork);
            this.backgroundWorkerTranslation.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerTranslation_ProgressChanged);
            this.backgroundWorkerTranslation.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerTranslation_RunWorkerCompleted);
            // 
            // backgroundWorkerWriteSubsToDataGrid
            // 
            this.backgroundWorkerWriteSubsToDataGrid.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerWriteSubsToDataGrid_DoWork);
            this.backgroundWorkerWriteSubsToDataGrid.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerWriteSubsToDataGrid_ProgressChanged);
            this.backgroundWorkerWriteSubsToDataGrid.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerWriteSubsToDataGrid_RunWorkerCompleted);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 470);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(459, 10);
            this.progressBar.TabIndex = 14;
            // 
            // panelSettings
            // 
            this.panelSettings.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelSettings.Controls.Add(this.buttonHelpOnSettingsPanel);
            this.panelSettings.Controls.Add(this.linkLabelYandexAPIKeysList);
            this.panelSettings.Controls.Add(this.linkLabelDownloadMKVToolnix);
            this.panelSettings.Controls.Add(this.buttonBrowsePathToMKVToolnix);
            this.panelSettings.Controls.Add(this.labelPathToTemp);
            this.panelSettings.Controls.Add(this.buttonBrowsePathToTemp);
            this.panelSettings.Controls.Add(this.textBoxPathToMKVToolnix);
            this.panelSettings.Controls.Add(this.textBoxPathToTemp);
            this.panelSettings.Controls.Add(this.labelPathToMKVToolnix);
            this.panelSettings.Controls.Add(this.pictureBoxMKVExtractLogo);
            this.panelSettings.Controls.Add(this.pictureBoxMKVToolnix);
            this.panelSettings.Controls.Add(this.buttonWriteBilingualSubtitles);
            this.panelSettings.Controls.Add(this.pictureBoxYandexTranslator);
            this.panelSettings.Controls.Add(this.linkLabelGetYandexAPIKey);
            this.panelSettings.Controls.Add(this.buttonResetToDefault);
            this.panelSettings.Controls.Add(this.richTextBoxLabelEnterYandexAPIKey);
            this.panelSettings.Controls.Add(this.richTextBoxForYandexAPIKey);
            this.panelSettings.Controls.Add(this.buttonSaveAndBackToTheMainFormOnSettingsPanel);
            this.panelSettings.Controls.Add(this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel);
            this.panelSettings.Controls.Add(this.label2);
            this.panelSettings.Controls.Add(this.checkBoxSecondarySubsInOneLineOnSettingsPanel);
            this.panelSettings.Controls.Add(this.label3);
            this.panelSettings.Controls.Add(this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel);
            this.panelSettings.Controls.Add(this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel);
            this.panelSettings.Location = new System.Drawing.Point(573, 0);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(564, 489);
            this.panelSettings.TabIndex = 17;
            // 
            // linkLabelYandexAPIKeysList
            // 
            this.linkLabelYandexAPIKeysList.AutoSize = true;
            this.linkLabelYandexAPIKeysList.Location = new System.Drawing.Point(318, 261);
            this.linkLabelYandexAPIKeysList.Name = "linkLabelYandexAPIKeysList";
            this.linkLabelYandexAPIKeysList.Size = new System.Drawing.Size(243, 13);
            this.linkLabelYandexAPIKeysList.TabIndex = 42;
            this.linkLabelYandexAPIKeysList.TabStop = true;
            this.linkLabelYandexAPIKeysList.Text = "Открыть список уже полученных вами ключей";
            this.linkLabelYandexAPIKeysList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelYandexAPIKeysList_LinkClicked);
            // 
            // linkLabelDownloadMKVToolnix
            // 
            this.linkLabelDownloadMKVToolnix.AutoSize = true;
            this.linkLabelDownloadMKVToolnix.Location = new System.Drawing.Point(12, 347);
            this.linkLabelDownloadMKVToolnix.Name = "linkLabelDownloadMKVToolnix";
            this.linkLabelDownloadMKVToolnix.Size = new System.Drawing.Size(108, 13);
            this.linkLabelDownloadMKVToolnix.TabIndex = 41;
            this.linkLabelDownloadMKVToolnix.TabStop = true;
            this.linkLabelDownloadMKVToolnix.Text = "Скачать MKVToolnix";
            this.linkLabelDownloadMKVToolnix.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDownloadMKVToolnix_LinkClicked);
            // 
            // labelPathToTemp
            // 
            this.labelPathToTemp.AutoSize = true;
            this.labelPathToTemp.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelPathToTemp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelPathToTemp.Location = new System.Drawing.Point(42, 375);
            this.labelPathToTemp.Name = "labelPathToTemp";
            this.labelPathToTemp.Size = new System.Drawing.Size(343, 13);
            this.labelPathToTemp.TabIndex = 40;
            this.labelPathToTemp.Text = "Выберите папку, в которой будут содержаться временные файлы";
            // 
            // textBoxPathToMKVToolnix
            // 
            this.textBoxPathToMKVToolnix.Location = new System.Drawing.Point(12, 324);
            this.textBoxPathToMKVToolnix.Name = "textBoxPathToMKVToolnix";
            this.textBoxPathToMKVToolnix.Size = new System.Drawing.Size(516, 20);
            this.textBoxPathToMKVToolnix.TabIndex = 38;
            // 
            // textBoxPathToTemp
            // 
            this.textBoxPathToTemp.Location = new System.Drawing.Point(12, 406);
            this.textBoxPathToTemp.Name = "textBoxPathToTemp";
            this.textBoxPathToTemp.Size = new System.Drawing.Size(516, 20);
            this.textBoxPathToTemp.TabIndex = 37;
            // 
            // labelPathToMKVToolnix
            // 
            this.labelPathToMKVToolnix.AutoSize = true;
            this.labelPathToMKVToolnix.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelPathToMKVToolnix.Location = new System.Drawing.Point(42, 291);
            this.labelPathToMKVToolnix.Name = "labelPathToMKVToolnix";
            this.labelPathToMKVToolnix.Size = new System.Drawing.Size(360, 26);
            this.labelPathToMKVToolnix.TabIndex = 36;
            this.labelPathToMKVToolnix.Text = "Выберите папку, в которой установлен mkvtoolnix \r\n(в папке должны сорежаться прил" +
    "ожения \"mkvmerge\" и \"mkvextract\")";
            // 
            // linkLabelGetYandexAPIKey
            // 
            this.linkLabelGetYandexAPIKey.AutoSize = true;
            this.linkLabelGetYandexAPIKey.Location = new System.Drawing.Point(12, 261);
            this.linkLabelGetYandexAPIKey.Name = "linkLabelGetYandexAPIKey";
            this.linkLabelGetYandexAPIKey.Size = new System.Drawing.Size(234, 13);
            this.linkLabelGetYandexAPIKey.TabIndex = 32;
            this.linkLabelGetYandexAPIKey.TabStop = true;
            this.linkLabelGetYandexAPIKey.Text = "Получить ключ для API Яндекс.Переводчика";
            this.linkLabelGetYandexAPIKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGetYandexAPIKey_LinkClicked);
            // 
            // richTextBoxLabelEnterYandexAPIKey
            // 
            this.richTextBoxLabelEnterYandexAPIKey.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxLabelEnterYandexAPIKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxLabelEnterYandexAPIKey.Location = new System.Drawing.Point(41, 131);
            this.richTextBoxLabelEnterYandexAPIKey.Name = "richTextBoxLabelEnterYandexAPIKey";
            this.richTextBoxLabelEnterYandexAPIKey.ReadOnly = true;
            this.richTextBoxLabelEnterYandexAPIKey.Size = new System.Drawing.Size(237, 21);
            this.richTextBoxLabelEnterYandexAPIKey.TabIndex = 30;
            this.richTextBoxLabelEnterYandexAPIKey.Text = "Введите ключ для API Яндекс.Переводчика";
            // 
            // richTextBoxForYandexAPIKey
            // 
            this.richTextBoxForYandexAPIKey.Location = new System.Drawing.Point(12, 158);
            this.richTextBoxForYandexAPIKey.Name = "richTextBoxForYandexAPIKey";
            this.richTextBoxForYandexAPIKey.Size = new System.Drawing.Size(540, 96);
            this.richTextBoxForYandexAPIKey.TabIndex = 29;
            this.richTextBoxForYandexAPIKey.Text = "";
            // 
            // checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel
            // 
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.AutoSize = true;
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.Location = new System.Drawing.Point(12, 88);
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.Name = "checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel";
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.Size = new System.Drawing.Size(211, 17);
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.TabIndex = 25;
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.Text = "Удалить стили вторичных субтитров";
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.UseVisualStyleBackColor = true;
            this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel.CheckedChanged += new System.EventHandler(this.checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(113, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Текущий цвет первичных субтитров";
            // 
            // checkBoxSecondarySubsInOneLineOnSettingsPanel
            // 
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.AutoSize = true;
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.Location = new System.Drawing.Point(12, 68);
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.Name = "checkBoxSecondarySubsInOneLineOnSettingsPanel";
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.Size = new System.Drawing.Size(266, 17);
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.TabIndex = 24;
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.Text = "Разместить вторичные субтитры в одну строку";
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.UseVisualStyleBackColor = true;
            this.checkBoxSecondarySubsInOneLineOnSettingsPanel.CheckedChanged += new System.EventHandler(this.checkBoxSecondarySubsInOneLineOnSettingsPanel_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(113, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Текущий цвет вторичных субтитров";
            // 
            // buttonCurrentSecondarySubtitlesColorOnSettingsPanel
            // 
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.FlatAppearance.BorderSize = 0;
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.Location = new System.Drawing.Point(12, 33);
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.Name = "buttonCurrentSecondarySubtitlesColorOnSettingsPanel";
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.Size = new System.Drawing.Size(95, 20);
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.TabIndex = 21;
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.UseVisualStyleBackColor = true;
            this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel.Click += new System.EventHandler(this.buttonCurrentSecondarySubtitlesColorOnSettingsPanel_Click);
            // 
            // buttonCurrentPrimarySubtitlesColorOnSettingsPanel
            // 
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.FlatAppearance.BorderSize = 0;
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.Location = new System.Drawing.Point(12, 7);
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.Name = "buttonCurrentPrimarySubtitlesColorOnSettingsPanel";
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.Size = new System.Drawing.Size(95, 20);
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.TabIndex = 22;
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.UseVisualStyleBackColor = true;
            this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel.Click += new System.EventHandler(this.buttonCurrentPrimarySubtitlesColorOnSettingsPanel_Click);
            // 
            // openFileDialogMKV
            // 
            this.openFileDialogMKV.FileName = "openFileDialog1";
            // 
            // labelCurrentProcess
            // 
            this.labelCurrentProcess.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCurrentProcess.Location = new System.Drawing.Point(12, 483);
            this.labelCurrentProcess.Name = "labelCurrentProcess";
            this.labelCurrentProcess.Size = new System.Drawing.Size(126, 13);
            this.labelCurrentProcess.TabIndex = 18;
            this.labelCurrentProcess.Text = "Идет процесс такой-то:";
            this.labelCurrentProcess.TextChanged += new System.EventHandler(this.labelCurrentProcess_TextChanged);
            // 
            // labelProcessPercenage
            // 
            this.labelProcessPercenage.AutoSize = true;
            this.labelProcessPercenage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelProcessPercenage.Location = new System.Drawing.Point(439, 483);
            this.labelProcessPercenage.Name = "labelProcessPercenage";
            this.labelProcessPercenage.Size = new System.Drawing.Size(27, 13);
            this.labelProcessPercenage.TabIndex = 19;
            this.labelProcessPercenage.Text = "39%";
            // 
            // buttonHelpOnSettingsPanel
            // 
            this.buttonHelpOnSettingsPanel.FlatAppearance.BorderSize = 0;
            this.buttonHelpOnSettingsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonHelpOnSettingsPanel.Image = global::BilingualSubtitler.Properties.Resources._48pxHelpBookIcon;
            this.buttonHelpOnSettingsPanel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonHelpOnSettingsPanel.Location = new System.Drawing.Point(323, 7);
            this.buttonHelpOnSettingsPanel.Name = "buttonHelpOnSettingsPanel";
            this.buttonHelpOnSettingsPanel.Size = new System.Drawing.Size(78, 109);
            this.buttonHelpOnSettingsPanel.TabIndex = 43;
            this.buttonHelpOnSettingsPanel.Text = "Справка";
            this.buttonHelpOnSettingsPanel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonHelpOnSettingsPanel.UseVisualStyleBackColor = true;
            this.buttonHelpOnSettingsPanel.Click += new System.EventHandler(this.buttonHelpOnSettingsPanel_Click);
            // 
            // buttonBrowsePathToMKVToolnix
            // 
            this.buttonBrowsePathToMKVToolnix.Image = global::BilingualSubtitler.Properties.Resources._20pxOpenIcon;
            this.buttonBrowsePathToMKVToolnix.Location = new System.Drawing.Point(525, 324);
            this.buttonBrowsePathToMKVToolnix.Name = "buttonBrowsePathToMKVToolnix";
            this.buttonBrowsePathToMKVToolnix.Size = new System.Drawing.Size(20, 20);
            this.buttonBrowsePathToMKVToolnix.TabIndex = 38;
            this.buttonBrowsePathToMKVToolnix.UseVisualStyleBackColor = true;
            this.buttonBrowsePathToMKVToolnix.Click += new System.EventHandler(this.buttonBrowsePathToMKVToolnix_Click);
            // 
            // buttonBrowsePathToTemp
            // 
            this.buttonBrowsePathToTemp.Image = global::BilingualSubtitler.Properties.Resources._20pxOpenIcon;
            this.buttonBrowsePathToTemp.Location = new System.Drawing.Point(525, 405);
            this.buttonBrowsePathToTemp.Name = "buttonBrowsePathToTemp";
            this.buttonBrowsePathToTemp.Size = new System.Drawing.Size(20, 20);
            this.buttonBrowsePathToTemp.TabIndex = 39;
            this.buttonBrowsePathToTemp.UseVisualStyleBackColor = true;
            this.buttonBrowsePathToTemp.Click += new System.EventHandler(this.buttonBrowsePathToTemp_Click);
            // 
            // pictureBoxMKVExtractLogo
            // 
            this.pictureBoxMKVExtractLogo.Image = global::BilingualSubtitler.Properties.Resources._25pxMkvExtractLogo;
            this.pictureBoxMKVExtractLogo.Location = new System.Drawing.Point(12, 375);
            this.pictureBoxMKVExtractLogo.Name = "pictureBoxMKVExtractLogo";
            this.pictureBoxMKVExtractLogo.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxMKVExtractLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMKVExtractLogo.TabIndex = 35;
            this.pictureBoxMKVExtractLogo.TabStop = false;
            // 
            // pictureBoxMKVToolnix
            // 
            this.pictureBoxMKVToolnix.Image = global::BilingualSubtitler.Properties.Resources._25pxMkvtoolnixLogo;
            this.pictureBoxMKVToolnix.Location = new System.Drawing.Point(12, 291);
            this.pictureBoxMKVToolnix.Name = "pictureBoxMKVToolnix";
            this.pictureBoxMKVToolnix.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxMKVToolnix.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMKVToolnix.TabIndex = 34;
            this.pictureBoxMKVToolnix.TabStop = false;
            // 
            // buttonWriteBilingualSubtitles
            // 
            this.buttonWriteBilingualSubtitles.Image = global::BilingualSubtitler.Properties.Resources._32pxWriteFileToHDDIcon;
            this.buttonWriteBilingualSubtitles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonWriteBilingualSubtitles.Location = new System.Drawing.Point(314, 426);
            this.buttonWriteBilingualSubtitles.Name = "buttonWriteBilingualSubtitles";
            this.buttonWriteBilingualSubtitles.Size = new System.Drawing.Size(182, 47);
            this.buttonWriteBilingualSubtitles.TabIndex = 6;
            this.buttonWriteBilingualSubtitles.Text = "Сохранить\r\nсозданные субтитры";
            this.buttonWriteBilingualSubtitles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonWriteBilingualSubtitles.UseVisualStyleBackColor = true;
            this.buttonWriteBilingualSubtitles.Visible = false;
            this.buttonWriteBilingualSubtitles.Click += new System.EventHandler(this.buttonWriteBilingualSubtitles_Click);
            // 
            // pictureBoxYandexTranslator
            // 
            this.pictureBoxYandexTranslator.Enabled = false;
            this.pictureBoxYandexTranslator.Image = global::BilingualSubtitler.Properties.Resources._25pxYandexTranslateIcon;
            this.pictureBoxYandexTranslator.Location = new System.Drawing.Point(12, 125);
            this.pictureBoxYandexTranslator.Name = "pictureBoxYandexTranslator";
            this.pictureBoxYandexTranslator.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxYandexTranslator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxYandexTranslator.TabIndex = 33;
            this.pictureBoxYandexTranslator.TabStop = false;
            // 
            // buttonResetToDefault
            // 
            this.buttonResetToDefault.FlatAppearance.BorderSize = 0;
            this.buttonResetToDefault.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonResetToDefault.Image = global::BilingualSubtitler.Properties.Resources._48pxBackIcon;
            this.buttonResetToDefault.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonResetToDefault.Location = new System.Drawing.Point(479, 7);
            this.buttonResetToDefault.Name = "buttonResetToDefault";
            this.buttonResetToDefault.Size = new System.Drawing.Size(78, 109);
            this.buttonResetToDefault.TabIndex = 31;
            this.buttonResetToDefault.Text = "Вернуться к основному окну";
            this.buttonResetToDefault.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonResetToDefault.UseVisualStyleBackColor = true;
            this.buttonResetToDefault.Click += new System.EventHandler(this.buttonResetToDefault_Click);
            // 
            // buttonSaveAndBackToTheMainFormOnSettingsPanel
            // 
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.FlatAppearance.BorderSize = 0;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.Image = global::BilingualSubtitler.Properties.Resources._48pxSaveIcon1;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.Location = new System.Drawing.Point(401, 7);
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.Name = "buttonSaveAndBackToTheMainFormOnSettingsPanel";
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.Size = new System.Drawing.Size(78, 109);
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.TabIndex = 26;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.Text = "Сохранить значения";
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.UseVisualStyleBackColor = true;
            this.buttonSaveAndBackToTheMainFormOnSettingsPanel.Click += new System.EventHandler(this.buttonSaveAndBackToTheMainFormOnSettingsPanel_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonCancel.Image = global::BilingualSubtitler.Properties.Resources._16pxCancelIcon;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(480, 470);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 29);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click_1);
            // 
            // buttonRemoveHI
            // 
            this.buttonRemoveHI.Enabled = false;
            this.buttonRemoveHI.FlatAppearance.BorderSize = 0;
            this.buttonRemoveHI.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonRemoveHI.Image = global::BilingualSubtitler.Properties.Resources._48pxRemoveHIIcon;
            this.buttonRemoveHI.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonRemoveHI.Location = new System.Drawing.Point(78, 4);
            this.buttonRemoveHI.Name = "buttonRemoveHI";
            this.buttonRemoveHI.Size = new System.Drawing.Size(70, 100);
            this.buttonRemoveHI.TabIndex = 20;
            this.buttonRemoveHI.Text = "Убрать \r\nтитры для \r\nслабо-\r\nслышащих\r\n";
            this.buttonRemoveHI.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonRemoveHI.UseVisualStyleBackColor = true;
            this.buttonRemoveHI.Click += new System.EventHandler(this.buttonRemoveHI_Click);
            // 
            // buttonWriteBilingualSubtitlesAfterTranlation
            // 
            this.buttonWriteBilingualSubtitlesAfterTranlation.FlatAppearance.BorderSize = 0;
            this.buttonWriteBilingualSubtitlesAfterTranlation.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonWriteBilingualSubtitlesAfterTranlation.Image = global::BilingualSubtitler.Properties.Resources._48pxWriteFileToHDDIcon1;
            this.buttonWriteBilingualSubtitlesAfterTranlation.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonWriteBilingualSubtitlesAfterTranlation.Location = new System.Drawing.Point(398, 3);
            this.buttonWriteBilingualSubtitlesAfterTranlation.Name = "buttonWriteBilingualSubtitlesAfterTranlation";
            this.buttonWriteBilingualSubtitlesAfterTranlation.Size = new System.Drawing.Size(75, 100);
            this.buttonWriteBilingualSubtitlesAfterTranlation.TabIndex = 17;
            this.buttonWriteBilingualSubtitlesAfterTranlation.Text = "Сохранить\r\nсозданные \r\nсубтитры";
            this.buttonWriteBilingualSubtitlesAfterTranlation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonWriteBilingualSubtitlesAfterTranlation.UseVisualStyleBackColor = true;
            this.buttonWriteBilingualSubtitlesAfterTranlation.Click += new System.EventHandler(this.buttonWriteBilingualSubtitlesAfterTranlation_Click);
            // 
            // buttonBackToMainForm
            // 
            this.buttonBackToMainForm.FlatAppearance.BorderSize = 0;
            this.buttonBackToMainForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonBackToMainForm.Image = global::BilingualSubtitler.Properties.Resources._48pxBackIcon;
            this.buttonBackToMainForm.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonBackToMainForm.Location = new System.Drawing.Point(472, 3);
            this.buttonBackToMainForm.Name = "buttonBackToMainForm";
            this.buttonBackToMainForm.Size = new System.Drawing.Size(78, 99);
            this.buttonBackToMainForm.TabIndex = 8;
            this.buttonBackToMainForm.Text = "Вернуться к основному окну";
            this.buttonBackToMainForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonBackToMainForm.UseVisualStyleBackColor = true;
            this.buttonBackToMainForm.Click += new System.EventHandler(this.buttonBackToMainForm_Click);
            // 
            // buttonTranslate
            // 
            this.buttonTranslate.FlatAppearance.BorderSize = 0;
            this.buttonTranslate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonTranslate.Image = global::BilingualSubtitler.Properties.Resources.smallTranslateToRus;
            this.buttonTranslate.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonTranslate.Location = new System.Drawing.Point(7, 4);
            this.buttonTranslate.Name = "buttonTranslate";
            this.buttonTranslate.Size = new System.Drawing.Size(72, 100);
            this.buttonTranslate.TabIndex = 6;
            this.buttonTranslate.Text = "Перевести";
            this.buttonTranslate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonTranslate.UseVisualStyleBackColor = true;
            this.buttonTranslate.Click += new System.EventHandler(this.buttonTranslate_Click);
            // 
            // buttonOpenMKV
            // 
            this.buttonOpenMKV.FlatAppearance.BorderSize = 0;
            this.buttonOpenMKV.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOpenMKV.Image = global::BilingualSubtitler.Properties.Resources._32pxMKVIcon;
            this.buttonOpenMKV.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOpenMKV.Location = new System.Drawing.Point(3, 54);
            this.buttonOpenMKV.Name = "buttonOpenMKV";
            this.buttonOpenMKV.Size = new System.Drawing.Size(182, 44);
            this.buttonOpenMKV.TabIndex = 14;
            this.buttonOpenMKV.Text = "Открыть субтитры\r\nиз файла MKV";
            this.buttonOpenMKV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOpenMKV.UseVisualStyleBackColor = true;
            this.buttonOpenMKV.Click += new System.EventHandler(this.buttonOpenMKV_Click);
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.FlatAppearance.BorderSize = 0;
            this.buttonOpenFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonOpenFile.Image = global::BilingualSubtitler.Properties.Resources._32pxSRTIcon;
            this.buttonOpenFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOpenFile.Location = new System.Drawing.Point(3, 3);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(182, 44);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Открыть\r\nсубтитры из файла";
            this.buttonOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.FlatAppearance.BorderSize = 0;
            this.buttonAbout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonAbout.Image = global::BilingualSubtitler.Properties.Resources._16pxAboutIcon;
            this.buttonAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAbout.Location = new System.Drawing.Point(402, 36);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(153, 23);
            this.buttonAbout.TabIndex = 11;
            this.buttonAbout.Text = "О программе";
            this.buttonAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // buttonSubtitlesCreationPanel
            // 
            this.buttonSubtitlesCreationPanel.FlatAppearance.BorderSize = 0;
            this.buttonSubtitlesCreationPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSubtitlesCreationPanel.Image = global::BilingualSubtitler.Properties.Resources._48pxSubtitlesIconLightDarkBlue;
            this.buttonSubtitlesCreationPanel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSubtitlesCreationPanel.Location = new System.Drawing.Point(191, 3);
            this.buttonSubtitlesCreationPanel.Name = "buttonSubtitlesCreationPanel";
            this.buttonSubtitlesCreationPanel.Size = new System.Drawing.Size(122, 97);
            this.buttonSubtitlesCreationPanel.TabIndex = 2;
            this.buttonSubtitlesCreationPanel.Text = "Создать двуязычные субтитры";
            this.buttonSubtitlesCreationPanel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSubtitlesCreationPanel.UseVisualStyleBackColor = true;
            this.buttonSubtitlesCreationPanel.Click += new System.EventHandler(this.buttonSubtitlesCreationPanel_Click);
            // 
            // buttonHelp
            // 
            this.buttonHelp.FlatAppearance.BorderSize = 0;
            this.buttonHelp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonHelp.Image = global::BilingualSubtitler.Properties.Resources._16pxHelpIcon;
            this.buttonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHelp.Location = new System.Drawing.Point(402, 6);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(153, 23);
            this.buttonHelp.TabIndex = 10;
            this.buttonHelp.Text = "Справка";
            this.buttonHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            // 
            // buttonSettings
            // 
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonSettings.Image = global::BilingualSubtitler.Properties.Resources._48pxSettingsV3;
            this.buttonSettings.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonSettings.Location = new System.Drawing.Point(319, 3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(76, 97);
            this.buttonSettings.TabIndex = 7;
            this.buttonSettings.Text = "Настройки";
            this.buttonSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1168, 502);
            this.Controls.Add(this.labelProcessPercenage);
            this.Controls.Add(this.labelCurrentProcess);
            this.Controls.Add(this.panelSettings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.panelSubtitlesCreation);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.dataGridView1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Создание двуязычных субтитров";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelSubtitlesCreation.ResumeLayout(false);
            this.panelSubtitlesCreation.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panelSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVExtractLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMKVToolnix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYandexTranslator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonSubtitlesCreationPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonWriteBilingualSubtitles;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelSubtitlesCreation;
        private System.Windows.Forms.Button buttonBackToMainForm;
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.Label labelCurrentSecondarySubtitlesColor;
        private System.Windows.Forms.Button buttonCurrentSecondarySubtitlesColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIming;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrigSub;
        private System.Windows.Forms.DataGridViewTextBoxColumn RusSub;
        private System.ComponentModel.BackgroundWorker backgroundWorkerTranslation;
        private System.ComponentModel.BackgroundWorker backgroundWorkerWriteSubsToDataGrid;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelCurrentPrimarySubtitlesColor;
        private System.Windows.Forms.Button buttonCurrentPrimarySubtitlesColor;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonWriteBilingualSubtitlesAfterTranlation;
        private System.Windows.Forms.CheckBox checkBoxRemoveStylesFromSecondarySubs;
        private System.Windows.Forms.CheckBox checkBoxSecondarySubsInOneLine;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.CheckBox checkBoxRemoveStylesFromSecondarySubsOnSettingsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxSecondarySubsInOneLineOnSettingsPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCurrentSecondarySubtitlesColorOnSettingsPanel;
        private System.Windows.Forms.Button buttonCurrentPrimarySubtitlesColorOnSettingsPanel;
        private System.Windows.Forms.Button buttonSaveAndBackToTheMainFormOnSettingsPanel;
        private System.Windows.Forms.RichTextBox richTextBoxForYandexAPIKey;
        private System.Windows.Forms.RichTextBox richTextBoxLabelEnterYandexAPIKey;
        private System.Windows.Forms.Button buttonResetToDefault;
        private System.Windows.Forms.LinkLabel linkLabelGetYandexAPIKey;
        private System.Windows.Forms.PictureBox pictureBoxYandexTranslator;
        private System.Windows.Forms.Button buttonOpenMKV;
        private System.Windows.Forms.OpenFileDialog openFileDialogMKV;
        private System.Windows.Forms.PictureBox pictureBoxMKVToolnix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMKVToolnixFolder;
        private System.Windows.Forms.Label labelCurrentProcess;
        private System.Windows.Forms.Label labelProcessPercenage;
        private System.Windows.Forms.PictureBox pictureBoxMKVExtractLogo;
        private System.Windows.Forms.Label labelPathToTemp;
        private System.Windows.Forms.Button buttonBrowsePathToTemp;
        private System.Windows.Forms.TextBox textBoxPathToMKVToolnix;
        private System.Windows.Forms.TextBox textBoxPathToTemp;
        private System.Windows.Forms.Label labelPathToMKVToolnix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogMKVToolnix;
        private System.Windows.Forms.Button buttonBrowsePathToMKVToolnix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogTempSubs;
        private System.Windows.Forms.LinkLabel linkLabelDownloadMKVToolnix;
        private System.Windows.Forms.LinkLabel linkLabelYandexAPIKeysList;
        private System.Windows.Forms.Button buttonHelpOnSettingsPanel;
        private System.Windows.Forms.Button buttonRemoveHI;
    }
}

