using Arkanoid.Capsules;
using System;
using UnityEngine;

namespace Arkanoid.Bricks
{
    [Serializable]
    public class BrickDTO
    {
        public BrickType Type;
        public FxType FxType;
        public Vector2 Position;
        public bool IsDestroyable;
    }
}
