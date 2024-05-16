using Arkanoid.Paddle.FX.Laser;
using UnityEngine;

namespace Arkanoid.Paddle
{
    public class PaddleView : MonoBehaviour, IMovable, IBallInitialTransform
    {
        [field: SerializeField] public WidthChanger WidthChanger { get; private set; }
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public LaserGun LaserGun { get; private set; }

        [Space]
        [SerializeField] private Rigidbody2D _rigidbody;    

        public float Width => WidthChanger.Width;

        public Vector2 CurrentPosition
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector2 Velocity
        {
            get => _rigidbody.velocity;
            set => _rigidbody.velocity = value;
        }      
    }
}
