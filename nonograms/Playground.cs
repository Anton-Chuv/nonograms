using System;
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
using System.Xml.Linq;
using System.Diagnostics;



namespace nonograms {


    public partial class Playground : Form {
        int GridHeight;
        int GridWidth;
        char[,] GridGame;
        char[,] GridAns;
        string levelName;
        List<char> usedColors;

        List<(int len, char color)>[] leftNumRows;
        List<(int len, char color)>[] topNumCols;
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

            //this.leftNumRows.Item = level.leftNumberRows;
            this.maxLeftRowLen = 0;
            for (int i = 0; i < leftNumRows.Length; i++)
                if (leftNumRows[i].Count > maxLeftRowLen)
                    maxLeftRowLen = leftNumRows[i].Count;
            this.LeftPanel.Width = maxLeftRowLen; //                             (если собираешся испольсозовать правый верхний угол, чтобы он не был слишком маленьким))

            //this.topNumCols = level.topNumberColumns;
            this.maxTopColLen = 0;
            for (int i = 0; i < topNumCols.Length; i++)
                if (topNumCols[i].Count > maxTopColLen)
                    maxTopColLen = topNumCols[i].Count;
            this.TopPanel.Height = maxTopColLen; // нужен ли минимальный размер (наверно стоит сделать минимальный размер на всякий случий \
            if (maxTopColLen < 3)
                maxTopColLen = 3;
            if (maxLeftRowLen < 3) 
                maxLeftRowLen = 3;
            // this.Size = new System.Drawing.Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            
        }

        public Playground(String name) {
            InitializeComponent();
            usedColors = new List<char>();
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
            if (maxLeftRowLen < 3)
                maxLeftRowLen = 3;
            this.LeftPanel.Width = maxLeftRowLen; //                             (если собираешся испольсозовать правый верхний угол, чтобы он не был слишком маленьким))

            this.maxTopColLen = 0;
            for (int i = 0; i < topNumCols.Length; i++)
                if (topNumCols[i].Count > maxTopColLen)
                    maxTopColLen = topNumCols[i].Count;
            if (maxTopColLen < 3)
                maxTopColLen = 3;
            this.TopPanel.Height = maxTopColLen; // нужен ли минимальный размер (наверно стоит сделать минимальный размер на всякий случий \
            if (maxTopColLen < 3)
                maxTopColLen = 3;
            if (usedColors.Contains('Ч') || usedColors.Contains('1'))
                this.ColorBox.Items.Add("Черный");
            if (usedColors.Contains('К'))
                this.ColorBox.Items.Add("Красный");
            if (usedColors.Contains('О'))
                this.ColorBox.Items.Add("Оранжевый");
            if (usedColors.Contains('Ж'))
                this.ColorBox.Items.Add("Желтый");
            if (usedColors.Contains('З'))
                this.ColorBox.Items.Add("Зеленый");
            if (usedColors.Contains('Г'))
                this.ColorBox.Items.Add("Голубой");
            if (usedColors.Contains('С'))
                this.ColorBox.Items.Add("Синий");
            if (usedColors.Contains('Ф'))
                this.ColorBox.Items.Add("Фиолетовый");
            this.ColorBox.SelectedIndex = 0;
            if (this.ColorBox.Items.Count == 1)
                this.ColorBox.Hide();
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

            this.ColorPanel.Location = new Point(LeftPanel.Width - 25-ColorBox.Width, TopPanel.Height - 25);
            this.ColorBox.Location = new Point(LeftPanel.Width - 25, TopPanel.Height - 25);
        }

