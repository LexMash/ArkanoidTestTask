using Arkanoid.Levels;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelEditor.Editor
{
    public class LevelDataService : ILevelDataService
    {
        public event Action Loaded;
        public event Action Saved;

        private readonly string _mainPath;

        public LevelDataService()
        {
            _mainPath = Application.streamingAssetsPath;
        }

        public void SaveLevelList(IEnumerable<string> levels)
        {
            string path = _mainPath + "/Levels.lvl";

            string json = JsonConvert.SerializeObject(levels.ToArray());

            File.WriteAllText(path, json);

            Saved?.Invoke();
        }

        public List<string> LoadLevelList()
        {
            string path = _mainPath + "/Levels.lvl";

            List<string> levelList = new();

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);

                levelList.AddRange(JsonConvert.DeserializeObject<string[]>(json));

                Loaded?.Invoke();
            }

            return levelList;
        }

        public void SaveLevel(LevelData levelData)
        {
            string path = BuildPath(levelData.Name, "/Levels/");

            string json = JsonConvert.SerializeObject(levelData);

            File.WriteAllText(path, json);

            Saved?.Invoke();
        }

        public LevelData LoadLevelData(string name)
        {
            string path = BuildPath(name, "/Levels/");

            LevelData loadedData = null;

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);

                loadedData = JsonConvert.DeserializeObject<LevelData>(json);

                Loaded?.Invoke();

                return loadedData;
            }

            Debug.LogError($"Level with name {name} not exists");

            return loadedData;
        }

        private string BuildPath(string levelName, string subFolderName)
        {
            return Path.Combine(_mainPath + subFolderName, levelName + ".lvl");
        }
    }
}
