using System;
using UnityEngine;

namespace Arkanoid
{
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<Collision2D> OnCollisionEnter;
        public event Action<Collider2D> OnTriggerEnter;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter?.Invoke(collision);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            OnTriggerEnter?.Invoke(collider);
        }
    }
}
