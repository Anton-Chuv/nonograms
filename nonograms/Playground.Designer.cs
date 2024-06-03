namespace nonograms {
    partial class Playground {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.GridPanel = new System.Windows.Forms.Panel();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.LeftPanel = new System.Windows.Forms.Panel();
            this.ColorPanel = new System.Windows.Forms.Panel();
            this.ColorBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.Color.White;
            this.GridPanel.Location = new System.Drawing.Point(176, 244);
            this.GridPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(657, 428);
            this.GridPanel.TabIndex = 0;
            this.GridPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GridPanel_Paint);
            this.GridPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseClick);
            this.GridPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseDown);
            this.GridPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseMove);
            this.GridPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseUp);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TopPanel.Location = new System.Drawing.Point(176, 114);
            this.TopPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(657, 120);
            this.TopPanel.TabIndex = 0;
            this.TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.TopPanel_Paint);
            // 
            // LeftPanel
            // 
            this.LeftPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.LeftPanel.Location = new System.Drawing.Point(18, 244);
            this.LeftPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LeftPanel.Name = "LeftPanel";
            this.LeftPanel.Size = new System.Drawing.Size(148, 424);
            this.LeftPanel.TabIndex = 1;
            this.LeftPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.LeftPanel_Paint);
            // 
            // ColorPanel
            // 
            this.ColorPanel.BackColor = System.Drawing.Color.Black;
            this.ColorPanel.Location = new System.Drawing.Point(12, 12);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(20, 20);
            this.ColorPanel.TabIndex = 12;
            // 
            // ColorBox
            // 
            this.ColorBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ColorBox.FormattingEnabled = true;
            this.ColorBox.Location = new System.Drawing.Point(38, 12);
            this.ColorBox.Name = "ColorBox";
            this.ColorBox.Size = new System.Drawing.Size(112, 27);
            this.ColorBox.TabIndex = 13;
            this.ColorBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColorBox_DrawItem);
            this.ColorBox.SelectedIndexChanged += new System.EventHandler(this.ColorBox_SelectedIndexChanged);
            // 
            // Playground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(850, 689);
            this.Controls.Add(this.ColorBox);
            this.Controls.Add(this.ColorPanel);
            this.Controls.Add(this.LeftPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.GridPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Playground";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Playground";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Playground_FormClosed);
            this.Load += new System.EventHandler(this.Playground_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gameGrid_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel LeftPanel;
        private System.Windows.Forms.Panel ColorPanel;
        private System.Windows.Forms.ComboBox ColorBox;
    }
}