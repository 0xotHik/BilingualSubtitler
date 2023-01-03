namespace BilingualSubtitler
{
    partial class FileToUseFromZipForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileToUseFromZipForm));
            this.labelTrackToOpen = new System.Windows.Forms.Label();
            this.dataGridViewFilesInAcrhive = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Extentions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilesInAcrhive)).BeginInit();
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
            // dataGridViewFilesInAcrhive
            // 
            this.dataGridViewFilesInAcrhive.AllowUserToAddRows = false;
            this.dataGridViewFilesInAcrhive.AllowUserToDeleteRows = false;
            this.dataGridViewFilesInAcrhive.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewFilesInAcrhive.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFilesInAcrhive.BackgroundColor = System.Drawing.Color.DarkGoldenrod;
            this.dataGridViewFilesInAcrhive.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFilesInAcrhive.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Extentions});
            this.dataGridViewFilesInAcrhive.Location = new System.Drawing.Point(19, 35);
            this.dataGridViewFilesInAcrhive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridViewFilesInAcrhive.MultiSelect = false;
            this.dataGridViewFilesInAcrhive.Name = "dataGridViewFilesInAcrhive";
            this.dataGridViewFilesInAcrhive.ReadOnly = true;
            this.dataGridViewFilesInAcrhive.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFilesInAcrhive.Size = new System.Drawing.Size(478, 228);
            this.dataGridViewFilesInAcrhive.TabIndex = 1;
            // 
            // ID
            // 
            this.ID.HeaderText = "Имена файлов в архиве";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Extentions
            // 
            this.Extentions.HeaderText = "Расширения";
            this.Extentions.Name = "Extentions";
            this.Extentions.ReadOnly = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonOk.Image = global::BilingualSubtitler.Properties.Resources._16pxOkIcon;
            this.buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOk.Location = new System.Drawing.Point(374, 282);
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
            // FileToUseFromZipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(510, 335);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.dataGridViewFilesInAcrhive);
            this.Controls.Add(this.labelTrackToOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FileToUseFromZipForm";
            this.Text = "Выберите файл субтитров";
            this.Load += new System.EventHandler(this.TrackToExtractFromMKVForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFilesInAcrhive)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTrackToOpen;
        private System.Windows.Forms.DataGridView dataGridViewFilesInAcrhive;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Extentions;
    }
}