namespace nonograms {
    partial class AddForm {
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
            this.numericH = new System.Windows.Forms.NumericUpDown();
            this.numericW = new System.Windows.Forms.NumericUpDown();
            this.labelW = new System.Windows.Forms.Label();
            this.labelH = new System.Windows.Forms.Label();
            this.ReloadGridBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.NameBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.ColorBox = new System.Windows.Forms.ComboBox();
            this.ColorPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericW)).BeginInit();
            this.SuspendLayout();
            // 
            // GridPanel
            // 
            this.GridPanel.AutoSize = true;
            this.GridPanel.BackColor = System.Drawing.Color.White;
            this.GridPanel.Location = new System.Drawing.Point(13, 128);
            this.GridPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Size = new System.Drawing.Size(120, 42);
            this.GridPanel.TabIndex = 1;
            this.GridPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GridPanel_Paint);
            this.GridPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseDown);
            this.GridPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseMove);
            this.GridPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GridPanel_MouseUp);
            // 
            // numericH
            // 
            this.numericH.Location = new System.Drawing.Point(75, 10);
            this.numericH.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericH.Name = "numericH";
            this.numericH.Size = new System.Drawing.Size(40, 20);
            this.numericH.TabIndex = 2;
            this.numericH.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericH.ValueChanged += new System.EventHandler(this.numericH_ValueChanged);
            // 
            // numericW
            // 
            this.numericW.Location = new System.Drawing.Point(75, 40);
            this.numericW.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericW.Name = "numericW";
            this.numericW.Size = new System.Drawing.Size(40, 20);
            this.numericW.TabIndex = 3;
            this.numericW.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericW.ValueChanged += new System.EventHandler(this.numericW_ValueChanged);
            // 
            // labelW
            // 
            this.labelW.AutoSize = true;
            this.labelW.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelW.Location = new System.Drawing.Point(10, 40);
            this.labelW.Name = "labelW";
            this.labelW.Size = new System.Drawing.Size(59, 17);
            this.labelW.TabIndex = 4;
            this.labelW.Text = "Ширина";
            // 
            // labelH
            // 
            this.labelH.AutoSize = true;
            this.labelH.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelH.Location = new System.Drawing.Point(10, 10);
            this.labelH.Name = "labelH";
            this.labelH.Size = new System.Drawing.Size(57, 17);
            this.labelH.TabIndex = 5;
            this.labelH.Text = "Высота";
            // 
            // ReloadGridBtn
            // 
            this.ReloadGridBtn.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ReloadGridBtn.FlatAppearance.BorderSize = 0;
            this.ReloadGridBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReloadGridBtn.Location = new System.Drawing.Point(125, 10);
            this.ReloadGridBtn.Name = "ReloadGridBtn";
            this.ReloadGridBtn.Size = new System.Drawing.Size(50, 50);
            this.ReloadGridBtn.TabIndex = 6;
            this.ReloadGridBtn.Text = "S";
            this.ReloadGridBtn.UseVisualStyleBackColor = false;
            this.ReloadGridBtn.Click += new System.EventHandler(this.ReloadGridBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.SpringGreen;
            this.SaveBtn.FlatAppearance.BorderSize = 0;
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.SaveBtn.Location = new System.Drawing.Point(155, 101);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(20, 20);
            this.SaveBtn.TabIndex = 7;
            this.SaveBtn.Text = "Сохранить";
            this.SaveBtn.UseVisualStyleBackColor = false;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // NameBox
            // 
            this.NameBox.Location = new System.Drawing.Point(90, 70);
            this.NameBox.Name = "NameBox";
            this.NameBox.Size = new System.Drawing.Size(85, 20);
            this.NameBox.TabIndex = 8;
            this.NameBox.TextChanged += new System.EventHandler(this.NameBox_TextChanged);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NameLabel.Location = new System.Drawing.Point(10, 70);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(72, 17);
            this.NameLabel.TabIndex = 9;
            this.NameLabel.Text = "Название";
            // 
            // ColorBox
            // 
            this.ColorBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ColorBox.FormattingEnabled = true;
            this.ColorBox.Location = new System.Drawing.Point(37, 101);
            this.ColorBox.Name = "ColorBox";
            this.ColorBox.Size = new System.Drawing.Size(112, 21);
            this.ColorBox.TabIndex = 10;
            this.ColorBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox1_DrawItem);
            this.ColorBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ColorPanel
            // 
            this.ColorPanel.BackColor = System.Drawing.Color.Black;
            this.ColorPanel.Location = new System.Drawing.Point(13, 100);
            this.ColorPanel.Name = "ColorPanel";
            this.ColorPanel.Size = new System.Drawing.Size(20, 20);
            this.ColorPanel.TabIndex = 11;
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.ColorPanel);
            this.Controls.Add(this.ColorBox);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.NameBox);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.ReloadGridBtn);
            this.Controls.Add(this.labelH);
            this.Controls.Add(this.labelW);
            this.Controls.Add(this.numericW);
            this.Controls.Add(this.numericH);
            this.Controls.Add(this.GridPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddForm";
            this.Text = "AddForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddForm_FormClosed);
            this.Load += new System.EventHandler(this.AddForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel GridPanel;
        private System.Windows.Forms.NumericUpDown numericH;
        private System.Windows.Forms.NumericUpDown numericW;
        private System.Windows.Forms.Label labelW;
        private System.Windows.Forms.Label labelH;
        private System.Windows.Forms.Button ReloadGridBtn;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.TextBox NameBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.ComboBox ColorBox;
        private System.Windows.Forms.Panel ColorPanel;
    }
}