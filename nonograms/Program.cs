using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nonograms {
    public class Level {
        int[,] testSample = {
            {1,1,1,1,1,1,1,1,1,1},
            {1,0,1,1,0,0,1,1,0,1},
            {1,0,1,1,0,0,1,1,0,1},
            {1,0,1,1,0,0,1,1,0,1},
            {0,1,1,1,0,0,1,1,1,0},
            {0,0,0,1,1,1,1,0,0,0},
            {0,0,0,0,1,1,0,0,0,0},
            {0,0,0,0,1,1,0,0,0,0},
            {0,0,0,1,1,1,1,0,0,0},
            {0,0,1,1,1,1,1,1,0,0},
            {0,1,1,1,1,1,1,1,1,0},
        };
        int[] upNumberColumn;
        int[] leftNumberRow;
        public int[,] getLevelGrid() {
            return testSample;
        }
        public Level() {

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
