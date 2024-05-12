using UnityEngine;

namespace Arkanoid.Ball
{
    public interface IBallFactory
    {
        BallView Create();
        BallView CreateAtPosition(Vector3 position);
    }
}
