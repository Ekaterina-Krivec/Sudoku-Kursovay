using System;

namespace Судоку
{
    [Serializable()]
    public class save
    {
        public int[][] grid; //полное поле
        public int[][] gridGame; //поле значение, которые ввел игрок 
        public bool[][] fillType; //маска (пустые значения)
        public int hide;
        public int level;

        public save()
        {
            
        }

        public save(int[,] Gridd, int[,] GriddGame, bool[,] FilllType, int Hide, int Level)
        {
            grid = new int[9][];
            gridGame = new int[9][];
            fillType = new bool[9][];

            for (int i = 0; i < 9; i++)
            {
                grid[i] = new int[9];
                gridGame[i] = new int[9];
                fillType[i] = new bool[9];
                for (int j = 0; j < 9; j++)
                {
                    grid[i][j] = Gridd[i, j];
                    gridGame[i][j] = GriddGame[i, j];
                    fillType[i][j] = FilllType[i, j];
                }
            }
            hide = Hide;
            level = Level;
        }

        public int[][] Grid
        {
            set
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        grid[i][j] = value[i][j];
                    }
                }
            }
            get
            {
                return grid;
            }
        }
        public int[][] GridGame
        {
            set
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        gridGame[i][j] = value[i][j];
                    }
                }
            }
            get
            {
                return gridGame;
            }
        }
        public bool[][] FillType
        {
            set
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        fillType[i][j] = value[i][j];
                    }
                }
            }
            get
            {
                return fillType;
            }
        }
    }
}
