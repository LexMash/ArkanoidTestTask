using Arkanoid.PowerUPs;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Arkanoid.Bricks
{
    [Serializable]
    public class BrickDTO
    {
        [JsonProperty("T")]
        public BrickType Type;
        [JsonProperty("M")]
        public ModType FxType;
        [JsonProperty("X")]
        public float XPosition;
        [JsonProperty("Y")]
        public float YPosition;
        [JsonProperty("D")]
        public bool IsDestroyable;

        public BrickDTO(BrickType type, ModType fxType, Vector2 position, bool isDestroyable)
        {
            Type = type;
            FxType = fxType;

            XPosition = position.x;
            YPosition = position.y;

            IsDestroyable = isDestroyable;
        }
    }
}
