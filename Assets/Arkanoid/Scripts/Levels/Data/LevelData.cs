using Arkanoid.Bricks;
using System;

namespace Arkanoid.Levels
{
    [Serializable]
    public class LevelData
    {
        public int LevelIndex;
        public BrickDTO[] BricksData;
    }
}
