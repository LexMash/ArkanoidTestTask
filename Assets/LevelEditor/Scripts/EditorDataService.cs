using Arkanoid.Levels;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace LevelEditor.Editor
{
    public class EditorDataService
    {
        public event Action Loaded;
        public event Action Saved;

        private readonly string _mainPath;

        public EditorDataService()
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

        public void SaveLevel(Level levelData)
        {
            string path = BuildPath(levelData.Name, "/Levels/");

            string json = JsonConvert.SerializeObject(levelData);

            File.WriteAllText(path, json);

            Saved?.Invoke();
        }

        public async UniTask<Level> LoadLevelData(string name)
        {
            string path = BuildPath(name, "/Levels/");

            Level loadedData = null;
            string json = string.Empty;

            using (UnityWebRequest www = UnityWebRequest.Get(path))
            {
                await UniTask.WaitUntil(() => www.SendWebRequest().webRequest.isDone);

                if(www.result == UnityWebRequest.Result.Success)
                {
                    json = www.downloadHandler.text;

                    loadedData = JsonConvert.DeserializeObject<Level>(json);
                }
            }
           
            Loaded?.Invoke();

            return loadedData;
        }

        private string BuildPath(string levelName, string subFolderName)
        {
            return Path.Combine(_mainPath + subFolderName, levelName + ".lvl");
        }
    }
}
