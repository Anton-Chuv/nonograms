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
            this.HeadPanel = new System.Windows.Forms.Panel();
            this.AddLevelBtn = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.HeadPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.topPanel.Location = new System.Drawing.Point(0, 50);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(300, 450);
            this.topPanel.TabIndex = 0;
            // 
            // HeadPanel
            // 
            this.HeadPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.HeadPanel.Controls.Add(this.CloseButton);
            this.HeadPanel.Controls.Add(this.AddLevelBtn);
            this.HeadPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeadPanel.Location = new System.Drawing.Point(0, 0);
            this.HeadPanel.Name = "HeadPanel";
            this.HeadPanel.Size = new System.Drawing.Size(300, 50);
            this.HeadPanel.TabIndex = 1;
            this.HeadPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HeadPanel_MouseDown);
            this.HeadPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HeadPanel_MouseMove);
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
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Pink;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseButton.Location = new System.Drawing.Point(285, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(15, 15);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "X";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 500);
            this.Controls.Add(this.HeadPanel);
            this.Controls.Add(this.topPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainWindow";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.HeadPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel HeadPanel;
        private System.Windows.Forms.Button AddLevelBtn;
        private System.Windows.Forms.Button CloseButton;
    }
}