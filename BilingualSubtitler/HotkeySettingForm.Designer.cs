﻿namespace BilingualSubtitler
{
    partial class HotkeySettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HotkeySettingForm));
            this.labelInfo = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.warningLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Location = new System.Drawing.Point(12, 40);
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
            this.clearButton.Location = new System.Drawing.Point(12, 147);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(181, 43);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Другая\r\n(очистить теущее назначение)";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Visible = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.okButton.Location = new System.Drawing.Point(451, 147);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(47, 43);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Visible = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // warningLabel
            // 
            this.warningLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.warningLabel.Location = new System.Drawing.Point(12, 88);
            this.warningLabel.Name = "warningLabel";
            this.warningLabel.Size = new System.Drawing.Size(486, 43);
            this.warningLabel.TabIndex = 3;
            this.warningLabel.Text = resources.GetString("warningLabel.Text");
            this.warningLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.warningLabel.Visible = false;
            // 
            // HotkeySettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(511, 202);
            this.Controls.Add(this.warningLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.labelInfo);
            this.Name = "HotkeySettingForm";
            this.Text = "KeySettingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label warningLabel;
    }
}