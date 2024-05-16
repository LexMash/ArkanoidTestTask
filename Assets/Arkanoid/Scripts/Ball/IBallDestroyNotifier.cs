using System;

namespace Arkanoid.Ball
{
    public interface IBallDestroyNotifier
    {
        event Action BallDestroed;
    }
}