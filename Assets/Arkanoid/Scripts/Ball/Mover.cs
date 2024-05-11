using UnityEngine;

namespace Arkanoid.Ball
{
    /// <summary>
    /// Компонент для движения шара
    /// </summary>
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _smoothTime = 1f;

        private float _speed = 0f;

        private void Awake()
        {
            SetSpeed(5f);
            SetDirection(Vector2.one);
        }

        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void SetDirection(Vector2 direction)
        {
            _rigidbody2D.velocity = direction.normalized;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _speed; //пожалуй, самый странный способ ограничить скорость...
        }
    }
}
