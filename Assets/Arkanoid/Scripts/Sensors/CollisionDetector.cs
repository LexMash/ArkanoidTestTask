using System;
using UnityEngine;

namespace Arkanoid
{
    public class CollisionDetector : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

        public event Action<Collision2D> CollisionEnter;
        public event Action<Collider2D> TriggerEnter;

        public void SetTriggerMode(bool isTrigger)
        {
            _collider.isTrigger = isTrigger;
        }
        
        public void CollisionEnable(bool isEnable)
        {
            _collider.enabled = isEnable;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEnter?.Invoke(collision);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            TriggerEnter?.Invoke(collider);
        }


        private void Reset()
        {
            _collider = GetComponent<Collider2D>();
        }
    }
}