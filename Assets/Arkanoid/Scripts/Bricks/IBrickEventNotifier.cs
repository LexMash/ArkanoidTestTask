using System;

namespace Arkanoid.Bricks
{
    public interface IBrickEventNotifier
    {
        event Action AllBricksRemoved;
        event Action<HitBrickData> OnDestroyBrick;
        event Action<HitBrickData> OnHitBrick;
    }
}