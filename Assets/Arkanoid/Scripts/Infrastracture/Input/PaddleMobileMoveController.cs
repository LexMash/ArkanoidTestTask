using Arkanoid.Paddle;
using UnityEngine;

namespace Arkanoid.Input
{
    public class PaddleMobileMoveController : MonoBehaviour, IPaddleMoveController
    {
        [SerializeField] private UITouchZone _touchZone;
        [SerializeField] private Camera _camera;

        [SerializeField] private PaddleView _paddle;

        [SerializeField] private float _smoothFactor = 0.5f;
        [SerializeField] private float _fieldWidth;

        private float _targetXPosition;
        private float _yPosition;
        private Vector2 _velocity;

        public void Construct(PaddleConfig config, PaddleView paddle)
        {
            _smoothFactor = config.SmoothMoveFactor;

            _paddle = paddle;

            _yPosition = _paddle.transform.position.y;
        }

        private void OnEnable()
        {
            _touchZone.MovePerformed += OnMovePerformed;
        }

        private void OnDisable()
        {
            _touchZone.MovePerformed -= OnMovePerformed;
        }

        private void FixedUpdate()
        {
            _paddle.SetVelocity(_velocity);
        }

        private void Update()
        {
            var targetPosition = new Vector2(_targetXPosition, _yPosition);

            _paddle.transform.position = Vector2.SmoothDamp(_paddle.transform.position, targetPosition, ref _velocity, _smoothFactor * Time.deltaTime);          
        }

        private void OnMovePerformed(Vector2 position)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_touchZone.RectTransform, position, _camera, out Vector3 worldPoint);

            _targetXPosition = ClampXCoordinate(worldPoint, _paddle.Width / 2f);
        }

        private float ClampXCoordinate(Vector3 worldPoint, float paddleHalfWidth) 
            => Mathf.Clamp(worldPoint.x, -_fieldWidth + paddleHalfWidth, _fieldWidth - paddleHalfWidth);
    }
}
