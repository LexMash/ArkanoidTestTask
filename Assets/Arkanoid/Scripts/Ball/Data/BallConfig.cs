using UnityEngine;

namespace Arkanoid.Ball.Data
{
    [CreateAssetMenu(fileName = "New BallConfig", menuName = "Arkanoid/Ball/BallConfig")]
    public class BallConfig : ScriptableObject
    {
        [field: SerializeField] public float MainSpeed { get; private set; } = 10f;
        [field: SerializeField] public float SlowSpeed { get; private set; } = 5f;
        [field: SerializeField] public int SplitAmount { get; private set; } = 3;
    }
}
