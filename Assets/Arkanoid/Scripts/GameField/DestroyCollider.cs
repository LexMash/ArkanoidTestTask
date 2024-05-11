using UnityEngine;

namespace Arkanoid.GameField
{
    public class DestroyCollider : MonoBehaviour
    {
        [SerializeField] private CollisionDetector _collisionDetector;

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

        private void TriggerEnter(Collider2D collider) => Destroy(collider);

        private void CollisionEnter(Collision2D collision) => Destroy(collision.collider);

        private void Destroy(Collider2D collider) => collider.GetComponent<IDestroyable>()?.Destroy();

        private void Reset() => _collisionDetector = GetComponent<CollisionDetector>();
    }
}
