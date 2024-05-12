﻿using UnityEngine;

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