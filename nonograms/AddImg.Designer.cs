namespace nonograms
{
    partial class AddImg
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
            this.DropBox = new System.Windows.Forms.PictureBox();
            this.GridPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DropBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DropBox
            // 
            this.DropBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DropBox.Image = global::nonograms.Properties.Resources._2849811_refresh_arrows_multimedia_media_icon;
            this.DropBox.Location = new System.Drawing.Point(12, 12);
            this.DropBox.Name = "DropBox";
            this.DropBox.Size = new System.Drawing.Size(100, 100);
            this.DropBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DropBox.TabIndex = 0;
            this.DropBox.TabStop = false;
            this.DropBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.DropBox_DragDrop);
            this.DropBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.DropBox_DragEnter);
            // 
            // GridPanel
            // 
            this.GridPanel.AutoSize = true;
            this.GridPanel.BackColor = System.Drawing.Color.White;
            this.GridPanel.Location = new System.Drawing.Point(12, 120);
            this.GridPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(120, 42);
            this.GridPanel.TabIndex = 2;
            // 
            // AddImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.DropBox);
            this.Name = "AddImg";
            this.Text = "AddImg";
            this.Load += new System.EventHandler(this.AddImg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DropBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DropBox;
        private System.Windows.Forms.Panel GridPanel;
    }
}