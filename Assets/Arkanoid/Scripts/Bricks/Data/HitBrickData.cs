using Arkanoid.Capsules;
using UnityEngine;

namespace Arkanoid.Bricks
{
    public struct HitBrickData
    {
        public ModType ModType;
        public Vector2 Position;
        public bool IsDestroyable;

        public HitBrickData(Vector2 position, bool isDestroyable, ModType modType = ModType.None)
        {
            ModType = modType;
            Position = position;
            IsDestroyable = isDestroyable;
        }
    }
}
