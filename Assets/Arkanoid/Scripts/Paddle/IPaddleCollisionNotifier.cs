using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Paddle
{
    public interface IPaddleCollisionNotifier
    {
        event Action<ModType> ModTaken;
        event Action BallCollision;
    }
}