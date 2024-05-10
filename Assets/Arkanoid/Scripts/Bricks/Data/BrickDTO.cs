using Arkanoid.Capsules;
using System;
using UnityEngine;

namespace Arkanoid.Bricks
{
    [Serializable]
    public class BrickDTO
    {
        public BrickType Type;
        public ModType FxType;
        public Vector2 Position;
        public bool IsDestroyable;
    }
}
