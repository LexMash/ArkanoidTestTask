using UnityEngine;

namespace Arkanoid.Paddle
{
    public class PaddleView : MonoBehaviour
    {
        [field: SerializeField] public WidthChanger WidthChanger { get; private set; }
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }

        [Space]
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private Rigidbody2D _rigidbody;

        public float Width => _collider.size.x;

        public void SetVelocity(Vector2 velocity)
        {
            _rigidbody.velocity = velocity;
        }
    }
}
