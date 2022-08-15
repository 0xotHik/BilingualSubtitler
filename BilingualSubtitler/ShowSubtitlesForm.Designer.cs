namespace BilingualSubtitler
{
    partial class ShowSubtitlesForm
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
            this.originalSubtitlesDataGridView = new System.Windows.Forms.DataGridView();
            this.firstRussianSubtitlesDataGridView = new System.Windows.Forms.DataGridView();
            this.secondRussianSubtitlesDataGridView = new System.Windows.Forms.DataGridView();
            this.thirdRussianSubtitlesDataGridView = new System.Windows.Forms.DataGridView();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalSubtitlesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstRussianSubtitlesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondRussianSubtitlesDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thirdRussianSubtitlesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // originalSubtitlesDataGridView
            // 
            this.originalSubtitlesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.originalSubtitlesDataGridView.Location = new System.Drawing.Point(34, 40);
            this.originalSubtitlesDataGridView.Name = "originalSubtitlesDataGridView";
            this.originalSubtitlesDataGridView.RowTemplate.Height = 25;
            this.originalSubtitlesDataGridView.Size = new System.Drawing.Size(270, 267);
            this.originalSubtitlesDataGridView.TabIndex = 0;
            // 
            // firstRussianSubtitlesDataGridView
            // 
            this.firstRussianSubtitlesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.firstRussianSubtitlesDataGridView.Location = new System.Drawing.Point(324, 40);
            this.firstRussianSubtitlesDataGridView.Name = "firstRussianSubtitlesDataGridView";
            this.firstRussianSubtitlesDataGridView.RowTemplate.Height = 25;
            this.firstRussianSubtitlesDataGridView.Size = new System.Drawing.Size(270, 267);
            this.firstRussianSubtitlesDataGridView.TabIndex = 1;
            // 
            // secondRussianSubtitlesDataGridView
            // 
            this.secondRussianSubtitlesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.secondRussianSubtitlesDataGridView.Location = new System.Drawing.Point(614, 40);
            this.secondRussianSubtitlesDataGridView.Name = "secondRussianSubtitlesDataGridView";
            this.secondRussianSubtitlesDataGridView.RowTemplate.Height = 25;
            this.secondRussianSubtitlesDataGridView.Size = new System.Drawing.Size(270, 267);
            this.secondRussianSubtitlesDataGridView.TabIndex = 2;
            // 
            // thirdRussianSubtitlesDataGridView
            // 
            this.thirdRussianSubtitlesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.thirdRussianSubtitlesDataGridView.Location = new System.Drawing.Point(904, 40);
            this.thirdRussianSubtitlesDataGridView.Name = "thirdRussianSubtitlesDataGridView";
            this.thirdRussianSubtitlesDataGridView.RowTemplate.Height = 25;
            this.thirdRussianSubtitlesDataGridView.Size = new System.Drawing.Size(270, 267);
            this.thirdRussianSubtitlesDataGridView.TabIndex = 3;
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(9, 40);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 267);
            this.vScrollBar1.TabIndex = 4;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(317, 366);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(83, 19);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(317, 391);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(83, 19);
            this.checkBox2.TabIndex = 6;
            this.checkBox2.Text = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // ShowSubtitlesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 450);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.thirdRussianSubtitlesDataGridView);
            this.Controls.Add(this.secondRussianSubtitlesDataGridView);
            this.Controls.Add(this.firstRussianSubtitlesDataGridView);
            this.Controls.Add(this.originalSubtitlesDataGridView);
            this.Name = "ShowSubtitlesForm";
            this.Text = "ShowSubtitlesForm";
            ((System.ComponentModel.ISupportInitialize)(this.originalSubtitlesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firstRussianSubtitlesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondRussianSubtitlesDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thirdRussianSubtitlesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView originalSubtitlesDataGridView;
        private System.Windows.Forms.DataGridView firstRussianSubtitlesDataGridView;
        private System.Windows.Forms.DataGridView secondRussianSubtitlesDataGridView;
        private System.Windows.Forms.DataGridView thirdRussianSubtitlesDataGridView;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}