﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;



namespace nonograms {


    public partial class Playground : Form {
        int GridHeight;
        int GridWidth;
        char[,] GridGame;
        char[,] GridAns;
        string levelName;

        List<int>[] leftNumRows;
        List<int>[] topNumCols;
        int maxLeftRowLen = 0;
        int maxTopColLen = 0;

        Thread drawing = null;
        bool stop = false;

        int CellSize = 20;
        public Playground(Level level) {
            InitializeComponent();
            Text = level.Name;
            // this.GridGame = level.levelGrid;
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

        public Playground(String name) {
            InitializeComponent();
            levelName = name;
            string sqlExpression = $"SELECT * FROM nonogramlevels where name = '{name}'";
            using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    if (reader.HasRows) {
                        reader.Read();
                        //string name = (string)reader.GetValue(0);
                        this.Text = name;
                        int height = reader.GetInt32(1);
                        this.GridHeight = height;
                        var width = reader.GetInt32(2);
                        this.GridWidth = width;
                        string answer = (string)reader.GetValue(3);
                        this.GridAns = new char[height, width];
                        for (int i = 0; i < height; i++)
                            for (int j = 0; j < width; j++)
                                this.GridAns[i,j] = answer[i*width + j];
                        string progress = (string)reader.GetValue(4);
                        this.GridGame = new char[height, width];
                        for (int i = 0; i < height; i++)
                            for (int j = 0; j < width; j++)
                                this.GridGame[i, j] = progress[i * width + j];
                    }
                }
            }
            fillLeftNumberRows(this.GridAns);
            fillTopNumberColumns(this.GridAns);
            this.maxLeftRowLen = 0;
            for (int i = 0; i < leftNumRows.Length; i++)
                if (leftNumRows[i].Count > maxLeftRowLen)
                    maxLeftRowLen = leftNumRows[i].Count;
            this.LeftPanel.Width = maxLeftRowLen; //                             (если собираешся испольсозовать правый верхний угол, чтобы он не был слишком маленьким))

            this.maxTopColLen = 0;
            for (int i = 0; i < topNumCols.Length; i++)
                if (topNumCols[i].Count > maxTopColLen)
                    maxTopColLen = topNumCols[i].Count;
            this.TopPanel.Height = maxTopColLen; // нужен ли минимальный размер (наверно стоит сделать минимальный размер на всякий случий \
        }

        private void Playground_Load(object sender, EventArgs e) {

            this.TopPanel.Size = new Size(this.GridWidth * CellSize + 1, this.TopPanel.Height * CellSize + 1);
            this.LeftPanel.Size = new Size(this.LeftPanel.Width * CellSize + 1, this.GridHeight * CellSize + 1);
            
            this.LeftPanel.Location = new Point(0, TopPanel.Height);
            this.TopPanel.Location = new Point(LeftPanel.Width, 0);


            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(LeftPanel.Width, TopPanel.Height);

            this.Size = new Size(LeftPanel.Width + GridPanel.Width, TopPanel.Height + GridPanel.Height);
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

        }

        void fillLeftNumberRows(char[,] levelGrid) {
            leftNumRows = new List<int>[levelGrid.GetLength(0)];
            for (int i = 0; i < levelGrid.GetLength(0); i++) {
                leftNumRows[i] = new List<int>();
                for (int j = 0; j < levelGrid.GetLength(1); j++) {
                    int lenFilledCell = 0;
                    while (j < levelGrid.GetLength(1) && this.GridAns[i, j] == '1') {
                        lenFilledCell++;
                        j++;
                    }
                    if (lenFilledCell > 0)
                        leftNumRows[i].Add(lenFilledCell);
                }
            }
        }
        void fillTopNumberColumns(char[,] levelGrid) {
            topNumCols = new List<int>[levelGrid.GetLength(1)];
            for (int i = 0; i < levelGrid.GetLength(1); i++) {
                topNumCols[i] = new List<int>();
                for (int j = 0; j < levelGrid.GetLength(0); j++) {
                    int lenFilledCell = 0;
                    while (j < levelGrid.GetLength(0) && GridAns[j, i] == '1') {
                        lenFilledCell++;
                        j++;
                    }
                    if (lenFilledCell > 0)
                        topNumCols[i].Add(lenFilledCell);
                }
            }
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
                    if (GridGame[i, j] == '1')
                        e.Graphics.FillRectangle(blackBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
        }
        Point click;

        private void GridPanel_MouseClick(object sender, MouseEventArgs e) {
            // TODO пока реагирует только на единичное нажатие, добавь ЗАЖАТИЕ
            // TODO2 левая и правая кнопка мыши работают одинаково, исправь
            
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

        private void GridPanel_MouseDown(object sender, MouseEventArgs e) {
            stop = false;

            click = new Point(e.X,e.Y);
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
            using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                string progressStr = "";
                foreach (char x in GridGame)
                    progressStr += x;
                command.CommandText = $"update nonogramlevels set Progress = '{progressStr}' where Name = '{this.levelName}'";
                command.ExecuteNonQuery();
            }
        }

        private void GridPanel_MouseMove(object sender, MouseEventArgs e) {
            click = new Point(e.X, e.Y);
        }
    }
}
