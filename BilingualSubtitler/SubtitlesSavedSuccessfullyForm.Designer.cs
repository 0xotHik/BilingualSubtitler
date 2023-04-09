namespace BilingualSubtitler
{
    partial class SubtitlesSavedSuccessfullyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubtitlesSavedSuccessfullyForm));
            this.fileOrFilesLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.dotLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // fileOrFilesLabel
            // 
            this.fileOrFilesLabel.AutoSize = true;
            this.fileOrFilesLabel.Location = new System.Drawing.Point(12, 9);
            this.fileOrFilesLabel.Name = "fileOrFilesLabel";
            this.fileOrFilesLabel.Size = new System.Drawing.Size(87, 15);
            this.fileOrFilesLabel.TabIndex = 0;
            this.fileOrFilesLabel.Text = "fileOrFilesLabel";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(35, 35);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(38, 15);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "label2";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(559, 65);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // dotLabel
            // 
            this.dotLabel.AutoSize = true;
            this.dotLabel.Location = new System.Drawing.Point(12, 35);
            this.dotLabel.Name = "dotLabel";
            this.dotLabel.Size = new System.Drawing.Size(12, 15);
            this.dotLabel.TabIndex = 4;
            this.dotLabel.Text = "•";
            // 
            // SubtitlesSavedSuccessfullyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 103);
            this.Controls.Add(this.dotLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.fileOrFilesLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubtitlesSavedSuccessfullyForm";
            this.Text = "Успешно";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fileOrFilesLabel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label dotLabel;
    }
}