using System;

namespace Arkanoid.Gameplay.Data
{
    [Serializable]
    public class GameData : IReadOnlyGameData
    {
        public int CurrentLevelIndex;
        public int HighScore;

        int IReadOnlyGameData.CurrentLevelIndex => CurrentLevelIndex;
        int IReadOnlyGameData.HighScore => HighScore;
    }
}
