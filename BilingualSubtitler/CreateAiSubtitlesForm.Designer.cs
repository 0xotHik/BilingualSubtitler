namespace BilingualSubtitler
{
    partial class CreateAiSubtitlesForm
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
            primarySubtitlesOpenOrCloseButton = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // primarySubtitlesOpenOrCloseButton
            // 
            primarySubtitlesOpenOrCloseButton.AllowDrop = true;
            primarySubtitlesOpenOrCloseButton.BackColor = System.Drawing.SystemColors.ControlLightLight;
            primarySubtitlesOpenOrCloseButton.Location = new System.Drawing.Point(13, 12);
            primarySubtitlesOpenOrCloseButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            primarySubtitlesOpenOrCloseButton.Name = "primarySubtitlesOpenOrCloseButton";
            primarySubtitlesOpenOrCloseButton.Size = new System.Drawing.Size(72, 57);
            primarySubtitlesOpenOrCloseButton.TabIndex = 12;
            primarySubtitlesOpenOrCloseButton.Text = "📁 \r\nОткрыть \r\nиз файла";
            primarySubtitlesOpenOrCloseButton.UseVisualStyleBackColor = false;
            primarySubtitlesOpenOrCloseButton.Click += this.primarySubtitlesOpenOrCloseButton_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(123, 15);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new System.Drawing.Size(240, 150);
            dataGridView1.TabIndex = 13;
            // 
            // CreateAiSubtitlesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(dataGridView1);
            Controls.Add(primarySubtitlesOpenOrCloseButton);
            Name = "CreateAiSubtitlesForm";
            Text = "CreateAiSubtitles";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button primarySubtitlesOpenOrCloseButton;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}