using Arkanoid.Paddle;
using UnityEngine;

namespace Arkanoid.Input
{
    public class PaddleMobileMoveController : MonoBehaviour, IPaddleMoveController
    {                
        private IInput _input;
        private IMovable _paddle;
        private float _fieldWidth;

        private float _targetXPosition;
        private float _yPosition;
        private Vector2 _velocity;
        private float _smoothFactor;

        public void Construct(PaddleConfig config, IMovable paddle, IInput input)
        {
            _smoothFactor = config.SmoothMoveFactor;
            _paddle = paddle;
            _yPosition = _paddle.CurrentPosition.y;
            _fieldWidth = config.FieldWidth;

            _input = input;
            _input.MovePerformed += OnMovePerformed;
        }

        private void OnDestroy()
        {
            _input.MovePerformed -= OnMovePerformed;

            _input = null;
            _paddle = null;           
        }

        private void FixedUpdate()
        {
            _paddle.Velocity = _velocity; //по моему коллизии с шариком так лучше обрабатываются
        }

        private void Update()
        {
            var targetPosition = new Vector2(_targetXPosition, _yPosition);

            _paddle.CurrentPosition = Vector2.SmoothDamp(_paddle.CurrentPosition, targetPosition, ref _velocity, _smoothFactor * Time.deltaTime);          
        }

        private void OnMovePerformed(Vector3 position)
        {
            _targetXPosition = ClampXCoordinate(position, _paddle.Width / 2f);
        }

        private float ClampXCoordinate(Vector3 worldPoint, float paddleHalfWidth) 
            => Mathf.Clamp(worldPoint.x, -_fieldWidth + paddleHalfWidth, _fieldWidth - paddleHalfWidth);
    }
}
