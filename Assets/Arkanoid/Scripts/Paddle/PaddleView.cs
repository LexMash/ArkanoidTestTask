using UnityEngine;

namespace Arkanoid.Paddle
{
    public class PaddleView : MonoBehaviour, IMovable
    {
        [field: SerializeField] public WidthChanger WidthChanger { get; private set; }
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }

        [Space]
        [SerializeField] private Rigidbody2D _rigidbody;

        public float Width => WidthChanger.Width;

        public Vector2 CurrentPosition
        {
            get => _rigidbody.position;
            set => _rigidbody.position = value;
        }

        public Vector2 Velocity
        {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
        }
    }
}
