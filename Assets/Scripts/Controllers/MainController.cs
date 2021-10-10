using System.Collections.Generic;
using UnityEngine;


namespace GB.Controllers
{
    public class MainController : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _wallPrefab;
        [SerializeField] private GameObject _floorPrefab;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _goodBonusPrefab;

        private GameObject _floor;
        private GameObject _player;
        private readonly float _planeScale = 10.0f;

        #endregion


        #region UnityMethods

        private void Start()
        {
            GenerateMap();

            CreateFloor();

            GenerateLevel(Program.map);

            CreatePlayer();
        }

        #endregion


        #region Methods

        private void GenerateMap()
        {
            Program.ClearMap(ref Program.map);
            Program.RemoveWall(ref Program.map);
            Program.GenerateGoodBouses(ref Program.map, 25);
        }

        private void CreateFloor()
        {
            _floor = Instantiate(_floorPrefab, Vector3.zero, Quaternion.identity);
            SetFloorDimensions(_floor, _wallPrefab.transform.localScale, Program.map.GetLength(0), Program.map.GetLength(1));
        }

        private void CreatePlayer()
        {
            float x = _wallPrefab.transform.localScale.x;
            float y = 1.0f;
            float z = _wallPrefab.transform.localScale.z;
            _player = Instantiate(_playerPrefab, new Vector3(x, y, z), Quaternion.identity);
        }

        private void SetFloorDimensions(GameObject floor, Vector3 wallScale, int height, int width)
        {
            float x = wallScale.x * height / _planeScale;
            float y = 1.0f;
            float z = wallScale.z * width / _planeScale;
            floor.transform.localScale = new Vector3(x, y, z);

            x = 0.5f * (_planeScale * floor.transform.localScale.x - wallScale.x);
            y = 0.0f;
            z = 0.5f * (_planeScale * floor.transform.localScale.z - wallScale.z);
            floor.transform.position = new Vector3(x, y, z);
        }

        private void GenerateLevel(genCell[,] map)
        {
            float x = 0.0f;
            float y = 0.5f * _wallPrefab.transform.localScale.y;
            float z = 0.0f;

            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    x = i * _wallPrefab.transform.localScale.x;
                    z = j * _wallPrefab.transform.localScale.z;

                    switch (map[i, j].Value)
                    {
                        case -1:
                            Instantiate(_wallPrefab, new Vector3(x, y, z), Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(_goodBonusPrefab, new Vector3(x, y, z), Quaternion.identity);
                            break;
                    }
                }
            }
        }

        #endregion


        #region Govnokod

        struct genCell
        {
            public int Row { get; set; }
            public int Col { get; set; }
            public bool Visited { get; set; }
            public int Value { get; set; }
        }

        static class Program
        {
            static public int Rows = 49;
            static public int Cols = 49;
            static public System.Random rand = new System.Random();

            static public genCell[,] map = new genCell[Rows, Cols];

            static public void ClearMap(ref genCell[,] M)
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

            static public void RemoveWall(ref genCell[,] M)
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
                        genCell selected = cells[Random.Range(0, cells.Count)];
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

            static public void RemoveCurrentWall(ref genCell[,] M, genCell current, genCell selected)
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

            static public void GenerateGoodBouses(ref genCell[,] M, int count)
            {
                for(var i = 0; i < count; i++)
                {
                    var x = Random.Range(0, M.GetLength(0));
                    var y = Random.Range(0, M.GetLength(1));

                    if(M[x,y].Value == 0)
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
                                if(x >= M.GetLength(1))
                                {
                                    x = 0;
                                }
                            }
                        } while ((M[x, y].Value != 0) || ((x == saved_x) && (y == saved_y)));

                        if(M[x,y].Value == 0)
                        {
                            PlaceBonus(ref M, x, y, 1);
                        }
                    }
                }
            }

            static void PlaceBonus(ref genCell[,] M, int x, int y, int type)
            {
                M[x, y].Value = type;
            }
        }

        #endregion
    }
}
