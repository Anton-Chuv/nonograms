using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nonograms {
    public partial class AddForm : Form {
        string Name;
        int GridHeight;
        int GridWidth;
        char[,] GridGame;


        Thread drawing = null;
        bool stop = false;

        int CellSize = 20;

        public AddForm() {
            InitializeComponent();
            GridHeight = (int)numericH.Value;
            GridWidth = (int)numericW.Value;
            GridGame = new char[GridHeight, GridWidth];
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    GridGame[i, j] = '0';
        }

        private void GridPanel_Paint(object sender, PaintEventArgs e) {
            for (int i = 0; i < GridWidth + 1; i++)
                e.Graphics.DrawLine(Pens.LightGray, i * CellSize, 0, i * CellSize, GridHeight * CellSize);
            for (int i = 0; i < GridHeight + 1; i++)
                e.Graphics.DrawLine(Pens.LightGray, 0, i * CellSize, GridWidth * CellSize, i * CellSize);

        }



        private void AddForm_Load(object sender, EventArgs e) {
            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(0, 130);

            this.Size = new Size(GridPanel.Width, GridPanel.Location.Y + GridPanel.Height);
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
        }

        Point click;

        private void GridPanel_MouseDown(object sender, MouseEventArgs e) {
            stop = false;

            click = new Point(e.X, e.Y);
            if (click.Y % 20 == 0 || click.X % 20 == 0)
                return;
            switch (e.Button) {
                case MouseButtons.Right:
                    drawing = new Thread(() => {
                        using (Graphics g = this.GridPanel.CreateGraphics()) {
                            SolidBrush whiteBrush = new SolidBrush(Color.White);
                            while (!stop) {
                                try { GridGame[click.Y / 20, click.X / 20] = '0'; } catch { }
                                g.FillRectangle(whiteBrush, 1 + click.X - (click.X % CellSize), 1 + click.Y - (click.Y % CellSize), CellSize - 1, CellSize - 1);
                            }
                            whiteBrush.Dispose();
                        }
                    });
                    drawing.Start();
                    break;
                case MouseButtons.Left:
                    drawing = new Thread(() => {
                        using (Graphics g = this.GridPanel.CreateGraphics()) {
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            while (!stop) {
                                try { GridGame[click.Y / 20, click.X / 20] = '1'; } catch { }
                                g.FillRectangle(blackBrush, 1 + click.X - (click.X % CellSize), 1 + click.Y - (click.Y % CellSize), CellSize - 1, CellSize - 1);
                            }
                            blackBrush.Dispose();
                        }
                    });
                    drawing.Start();

                    break;
                default:
                    break;
            }
        }

        private void GridPanel_MouseUp(object sender, MouseEventArgs e) {
            stop = true;
        }

        private void GridPanel_MouseMove(object sender, MouseEventArgs e) {
            click = new Point(e.X, e.Y);

        }

        private void NameBox_TextChanged(object sender, EventArgs e) {
            Name = NameBox.Text;
            Console.WriteLine(Name);
        }

        void SaveBtn_Click(object sender, EventArgs e) {
            string progressStr = "";
            string ansStr = "";
            foreach (char x in GridGame) {
                ansStr += x;
                progressStr += '0';
            }
            Console.WriteLine(progressStr);
            Console.WriteLine(ansStr);
            using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = $"INSERT INTO nonogramlevels (Name, Height, Width, Answer, Progress) " +
                    $"VALUES ('{Name}', {GridHeight}, {GridWidth}, '{ansStr}', '{progressStr}')";
                command.ExecuteNonQuery();

            }
        }

        private void AddForm_FormClosed(object sender, FormClosedEventArgs e) {

        }

        private void numericH_ValueChanged(object sender, EventArgs e) {
            GridHeight = (int)numericH.Value;
        }
        private void numericW_ValueChanged(object sender, EventArgs e) {
            GridWidth = (int)numericW.Value;
        }
    }
}
