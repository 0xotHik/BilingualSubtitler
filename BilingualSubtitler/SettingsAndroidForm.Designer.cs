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
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            okButton.Image = Properties.Resources._16pxOkIcon;
            okButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            okButton.Location = new System.Drawing.Point(552, 664);
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
            cancelButton.Location = new System.Drawing.Point(21, 664);
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
            subtitlesAppearanceSettingsControl.Size = new System.Drawing.Size(654, 626);
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
            // SettingsAndroidForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            ClientSize = new System.Drawing.Size(676, 715);
            Controls.Add(checkBox1);
            Controls.Add(subtitlesAppearanceSettingsControl);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "SettingsAndroidForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки программы";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private SubtitlesAppearanceSettings subtitlesAppearanceSettingsControl;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}