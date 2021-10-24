using System.IO;
using UnityEngine;

namespace RollABall
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public sealed class Data : ScriptableObject
    {
        [SerializeField] private string _playerDataPath;
        private PlayerData _player;

        public PlayerData Player
        {
            get
            {
                if(_player == null)
                {
                    _player = Load<PlayerData>(Path.Combine("Data", _playerDataPath));
                }

                return _player;
            }
        }

        private T Load<T>(string resourcesPath) where T : Object =>
            Resources.Load<T>(Path.ChangeExtension(resourcesPath, null));
    }
}
