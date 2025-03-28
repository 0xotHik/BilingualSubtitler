namespace BilingualSubtitler
{
    partial class SaveFileReportSuccessAskToOpenInDefaultAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveFileReportSuccessAskToOpenInDefaultAppForm));
            label1 = new System.Windows.Forms.Label();
            fileNameLabel = new System.Windows.Forms.Label();
            okButton = new System.Windows.Forms.Button();
            openFileInDefaultAppButton = new System.Windows.Forms.Button();
            openTranslatorButton = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(202, 15);
            label1.TabIndex = 0;
            label1.Text = "Субтитры были сохранены в файл:";
            // 
            // fileNameLabel
            // 
            fileNameLabel.AutoSize = true;
            fileNameLabel.Location = new System.Drawing.Point(12, 35);
            fileNameLabel.Name = "fileNameLabel";
            fileNameLabel.Size = new System.Drawing.Size(38, 15);
            fileNameLabel.TabIndex = 1;
            fileNameLabel.Text = "label2";
            // 
            // okButton
            // 
            okButton.Location = new System.Drawing.Point(559, 65);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "Ok";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // openFileInDefaultAppButton
            // 
            openFileInDefaultAppButton.Location = new System.Drawing.Point(12, 65);
            openFileInDefaultAppButton.Name = "openFileInDefaultAppButton";
            openFileInDefaultAppButton.Size = new System.Drawing.Size(265, 23);
            openFileInDefaultAppButton.TabIndex = 3;
            openFileInDefaultAppButton.Text = "Открыть файл в программе по умолчанию";
            openFileInDefaultAppButton.UseVisualStyleBackColor = true;
            openFileInDefaultAppButton.Click += openFileInDefaultAppButton_Click;
            // 
            // openTranslatorButton
            // 
            openTranslatorButton.Location = new System.Drawing.Point(283, 65);
            openTranslatorButton.Name = "openTranslatorButton";
            openTranslatorButton.Size = new System.Drawing.Size(265, 23);
            openTranslatorButton.TabIndex = 4;
            openTranslatorButton.Text = "Открыть страницу переводчика";
            openTranslatorButton.UseVisualStyleBackColor = true;
            openTranslatorButton.Click += button1_Click;
            // 
            // ReportSuccessfullySavedAndAskToOpenSavedFileInDefaultAppForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(646, 103);
            Controls.Add(openTranslatorButton);
            Controls.Add(openFileInDefaultAppButton);
            Controls.Add(okButton);
            Controls.Add(fileNameLabel);
            Controls.Add(label1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "ReportSuccessfullySavedAndAskToOpenSavedFileInDefaultAppForm";
            Text = "Успешно";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button openFileInDefaultAppButton;
        private System.Windows.Forms.Button openTranslatorButton;
    }
}