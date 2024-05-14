using Arkanoid.Bricks;
using Newtonsoft.Json;
using System;

namespace Arkanoid.Levels
{
    [Serializable]
    public class Level
    {
        public readonly string Name;
        [JsonProperty("Data")]
        public readonly BrickDTO[] BricksData;

        public Level(string name, BrickDTO[] bricksData)
        {
            Name = name;
            BricksData = bricksData;
        }
    }
}
