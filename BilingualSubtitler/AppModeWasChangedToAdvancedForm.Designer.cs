namespace BilingualSubtitler
{
    partial class AppModeWasChangedToAdvancedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppModeWasChangedToAdvancedForm));
            numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new System.Drawing.Point(440, 103);
            numericUpDown1.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(54, 23);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button1.Image = Properties.Resources._16pxOkIcon;
            button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button1.Location = new System.Drawing.Point(432, 234);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(172, 39);
            button1.TabIndex = 1;
            button1.Text = "Да, установить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            button2.Image = Properties.Resources._16pxCancelIcon;
            button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button2.Location = new System.Drawing.Point(121, 234);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(305, 39);
            button2.TabIndex = 2;
            button2.Text = "Нет, не интересует; вернуться к окну настроек";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(672, 60);
            label1.TabIndex = 3;
            label1.Text = resources.GetString("label1.Text");
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 129);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(740, 30);
            label3.TabIndex = 5;
            label3.Text = "А для следующего за ним потока, с уже вручную переведенными субтитрами — подчеркивание снизу, для лучшего их различения.\r\nУстановить такой стиль? Если да — для потока переведенных субтитров №:";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new System.Drawing.Point(440, 147);
            numericUpDown2.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new System.Drawing.Size(54, 23);
            numericUpDown2.TabIndex = 6;
            numericUpDown2.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(12, 88);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(638, 30);
            label4.TabIndex = 7;
            label4.Text = "Для потока субтитров с машинным переводом я рекомендую шрифт Consolas размером на 2 меньше обычного.\r\nУстановить такой стиль? Если да — для потока переведенных субтитров №:\r\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 198);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(720, 15);
            label2.TabIndex = 8;
            label2.Text = "Также — включить отображение 2 и 3 потока переведенных субтитров в главном окне программы, если оно не было включено";
            // 
            // AppModeWasChangedToAdvancedForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(759, 277);
            Controls.Add(label2);
            Controls.Add(numericUpDown1);
            Controls.Add(label4);
            Controls.Add(numericUpDown2);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "AppModeWasChangedToAdvancedForm";
            Text = "Поток субтитров с машинным переводом";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}