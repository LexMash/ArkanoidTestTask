using UnityEngine;

namespace Arkanoid.GameField
{
    public class DestroyCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider) => Destroy(collider);

        private void OnCollisionEnter2D(Collision2D collision) => Destroy(collision.collider);

        private void Destroy(Collider2D collider) => collider.GetComponent<IDestroyable>()?.Destroy();
    }
}
