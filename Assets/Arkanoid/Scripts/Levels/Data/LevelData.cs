using Arkanoid.Bricks;
using Newtonsoft.Json;
using System;

namespace Arkanoid.Levels
{
    [Serializable]
    public class LevelData
    {
        public readonly string Name;
        [JsonProperty("Data")]
        public readonly BrickDTO[] BricksData;

        public LevelData(string name, BrickDTO[] bricksData)
        {
            Name = name;
            BricksData = bricksData;
        }
    }
}
