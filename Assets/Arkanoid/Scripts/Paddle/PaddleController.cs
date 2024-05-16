using Arkanoid.Input;
using Arkanoid.Paddle.FX.Laser;
using Arkanoid.PowerUPs;
using System;
using UnityEngine;

namespace Arkanoid.Paddle
{
    public class PaddleController : IDisposable, IPaddleCollisionNotifier, ISizeChangeNotifier
    {
        public event Action<ModType> PowerUPTaken;
        public event Action BallCollision;

        public event Action Decreased;
        public event Action Increased;

        private readonly Vector2 _initialPosition = Vector2.zero;

        private readonly IInput _input;
        private readonly IPaddleMoveController _moveController;
        private readonly IPaddleSizeController _sizeController;
        private readonly CollisionDetector _collisionDetector;
        private readonly LaserGun _laserGun;

        public PaddleController(IInput input, 
            IPaddleMoveController moveController,
            PaddleView paddle)
        {
            _input = input;
            _moveController = moveController;
            _sizeController = paddle.WidthChanger;
            _laserGun = paddle.LaserGun;
            _collisionDetector = paddle.CollisionDetector;

            _input.MovePerformed += InputMovePerformed;

            _collisionDetector.TriggerEnter += OnTriggerEnter;
            _collisionDetector.CollisionEnter += OnCollisionEnter;
        }

        public void Dispose()
        {
            _input.MovePerformed -= InputMovePerformed;

            _collisionDetector.TriggerEnter -= OnTriggerEnter;
            _collisionDetector.CollisionEnter -= OnCollisionEnter;
        }

        public void SetInitialState()
        {
            _moveController.SetTargetPosition(_initialPosition);
            _sizeController.SetInitialSize();
        }

        public void IncreaseSize()
        {
            if (_sizeController.CanIncrease())
            {
                _sizeController.Increase();
                Increased?.Invoke();
            }
        }

        public void DecreaseSize()
        {
            if (_sizeController.CanDecrease())
            {
                _sizeController.Decrease();
                Decreased?.Invoke();
            }
        }

        public void GunEnable() => _laserGun.Enable();
        public void GunDisable() => _laserGun.Disable();

        private void InputMovePerformed(Vector3 vector) => _moveController.SetTargetPosition(vector);

        private void OnTriggerEnter(Collider2D collider)
        {
            var powerUp = collider.GetComponent<PowerUpView>();

            PowerUPTaken?.Invoke(powerUp.ModType);

            powerUp.Destroy();
        }

        private void OnCollisionEnter(Collision2D collision) => BallCollision?.Invoke();
    }
}
