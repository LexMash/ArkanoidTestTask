using Arkanoid.Infrastracture.Pool;
using System;
using UnityEngine;

namespace Arkanoid.Bricks
{
    public class BrickView : MonoBehaviour, IReusable
    {
        [field: SerializeField] public BrickType Type { get; private set; }

        [SerializeField] private CollisionDetector _collisionDetector;
        [SerializeField] private DestroyFxBase _destroyFx;

        public event Action<BrickView> Hited;
        public event Action<IReusable> Released;

        private void OnEnable()
        {
            _collisionDetector.CollisionEnter += CollisionEnter;
            _collisionDetector.TriggerEnter += TriggerEnter;
        }

        private void OnDisable()
        {
            _collisionDetector.CollisionEnter -= CollisionEnter;
            _collisionDetector.TriggerEnter -= TriggerEnter;
        }

        public void CollisionEnable(bool isEnable)
        {
            _collisionDetector.SetTriggerMode(isEnable);
        }

        public void Destroy()
        {
            if(_destroyFx != null)
            {
                _destroyFx.Played += DestroyFxOnPlayed;
                _destroyFx.Play();
            }
            else
            {
                Released?.Invoke(this);
                gameObject.SetActive(false);               
            }     
        }

        private void DestroyFxOnPlayed()
        {
            _destroyFx.Played -= DestroyFxOnPlayed;
            Released?.Invoke(this);
        }

        private void CollisionEnter(Collision2D collision)
        {
            Hited?.Invoke(this);
        }

        private void TriggerEnter(Collider2D collider)
        {
            Hited?.Invoke(this);
        }
    }
}
