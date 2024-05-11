using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Bricks
{
    [CreateAssetMenu(fileName = "New BricksConfig", menuName = "Arkanoid/Bricks/BricksConfig")]
    public class BrickConfig : ScriptableObject
    {
        [SerializeField] private List<BrickMetaData> _datas;

        public IReadOnlyList<BrickMetaData> Datas => _datas;
    }

    [Serializable]
    public class BrickMetaData
    {
        [field: SerializeField] public BrickType Type { get; private set; } = BrickType.White;
        [field: SerializeField] public bool IsDestroyable { get; private set; } = false;
        [field: SerializeField, Range(0, 1000)] public int Score { get; private set; } = 50;
        [field: SerializeField, Range(1, 5)] public int Health { get; private set; } = 1;
    }
}