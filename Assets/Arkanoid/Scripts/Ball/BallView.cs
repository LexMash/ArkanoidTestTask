using Arkanoid.Infrastracture.Pool;
using System;
using UnityEngine;

namespace Arkanoid.Ball
{
    /// <summary>
    /// View шара
    /// </summary>
    public class BallView : MonoBehaviour, IReusable, IDestroyable
    {
        [field: SerializeField] public BallMover Mover { get; private set; }
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private LayerMask _paddleLayer;
        [SerializeField] private LayerMask _brickLayer;

        public event Action<IReusable> Released;
        public event Action<IDestroyable> Destroyed;

        public event Action WallCollision;
        public event Action<BallView> PaddleCollision;
        public event Action BrickCollision;

        public void Destroy()
        {
            Released?.Invoke(this);
            Destroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            int layer = collision.gameObject.layer;

            if (IsOnLayer(_wallLayer, layer))
                WallCollision?.Invoke();

            if (IsOnLayer(_paddleLayer, layer))
                PaddleCollision?.Invoke(this);

            if (IsOnLayer(_brickLayer, layer))
                BrickCollision?.Invoke();
        }

        private bool IsOnLayer(LayerMask mask, int layer)
            => mask == (mask | (1 << layer));
    }
}
