using UnityEngine;

namespace Arkanoid.Ball
{
    /// <summary>
    /// View шара
    /// </summary>
    public class BallView : MonoBehaviour
    {
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }
    }
}
