using System;

namespace Arkanoid.Ball
{
    public interface IBallDestroyNotificator
    {
        event Action BallDestroed;
    }
}