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
        [SerializeField] private int _rows = 49;
        [SerializeField] private int _cols = 49;
        [SerializeField] private int _goodBonuses = 25;
        [SerializeField] private int _badBonuses = 5;

        private GameObject _floor;
        private GameObject _player;
        private MapGenerator _map;
        private readonly float _planeScale = 10.0f;

        #endregion


        #region UnityMethods

        private void Start()
        {
            _map = new MapGenerator(_rows, _cols, _goodBonuses, _badBonuses);

            CreateFloor();

            GenerateLevel(_map);

            CreatePlayer();
        }

        #endregion


        #region Methods

        private void CreateFloor()
        {
            _floor = Instantiate(_floorPrefab, Vector3.zero, Quaternion.identity);
            SetFloorDimensions(_floor, _wallPrefab.transform.localScale, _map.Rows, _map.Cols);
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

        private void GenerateLevel(MapGenerator map)
        {
            float x = 0.0f;
            float y = 0.5f * _wallPrefab.transform.localScale.y;
            float z = 0.0f;

            for (var i = 0; i < map.Rows; i++)
            {
                for (var j = 0; j < map.Cols; j++)
                {
                    x = i * _wallPrefab.transform.localScale.x;
                    z = j * _wallPrefab.transform.localScale.z;

                    switch (map.Map[i, j].Value)
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

    }
}
