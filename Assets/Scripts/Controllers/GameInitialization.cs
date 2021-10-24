using UnityEngine;
using Model;

namespace RollABall
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, Data data)
        {
            Camera camera = Camera.main;
            var PlayerModel = new PlayerModel(data.Player.Prefab, data.Player.Speed, data.Player.Position, data.Player.Name);
        }
    }
}
