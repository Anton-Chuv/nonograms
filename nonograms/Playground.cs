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
    public partial class Playground : Form {
        public Playground() {
            InitializeComponent();
            Text = "Cup";
            this.Size = new System.Drawing.Size(300,300);
        }

        public Playground(int[,] arr, int h, int w) {
            
        }

        private void Playground_Load(object sender, EventArgs e) {

        }


        Graphics g;
        private void gameGrid_Paint(object sender, PaintEventArgs e) {
            g = CreateGraphics();
            g.Clear(Color.White);
            for (int i = 0; i < 11; i++)
                g.DrawLine(Pens.Black, i * 20, 0, i * 20, 200);
            for (int i = 0; i < 11; i++)
                g.DrawLine(Pens.Black, 0, i*20, 200, i*20);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            for (int i = 0; i < 10; i++)
                g.FillRectangle(blueBrush, 1 + i * 20, 1 + i * 20, 19, 19);
        }
    }
}
