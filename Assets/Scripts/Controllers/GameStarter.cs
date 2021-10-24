using UnityEngine;

namespace RollABall
{
    public sealed class GameStarter : MonoBehaviour
    {
        private Controllers _controllers;

        private void Start()
        {
            _controllers = new Controllers();
            //new GameInitialization(_controllers);
            _controllers.Initialization();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.Cleanup();
        }
    }

}
