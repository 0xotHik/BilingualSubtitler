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
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Language = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrackName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSubTracks)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTrackToOpen
            // 
            this.labelTrackToOpen.AutoSize = true;
            this.labelTrackToOpen.Location = new System.Drawing.Point(13, 13);
            this.labelTrackToOpen.Name = "labelTrackToOpen";
            this.labelTrackToOpen.Size = new System.Drawing.Size(339, 13);
            this.labelTrackToOpen.TabIndex = 0;
            this.labelTrackToOpen.Text = "Пожалуйста, укажите, какие из субтитров следует использовать";
            // 
            // dataGridViewSubTracks
            // 
            this.dataGridViewSubTracks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSubTracks.BackgroundColor = System.Drawing.Color.DarkGoldenrod;
            this.dataGridViewSubTracks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSubTracks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Language,
            this.TrackName});
            this.dataGridViewSubTracks.Location = new System.Drawing.Point(16, 30);
            this.dataGridViewSubTracks.MultiSelect = false;
            this.dataGridViewSubTracks.Name = "dataGridViewSubTracks";
            this.dataGridViewSubTracks.ReadOnly = true;
            this.dataGridViewSubTracks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSubTracks.Size = new System.Drawing.Size(410, 198);
            this.dataGridViewSubTracks.TabIndex = 1;
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
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonOk.Image = global::BilingualSubtitler.Properties.Resources._16pxOkIcon;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(321, 244);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(105, 34);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "Использовать";
            this.buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonCancel.Image = global::BilingualSubtitler.Properties.Resources._16pxCancelIconAnother;
            this.buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCancel.Location = new System.Drawing.Point(16, 244);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(74, 34);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // TrackToExtractFromMKVForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(437, 290);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dataGridViewSubTracks);
            this.Controls.Add(this.labelTrackToOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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