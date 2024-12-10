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
            this.labelH = new System.Windows.Forms.Label();
            this.labelW = new System.Windows.Forms.Label();
            this.numericW = new System.Windows.Forms.NumericUpDown();
            this.numericH = new System.Windows.Forms.NumericUpDown();
            this.ReloadGridBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DropBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericH)).BeginInit();
            this.SuspendLayout();
            // 
            // DropBox
            // 
            this.DropBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.DropBox.Image = global::nonograms.Properties.Resources._2849811_refresh_arrows_multimedia_media_icon;
            this.DropBox.Location = new System.Drawing.Point(220, 9);
            this.DropBox.Name = "DropBox";
            this.DropBox.Size = new System.Drawing.Size(45, 50);
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
            // labelH
            // 
            this.labelH.AutoSize = true;
            this.labelH.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelH.Location = new System.Drawing.Point(12, 9);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(57, 17);
            this.labelH.TabIndex = 9;
            this.labelH.Text = "Высота";
            // 
            // labelW
            // 
            this.labelW.AutoSize = true;
            this.labelW.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelW.Location = new System.Drawing.Point(12, 39);
            this.labelW.Name = "labelW";
            this.labelW.Size = new System.Drawing.Size(59, 17);
            this.labelW.TabIndex = 8;
            this.labelW.Text = "Ширина";
            // 
            // numericW
            // 
            this.numericW.Location = new System.Drawing.Point(77, 39);
            this.numericW.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericW.Name = "numericW";
            this.numericW.Size = new System.Drawing.Size(40, 20);
            this.numericW.TabIndex = 7;
            this.numericW.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericH
            // 
            this.numericH.Location = new System.Drawing.Point(77, 9);
            this.numericH.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericH.Name = "numericH";
            this.numericH.Size = new System.Drawing.Size(40, 20);
            this.numericH.TabIndex = 6;
            this.numericH.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // ReloadGridBtn
            // 
            this.ReloadGridBtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ReloadGridBtn.FlatAppearance.BorderSize = 0;
            this.ReloadGridBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReloadGridBtn.Image = global::nonograms.Properties.Resources._2849811_refresh_arrows_multimedia_media_icon;
            this.ReloadGridBtn.Location = new System.Drawing.Point(123, 9);
            this.ReloadGridBtn.Name = "ReloadGridBtn";
            this.ReloadGridBtn.Size = new System.Drawing.Size(50, 50);
            this.ReloadGridBtn.TabIndex = 10;
            this.ReloadGridBtn.UseVisualStyleBackColor = false;
            // 
            // AddImg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.ReloadGridBtn);
            this.Controls.Add(this.labelH);
            this.Controls.Add(this.labelW);
            this.Controls.Add(this.numericW);
            this.Controls.Add(this.numericH);
            this.Controls.Add(this.GridPanel);
            this.Controls.Add(this.DropBox);
            this.Name = "AddImg";
            this.Text = "AddImg";
            this.Load += new System.EventHandler(this.AddImg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DropBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DropBox;
        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.Label labelW;
        private System.Windows.Forms.NumericUpDown numericW;
        private System.Windows.Forms.NumericUpDown numericH;
        private System.Windows.Forms.Button ReloadGridBtn;
    }
}