using UnityEngine;

namespace RollABall
{
    [CreateAssetMenu(fileName = "UnitSettings", menuName = "Data/Unit/UnitSettings")]
    public sealed class PlayerData : ScriptableObject
    {
        public GameObject _playerPrefab;
        [SerializeField] private string _name;
        public string Name => _name;
    }
}
