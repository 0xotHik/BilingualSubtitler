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
            this.labelTrackToOpen = new System.Windows.Forms.Label();
            this.dataGridViewSubTracks = new System.Windows.Forms.DataGridView();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Language = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubTracks)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTrackToOpen
            // 
            this.labelTrackToOpen.AutoSize = true;
            this.labelTrackToOpen.Location = new System.Drawing.Point(15, 15);
            this.labelTrackToOpen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTrackToOpen.Name = "labelTrackToOpen";
            this.labelTrackToOpen.Size = new System.Drawing.Size(360, 15);
            this.labelTrackToOpen.TabIndex = 0;
            this.labelTrackToOpen.Text = "Пожалуйста, укажите, какие из субтитров следует использовать";
            // 
            // dataGridViewSubTracks
            // 
            this.dataGridViewSubTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSubTracks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSubTracks.BackgroundColor = System.Drawing.Color.DarkGoldenrod;
            this.dataGridViewSubTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSubTracks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Language,
            this.TrackName});
            this.dataGridViewSubTracks.Location = new System.Drawing.Point(19, 35);
            this.dataGridViewSubTracks.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewSubTracks.MultiSelect = false;
            this.dataGridViewSubTracks.Name = "dataGridViewSubTracks";
            this.dataGridViewSubTracks.ReadOnly = true;
            this.dataGridViewSubTracks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSubTracks.Size = new System.Drawing.Size(652, 228);
            this.dataGridViewSubTracks.TabIndex = 1;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonOk.Image = global::BilingualSubtitler.Properties.Resources._16pxOkIcon;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(548, 282);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(122, 39);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "Использовать";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonCancel.Image = global::BilingualSubtitler.Properties.Resources._16pxCancelIconAnother;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(19, 282);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(86, 39);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID трека";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Language
            // 
            this.Language.HeaderText = "Язык субтитров";
            this.Language.Name = "Language";
            this.Language.ReadOnly = true;
            // 
            // TrackName
            // 
            this.TrackName.HeaderText = "Название трека";
            this.TrackName.Name = "TrackName";
            this.TrackName.ReadOnly = true;
            // 
            // TrackToExtractFromMKVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(684, 335);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dataGridViewSubTracks);
            this.Controls.Add(this.labelTrackToOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "TrackToExtractFromMKVForm";
            this.Text = "Выберите трек субтитров";
            this.Load += new System.EventHandler(this.TrackToExtractFromMKVForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubTracks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTrackToOpen;
        private System.Windows.Forms.DataGridView dataGridViewSubTracks;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Language;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrackName;
    }
}