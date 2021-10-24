using GB.Interfaces;
using UnityEngine;


namespace GB.Controllers
{
    public sealed class GameController : MonoBehaviour
    {
        private InteractiveObject[] _interactiveObjects;

        private void Awake()
        {
            _interactiveObjects = FindObjectsOfType<InteractiveObject>();
        }

        private void Update()
        {
            for(var i = 0; i < _interactiveObjects.Length; i++)
            {
                var interactiveObject = _interactiveObjects[i];

                if(interactiveObject == null)
                {
                    continue;
                }

                if(interactiveObject is IFly fly)
                {
                    fly.Fly();
                }

                if (interactiveObject is IFlicker flicker)
                {
                    flicker.Flicker();
                }

                if(interactiveObject is IRotation rotation)
                {
                    rotation.Rotation();
                }
            }
        }
    }
}
