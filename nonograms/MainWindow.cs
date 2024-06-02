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


namespace nonograms {
    public partial class MainWindow : Form {
        public MainWindow(List<string> Names) {
            InitializeComponent();
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
            List<Label> labels = new List<Label>();
            List<Button> buttons = new List<Button>();
            foreach (string name in Names) {
                Label label = new Label();
                label.AutoSize = true;
                labels.Add(label);
                label.Text = name;
                label.BackColor = Color.Transparent;
                label.Font = new Font(label.Font.FontFamily, 20, FontStyle.Regular);
                label.Location = new Point(20, i * 80 + 15);
                topPanel.Controls.Add(label);

                Button btn = new Button();
                btn.Text = "Запуск";
                btn.BackColor = Color.Green;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatStyle = FlatStyle.Flat;
                btn.AutoSize = true;
                buttons.Add(btn);
                btn.Location = new Point(25, i * 80 + 55);
                btn.Click += (object s, EventArgs ev) => {
                    this.Hide();
                    Playground levelform = new Playground(name);
                    levelform.FormClosed += (object o, FormClosedEventArgs e) => { this.Show(); };
                    levelform.Show();
                };
                topPanel.Controls.Add(btn);

                Panel space = new Panel();
                space.Location = new Point(0, i * 80 + 100);
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
    }
}
