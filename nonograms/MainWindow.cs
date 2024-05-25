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
        public MainWindow() {
            InitializeComponent();
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
    }
}
