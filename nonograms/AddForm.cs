using BackgroundRemovalSample.App;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
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
        List<string> colors = new List<string>();


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

            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            this.ColorBox.Items.Add("Черный");
            this.ColorBox.Items.Add("Красный");
            this.ColorBox.Items.Add("Оранжевый");
            this.ColorBox.Items.Add("Желтый");
            this.ColorBox.Items.Add("Зеленый");
            this.ColorBox.Items.Add("Голубой");
            this.ColorBox.Items.Add("Синий");
            this.ColorBox.Items.Add("Серый");
            this.ColorBox.SelectedIndex = ColorBox.FindStringExact("Черный");

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
                        case 'g':
                            SolidBrush purpleBrush = new SolidBrush(Color.Purple);
                            e.Graphics.FillRectangle(purpleBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'b':
                            SolidBrush brownBrush = new SolidBrush(Color.FromArgb(0xf1, 0xc2, 0x7d));
                            e.Graphics.FillRectangle(brownBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                        case 'Ф':
                            SolidBrush grayBrush = new SolidBrush(Color.Gray);
                            e.Graphics.FillRectangle(grayBrush, 1 + j * CellSize, 1 + i * CellSize, CellSize - 1, CellSize - 1);
                            break;
                    }
        }



        private void AddForm_Load(object sender, EventArgs e) {
            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(0, 130);

            this.Size = new Size(GridPanel.Width + 16, GridPanel.Location.Y + GridPanel.Height + 39);
            if (this.Size.Width <= 200)
                this.Size = new Size(200, Size.Height);
            if (this.Size.Height <= 300)
                this.Size = new Size(Size.Width, 300);

            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);
            DropBox.AllowDrop = true;


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
                    string color = ColorBox.SelectedItem.ToString();
                    drawing = new Thread(() => {
                        using (Graphics g = this.GridPanel.CreateGraphics()) {
                            SolidBrush blackBrush = new SolidBrush(ColorPanel.BackColor);
                            while (!stop) {
                                char colorChar = 'ч';
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
        }

        private void GridPanel_MouseMove(object sender, MouseEventArgs e) {
            click = new Point(e.X, e.Y);

        }

        private void NameBox_TextChanged(object sender, EventArgs e) {
            Name = NameBox.Text;
            Console.WriteLine(Name);
        }

        void SaveBtn_Click(object sender, EventArgs e) {
            if (GetNames().Contains(Name)) {
                MessageBox.Show("Название занято\nВыберети другое", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

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
            this.Close();
        }

        private void AddForm_FormClosed(object sender, FormClosedEventArgs e) {

        }

        private void numericH_ValueChanged(object sender, EventArgs e) {
            GridHeight = (int)numericH.Value;
            GridHeight = (int)numericH.Value;
            GridWidth = (int)numericW.Value;
            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(0, 130);

            this.Size = new Size(GridPanel.Width + 16, GridPanel.Location.Y + GridPanel.Height + 39);
            if (this.Size.Width <= 200)
                this.Size = new Size(200, Size.Height);
            if (this.Size.Height <= 300)
                this.Size = new Size(Size.Width, 300);

            //this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            char[,] NewGridGame;
            NewGridGame = new char[GridHeight, GridWidth];
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    NewGridGame[i, j] = '0';
            for (int i = 0; i < GridHeight && i < GridGame.GetLength(0); i++)
                for (int j = 0; j < GridWidth && j < GridGame.GetLength(1); j++)
                    NewGridGame[i, j] = GridGame[i, j];
            GridGame = NewGridGame;
        }
        private void numericW_ValueChanged(object sender, EventArgs e) {
            GridWidth = (int)numericW.Value;
            GridHeight = (int)numericH.Value;
            GridWidth = (int)numericW.Value;
            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(0, 130);

            this.Size = new Size(GridPanel.Width + 16, GridPanel.Location.Y + GridPanel.Height + 39);
            if (this.Size.Width <= 200)
                this.Size = new Size(200, Size.Height);
            if (this.Size.Height <= 300)
                this.Size = new Size(Size.Width, 300);

            //this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            char[,] NewGridGame;
            NewGridGame = new char[GridHeight, GridWidth];
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    NewGridGame[i, j] = '0';
            for (int i = 0; i < GridHeight && i < GridGame.GetLength(0); i++)
                for (int j = 0; j < GridWidth && j < GridGame.GetLength(1); j++)
                    NewGridGame[i, j] = GridGame[i, j];
            GridGame = NewGridGame;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
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
                case "Серый":
                    c = Color.FromName("LightGray");
                    break;

                default:
                    break;
            }
            ColorPanel.BackColor = c;

        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e) { 
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
                    case "Серый":
                        c = Color.FromName("LightGray");
                        break;
                }

                Brush b = new SolidBrush(c);
                g.DrawString(nrus, f, Brushes.Black, rect.X + 15, rect.Top);
                g.FillRectangle(b, rect.X, rect.Y, 15, 15);
            }
        }

        private void ReloadGridBtn_Click(object sender, EventArgs e) {
            GridHeight = (int)numericH.Value;
            GridWidth = (int)numericW.Value;
            this.GridPanel.Size = new Size(this.GridWidth * CellSize + 1, this.GridHeight * CellSize + 1);
            this.GridPanel.Location = new Point(0, 130);

            this.Size = new Size(GridPanel.Width + 16, GridPanel.Location.Y + GridPanel.Height + 39);
            if (this.Size.Width <= 200)
                this.Size = new Size(200, Size.Height);
            if (this.Size.Height <= 300)
                this.Size = new Size(Size.Width, 300);

            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - this.Height) / 2);

            char[,] NewGridGame;
            NewGridGame = new char[GridHeight, GridWidth];
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    NewGridGame[i, j] = '0';
            for (int i = 0; i < GridHeight && i < GridGame.GetLength(0); i++)
                for (int j = 0; j < GridWidth && j < GridGame.GetLength(1); j++)
                    NewGridGame[i, j] = GridGame[i, j];
            GridGame = NewGridGame;
            this.Refresh();

        }
        static List<string> GetNames() {
            List<string> Names = new List<string>();
            string sqlExpression = "SELECT Name FROM nonogramlevels";
            using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            string name = (string)reader.GetValue(0);
                            Names.Add(name);
                        }
                    }
                }
            }
            return Names;
        }

        private void DropBox_DragDrop(object sender, DragEventArgs e)
        {
            var image = e.Data.GetData(DataFormats.FileDrop);
            if (image != null)
            {
                var fileNames = image as string[];
                if (fileNames.Length > 0)
                {
                    this.DropBox.Image = Image.FromFile(fileNames[0]);
                    var filter = new RemoveBackgroundOpenCvFilter();
                    var res = filter.Apply(OpenCvSharp.Extensions.BitmapConverter.ToMat(new Bitmap(this.DropBox.Image)));
                    this.DropBox.Image = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(res);

                    Bitmap bitmap = new Bitmap(ResizeImage(this.DropBox.Image, (int)this.numericW.Value, (int)this.numericH.Value));

                    var clrdic = new Dictionary<char, Color>()
                    {
                        { 'К', Color.Red },
                        { 'О', Color.Orange },
                        { 'Ж', Color.Yellow },
                        { 'З', Color.Green },
                        { 'Г', Color.Cyan },
                        { 'С', Color.Blue },
                        { 'Ч', Color.Black },
                        //{ 'Ф', Color.Purple },
                        //{ 'b', Color.Brown },
                        { 'Ф', Color.LightGray },
                    };

                    for (int i = 0; i < (int)this.numericW.Value; i++) {
                        for (int j = 0; j < (int)this.numericH.Value; j++) {
                            var pixel = bitmap.GetPixel(i, j);
                            if (pixel.A >= 50)
                            {
                                var nearclr = '0';
                                double dist = 100000000.0f;
                                foreach (var color in clrdic)
                                {
                                    var dist_x = Math.Pow(pixel.R - color.Value.R, 2) + Math.Pow(pixel.G - color.Value.G, 2) + Math.Pow(pixel.B - color.Value.B, 2);
                                    if (dist_x < dist)
                                    {
                                        dist = (dist_x);
                                        nearclr = color.Key;
                                        if (Color.FromArgb(color.Key) == Color.LightGray) nearclr = 'Ч';
                                    }
                                }
                                GridGame[j, i] = nearclr;

                                //int bporog = 8;
                                //var aver = (pixel.R + pixel.G + pixel.B) / 3;
                                //if (Math.Abs(aver - pixel.R) < bporog && Math.Abs(aver - pixel.G) < bporog && Math.Abs(aver - pixel.B) < bporog)
                                //    GridGame[j, i] = 'Ч';
                            }
                            if (pixel.R < 10 && pixel.G < 10 && pixel.B < 10)
                                GridGame[j, i] = '0';
                        }
                    }
                    this.Refresh();
                    //this.PictureBox.
                }
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void DropBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;

        }

        private void DropBox_DragDrop_1(object sender, DragEventArgs e)
        {

        }
    }
}

