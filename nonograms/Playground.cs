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
        int GridHeight;
        int GridWidth;
        int[,] GridGame;
        int CellSize = 20;

        public Playground() {
            InitializeComponent();
            Text = "Cup";
            this.Size = new System.Drawing.Size(300,300);
        }

        public Playground(Level level) {
            InitializeComponent();
            Text = "Cup";
            this.FormBorderStyle = FormBorderStyle.None;
            this.GridGame = level.getLevelGrid();
            this.GridHeight = level.getLevelGrid().GetLength(0);
            this.GridWidth = level.getLevelGrid().GetLength(1);
            // this.Size = new System.Drawing.Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            
        }

        private void Playground_Load(object sender, EventArgs e) {
            //this.Controls.Add( TopPanel );
            //this.Controls.Add( GridPanel );
            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(this.Width - this.GridWidth * CellSize - 1, 
                                                this.Height - this.GridHeight * CellSize - 1);

        }



        Graphics g;
        private void gameGrid_Paint(object sender, PaintEventArgs e) {
            /*
            g = CreateGraphics();
            g.Clear(Color.WhiteSmoke);
            for (int i = 0; i < GridWidth + 1; i++)
                g.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, GridHeight * CellSize);
            for (int i = 0; i < GridHeight + 1; i++)
                g.DrawLine(Pens.Black, 0, i * CellSize, GridWidth * CellSize, i * CellSize);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    if (GridGame[i,j] == 1)
                        g.FillRectangle(blackBrush, 2 + j * CellSize, 2 + i * CellSize, CellSize-3, CellSize-3);
            */
        }
        private void GridPanel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < GridWidth + 1; i++)
                e.Graphics.DrawLine(Pens.Black, i * CellSize, 0, i * CellSize, GridHeight * CellSize);
            for (int i = 0; i < GridHeight + 1; i++)
                e.Graphics.DrawLine(Pens.Black, 0, i * CellSize, GridWidth * CellSize, i * CellSize);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    if (GridGame[i, j] == 1)
                        e.Graphics.FillRectangle(blackBrush, 2 + j * CellSize, 2 + i * CellSize, CellSize - 3, CellSize - 3);
        }
        Point click;

        private void GridPanel_MouseClick(object sender, MouseEventArgs e) {
            // TODO пока реагирует только на единичное нажатие, добавь ЗАЖАТИЕ
            // TODO2 левая и правая кнопка мыши работают одинаково, исправь
            click = e.Location;
            if (click.Y % 20 == 0 || click.X % 20 == 0)
                return;
            switch (GridGame[click.Y / 20, click.X / 20]) {
                case 1:
                    GridGame[click.Y / 20, click.X / 20] = 0;
                    using (Graphics g = this.GridPanel.CreateGraphics()) {
                        SolidBrush whiteBrush = new SolidBrush(Color.WhiteSmoke);

                        g.FillRectangle(whiteBrush, 2 + click.X - (click.X % CellSize), 2 + click.Y - (click.Y % CellSize), CellSize - 3, CellSize - 3);
                        whiteBrush.Dispose();
                    }
                    break;
                case 0:
                    GridGame[click.Y / 20, click.X / 20] = 1;
                    using (Graphics g = this.GridPanel.CreateGraphics()) {
                        SolidBrush blackBrush = new SolidBrush(Color.Black);

                        g.FillRectangle(blackBrush, 2 + click.X - (click.X % CellSize), 2 + click.Y - (click.Y % CellSize), CellSize - 3, CellSize - 3);
                        blackBrush.Dispose();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
