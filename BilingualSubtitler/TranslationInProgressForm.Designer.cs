namespace BilingualSubtitler
{
    partial class TranslationInProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TranslationInProgressForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonBackToMainForm = new System.Windows.Forms.Button();
            this.buttonColorDialog = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxCurrentColor = new System.Windows.Forms.TextBox();
            this.labelCurrentColor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 132);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(556, 29);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // buttonBackToMainForm
            // 
            this.buttonBackToMainForm.Image = global::BilingualSubtitler.Properties.Resources._48pxBackIcon;
            this.buttonBackToMainForm.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonBackToMainForm.Location = new System.Drawing.Point(484, 13);
            this.buttonBackToMainForm.Name = "buttonBackToMainForm";
            this.buttonBackToMainForm.Size = new System.Drawing.Size(78, 99);
            this.buttonBackToMainForm.TabIndex = 5;
            this.buttonBackToMainForm.Text = "Вернуться к основному окну";
            this.buttonBackToMainForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonBackToMainForm.UseVisualStyleBackColor = true;
            this.buttonBackToMainForm.Click += new System.EventHandler(this.buttonBackToMainForm_Click);
            // 
            // buttonColorDialog
            // 
            this.buttonColorDialog.Image = global::BilingualSubtitler.Properties.Resources._48pxBrush;
            this.buttonColorDialog.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonColorDialog.Location = new System.Drawing.Point(90, 12);
            this.buttonColorDialog.Name = "buttonColorDialog";
            this.buttonColorDialog.Size = new System.Drawing.Size(75, 100);
            this.buttonColorDialog.TabIndex = 4;
            this.buttonColorDialog.Text = "Выбрать цвет";
            this.buttonColorDialog.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonColorDialog.UseVisualStyleBackColor = true;
            this.buttonColorDialog.Click += new System.EventHandler(this.buttonColorDialog_Click);
            // 
            // button1
            // 
            this.button1.Image = global::BilingualSubtitler.Properties.Resources.smallTranslateToRus;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 100);
            this.button1.TabIndex = 3;
            this.button1.Text = "Перевести";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxCurrentColor
            // 
            this.textBoxCurrentColor.Location = new System.Drawing.Point(171, 13);
            this.textBoxCurrentColor.Name = "textBoxCurrentColor";
            this.textBoxCurrentColor.Size = new System.Drawing.Size(19, 20);
            this.textBoxCurrentColor.TabIndex = 6;
            // 
            // labelCurrentColor
            // 
            this.labelCurrentColor.AutoSize = true;
            this.labelCurrentColor.Location = new System.Drawing.Point(197, 19);
            this.labelCurrentColor.Name = "labelCurrentColor";
            this.labelCurrentColor.Size = new System.Drawing.Size(188, 13);
            this.labelCurrentColor.TabIndex = 7;
            this.labelCurrentColor.Text = "Текущий цвет вторичных субтитров";
            // 
            // TranslationInProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 173);
            this.Controls.Add(this.labelCurrentColor);
            this.Controls.Add(this.textBoxCurrentColor);
            this.Controls.Add(this.buttonBackToMainForm);
            this.Controls.Add(this.buttonColorDialog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TranslationInProgressForm";
            this.Text = "TranslationInProgressForm";
            this.Load += new System.EventHandler(this.TranslationInProgressForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonColorDialog;
        private System.Windows.Forms.Button buttonBackToMainForm;
        private System.Windows.Forms.TextBox textBoxCurrentColor;
        private System.Windows.Forms.Label labelCurrentColor;
    }
}