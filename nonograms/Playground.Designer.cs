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
            this.SuspendLayout();
            // 
            // GridPanel
            // 
            this.GridPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.GridPanel.Location = new System.Drawing.Point(117, 158);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(438, 278);
            this.GridPanel.TabIndex = 0;
            this.GridPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GridPanel_Paint);
            this.GridPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseClick);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.TopPanel.Location = new System.Drawing.Point(117, 74);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(438, 78);
            this.TopPanel.TabIndex = 0;
            // 
            // Playground
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Bisque;
            this.ClientSize = new System.Drawing.Size(567, 448);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.GridPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Playground";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Playground";
            this.Load += new System.EventHandler(this.Playground_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.gameGrid_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.Panel TopPanel;
    }
}