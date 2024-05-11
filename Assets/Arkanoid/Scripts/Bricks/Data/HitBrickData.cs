using Arkanoid.PowerUPs;
using UnityEngine;

namespace Arkanoid.Bricks
{
    public struct HitBrickData
    {
        public BrickType BrickType;
        public ModType ModType;
        public Vector2 Position;
        public bool IsDestroyable;

        public HitBrickData(BrickType type,Vector2 position, bool isDestroyable, ModType modType = ModType.None)
        {
            BrickType = type;
            ModType = modType;
            Position = position;
            IsDestroyable = isDestroyable;
        }
    }
}
