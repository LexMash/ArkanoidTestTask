﻿using Arkanoid.Paddle;
using UnityEngine;

namespace Arkanoid.Input
{
    public class PaddleMobileMoveController : MonoBehaviour, IPaddleMoveController
    {                
        private IMovable _paddle;
        private float _fieldWidth;

        private float _targetXPosition;
        private float _yPosition;
        private Vector2 _velocity;
        private float _smoothFactor;

        public void Construct(PaddleConfig config, IMovable paddle)
        {
            _smoothFactor = config.SmoothMoveFactor;
            _paddle = paddle;
            _yPosition = _paddle.CurrentPosition.y;
            _fieldWidth = config.FieldWidth;
        }

        public void SetTargetPosition(Vector3 position)
        {
            _targetXPosition = ClampXCoordinate(position, _paddle.Width / 2f);
        }

        private void FixedUpdate()  //по моему коллизии с шариком так лучше обрабатываются, несмотря на движение через трансформ
            => _paddle.Velocity = _velocity;

        private void Update()
        {
            var targetPosition = new Vector2(_targetXPosition, _yPosition);

            _paddle.CurrentPosition = Vector2.SmoothDamp(_paddle.CurrentPosition, targetPosition, ref _velocity, _smoothFactor * Time.deltaTime);          
        }

        private float ClampXCoordinate(Vector3 worldPoint, float paddleHalfWidth)
            => Mathf.Clamp(worldPoint.x, -_fieldWidth + paddleHalfWidth, _fieldWidth - paddleHalfWidth);

        private void OnDestroy()
        {
            _paddle = null;
        }
    }
}