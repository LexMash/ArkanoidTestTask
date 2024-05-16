using Arkanoid.Ball.Data;
using Arkanoid.Paddle;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Ball
{
    public class BallController : IDisposable, IBallController, IBallCollisionNotifier, IBallDestroyNotifier
    {
        public event Action BallDestroed;
        public event Action BrickCollision;
        public event Action PaddleCollision;
        public event Action WallCollision;

        public int BallsCount => _balls.Count;

        private readonly List<BallView> _balls = new();
        private readonly Dictionary<BallView, Vector2> _velocityMap = new();

        private readonly BallConfig _ballConfig;
        private readonly BallFactory _factory;
        private readonly IBallInitialTransform _initTransform;

        private bool _magnetModeEnabled;

        public BallController(BallConfig ballConfig, BallFactory factory, IBallInitialTransform initTransform)
        {
            _ballConfig = ballConfig;
            _factory = factory;
            _initTransform = initTransform;
        }

        private BallView FirstBall => _balls[0];

        public void SetInitialState()
        {
            BallView ball = _factory.Create();

            SubscribeToBallEvents(ball);

            _balls.Add(ball);

            SetStartBallPosition();
        }

        public void SpeedDown() => ChangeSpeed(_ballConfig.SlowSpeed);

        public void ResetSpeed() => ChangeSpeed(_ballConfig.MainSpeed);

        public void MagnetModeEnable(bool isEnable)
        {
            _magnetModeEnabled = isEnable;

            if (!_magnetModeEnabled && _velocityMap.Count > 0)
                ReleaseMagnet();
        }

        public void ReleaseMagnet()
        {
            foreach (var kvp in _velocityMap)
            {
                BallView ball = kvp.Key;
                Vector2 velocity = kvp.Value;

                ball.transform.SetParent(null);
                ball.Mover.MovementEnable(true);
                ball.Mover.SetSpeed(velocity.magnitude);
                ball.Mover.SetDirection(velocity);
            }

            _velocityMap.Clear();
        }

        public void AddBall()
        {
            BallView ball = _factory.Create(FirstBall.transform.position);

            SubscribeToBallEvents(ball);

            _balls.Add(ball);

            LaunchToRandomDirection(ball);
        }

        public void FirstLaunch()
        {
            FirstBall.Mover.MovementEnable(true);

            FirstBall.transform.SetParent(null);
            FirstBall.Mover.SetSpeed(_ballConfig.MainSpeed);

            int randomSide = UnityEngine.Random.Range(-1, 2);

            FirstBall.Mover.SetDirection(new Vector2(randomSide, 1f));
        }

        public void DestroyExtraBalls()
        {
            var count = _balls.Count;

            for (int i = 0; i < count; i++)
            {
                var ball = _balls[i];

                UnsubscribeFromBallEvents(ball);

                ball.Destroy();
            }

            _balls.Clear();
        }

        public void Dispose()
        {
            foreach (var ball in _balls)
            {
                UnsubscribeFromBallEvents(ball);
            }

            _balls.Clear();
            _velocityMap.Clear();
        }

        private void OnBrickCollision() => BrickCollision?.Invoke();

        private void OnPaddleCollision(BallView ball)
        {
            PaddleCollision?.Invoke();

            if (_magnetModeEnabled)
            {
                MagnetizeBall(ball);
            }
        }

        private void MagnetizeBall(BallView ball)
        {
            _velocityMap[ball] = ball.Mover.Velocity;

            ball.transform.SetParent(_initTransform.Transform);
            ball.Mover.MovementEnable(false);
        }

        private void OnWallCollision() => WallCollision?.Invoke();

        private void LaunchToRandomDirection(BallView ball)
        {
            ball.Mover.MovementEnable(true);

            ball.Mover.SetSpeed(_ballConfig.MainSpeed);

            float randomX = UnityEngine.Random.Range(-1f, 1f);
            float randomY = UnityEngine.Random.Range(-1f, 1f);

            ball.Mover.SetDirection(new Vector2(randomX, randomY));
        }

        private void OnBallDestroyed(IDestroyable destroyable)
        {
            var ball = destroyable as BallView;

            UnsubscribeFromBallEvents(ball);

            _balls.Remove(ball);

            if (_balls.Count == 0)
            {
                BallDestroed?.Invoke();

                return;
            }
        }

        private void ChangeSpeed(float speed)
        {
            for (int i = 0; i < _balls.Count; i++)
            {
                _balls[i].Mover.SetSpeed(speed);
            }
        }

        private void SetStartBallPosition()
        {
            FirstBall.Mover.SetSpeed(0);

            FirstBall.transform.SetParent(_initTransform.Transform);
            FirstBall.transform.localPosition = Vector3.zero;
        }

        private void SubscribeToBallEvents(BallView ball)
        {
            ball.Destroyed += OnBallDestroyed;

            ball.WallCollision += OnWallCollision;
            ball.PaddleCollision += OnPaddleCollision;
            ball.BrickCollision += OnBrickCollision;
        }

        private void UnsubscribeFromBallEvents(BallView ball)
        {
            ball.Destroyed -= OnBallDestroyed;

            ball.WallCollision -= OnWallCollision;
            ball.PaddleCollision -= OnPaddleCollision;
            ball.BrickCollision -= OnBrickCollision;
        }
    }
}
