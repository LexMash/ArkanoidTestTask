using UnityEngine;

namespace Arkanoid.Ball
{
    /// <summary>
    /// Компонент для движения шара
    /// </summary>
    public class BallMover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public Vector2 Velocity => _rigidbody2D.velocity;

        private float _speed = 0f;
        private bool _movementEnable;

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetDirection(Vector2 direction)
        {
            _rigidbody2D.velocity = direction.normalized;
        }

        public void MovementEnable(bool enable)
        {
            _rigidbody2D.isKinematic = !enable;
            _movementEnable = enable;
        }

        private void FixedUpdate()
        {
            if (!_movementEnable)
                return;

            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _speed; //пожалуй, самый странный способ ограничить скорость...
        }
    }
}
