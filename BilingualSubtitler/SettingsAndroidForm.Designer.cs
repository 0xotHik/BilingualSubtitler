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
            buttonOk = new Button();
            buttonCancel = new Button();
            button5 = new Button();
            subtitlesAppearanceSettingsControl = new SubtitlesAppearanceSettings();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            buttonOk.Image = Properties.Resources._16pxOkIcon;
            buttonOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonOk.Location = new System.Drawing.Point(552, 939);
            buttonOk.Margin = new Padding(4, 3, 4, 3);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(111, 40);
            buttonOk.TabIndex = 5;
            buttonOk.Text = "Применить";
            buttonOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonOk.UseVisualStyleBackColor = false;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            buttonCancel.Image = Properties.Resources._16pxCancelIconAnother;
            buttonCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            buttonCancel.Location = new System.Drawing.Point(21, 939);
            buttonCancel.Margin = new Padding(4, 3, 4, 3);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(86, 40);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Отмена";
            buttonCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            buttonCancel.UseVisualStyleBackColor = false;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            button5.Location = new System.Drawing.Point(184, 939);
            button5.Margin = new Padding(4, 3, 4, 3);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(273, 40);
            button5.TabIndex = 48;
            button5.Text = "📖  Сбросить все настройки программы\r\n к значениям по умолчанию";
            button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
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
            ClientSize = new System.Drawing.Size(676, 991);
            Controls.Add(checkBox1);
            Controls.Add(subtitlesAppearanceSettingsControl);
            Controls.Add(button5);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "SettingsAndroidForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Настройки программы";
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button button5;
        private SubtitlesAppearanceSettings subtitlesAppearanceSettingsControl;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}