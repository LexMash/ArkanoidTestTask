using System;
using UnityEngine;

namespace Arkanoid.Bricks
{

    [Serializable]
    public class BrickMetaData
    {
        [field: SerializeField] public BrickType Type { get; private set; } = BrickType.White;
        [field: SerializeField] public bool IsDestroyable { get; private set; } = false;
        [field: SerializeField, Range(0, 1000)] public int Score { get; private set; } = 50;
        [field: SerializeField, Range(1, 5)] public int Health { get; private set; } = 1;
    }
}