﻿using UnityEngine;

namespace RollABall
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, Data data)
        {
            Camera camera = Camera.main;

            var inputInitialization = new InputInitialization();
            var playerModel = new PlayerModel(data.Player.Prefab, data.Player.Speed, data.Player.Position, data.Player.Name);

            controllers.Add(inputInitialization);

            //controllers.Add(new MapController());
            controllers.Add(new LevelController(data.Level));
            controllers.Add(new InputController(inputInitialization.GetInput()));
            controllers.Add(new MoveController(inputInitialization.GetInput(), playerInitialization.GetPlayer(), playerModel));
        }
    }
}
