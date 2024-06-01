using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nonograms {
    public partial class MainWindow : Form {
        public MainWindow(List<string> Names) {
            InitializeComponent();
            Panel panel;
            panel = new Panel();
            panel.Location = new Point(200, 200);
            topPanel.Controls.Add(panel);
            int i = 0;
            List<Label> labels = new List<Label>();
            List<Button> buttons = new List<Button>();
            foreach (string name in Names) {
                Label label = new Label();
                label.AutoSize = true;
                labels.Add(label);
                label.Text = name;
                label.BackColor = Color.Transparent;
                label.Font = new Font(label.Font.FontFamily, 20, FontStyle.Regular);
                label.Location = new Point(20, i * 100 + 20);
                topPanel.Controls.Add(label);

                Button btn = new Button();
                btn.Text = "Запуск";
                btn.FlatStyle = FlatStyle.Flat;
                btn.AutoSize = true;
                buttons.Add(btn);
                btn.Location = new Point(20, i * 100 + 70);
                btn.Click += (object s, EventArgs ev) => {
                    this.Hide();
                    Playground levelform = new Playground(name);
                    levelform.FormClosed += (object o, FormClosedEventArgs e) => { this.Show(); };
                    levelform.Show();
                };
                topPanel.Controls.Add(btn);

                i++;
            }
        }
        
        

        private void closeButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        Point LastPoint;
        private void mainLabel_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.Left += e.X - LastPoint.X;
                this.Top += e.Y - LastPoint.Y;
            }
        }

        private void mainLabel_MouseDown(object sender, MouseEventArgs e) {
            LastPoint = new Point(e.X, e.Y);
        }

        private void mainLabel_Click(object sender, EventArgs e) {

        }

        private void button1_Click(object sender, EventArgs e) {
            this.Hide();
            Playground levelform = new Playground("Cup");
            levelform.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); };
            levelform.Show();
        }
    }
}
