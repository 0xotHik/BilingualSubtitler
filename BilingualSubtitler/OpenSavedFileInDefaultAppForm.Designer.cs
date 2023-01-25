namespace BilingualSubtitler
{
    partial class OpenSavedFileInDefaultAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenSavedFileInDefaultAppForm));
            this.label1 = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.openFileInDefaultAppButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Субтитры были сохранены в файл:";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(12, 35);
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
            // openFileInDefaultAppButton
            // 
            this.openFileInDefaultAppButton.Location = new System.Drawing.Point(12, 65);
            this.openFileInDefaultAppButton.Name = "openFileInDefaultAppButton";
            this.openFileInDefaultAppButton.Size = new System.Drawing.Size(265, 23);
            this.openFileInDefaultAppButton.TabIndex = 3;
            this.openFileInDefaultAppButton.Text = "Открыть файл в программе по умолчанию";
            this.openFileInDefaultAppButton.UseVisualStyleBackColor = true;
            this.openFileInDefaultAppButton.Click += new System.EventHandler(this.openFileInDefaultAppButton_Click);
            // 
            // OpenSavedFileInDefaultAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 103);
            this.Controls.Add(this.openFileInDefaultAppButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenSavedFileInDefaultAppForm";
            this.Text = "✅ Успешно";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button openFileInDefaultAppButton;
    }
}