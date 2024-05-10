using UnityEngine;

namespace Arkanoid.Capsules
{
    /// <summary>
    /// Капсулы с бонусами и модификаторами
    /// </summary>
    public class CapsuleView : MonoBehaviour
    {
        [field: SerializeField] public FxType FxType { get; private set; }
    }
}
