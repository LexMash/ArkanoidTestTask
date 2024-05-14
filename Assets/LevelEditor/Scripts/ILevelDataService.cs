using Arkanoid.Levels;

namespace LevelEditor.Editor
{
    public interface ILevelDataService
    {
        LevelData LoadLevelData(string name);
        void SaveLevel(LevelData levelData);
    }
}