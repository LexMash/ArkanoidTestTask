using System;

namespace Arkanoid.Levels
{
    public interface ILevelController
    {
        event Action<string> LevelLoaded;

        void Load(string levelName);
        void MakeBricksHollow();
        void MakeBricksSolid();
        void Restart();
    }
}