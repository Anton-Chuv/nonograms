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
        List<int>[] leftNumRows;
        List<int>[] topNumCols;
        int CellSize = 20;
        int maxLeftRowLen = 0;
        int maxTopColLen = 0;

        public Playground() {
            InitializeComponent();
            Text = "Cup";
            this.Size = new System.Drawing.Size(300,300);
        }

        public Playground(Level level) {
            InitializeComponent();
            Text = level.Name;
            this.GridGame = level.levelGrid;
            this.GridHeight = level.levelGrid.GetLength(0);
            this.GridWidth = level.levelGrid.GetLength(1);
            this.leftNumRows = level.leftNumberRows;
            this.maxLeftRowLen = 0;
            for (int i = 0; i < leftNumRows.Length; i++)
                if (leftNumRows[i].Count > maxLeftRowLen)
                    maxLeftRowLen = leftNumRows[i].Count;
            this.LeftPanel.Width = maxLeftRowLen; //                             (если собираешся испольсозовать правый верхний угол, чтобы он не был слишком маленьким))

            this.topNumCols = level.topNumberColumns;
            this.maxTopColLen = 0;
            for (int i = 0; i < topNumCols.Length; i++)
                if (topNumCols[i].Count > maxTopColLen)
                    maxTopColLen = topNumCols[i].Count;
            this.TopPanel.Height = maxTopColLen; // нужен ли минимальный размер (наверно стоит сделать минимальный размер на всякий случий \

            // this.Size = new System.Drawing.Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);

        }

        private void Playground_Load(object sender, EventArgs e) {

            this.TopPanel.Size = new Size(this.GridWidth * CellSize + 1, this.TopPanel.Height * CellSize + 1);
            this.LeftPanel.Size = new Size(this.LeftPanel.Width * CellSize + 1, this.GridHeight * CellSize + 1);
            
            this.LeftPanel.Location = new Point(0, TopPanel.Height);
            this.TopPanel.Location = new Point(LeftPanel.Width, 0);


            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(LeftPanel.Width, TopPanel.Height);

            this.Size = new Size(LeftPanel.Width + GridPanel.Width, TopPanel.Height + GridPanel.Height);
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
                e.Graphics.DrawLine(Pens.LightGray, i * CellSize, 0, i * CellSize, GridHeight * CellSize);
            for (int i = 0; i < GridHeight + 1; i++)
                e.Graphics.DrawLine(Pens.LightGray, 0, i * CellSize, GridWidth * CellSize, i * CellSize);

            SolidBrush blackBrush = new SolidBrush(Color.Black);
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    if (GridGame[i, j] == 1)
                        e.Graphics.FillRectangle(blackBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
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
                        SolidBrush whiteBrush = new SolidBrush(Color.White);

                        g.FillRectangle(whiteBrush, 1 + click.X - (click.X % CellSize), 1 + click.Y - (click.Y % CellSize), CellSize - 1, CellSize - 1);
                        whiteBrush.Dispose();
                    }
                    break;
                case 0:
                    GridGame[click.Y / 20, click.X / 20] = 1;
                    using (Graphics g = this.GridPanel.CreateGraphics()) {
                        SolidBrush blackBrush = new SolidBrush(Color.Black);

                        g.FillRectangle(blackBrush, 1 + click.X - (click.X % CellSize), 1 + click.Y - (click.Y % CellSize), CellSize - 1, CellSize - 1);
                        blackBrush.Dispose();
                    }
                    break;
                default:
                    break;
            }
        }

        private void TopPanel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < GridWidth; i++) {
                e.Graphics.DrawLine(Pens.LightGray, i * CellSize, 0, i * CellSize, TopPanel.Height * CellSize);
                for (int j = 0; j < topNumCols[i].Count; j++) {
                    Point p = new Point(i * CellSize + CellSize / 2,(j + (maxTopColLen - topNumCols[i].Count)) * CellSize + CellSize / 2);
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(topNumCols[i][j].ToString(), Font, new SolidBrush(Color.Black), p, sf);
                }
            }
            e.Graphics.DrawLine(Pens.LightGray, GridWidth * CellSize, 0, GridWidth * CellSize, TopPanel.Height * CellSize);
        }

        private void LeftPanel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < GridHeight; i++) {
                e.Graphics.DrawLine(Pens.LightGray, 0, i * CellSize, LeftPanel.Width * CellSize, i * CellSize);
                for (int j = 0; j < leftNumRows[i].Count; j++) {
                    Point p = new Point((j + (maxLeftRowLen - leftNumRows[i].Count) ) * CellSize + CellSize / 2, i * CellSize + CellSize/2);
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(leftNumRows[i][j].ToString(), Font, new SolidBrush(Color.Black), p, sf);
                }
            }
            e.Graphics.DrawLine(Pens.LightGray, 0, GridHeight * CellSize, LeftPanel.Width * CellSize, GridHeight * CellSize);
        }

        private void Playground_FormClosed(object sender, FormClosedEventArgs e) {
            
        }
    }
}
