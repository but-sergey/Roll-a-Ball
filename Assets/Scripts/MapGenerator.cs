using GB.Structs;
using System;
using System.Collections.Generic;

namespace GB
{
    public class MapGenerator
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int GoodBonuses { get; private set; }
        public int BadBonuses { get; private set; }
        public genCell[,] Map;
        private System.Random rand = new System.Random();

        public MapGenerator(int Rows, int Cols, int GoodBonuses, int BadBonuses)
        {
            this.Rows = Rows;
            this.Cols = Cols;
            Map = (genCell[,]) Array.CreateInstance(typeof(genCell), new int[] { Rows, Cols });
            ClearMap(ref Map);
            RemoveWall(ref Map);
            GenerateGoodBouses(ref Map, GoodBonuses);
        }

        private void ClearMap(ref genCell[,] M)
        {
            for (var i = 0; i < M.GetLength(0); i++)
            {
                for (var j = 0; j < M.GetLength(1); j++)
                {
                    if ((i % 2 != 0 && j % 2 != 0) && (i < Rows - 1))
                    {
                        M[i, j].Value = 0;
                    }
                    else
                    {
                        M[i, j].Value = -1;
                    }
                    M[i, j].Row = i;
                    M[i, j].Col = j;
                    M[i, j].Visited = false;
                }
            }
        }

        private void RemoveWall(ref genCell[,] M)
        {
            genCell current = M[1, 1];
            current.Visited = true;

            Stack<genCell> stack = new Stack<genCell>();

            do
            {
                List<genCell> cells = new List<genCell>();

                int row = current.Row;
                int col = current.Col;

                if (row - 1 > 0 && !M[row - 2, col].Visited) cells.Add(M[row - 2, col]);
                if (col - 1 > 0 && !M[row, col - 2].Visited) cells.Add(M[row, col - 2]);

                if (row < Rows - 3 && !M[row + 2, col].Visited) cells.Add(M[row + 2, col]);
                if (col < Cols - 3 && !M[row, col + 2].Visited) cells.Add(M[row, col + 2]);

                if (cells.Count > 0)
                {
                    genCell selected = cells[rand.Next(cells.Count)];
                    RemoveCurrentWall(ref M, current, selected);

                    selected.Visited = true;
                    M[selected.Row, selected.Col].Visited = true;
                    stack.Push(selected);
                    current = selected;
                }
                else
                {
                    current = stack.Pop();
                }

            } while (stack.Count > 0);
        }

        private void RemoveCurrentWall(ref genCell[,] M, genCell current, genCell selected)
        {
            if (current.Row == selected.Row)
            {
                if (current.Col > selected.Col)
                {
                    M[current.Row, current.Col - 1].Value = 0;
                }
                else
                {
                    M[selected.Row, selected.Col - 1].Value = 0;
                }
            }
            else
            {
                if (current.Row > selected.Row)
                {
                    M[current.Row - 1, current.Col].Value = 0;
                }
                else
                {
                    M[selected.Row - 1, selected.Col].Value = 0;
                }
            }
        }

        private void GenerateGoodBouses(ref genCell[,] M, int count)
        {
            for (var i = 0; i < count; i++)
            {
                var x = rand.Next(M.GetLength(0));
                var y = rand.Next(M.GetLength(1));

                if (M[x, y].Value == 0)
                {
                    PlaceBonus(ref M, x, y, 1);
                    continue;
                }
                else
                {
                    var saved_x = x;
                    var saved_y = y;
                    do
                    {
                        y++;
                        if (y >= M.GetLength(1))
                        {
                            y = 0;
                            x++;
                            if (x >= M.GetLength(1))
                            {
                                x = 0;
                            }
                        }
                    } while ((M[x, y].Value != 0) || ((x == saved_x) && (y == saved_y)));

                    if (M[x, y].Value == 0)
                    {
                        PlaceBonus(ref M, x, y, 1);
                    }
                }
            }
        }

        private void PlaceBonus(ref genCell[,] M, int x, int y, int type)
        {
            M[x, y].Value = type;
        }
 
    }
}
