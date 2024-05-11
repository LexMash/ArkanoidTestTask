using Arkanoid.PowerUPs;
using System;
using UnityEngine;

namespace Arkanoid.Paddle
{
    public class PaddleCollisionHandler : IPaddleCollisionNotifier, IDisposable
    {
        public event Action<ModType> ModTaken;
        public event Action BallCollision;

        private readonly CollisionDetector _detector;

        public PaddleCollisionHandler(CollisionDetector detector)
        {
            _detector = detector;

            _detector.TriggerEnter += TriggerEnter;
            _detector.CollisionEnter += CollisionEnter;
        }

        private void CollisionEnter(Collision2D collision)
        {
            BallCollision?.Invoke();
        }

        private void TriggerEnter(Collider2D collider)
        {
            ModType type = collider.GetComponent<PowerUpView>().ModType;

            ModTaken?.Invoke(type);
        }

        public void Dispose()
        {
            _detector.TriggerEnter -= TriggerEnter;
            _detector.CollisionEnter -= CollisionEnter;
        }
    }
}
