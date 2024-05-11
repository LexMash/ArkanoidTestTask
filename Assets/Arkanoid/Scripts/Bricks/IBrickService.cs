using System;
using System.Collections.Generic;

namespace Arkanoid.Bricks
{
    public interface IBrickService
    {
        event Action AllBricksRemoved;
        event Action<HitBrickData> OnDestroyBrick;
        event Action<HitBrickData> OnHitBrick;

        void EnableTriggerMode(bool enabled);
        void Init(Dictionary<BrickView, BrickData> brickMap);
    }
}