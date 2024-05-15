using Arkanoid.Levels;
using System;
using System.IO;
using UnityEngine;

namespace Arkanoid
{
    public class JsonSaveLoadService : ISaveLoadService
    {
        private readonly string _dataPath;
        private readonly ISerializationService _serializator;

        public JsonSaveLoadService(ISerializationService serializator)
        {
            _dataPath = Application.persistentDataPath;
            _serializator = serializator;
        }

        public T Load<T>(string fileName) where T : class
        {
            string path = BuildPath(fileName);

            T loadedData = default;

            if (File.Exists(path))
            {
                using (var fileStream = new StreamReader(path))
                {
                    var json = fileStream.ReadToEnd();

                    loadedData = _serializator.Deserialize<T>(json);
                }
            }

            return loadedData;
        }

        public void Save(object data, string fileName, Action onSaveCallback = null)
        {
            string path = BuildPath(fileName);

            string json = _serializator.Serialize(data);

            using (var fileStream = new StreamWriter(path))
            {
                fileStream.Write(json);
            }

            onSaveCallback?.Invoke();
        }

        private string BuildPath(string fileName) => Path.Combine(_dataPath, fileName);
    }
}
