using UnityEngine;


namespace GB
{
    public sealed class CameraController : MonoBehaviour
    {
        private Player _player;
        private Vector3 _offset = new Vector3(0.0f, 3.0f, -1.0f);

        private void LateUpdate()
        {
            if(_player is null)
            {
                FindPlayer();
            }
            else
            {
                transform.position = _player.transform.position + _offset;
                transform.LookAt(_player.transform);
            }
        }

        private void FindPlayer()
        {
            _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerBall>();
            //_offset = transform.position - _player.transform.position;
        }
    }
}
