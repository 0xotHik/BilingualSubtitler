namespace BilingualSubtitler
{
    partial class HotkeysToAuthorsExtendedDefaultsForm
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
            this.labelInfo = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(12, 57);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(486, 26);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "На данный момент для данной горячей клавиши не поддерживаются клавиши-модификатор" +
    "ы\r\nЕсли у вас есть в этом потребность — пожалуйста, напишите автору программы";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clearButton.Location = new System.Drawing.Point(12, 517);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(97, 43);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Отмена";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Visible = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.okButton.Location = new System.Drawing.Point(913, 517);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(47, 43);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Visible = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BilingualSubtitler.Properties.Resources.hotkeys_default;
            this.pictureBox1.Location = new System.Drawing.Point(15, 173);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(945, 325);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // HotkeysToAuthorsExtendedDefaultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(972, 572);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.labelInfo);
            this.Name = "HotkeysToAuthorsExtendedDefaultsForm";
            this.Text = "KeySettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}