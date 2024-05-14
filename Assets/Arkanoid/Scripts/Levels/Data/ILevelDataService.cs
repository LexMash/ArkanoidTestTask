using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Arkanoid.Levels
{
    public interface ILevelDataService
    {
        event Action Loaded;

        UniTask<Level> LoadLevelData(string levelName);
        UniTask<List<string>> LoadLevelList();
    }
}