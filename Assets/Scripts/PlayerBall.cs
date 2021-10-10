using UnityEngine;


namespace GB
{
    public sealed class PlayerBall : Player
    {
        private void FixedUpdate()
        {
            Move();
        }
    }
}
