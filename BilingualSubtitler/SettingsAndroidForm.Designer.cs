using System;
using System.Windows.Forms;

namespace BilingualSubtitler
{
    partial class SettingsAndroidForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsAndroidForm));
            okButton = new Button();
            cancelButton = new Button();
            subtitlesAppearanceSettingsControl = new SubtitlesAppearanceSettings();
            checkBox1 = new CheckBox();
            label1 = new Label();
            srtPackPathEndingTextBox = new TextBox();
            createSrtPackRadioButton = new RadioButton();
            createSrtSeparateSteamsRadioButton = new RadioButton();
            label2 = new Label();
            groupBox2 = new GroupBox();
            label3 = new Label();
            button5 = new Button();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            okButton.Image = Properties.Resources._16pxOkIcon;
            okButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            okButton.Location = new System.Drawing.Point(549, 759);
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
            cancelButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            cancelButton.Image = Properties.Resources._16pxCancelIconAnother;
            cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            cancelButton.Location = new System.Drawing.Point(18, 759);
            cancelButton.Margin = new Padding(4, 3, 4, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new System.Drawing.Size(86, 40);
            cancelButton.TabIndex = 4;
            cancelButton.Text = "Отмена";
            cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += buttonCancel_Click;
            // 
            // subtitlesAppearanceSettingsControl
            // 
            subtitlesAppearanceSettingsControl.AutoScroll = true;
            subtitlesAppearanceSettingsControl.AutoScrollMinSize = new System.Drawing.Size(934, 541);
            subtitlesAppearanceSettingsControl.Location = new System.Drawing.Point(18, 12);
            subtitlesAppearanceSettingsControl.Name = "subtitlesAppearanceSettingsControl";
            subtitlesAppearanceSettingsControl.Size = new System.Drawing.Size(654, 562);
            subtitlesAppearanceSettingsControl.TabIndex = 101;
            subtitlesAppearanceSettingsControl.Load += subtitlesAppearanceSettingsControl_Load;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(126, 66);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(117, 19);
            checkBox1.TabIndex = 102;
            checkBox1.Text = "Перезаписывать";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(83, 597);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(319, 15);
            label1.TabIndex = 106;
            label1.Text = "Окончание имени файла (-ов) переведенных субтитров:";
            // 
            // srtPackPathEndingTextBox
            // 
            srtPackPathEndingTextBox.Location = new System.Drawing.Point(136, 621);
            srtPackPathEndingTextBox.Margin = new Padding(4, 3, 4, 3);
            srtPackPathEndingTextBox.Name = "srtPackPathEndingTextBox";
            srtPackPathEndingTextBox.Size = new System.Drawing.Size(212, 23);
            srtPackPathEndingTextBox.TabIndex = 105;
            // 
            // createSrtPackRadioButton
            // 
            createSrtPackRadioButton.AutoSize = true;
            createSrtPackRadioButton.Location = new System.Drawing.Point(10, 40);
            createSrtPackRadioButton.Name = "createSrtPackRadioButton";
            createSrtPackRadioButton.Size = new System.Drawing.Size(135, 34);
            createSrtPackRadioButton.TabIndex = 107;
            createSrtPackRadioButton.TabStop = true;
            createSrtPackRadioButton.Text = "Общий файла пака\r\n со всеми потоками";
            createSrtPackRadioButton.UseVisualStyleBackColor = true;
            createSrtPackRadioButton.CheckedChanged += createSrtPackRadioButton_CheckedChanged;
            // 
            // createSrtSeparateSteamsRadioButton
            // 
            createSrtSeparateSteamsRadioButton.AutoSize = true;
            createSrtSeparateSteamsRadioButton.Location = new System.Drawing.Point(10, 81);
            createSrtSeparateSteamsRadioButton.Name = "createSrtSeparateSteamsRadioButton";
            createSrtSeparateSteamsRadioButton.Size = new System.Drawing.Size(130, 34);
            createSrtSeparateSteamsRadioButton.TabIndex = 108;
            createSrtSeparateSteamsRadioButton.TabStop = true;
            createSrtSeparateSteamsRadioButton.Text = "Отдельные файлы \r\nдля кажого потока";
            createSrtSeparateSteamsRadioButton.UseVisualStyleBackColor = true;
            createSrtSeparateSteamsRadioButton.CheckedChanged += createSrtSeparateSteamsRadioButton_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 17);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(176, 15);
            label2.TabIndex = 109;
            label2.Text = "с переведенными субтитрами:";
            label2.Click += label2_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(createSrtPackRadioButton);
            groupBox2.Controls.Add(createSrtSeparateSteamsRadioButton);
            groupBox2.Location = new System.Drawing.Point(461, 611);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(188, 126);
            groupBox2.TabIndex = 111;
            groupBox2.TabStop = false;
            groupBox2.Text = "Создавать файл(-ы)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 204);
            label3.Location = new System.Drawing.Point(18, 647);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(436, 90);
            label3.TabIndex = 112;
            label3.Text = resources.GetString("label3.Text");
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button5.Location = new System.Drawing.Point(190, 759);
            button5.Margin = new Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(273, 40);
            button5.TabIndex = 113;
            button5.Text = "📖  Сбросить все настройки для Android\r\n к значениям по умолчанию";
            button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click_1;
            // 
            // SettingsAndroidForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            ClientSize = new System.Drawing.Size(676, 813);
            Controls.Add(button5);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(groupBox2);
            Controls.Add(srtPackPathEndingTextBox);
            Controls.Add(checkBox1);
            Controls.Add(subtitlesAppearanceSettingsControl);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "SettingsAndroidForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки программы";
            Load += SettingsAndroidForm_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private SubtitlesAppearanceSettings subtitlesAppearanceSettingsControl;
        private System.Windows.Forms.CheckBox checkBox1;
        private Label label1;
        private TextBox srtPackPathEndingTextBox;
        private RadioButton createSrtPackRadioButton;
        private RadioButton createSrtSeparateSteamsRadioButton;
        private Label label2;
        private GroupBox groupBox2;
        private Label label3;
        private Button button5;
    }
}