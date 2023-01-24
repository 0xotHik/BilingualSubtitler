namespace BilingualSubtitler
{
    partial class VideoAndSubtitlesStateComboBoxWithBorder
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt = new System.Windows.Forms.Panel();
            this.VideoAndSubtitlesStateComboBox = new System.Windows.Forms.ComboBox();
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt
            // 
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.Controls.Add(this.VideoAndSubtitlesStateComboBox);
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.Location = new System.Drawing.Point(0,0);
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.Name = "panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt";
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.Size = new System.Drawing.Size(384, 25);
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.TabIndex = 12;
            // 
            // videoAndSubtitlesStateComboBox
            // 
            this.VideoAndSubtitlesStateComboBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.VideoAndSubtitlesStateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VideoAndSubtitlesStateComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VideoAndSubtitlesStateComboBox.FormattingEnabled = true;
            this.VideoAndSubtitlesStateComboBox.Location = new System.Drawing.Point(0, 0);
            this.VideoAndSubtitlesStateComboBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.VideoAndSubtitlesStateComboBox.Name = "videoAndSubtitlesStateComboBox";
            this.VideoAndSubtitlesStateComboBox.Size = new System.Drawing.Size(383, 23);
            this.VideoAndSubtitlesStateComboBox.TabIndex = 11;
            // 
            // VideoAndSubtitlesStateComboBoxWithBorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt);
            this.Name = "VideoAndSubtitlesStateComboBoxWithBorder";
            this.Size = new System.Drawing.Size(384, 25);
            this.panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWithVideoSubtitlesComboBoxToDrawBorderAroundIt;
        public System.Windows.Forms.ComboBox VideoAndSubtitlesStateComboBox;
    }
}
