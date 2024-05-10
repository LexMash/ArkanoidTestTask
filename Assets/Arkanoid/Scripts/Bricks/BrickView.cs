using UnityEngine;

namespace Arkanoid.Bricks
{
    public class BrickView : MonoBehaviour
    {
        [field: SerializeField] public BrickType Type { get; private set; }
    }
}
