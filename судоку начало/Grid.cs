using System;
using System.Drawing;
using System.Windows.Forms;

namespace Судоку
{
    public class Grid
    {
        public int[,] grid = new int[9, 9];
        Random rnd = new Random();

        public int[,] gridGame = new int[9, 9];
        public bool[,] fillType = new bool[9, 9];

        public int getGridij(int i, int j)
        {
            return this.grid[i, j];
        }
        public int getGridGameij(int i, int j)
        {
            return this.gridGame[i, j];
        }
        public bool getfillTypeij(int i, int j)
        {
            return this.fillType[i, j];
        }
        //синхронное ведение грида для сохранения игры
        internal void setGridGameij(int x, int i, int j)
        {
            gridGame[i, j] = x;
        }
        internal void setFillTypeij(int rX, int rY, bool type = true)
        {
            fillType[rX, rY] = type;
        }
       

        public Grid()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
        }

        //конструктор для того, чтобы загрузить поле с файла
        public Grid(int[][] Gridd, int[][] GriddGame, bool[][] FilllType)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    grid[i, j] = Gridd[i][j];
                    gridGame[i, j] = GriddGame[i][j];
                    fillType[i, j] = FilllType[i][j];
                }
            }
        }


        public bool Shaffl(int shuffleLevel)
        {
            Update(shuffleLevel);
            Transposition();
            Update(shuffleLevel);
            SwapBlocksInColumn();
            Update(shuffleLevel);
            SwapBlocksInRow();
            Update(shuffleLevel);
            SwapColumnsInBlock();
            Update(shuffleLevel);
            SwapRowsInBlock();
            Update(shuffleLevel);
            return true;
        }

        public bool ChangeTwoCell(int findValue1, int findValue2)
        {
            int xParm1, yParm1, xParm2, yParm2;
            xParm1 = yParm1 = xParm2 = yParm2 = 0;
            for (int i = 0; i < 9; i += 3)
            {
                for (int k = 0; k < 9; k += 3)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            if (grid[i + j, k + z] == findValue1)
                            {
                                xParm1 = i + j;
                                yParm1 = k + z;
                            }
                            if (grid[i + j, k + z] == findValue2)
                            {
                                xParm2 = i + j;
                                yParm2 = k + z;
                            }
                        }
                    }
                    grid[xParm1, yParm1] = findValue2;
                    grid[xParm2, yParm2] = findValue1;
                }
            }
            return true;
        }

        public bool Update(int shuffleLevel)
        {
            for (int repeat = 0; repeat < shuffleLevel; repeat++)
            {
                Random rand = new Random(Guid.NewGuid().GetHashCode());
                Random rand2 = new Random(Guid.NewGuid().GetHashCode());
                ChangeTwoCell(rand.Next(1, 10), rand2.Next(1, 10));
            }
            return true;
        }

        public void Transposition()
        {
            int[,] tGrid = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    tGrid[i, j] = grid[j, i];
                }
            }
            grid = tGrid;
        }

        public void SwapBlocksInColumn()
        {
            var block1 = rnd.Next(0, 3);
            var block2 = rnd.Next(0, 3);
            while (block1 == block2)
                block2 = rnd.Next(0, 3);
            block1 *= 3;
            block2 *= 3;
            for (int i = 0; i < 9; i++)
            {
                var k = block2;
                for (int j = block1; j < block1 + 3; j++)
                {
                    var temp = grid[i, j];
                    grid[i, j] = grid[i, k];
                    grid[i, k] = temp;
                    k++;
                }
            }
        }

        public void SwapBlocksInRow()
        {
            var block1 = rnd.Next(0, 3);
            var block2 = rnd.Next(0, 3);
            while (block1 == block2)
                block2 = rnd.Next(0, 3);
            block1 *= 3;
            block2 *= 3;
            for (int i = 0; i < 9; i++)
            {
                var k = block2;
                for (int j = block1; j < block1 + 3; j++)
                {
                    var temp = grid[j, i];
                    grid[j, i] = grid[k, i];
                    grid[k, i] = temp;
                    k++;
                }
            }
        }

        public void SwapRowsInBlock()
        {
            var block = rnd.Next(0, 3);
            var row1 = rnd.Next(0, 3);
            var line1 = block * 3 + row1;
            var row2 = rnd.Next(0, 3);
            while (row1 == row2)
                row2 = rnd.Next(0, 3);
            var line2 = block * 3 + row2;
            for (int i = 0; i < 9; i++)
            {
                var temp = grid[line1, i];
                grid[line1, i] = grid[line2, i];
                grid[line2, i] = temp;
            }
        }

        public void SwapColumnsInBlock()
        {
            var block = rnd.Next(0, 3);
            var row1 = rnd.Next(0, 3);
            var line1 = block * 3 + row1;
            var row2 = rnd.Next(0, 3);
            while (row1 == row2)
                row2 = rnd.Next(0, 3);
            var line2 = block * 3 + row2;
            for (int i = 0; i < 9; i++)
            {
                var temp = grid[i, line1];
                grid[i, line1] = grid[i, line2];
                grid[i, line2] = temp;
            }
        }

        public void ProverkaGrid(Label[,] lib)
        {
            int count = 0;
            int countEMPTY = 0;

            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    if (lib[i, j].Text == "")
                    {
                        countEMPTY++;

                    }
                    else
                    {
                        if (lib[i, j].ForeColor == Color.Blue || lib[i, j].ForeColor == Color.Red)
                        {
                            if (lib[i, j].Text != Convert.ToString(grid[i, j]))
                            {
                                lib[i, j].ForeColor = Color.Red;
                                count++;
                            }
                            else
                            {
                                lib[i, j].ForeColor = Color.Green;
                            }
                        }
                    }
                }
            }
            if (count > 0 && countEMPTY > 0)
            {
                MessageBox.Show("Неправильные: " + count + "\nНезаполненные: " + countEMPTY, "Обнаружены ошибки");
                return;
            }
            else if (count == 0 && countEMPTY > 0)
            {
                MessageBox.Show("\nНезаполненные: " + countEMPTY, "Обнаружены ошибки");
                return;
            }
            else if (count > 0 && countEMPTY == 0)
            {
                MessageBox.Show("Неправильные: " + count, "Обнаружены ошибки");
                return;
            }
            else
            {
                MessageBox.Show("Судоку решена верно!");
                return;
            }
        }

        public void HelpCheck(Label lib)
        { 
            string x = lib.Name[lib.Name.Length - 2].ToString();
            int row = Convert.ToInt32(x) - 1;
            x = lib.Name[lib.Name.Length - 1].ToString();
            int col = Convert.ToInt32(x) - 1;
            lib.Text = Convert.ToString(grid[row, col]);
            setFillTypeij(row, col, false);
            lib.ForeColor = Color.Black;
        }
    }
}
