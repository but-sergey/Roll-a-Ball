using UnityEngine;
using GB.Interfaces;
using GB.UI;


namespace GB
{
    public sealed class GoodBonus : InteractiveObject, IFly, IFlicker
    {
        private Material _material;
        private DisplayBonuses _displayBonuses;
        private float _lengthFly;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _displayBonuses = new DisplayBonuses();
            _lengthFly = Random.Range(1.0f, 5.0f);
        }

        protected override void Interaction(GameObject interactedGameObject)
        {
            var _playerBall = interactedGameObject.GetComponent<PlayerBall>();
            if(_playerBall != null)
            {
                _playerBall.AddBonus(5);
                _displayBonuses.Display(_playerBall.Score);
            }
        }

        public void Fly()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                Mathf.PingPong(Time.time, _lengthFly),
                transform.position.z);
        }

        public void Flicker()
        {
            _material.color = new Color(_material.color.r, _material.color.g, _material.color.b,
                Mathf.PingPong(Time.time, 1.0f));
        }
    }
}
