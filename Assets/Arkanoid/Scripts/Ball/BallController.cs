using Arkanoid.Ball.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid.Ball
{
    public class BallController : IDisposable, IBallController, IBallCollisionNotifier
    {
        public event Action BallDestroed;
        public event Action BrickCollision;
        public event Action PaddleCollision;
        public event Action WallCollision;

        public int BallsCount => _balls.Count;

        private readonly List<BallView> _balls = new();
        private readonly Dictionary<BallView, Vector2> _velocityMap = new();

        private readonly BallConfig _ballConfig;
        private readonly IBallFactory _factory;
        private readonly Transform _initTransform;

        private BallView _mainBall;
        private bool _magnetModeEnabled;
        
        public BallController(BallConfig ballConfig, IBallFactory factory, Transform initTransform)
        {
            _ballConfig = ballConfig;
            _factory = factory;
            _initTransform = initTransform;
        }

        public void SetInitialState()
        {
            if (_mainBall == null)
                _mainBall = _factory.Create();

            if (_balls.Count > 0)
                DestroyExtraBalls();

            SubscribeToBallEvents(_mainBall);

            _balls.Add(_mainBall);
            SetStartBallPosition();
        }

        public void FirstLaunch()
        {
            _mainBall.transform.SetParent(null);
            _mainBall.Mover.SetSpeed(_ballConfig.MainSpeed);

            int randomSide = UnityEngine.Random.Range(-1, 1);

            _mainBall.Mover.SetDirection(new Vector2(randomSide, 1f));
        }

        public void AddBall()
        {
            BallView ball = _factory.CreateAtPosition(_mainBall.transform.position);

            SubscribeToBallEvents(ball);

            _balls.Add(ball);

            LaunchToRandomDirection(ball);
        }

        private void DestroyExtraBalls()
        {
            var count = _balls.Count;

            for (int i = 0; i < count; i++)
            {
                var ball = _balls[i];
               
                if (ball == _mainBall)
                    continue;

                UnsubscribeFromBallEvents(ball);

                ball.Destroy();

                _balls.Remove(ball);
            }
        }

        public void SpeedDown()
        {
            ChangeSpeed(_ballConfig.SlowSpeed);
        }

        public void ResetSpeed()
        {
            ChangeSpeed(_ballConfig.MainSpeed);
        }

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
                ball.Mover.SetSpeed(velocity.magnitude);
                ball.Mover.SetDirection(velocity);
            }

            _velocityMap.Clear();
        }

        public void Dispose()
        {
            foreach (var ball in _balls)
            {
                UnsubscribeFromBallEvents(ball);
            }

            _balls.Clear();
            _velocityMap.Clear();

            _mainBall = null;
        }

        private void OnBrickCollision()
        {
            BrickCollision?.Invoke();
        }

        private void OnPaddleCollision(BallView ball)
        {
            PaddleCollision?.Invoke();

            if (_magnetModeEnabled)
            {
                MagnetizeBall(ball);
            }
        }

        private void MagnetizeBall(BallView view)
        {
            _velocityMap[view] = view.Mover.Velocity;

            view.transform.SetParent(_initTransform);
            view.Mover.SetSpeed(0f);
        }

        private void OnWallCollision()
        {
            WallCollision?.Invoke();
        }

        private void LaunchToRandomDirection(BallView ball)
        {
            ball.Mover.SetSpeed(_ballConfig.MainSpeed);

            float randomX = UnityEngine.Random.Range(-1f, 1f);
            float randomY = UnityEngine.Random.Range(-1f, 1f);

            ball.Mover.SetDirection(new Vector2(randomX, randomY));
        }

        private void OnBallDestroyed(IDestroyable destroyable)
        {
            var ball = destroyable as BallView;

            UnsubscribeFromBallEvents(ball);

            ball.gameObject.SetActive(false);

            _balls.Remove(ball);

            if (_balls.Count == 0)
            {
                BallDestroed?.Invoke();

                return;
            }

            if (ball == _mainBall)
            {
                _mainBall = _balls.First(ball => ball != null);
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
            _mainBall.Mover.SetSpeed(0);
            _mainBall.gameObject.SetActive(true);

            _mainBall.transform.SetParent(_initTransform);
            _mainBall.transform.position = Vector3.zero;
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
