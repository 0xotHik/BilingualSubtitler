using System;
using System.Windows.Forms;

namespace BilingualSubtitler
{
    partial class SettingsAiRecognitionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsAiRecognitionForm));
            okButton = new Button();
            cancelButton = new Button();
            label1 = new Label();
            srtPackPathEndingTextBox = new TextBox();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            okButton.Image = Properties.Resources._16pxOkIcon;
            okButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            okButton.Location = new System.Drawing.Point(518, 157);
            okButton.Margin = new Padding(4, 3, 4, 3);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(111, 40);
            okButton.TabIndex = 5;
            okButton.Text = "Применить";
            okButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            okButton.UseVisualStyleBackColor = false;
            okButton.Click += buttonOk_Click;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cancelButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            cancelButton.Image = Properties.Resources._16pxCancelIconAnother;
            cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            cancelButton.Location = new System.Drawing.Point(18, 157);
            cancelButton.Margin = new Padding(4, 3, 4, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(86, 40);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Отмена";
            cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += buttonCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(18, 24);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(115, 15);
            label1.TabIndex = 106;
            label1.Text = "Путь до mkvToolNix";
            // 
            // srtPackPathEndingTextBox
            // 
            srtPackPathEndingTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            srtPackPathEndingTextBox.Location = new System.Drawing.Point(186, 21);
            srtPackPathEndingTextBox.Margin = new Padding(4, 3, 4, 3);
            srtPackPathEndingTextBox.Name = "srtPackPathEndingTextBox";
            srtPackPathEndingTextBox.Size = new System.Drawing.Size(443, 23);
            srtPackPathEndingTextBox.TabIndex = 105;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(18, 64);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(152, 15);
            label2.TabIndex = 115;
            label2.Text = "Путь до Faster Whisper XXL";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new System.Drawing.Point(186, 61);
            textBox1.Margin = new Padding(4, 3, 4, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(443, 23);
            textBox1.TabIndex = 114;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(18, 106);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(163, 15);
            label3.TabIndex = 117;
            label3.Text = "Путь для временного файла";
            // 
            // textBox2
            // 
            textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox2.Location = new System.Drawing.Point(186, 103);
            textBox2.Margin = new Padding(4, 3, 4, 3);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(443, 23);
            textBox2.TabIndex = 116;
            // 
            // SettingsAiRecognitionForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            ClientSize = new System.Drawing.Size(648, 211);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(srtPackPathEndingTextBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "SettingsAiRecognitionForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки распознавания с помощью ИИ";
            Load += SettingsAndroidForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void SettingsAndroidForm_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private Label label1;
        private TextBox srtPackPathEndingTextBox;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
    }
}