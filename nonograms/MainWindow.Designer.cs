namespace nonograms
{
    partial class MainWindow
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
            this.topPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AddLevelBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.SkyBlue;
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.topPanel.Location = new System.Drawing.Point(0, 50);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(300, 450);
            this.topPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.AddLevelBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 50);
            this.panel1.TabIndex = 1;
            // 
            // AddLevelBtn
            // 
            this.AddLevelBtn.BackColor = System.Drawing.Color.Lavender;
            this.AddLevelBtn.FlatAppearance.BorderSize = 0;
            this.AddLevelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddLevelBtn.Location = new System.Drawing.Point(13, 13);
            this.AddLevelBtn.Name = "AddLevelBtn";
            this.AddLevelBtn.Size = new System.Drawing.Size(75, 24);
            this.AddLevelBtn.TabIndex = 0;
            this.AddLevelBtn.Text = "Добавить";
            this.AddLevelBtn.UseVisualStyleBackColor = false;
            this.AddLevelBtn.Click += new System.EventHandler(this.AddLevelBtn_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 500);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button AddLevelBtn;
    }
}