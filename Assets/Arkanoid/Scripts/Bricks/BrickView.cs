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
        [SerializeField] private GameObject _visual;

        public event Action<BrickView> Hited;
        public event Action<BrickView> Triggered;
        public event Action<IReusable> Released;

        private void OnEnable()
        {
            _visual.SetActive(true);
            _collisionDetector.CollisionEnable(true);
            _collisionDetector.SetTriggerMode(false);

            _collisionDetector.CollisionEnter += CollisionEnter;
            _collisionDetector.TriggerEnter += TriggerEnter;
        }

        private void OnDisable()
        {
            _collisionDetector.CollisionEnter -= CollisionEnter;
            _collisionDetector.TriggerEnter -= TriggerEnter;
        }

        public void SetTriggerMode(bool isTrigger) 
            => _collisionDetector.SetTriggerMode(isTrigger);

        public void Destroy(bool withFx = true)
        {           
            if (withFx && _destroyFx != null)
            {
                _collisionDetector.CollisionEnable(false);
                _destroyFx.Played += DestroyFxOnPlayed;
                _destroyFx.Play();
            }
            else
            {
                Released?.Invoke(this);            
            }     
        }

        private void DestroyFxOnPlayed()
        {
            _destroyFx.Played -= DestroyFxOnPlayed;
            Released?.Invoke(this);
        }

        private void CollisionEnter(Collision2D collision) 
            => Hited?.Invoke(this);

        private void TriggerEnter(Collider2D collider) 
            => Triggered?.Invoke(this);
    }
}
