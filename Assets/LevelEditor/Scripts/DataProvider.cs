using Arkanoid.Levels;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Arkanoid.Gameplay.Data;

namespace LevelEditor.Editor
{
    public class DataProvider
    {
        private readonly string _mainPath;

        public DataProvider()
        {
            _mainPath = Application.streamingAssetsPath;
        }

        public LevelData LoadLevelData(string name)
        {
            string path = BuildPath(name, "/Levels/");

            LevelData loadedData = null;

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);

                loadedData = JsonConvert.DeserializeObject<LevelData>(json);

                return loadedData;
            }

            Debug.LogError($"Level with name {name} not exists");

            return loadedData;
        }

        private string BuildPath(string levelName, string subFolderName)
        {
            return Path.Combine(_mainPath + subFolderName, levelName + ".lvl");
        }

        public void SaveLevel(LevelData levelData)
        {
            string path = BuildPath(levelData.Name, "/Levels/");

            string json = JsonConvert.SerializeObject(levelData);

            File.WriteAllText(path, json);
        }
    }
}
