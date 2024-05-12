using Arkanoid.Infrastracture;
using Arkanoid.Infrastracture.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Ball
{
    public class BallFactory : IBallFactory, IDisposable
    {
        private readonly IAssetProvider _assetProvider;

        private readonly List<BallView> _active = new();
        private readonly List<BallView> _released = new();

        private readonly BallView _ballPrefab;

        public BallFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public BallView CreateAtPosition(Vector3 position)
        {
            BallView ball = null;

            if (_released.Count != 0)
            {
                ball = _released[^1];
                _released.Remove(ball);
            }
            else
            {
                ball = Intantiate(position);
            }

            ball.Released += AddToReleased;

            _active.Add(ball);

            ball.gameObject.SetActive(true);

            return ball;
        }

        public BallView Create() => CreateAtPosition(Vector3.zero);

        public void Dispose()
        {
            foreach(BallView ball in _active)
            {
                ball.Released -= AddToReleased;
            }

            _active.Clear();
            _released.Clear();
        }

        private BallView Intantiate(Vector3 position)
        {
            return UnityEngine.Object.Instantiate(_ballPrefab, position, Quaternion.identity).GetComponent<BallView>();
        }

        private void AddToReleased(IReusable reusable)
        {
            var ball = reusable as BallView;

            ball.Released -= AddToReleased;

            _active.Remove(ball);
            _released.Add(ball);
        }       
    }
}
