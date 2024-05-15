using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Infrastracture
{
    public interface IAssetProvider
    {
        public UniTask<T> LoadAsset<T>(string reference) where T : class;
        public UniTask<T> LoadPrefab<T>(string reference) where T : MonoBehaviour;
        public UniTask<List<T>> LoadPrefabs<T>(string groupName) where T : MonoBehaviour;
        public void Release(string reference);
    }
}
