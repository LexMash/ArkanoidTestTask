using Arkanoid.Ball.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid.Ball
{
    public class BallController : IDisposable
    {
        public event Action BallDestroed;

        private readonly List<BallView> _balls = new();

        private readonly BallConfig _ballConfig;
        private readonly IBallFactory _factory;
        private readonly Transform _initTransform;

        private BallView _mainBall;

        public BallController(BallConfig ballConfig, IBallFactory factory, Transform initTransform)
        {
            _ballConfig = ballConfig;
            _factory = factory;
            _initTransform = initTransform;
        }

        public void ResetBall()
        {
            _mainBall = _factory.CreateAtPosition(_initTransform.position);
            _mainBall.transform.SetParent(_initTransform);
        }

        public void StartLaunch()
        {
            _mainBall.transform.SetParent(null);
            _mainBall.Mover.SetSpeed(_ballConfig.MainSpeed);

            int randomSide = UnityEngine.Random.Range(-1, 1);

            _mainBall.Mover.SetDirection(new Vector2(randomSide, 1f));
        }

        public void AddBall()
        {
            BallView ball = _factory.CreateAtPosition(_balls[0].transform.position);

            ball.Destroyed += OnBallDestroyed;

            _balls.Add(ball);

            ball.Mover.SetSpeed(_ballConfig.MainSpeed);

            float randomX = UnityEngine.Random.Range(-1f, 1f);
            float randomY = UnityEngine.Random.Range(-1f, 1f);

            ball.Mover.SetDirection(new Vector2(randomX, randomY));
        }

        public void SpeedDown()
        {
            ChangeSpeed(_ballConfig.SlowSpeed);
        }

        public void ResetSpeed()
        {
            ChangeSpeed(_ballConfig.MainSpeed);
        }

        public void Dispose()
        {
            foreach (var ball in _balls)
            {
                ball.Destroyed -= OnBallDestroyed;
            }

            _balls.Clear();

            _mainBall = null;
        }

        private void OnBallDestroyed(IDestroyable destroyable)
        {
            var ball = destroyable as BallView;

            ball.Destroyed -= OnBallDestroyed;

            ball.gameObject.SetActive(false);

            _balls.Remove(ball);

            if(_balls.Count == 0)
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
    }

    public interface IBallFactory
    {
        BallView CreateAtPosition(Vector2 position);
    }
}
