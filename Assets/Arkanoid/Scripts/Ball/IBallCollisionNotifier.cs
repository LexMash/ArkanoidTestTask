using System;

namespace Arkanoid.Ball
{
    public interface IBallCollisionNotifier
    {
        event Action BrickCollision;
        event Action PaddleCollision;
        event Action WallCollision;
    }
}