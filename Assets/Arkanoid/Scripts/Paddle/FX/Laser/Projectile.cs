using Arkanoid.Bricks;
using Arkanoid.Infrastracture.Pool;
using System;
using UnityEngine;

namespace Arkanoid.Paddle.FX.Laser
{
    public class Projectile : MonoBehaviour, IReusable
    {
        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private DestroyFxBase _destroyFx;
        [SerializeField] private GameObject _visual;

        public event Action<IReusable> Released;

        private void OnEnable()
        {
            _visual.SetActive(true);

            _collisionDetector.CollisionEnter += CollisionEnter;
        }

        private void OnDisable()
        {
            _collisionDetector.CollisionEnter -= CollisionEnter;
        }

        private void CollisionEnter(Collision2D collision)
        {
            _visual.SetActive(false);

            _destroyFx.Played += OnPlayed;
            _destroyFx.Play();
        }

        private void OnPlayed()
        {
            _destroyFx.Played -= OnPlayed;
            Released?.Invoke(this);
        }

        private void Reset()
        {
            _collisionDetector = GetComponent<CollisionDetector>();
            _destroyFx = GetComponent<DestroyFxBase>();
        }
    }
}
