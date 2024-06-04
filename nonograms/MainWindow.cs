using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nonograms {

    public partial class MainWindow : Form {
        List<Button> buttons = new List<Button>();
        List<Label> labels = new List<Label>();
        public MainWindow(List<string> Names) {
            InitializeComponent();
            this.SetStyle(ControlStyles.Selectable, false);

            AddCards(Names);

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

        private void AddLevelBtn_Click(object sender, EventArgs e) {
            this.Hide();
            AddForm addForm = new AddForm();
            addForm.FormClosed += (object s, FormClosedEventArgs ev) => { this.Show(); AddCards(GetNames()); };
            addForm.Show();

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
        private void AddCards(List<string> Names) {
            int i = 0;
            foreach (var bt in buttons) {
                topPanel.Controls.Remove(bt);
                bt.Dispose();
            }
            foreach (var lb in labels) {
                topPanel.Controls.Remove(lb);
                lb.Dispose();
            }
            buttons.Clear();
            labels.Clear();


            foreach (string name in Names) {
                Label label = new Label();
                label.AutoSize = true;
                label.Text = name;
                label.BackColor = Color.Transparent;
                label.Font = new Font(label.Font.FontFamily, 20, FontStyle.Regular);
                label.Location = new Point(20, i * 80 + 15 + 40);
                topPanel.Controls.Add(label);
                labels.Add(label);

                Button btn = new Button();
                btn.Text = "Запуск";
                btn.BackColor = Color.Green;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.AutoSize = true;
                btn.Location = new Point(25, i * 80 + 55 + 40);
                btn.Click += (object s, EventArgs ev) => {
                    this.Hide();
                    Playground levelform = new Playground(name);
                    levelform.FormClosed += (object o, FormClosedEventArgs e) => { this.Show(); };
                    levelform.Show();
                };
                topPanel.Controls.Add(btn);
                buttons.Add(btn);

                Button btnRst = new Button();
                btnRst.Text = "Сбросить";
                btnRst.BackColor = Color.LightBlue;
                btnRst.FlatAppearance.BorderSize = 0;
                btnRst.FlatStyle = FlatStyle.Flat;
                btnRst.AutoSize = true;
                btnRst.Location = new Point(110, i * 80 + 55 + 40);
                btnRst.Click += (object s, EventArgs ev) => {
                    string sqlExpression = $"SELECT * FROM nonogramlevels where name = '{name}'";
                    int len = 0;
                    using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                        connection.Open();
                        SQLiteCommand command = new SQLiteCommand(sqlExpression, connection);
                        using (SQLiteDataReader reader = command.ExecuteReader()) {
                            if (reader.HasRows) {
                                reader.Read();
                                //string name = (string)reader.GetValue(0);
                                int height = reader.GetInt32(1);
                                var width = reader.GetInt32(2);
                                len = height * width;
                            }
                        }
                        string progressStr = "";
                        for (int j = 0; j < len; j++)
                            progressStr += '0';
                        command.CommandText = $"update nonogramlevels set Progress = '{progressStr}' where Name = '{name}'";
                        command.ExecuteNonQuery();
                    }
                    AddCards(GetNames());
                };
                topPanel.Controls.Add(btnRst);
                buttons.Add(btnRst);

                Button btnDlt = new Button();
                btnDlt.Text = "Удалить";
                btnDlt.BackColor = Color.Pink;
                btnDlt.FlatAppearance.BorderSize = 0;
                btnDlt.FlatStyle = FlatStyle.Flat;
                btnDlt.AutoSize = true;
                btnDlt.Location = new Point(195, i * 80 + 55 + 40);
                btnDlt.Click += (object s, EventArgs ev) => {
                    using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                        connection.Open();
                        SQLiteCommand command = new SQLiteCommand();
                        command.Connection = connection;

                        command.CommandText = $"delete from nonogramlevels where Name = '{name}'";
                        command.ExecuteNonQuery();
                    }
                    AddCards(GetNames());
                };
                topPanel.Controls.Add(btnDlt);
                buttons.Add(btnDlt);

                Panel space = new Panel();
                space.Location = new Point(0, i * 80 + 100 + 40);
                space.Size = new Size(1, 1);
                topPanel.Controls.Add(space);

                i++;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e) {
            topPanel.AutoScroll = true;

        }
        Point last;
        private void HeadPanel_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                this.Left += e.X - last.X;
                this.Top += e.Y - last.Y;
            }
        }

        private void HeadPanel_MouseDown(object sender, MouseEventArgs e) {
            last = new Point(e.X, e.Y);
        }

        private void CloseButton_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void label1_Click(object sender, EventArgs e) {

        }
    }
    public class RoundButton : Button {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e) {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}
