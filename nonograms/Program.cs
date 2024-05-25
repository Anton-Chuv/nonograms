using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nonograms {
    public class Level {
        string Name;
        int[,] levelGrid;
        int[,] answerGrid;
        List<int>[] upNumberColumns;
        List<int>[] leftNumberRows;
        public int[,] getLevelGrid() { return answerGrid; } // TODO change to levelGrid
        public string getName() { return Name; }
        public List<int>[] getLeftNumRows() { return leftNumberRows; }
        public Level() {
            // тестовый уровень
            Name = "Cup";
            answerGrid = new int[,]{
                { 1,1,1,1,1,1,1,1,1,1},
                { 1,0,1,1,0,0,1,1,0,0},
                { 1,0,1,1,0,0,1,1,0,1},
                { 1,0,1,1,0,0,1,1,0,1},
                { 0,1,1,1,0,0,1,1,1,0},
                { 0,0,0,1,1,1,1,0,0,0},
                { 0,0,0,0,1,1,0,0,0,0},
                { 0,0,0,0,1,1,0,0,0,0},
                { 0,0,0,1,1,1,1,0,0,0},
                { 0,0,1,1,1,1,1,1,0,0},
                { 0,1,1,1,1,1,1,1,1,0},
            };
            levelGrid = new int[answerGrid.GetLength(0), answerGrid.GetLength(1)];
            for (int i = 0; i < levelGrid.GetLength(0); i++)
                for (int j = 0; j < levelGrid.GetLength(1); j++)
                    levelGrid[i, j] = 0;
            fillLeftNumberRows(answerGrid);
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
    }

    internal static class Program {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new MainWindow());
            var currentLevel = new Level();
            Application.Run(new Playground(currentLevel));

        }
    }
}
