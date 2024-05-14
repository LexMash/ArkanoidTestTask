using System;

namespace Arkanoid.Bricks
{
    public interface IBrickEventNotifier
    {
        event Action OnAllBricksRemoved;
        event Action<HitBrickData> OnBrickDestroyed;
        event Action<HitBrickData> OnBrickHitted;
    }
}