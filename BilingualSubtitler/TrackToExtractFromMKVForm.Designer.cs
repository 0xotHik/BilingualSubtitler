namespace BilingualSubtitler
{
    partial class TrackToExtractFromMKVForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackToExtractFromMKVForm));
            labelTrackToOpen = new System.Windows.Forms.Label();
            mkvTracksDGW = new System.Windows.Forms.DataGridView();
            ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            Language = new System.Windows.Forms.DataGridViewTextBoxColumn();
            TrackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            buttonOk = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            ArgsRichTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)mkvTracksDGW).BeginInit();
            SuspendLayout();
            // 
            // labelTrackToOpen
            // 
            labelTrackToOpen.AutoSize = true;
            labelTrackToOpen.Location = new System.Drawing.Point(15, 15);
            labelTrackToOpen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelTrackToOpen.Name = "labelTrackToOpen";
            labelTrackToOpen.Size = new System.Drawing.Size(360, 15);
            labelTrackToOpen.TabIndex = 0;
            labelTrackToOpen.Text = "Пожалуйста, укажите, какие из субтитров следует использовать";
            // 
            // mkvTracksDGW
            // 
            mkvTracksDGW.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            mkvTracksDGW.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            mkvTracksDGW.BackgroundColor = System.Drawing.Color.DarkGoldenrod;
            mkvTracksDGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            mkvTracksDGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { ID, Language, TrackName });
            mkvTracksDGW.Location = new System.Drawing.Point(19, 35);
            mkvTracksDGW.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            mkvTracksDGW.MultiSelect = false;
            mkvTracksDGW.Name = "mkvTracksDGW";
            mkvTracksDGW.ReadOnly = true;
            mkvTracksDGW.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            mkvTracksDGW.Size = new System.Drawing.Size(652, 228);
            mkvTracksDGW.TabIndex = 1;
            // 
            // ID
            // 
            ID.HeaderText = "ID трека";
            ID.Name = "ID";
            ID.ReadOnly = true;
            // 
            // Language
            // 
            Language.HeaderText = "Язык субтитров";
            Language.Name = "Language";
            Language.ReadOnly = true;
            // 
            // TrackName
            // 
            TrackName.HeaderText = "Название трека";
            TrackName.Name = "TrackName";
            TrackName.ReadOnly = true;
            // 
            // buttonOk
            // 
            buttonOk.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOk.Image = Properties.Resources._16pxOkIcon;
            buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonOk.Location = new System.Drawing.Point(548, 282);
            buttonOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(122, 39);
            buttonOk.TabIndex = 7;
            buttonOk.Text = "Использовать";
            buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            buttonCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            buttonCancel.Image = Properties.Resources._16pxCancelIconAnother;
            buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCancel.Location = new System.Drawing.Point(19, 282);
            buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(86, 39);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "Отмена";
            buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // ArgsRichTextBox
            // 
            ArgsRichTextBox.Location = new System.Drawing.Point(122, 269);
            ArgsRichTextBox.Name = "ArgsRichTextBox";
            ArgsRichTextBox.Size = new System.Drawing.Size(419, 63);
            ArgsRichTextBox.TabIndex = 8;
            ArgsRichTextBox.Text = "--check_files --language ru --output_dir source --output_format srt --standard --print_progress --model large-v2";
            // 
            // TrackToExtractFromMKVForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            ClientSize = new System.Drawing.Size(684, 335);
            Controls.Add(ArgsRichTextBox);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Controls.Add(mkvTracksDGW);
            Controls.Add(labelTrackToOpen);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "TrackToExtractFromMKVForm";
            Text = "Выберите трек субтитров";
            Load += TrackToExtractFromMKVForm_Load;
            ((System.ComponentModel.ISupportInitialize)mkvTracksDGW).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTrackToOpen;
        private System.Windows.Forms.DataGridView mkvTracksDGW;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackName;
        public System.Windows.Forms.RichTextBox ArgsRichTextBox;
    }
}