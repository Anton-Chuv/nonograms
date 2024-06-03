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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.topPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.HeadPanel = new System.Windows.Forms.Panel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.AddLevelBtn = new nonograms.RoundButton();
            this.topPanel.SuspendLayout();
            this.HeadPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.topPanel.Controls.Add(this.AddLevelBtn);
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.topPanel.Location = new System.Drawing.Point(0, 15);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(300, 485);
            this.topPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Добавить";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // HeadPanel
            // 
            this.HeadPanel.BackColor = System.Drawing.Color.RoyalBlue;
            this.HeadPanel.Controls.Add(this.CloseButton);
            this.HeadPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.HeadPanel.Location = new System.Drawing.Point(0, 0);
            this.HeadPanel.Name = "HeadPanel";
            this.HeadPanel.Size = new System.Drawing.Size(300, 15);
            this.HeadPanel.TabIndex = 1;
            this.HeadPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HeadPanel_MouseDown);
            this.HeadPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HeadPanel_MouseMove);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Pink;
            this.CloseButton.FlatAppearance.BorderSize = 0;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CloseButton.Image = ((System.Drawing.Image)(resources.GetObject("CloseButton.Image")));
            this.CloseButton.Location = new System.Drawing.Point(285, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(15, 15);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // AddLevelBtn
            // 
            this.AddLevelBtn.FlatAppearance.BorderSize = 0;
            this.AddLevelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddLevelBtn.Image = global::nonograms.Properties.Resources.create_plus_icon;
            this.AddLevelBtn.Location = new System.Drawing.Point(122, 14);
            this.AddLevelBtn.Name = "AddLevelBtn";
            this.AddLevelBtn.Size = new System.Drawing.Size(25, 25);
            this.AddLevelBtn.TabIndex = 2;
            this.AddLevelBtn.UseVisualStyleBackColor = true;
            this.AddLevelBtn.Click += new System.EventHandler(this.AddLevelBtn_Click);
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
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.HeadPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel HeadPanel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label label1;
        private RoundButton AddLevelBtn;
    }
}