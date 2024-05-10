using Arkanoid.Capsules;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Paddle.FX.Configs
{
    [CreateAssetMenu(fileName = "New FxConfig", menuName = "Arkanoid/FX/FxConfig")]
    public class FxConfig : ScriptableObject
    {
        [field: SerializeField] public List<FxData> FXs { get; private set; }
    }

    [Serializable]
    public class FxData
    {
        [field: SerializeField] public FxType Type { get; private set; }
        [field: SerializeField] public List<FxType> СancelingEffectType { get; private set; }
        [field: SerializeField] public float Time { get; private set; }
    }
}
