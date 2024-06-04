using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SQLite;


namespace nonograms {
    public class Level {
        public string Name;
        public int[,] levelGrid;
        public int[,] answerGrid;
        public List<int>[] topNumberColumns;
        public List<int>[] leftNumberRows;
        public class ColorList {

        }
        public Level() {
            // тестовый уровень
            Name = "Cup";
            answerGrid = new int[,]{
                {1,1,1,1,1,1,1,1,1,1},
                {1,0,1,1,0,0,1,1,0,1},
                {1,0,1,1,0,0,1,1,0,1},
                {1,0,1,1,0,0,1,1,0,1},
                {0,1,1,1,0,0,1,1,1,0},
                {0,0,0,1,1,1,1,0,0,0},
                {0,0,0,0,1,1,0,0,0,0},
                {0,0,0,0,1,1,0,0,0,0},
                {0,0,0,1,1,1,1,0,0,0},
                {0,1,1,1,1,1,1,1,1,0},
                {1,0,1,0,1,1,0,1,0,1}
            };
            levelGrid = new int[answerGrid.GetLength(0), answerGrid.GetLength(1)];
            for (int i = 0; i < levelGrid.GetLength(0); i++)
                for (int j = 0; j < levelGrid.GetLength(1); j++)
                    levelGrid[i, j] = 0;
            fillLeftNumberRows(answerGrid);
            fillTopNumberColumns(answerGrid);
        }
        void fillLeftNumberRows(int[,] levelGrid) {
            leftNumberRows = new List<int>[levelGrid.GetLength(0)];
            for (int i = 0;i < levelGrid.GetLength(0); i++) {
                leftNumberRows[i] = new List<int>();
                for (int j = 0;j < levelGrid.GetLength(1);j++) {
                    int lenFilledCell = 0;
                    while(j < levelGrid.GetLength(1) && answerGrid[i,j] == 1) {
                        lenFilledCell++;
                        j++;
                    }
                    if (lenFilledCell > 0)
                        leftNumberRows[i].Add(lenFilledCell);
                }
            }
        }
        void fillTopNumberColumns(int[,] levelGrid) {
            topNumberColumns = new List<int>[levelGrid.GetLength(1)];
            for (int i = 0; i < levelGrid.GetLength(1); i++) {
                topNumberColumns[i] = new List<int>();
                for (int j = 0; j < levelGrid.GetLength(0); j++) {
                    int lenFilledCell = 0;
                    while (j < levelGrid.GetLength(0) && answerGrid[j, i] == 1) {
                        lenFilledCell++;
                        j++;
                    }
                    if (lenFilledCell > 0)
                        topNumberColumns[i].Add(lenFilledCell);
                }
            }
        }
    }

    internal static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Level level = new Level();
            char[,] ansToChar = new char[level.answerGrid.GetLength(0), level.answerGrid.GetLength(1)];
            string ansStr = "";
            string levelStr = "";
            for (int i = 0; i < level.answerGrid.GetLength(0); i++) 
                for (int j = 0; j < level.answerGrid.GetLength(1); j++)
                    ansToChar[i,j] = Convert.ToChar(level.answerGrid[i, j]);
            foreach (var x in level.answerGrid)
                ansStr += x;
            foreach (var y in level.levelGrid)
                levelStr += y;

            using (var connection = new SQLiteConnection("Data Source=usersdata.db")) {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand();
                command.Connection = connection;
                command.CommandText = 
                    "CREATE TABLE IF NOT EXISTS nonogramlevels(" +
                    "_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
                    "Name TEXT NOT NULL, " +
                    "Height INTEGER NOT NULL, " +
                    "Width INTEGER NOT NULL, " +
                    "Answer TEXT NOT NULL, " +
                    "Progress TEXT NOT NULL" +
                    ")";
                command.ExecuteNonQuery();
                /* тестовый уровень
                command.CommandText = $"INSERT INTO nonogramlevels (Name, Height, Width, Answer, Progress) " +
                    $"VALUES ('{level.Name}', {level.answerGrid.GetLength(0)}, {level.answerGrid.GetLength(1)}, '{ansStr}', '{levelStr}')";
                command.ExecuteNonQuery();
                */
            }      
            Application.Run(new MainWindow(GetNames()));
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
    }
}
