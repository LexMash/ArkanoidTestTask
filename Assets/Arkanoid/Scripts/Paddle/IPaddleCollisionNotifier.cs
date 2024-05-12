using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Paddle
{
    public interface IPaddleCollisionNotifier
    {
        event Action<ModType> PowerUPTaken;
        event Action BallCollision;
    }
}