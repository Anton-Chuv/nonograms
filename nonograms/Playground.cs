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
        int GridHeight = 10;
        int GridWidth = 10;
        int[,] GridGame = {
            {1,1,1,1,1,1,1,1,1,1},
            {1,0,1,1,0,0,1,1,0,1},
            {1,0,1,1,0,0,1,1,0,1},
            {1,0,1,1,0,0,1,1,0,1},
            {0,1,1,1,0,0,1,1,1,0},
            {0,0,0,1,1,1,1,0,0,0},
            {0,0,0,0,1,1,0,0,0,0},
            {0,0,0,0,1,1,0,0,0,0},
            {0,0,0,1,1,1,1,0,0,0},
            {0,0,1,1,1,1,1,1,0,0},
        };

        public Playground() {
            InitializeComponent();
            Text = "Cup";
            this.Size = new System.Drawing.Size(300,300);
        }

        public Playground(int[,] arr, int h, int w) {
            InitializeComponent();
            Text = "Cup";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new System.Drawing.Size(h*20+1, w*20+1);
            this.GridHeight = h;
            this.GridWidth = w;
            this.GridGame = arr;
        }

        private void Playground_Load(object sender, EventArgs e) {
            Panel TopPanel = new Panel();
            TopPanel.Dock = DockStyle.Top;
            TopPanel.BackColor = Color.Blue;

        }


        Graphics g;
        private void gameGrid_Paint(object sender, PaintEventArgs e) {
            g = CreateGraphics();
            g.Clear(Color.WhiteSmoke);
            for (int i = 0; i < GridHeight + 1; i++)
                g.DrawLine(Pens.Black, i * 20, 0, i * 20, 200);
            for (int i = 0; i < GridWidth + 1; i++)
                g.DrawLine(Pens.Black, 0, i*20, 200, i*20);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    if (GridGame[i,j] == 1)
                        g.FillRectangle(blackBrush, 2 + j * 20, 2 + i * 20, 17, 17);
        }
    }
}
