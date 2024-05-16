using Newtonsoft.Json;
using System;

namespace Arkanoid.Gameplay.Data
{
    [Serializable]
    public class GameData : IReadOnlyGameData
    {
        [JsonProperty("lvl")]
        public int CurrentLevelIndex;
        [JsonProperty("hs")]
        public int HighScore;

        int IReadOnlyGameData.CurrentLevelIndex => CurrentLevelIndex;
        int IReadOnlyGameData.HighScore => HighScore;
    }
}