        void fillLeftNumberRows(char[,] levelGrid) {
            leftNumRows = new List<(int, char)>[levelGrid.GetLength(0)];
            for (int i = 0; i < levelGrid.GetLength(0); i++) {
                leftNumRows[i] = new List<(int, char)>();
                for (int j = 0; j < levelGrid.GetLength(1);) {
                    char c = '0';
                    int lenFilledCell = 0;
                    if (this.GridAns[i, j] != '0') {
                        c = GridAns[i, j];
                        lenFilledCell++;
                        j++;
                        while (j < levelGrid.GetLength(1) && this.GridAns[i, j] == c) {
                            c = GridAns[i, j];
                            lenFilledCell++;
                            j++;
                        }

                    }
                    else
                        j++;
                    if (lenFilledCell > 0) {
                        if (!usedColors.Contains(c))
                            usedColors.Add(c);
                        leftNumRows[i].Add((lenFilledCell, c));
                    }
                }
            }
        }
        void fillTopNumberColumns(char[,] levelGrid) {
            topNumCols = new List<(int, char)>[levelGrid.GetLength(1)];
            for (int i = 0; i < levelGrid.GetLength(1); i++) {
                topNumCols[i] = new List<(int, char)>();
                for (int j = 0; j < levelGrid.GetLength(0);) {
                    char c = '0';
                    int lenFilledCell = 0;
                    if (this.GridAns[j, i] != '0') {
                        c = GridAns[j, i];
                        while (j < levelGrid.GetLength(0) && this.GridAns[j, i] == c) {
                            lenFilledCell++;
                            j++;
                        }
                    }
                    else
                        j++;
                    if (lenFilledCell > 0)
                        topNumCols[i].Add((lenFilledCell, c));
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

            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    switch (GridGame[i, j]) {
                        case '1':
                        case 'Ч':
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            e.Graphics.FillRectangle(blackBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'С':
                            SolidBrush blueBrush = new SolidBrush(Color.Blue);
                            e.Graphics.FillRectangle(blueBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'З':
                            SolidBrush greenBrush = new SolidBrush(Color.Green);
                            e.Graphics.FillRectangle(greenBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'К':
                            SolidBrush redBrush = new SolidBrush(Color.Red);
                            e.Graphics.FillRectangle(redBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'Ж':
                            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
                            e.Graphics.FillRectangle(yellowBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'О':
                            SolidBrush orangeBrush = new SolidBrush(Color.Orange);
                            e.Graphics.FillRectangle(orangeBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'Г':
                            SolidBrush cyanBrush = new SolidBrush(Color.Cyan);
                            e.Graphics.FillRectangle(cyanBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'Ф':
                            SolidBrush purpleBrush = new SolidBrush(Color.Purple);
                            e.Graphics.FillRectangle(purpleBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                    }

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

                    switch (topNumCols[i][j].color) {
                        case '1':
                        case 'Ч':
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            e.Graphics.FillRectangle(blackBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'С':
                            SolidBrush blueBrush = new SolidBrush(Color.Blue);
                            e.Graphics.FillRectangle(blueBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'З':
                            SolidBrush greenBrush = new SolidBrush(Color.Green);
                            e.Graphics.FillRectangle(greenBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'К':
                            SolidBrush redBrush = new SolidBrush(Color.Red);
                            e.Graphics.FillRectangle(redBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'Ж':
                            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
                            e.Graphics.FillRectangle(yellowBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.Black), p, sf);
                            break;
                        case 'Ф':
                            SolidBrush purpleBrush = new SolidBrush(Color.Purple);
                            e.Graphics.FillRectangle(purpleBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'Г':
                            SolidBrush cyanBrush = new SolidBrush(Color.Cyan);
                            e.Graphics.FillRectangle(cyanBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.Black), p, sf);
                            break;
                        case 'О':
                            SolidBrush orangeBrush = new SolidBrush(Color.Orange);
                            e.Graphics.FillRectangle(orangeBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(topNumCols[i][j].len.ToString(), Font, new SolidBrush(Color.Black), p, sf);
                            break;
                    }
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
                    switch (leftNumRows[i][j].color) {
                        case '1':
                        case 'Ч':
                            SolidBrush blackBrush = new SolidBrush(Color.Black);
                            e.Graphics.FillRectangle(blackBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'С':
                            SolidBrush blueBrush = new SolidBrush(Color.Blue);
                            e.Graphics.FillRectangle(blueBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'З':
                            SolidBrush greenBrush = new SolidBrush(Color.Green);
                            e.Graphics.FillRectangle(greenBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize/2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'К':
                            SolidBrush redBrush = new SolidBrush(Color.Red);
                            e.Graphics.FillRectangle(redBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'Ж':
                            SolidBrush yellowBrush = new SolidBrush(Color.Yellow);
                            e.Graphics.FillRectangle(yellowBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.Black), p, sf);
                            break;
                        case 'Ф':
                            SolidBrush purpleBrush = new SolidBrush(Color.Purple);
                            e.Graphics.FillRectangle(purpleBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.White), p, sf);
                            break;
                        case 'Г':
                            SolidBrush cyanBrush = new SolidBrush(Color.Cyan);
                            e.Graphics.FillRectangle(cyanBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.Black), p, sf);
                            break;
                        case 'О':
                            SolidBrush orangeBrush = new SolidBrush(Color.Orange);
                            e.Graphics.FillRectangle(orangeBrush, 1 + p.X - CellSize / 2, 1 + p.Y - CellSize / 2, CellSize - 1, CellSize - 1);
                            e.Graphics.DrawString(leftNumRows[i][j].len.ToString(), Font, new SolidBrush(Color.Black), p, sf);
                            break;
                    }
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
                    string color = ColorBox.SelectedItem.ToString();
                    drawing = new Thread(() => {
                        using (Graphics g = this.GridPanel.CreateGraphics()) {
                            
                            SolidBrush blackBrush = new SolidBrush(ColorPanel.BackColor);
                            while (!stop) {
                                char colorChar = '0';
                                colorChar = color[0];
                                try { GridGame[click.Y / 20, click.X / 20] = colorChar; } catch { }
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
            string sqlExpression = $"SELECT * FROM nonogramlevels where name = '{this.levelName}'";
            using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    if (reader.HasRows) {
                        reader.Read();
                        //string name = (string)reader.GetValue(0);
                        string answer = (string)reader.GetValue(3);
                        string progress = (string)reader.GetValue(4);
                        if (String.Compare(answer, progress) == 0) {
                            Console.WriteLine(GridAns.ToString());
                            Console.WriteLine(GridGame.ToString());

                            Console.WriteLine("asdf");
                            MessageBox.Show("Уровень решен", "", MessageBoxButtons.OK);
                        }
                    }
                }
            }
        }

        private void GridPanel_MouseMove(object sender, MouseEventArgs e) {
            click = new Point(e.X, e.Y);
        }

        private void ColorBox_SelectedIndexChanged(object sender, EventArgs e) {
            string color = this.ColorBox.SelectedItem.ToString();
            Color c = Color.FromName("Black");
            switch (color) {
                case "Черный":
                    c = Color.FromName("Black");
                    break;
                case "Синий":
                    c = Color.FromName("Blue");
                    break;
                case "Красный":
                    c = Color.FromName("Red");
                    break;
                case "Зеленый":
                    c = Color.FromName("Green");
                    break;
                case "Желтый":
                    c = Color.FromName("Yellow");
                    break;
                case "Оранжевый":
                    c = Color.FromName("Orange");
                    break;
                case "Голубой":
                    c = Color.FromName("Cyan");
                    break;
                case "Фиолетовый":
                    c = Color.FromName("Purple");
                    break;

                default:
                    break;
            }
            ColorPanel.BackColor = c;

        }

        private void ColorBox_DrawItem(object sender, DrawItemEventArgs e) {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0) {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font(Font.FontFamily, 9, FontStyle.Regular);
                string nrus = n;
                Color c = Color.FromName("Black");
                switch (n) {
                    case "Черный":
                        c = Color.FromName("Black");
                        break;
                    case "Красный":
                        c = Color.FromName("Red");
                        break;
                    case "Оранжевый":
                        c = Color.FromName("Orange");
                        break;
                    case "Желтый":
                        c = Color.FromName("Yellow");
                        break;
                    case "Зеленый":
                        c = Color.FromName("Green");
                        break;
                    case "Голубой":
                        c = Color.FromName("Cyan");
                        break;
                    case "Синий":
                        c = Color.FromName("Blue");
                        break;
                    case "Фиолетовый":
                        c = Color.FromName("Purple");
                        break;
                }

                Brush b = new SolidBrush(c);
                g.DrawString(nrus, f, Brushes.Black, rect.X + 15, rect.Top);
                g.FillRectangle(b, rect.X, rect.Y, 15, 15);
            }
        }

        private void BackButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
