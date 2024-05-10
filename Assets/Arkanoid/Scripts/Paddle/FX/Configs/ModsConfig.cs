using Arkanoid.Capsules;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Paddle.FX.Configs
{
    [CreateAssetMenu(fileName = "New FxConfig", menuName = "Arkanoid/FX/FxConfig")]
    public class ModsConfig : ScriptableObject
    {
        [field: SerializeField] public List<ModificatorData> FXs { get; private set; }
    }

    [Serializable]
    public class ModificatorData
    {
        [field: SerializeField] public ModType Type { get; private set; }
        [field: SerializeField] public List<ModType> ConflictModType { get; private set; }
        [field: SerializeField] public float Time { get; private set; }
    }
}
