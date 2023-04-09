namespace BilingualSubtitler
{
    partial class FilesAlreadyExistsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilesAlreadyExistsForm));
            this.fileOrFilesLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.dotLabel = new System.Windows.Forms.Label();
            this.rewriteLabel = new System.Windows.Forms.Label();
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
            // yesButton
            // 
            this.yesButton.Location = new System.Drawing.Point(475, 94);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 2;
            this.yesButton.Text = "Да";
            this.yesButton.UseVisualStyleBackColor = true;
            this.yesButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // noButton
            // 
            this.noButton.Location = new System.Drawing.Point(556, 94);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(72, 23);
            this.noButton.TabIndex = 3;
            this.noButton.Text = "Отмена";
            this.noButton.UseVisualStyleBackColor = true;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
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
            // rewriteLabel
            // 
            this.rewriteLabel.AutoSize = true;
            this.rewriteLabel.Location = new System.Drawing.Point(12, 63);
            this.rewriteLabel.Name = "rewriteLabel";
            this.rewriteLabel.Size = new System.Drawing.Size(88, 15);
            this.rewriteLabel.TabIndex = 5;
            this.rewriteLabel.Text = "Перезаписать?";
            // 
            // FilesAlreadyExistsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 129);
            this.Controls.Add(this.rewriteLabel);
            this.Controls.Add(this.dotLabel);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.fileOrFilesLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FilesAlreadyExistsForm";
            this.Text = "Внимание!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label fileOrFilesLabel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.Label dotLabel;
        private System.Windows.Forms.Label rewriteLabel;
    }
}