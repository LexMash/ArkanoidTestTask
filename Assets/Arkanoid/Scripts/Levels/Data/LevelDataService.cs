﻿using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Arkanoid.Levels
{
    public class LevelDataService : ILevelDataService
    {
        public event Action Loaded;

        private const string EXTENSION = ".lvl";

        private readonly ISerializationService _serializator;

        private readonly string _levelsPath;
        private readonly string _listPath;

        public LevelDataService(ISerializationService serializator)
        {
            _serializator = serializator;

            _levelsPath = Application.streamingAssetsPath + "/Levels/";
            _listPath = Application.streamingAssetsPath + "/Levels" + EXTENSION;
        }

        public async UniTask<Level> LoadLevelData(string levelName)
        {
            string path = BuildLevelPath(levelName);

            Level loadedData = null;
            string json = string.Empty;

            if (Application.platform == RuntimePlatform.Android)
            {
                using (UnityWebRequest www = UnityWebRequest.Get(path))
                {
                    await UniTask.WaitWhile(() => www.SendWebRequest().webRequest.isDone);

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        json = www.downloadHandler.text;
                    }
                }
            }
            else
            {
                json = File.ReadAllText(path);
            }

            loadedData = _serializator.Deserialize<Level>(json);

            Loaded?.Invoke();

            return loadedData;
        }

        public async UniTask<List<string>> LoadLevelList()
        {
            List<string> levelList = new();
            string json = string.Empty;

            if (Application.platform == RuntimePlatform.Android)
            {
                using (UnityWebRequest www = UnityWebRequest.Get(_listPath))
                {
                    await UniTask.WaitUntil(() => www.SendWebRequest().webRequest.isDone);

                    if (www.result == UnityWebRequest.Result.Success)
                    {
                        json = www.downloadHandler.text;
                    }
                }
            }
            else
            {
                json = File.ReadAllText(_listPath);
            }

            string[] levels = _serializator.Deserialize<string[]>(json);

            levelList.AddRange(levels);

            Loaded?.Invoke();

            return levelList;
        }

        private string BuildLevelPath(string levelName)
            => System.IO.Path.Combine(_levelsPath, levelName + EXTENSION);
    }
}
