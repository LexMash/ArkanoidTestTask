using Arkanoid.PowerUPs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Paddle.FX.Configs
{
    [CreateAssetMenu(fileName = "New ModsConfig", menuName = "Arkanoid/FX/ModsConfig")]
    public class ModsConfig : ScriptableObject
    {
        [SerializeField] private List<ModificatorData> _modDatas;
        public IReadOnlyList<ModificatorData> ModDatas => _modDatas;
    }

    [Serializable]
    public class ModificatorData
    {
        [field: SerializeField] public ModType Type { get; private set; }
        [field: SerializeField] public float Time { get; private set; }

        [SerializeField] private List<ModType> _conflictModType;

        public IReadOnlyList<ModType> ConflictModType => _conflictModType;
    }
}
