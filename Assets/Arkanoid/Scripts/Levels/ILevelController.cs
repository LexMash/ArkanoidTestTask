using Cysharp.Threading.Tasks;
using System;

namespace Arkanoid.Levels
{
    public interface ILevelController
    {
        event Action<string> LevelLoaded;

        UniTask Init();
        UniTask Load(int levelIndex);
        void MakeBricksHollow();
        void MakeBricksSolid();
        void Restart();
    }
}