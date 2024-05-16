using System;

namespace Arkanoid.Levels
{
    public interface ILevelController
    {
        event Action<string> LevelLoaded;

        void Init();
        void Load(int levelIndex);
        void MakeBricksHollow();
        void MakeBricksSolid();
        void Restart();
    }
}