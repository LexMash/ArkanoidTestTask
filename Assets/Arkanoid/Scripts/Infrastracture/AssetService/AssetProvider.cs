using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace Arkanoid.Infrastracture.AssetService
{
    public class AssetProvider : IAssetProvider, IDisposable
    {
        private readonly Dictionary<string, AsyncOperationHandle> _handleMap = new();

        public async UniTask<T> LoadAsset<T>(string reference) where T : class
        {
            AsyncOperationHandle<T> hadle = Addressables.LoadAssetAsync<T>(reference);

            await hadle.Task;

            _handleMap[reference] = hadle;

            return hadle.Result;
        }

        public async UniTask<T> LoadPrefab<T>(string reference) where T : MonoBehaviour
        {
            AsyncOperationHandle<GameObject> hadle = Addressables.LoadAssetAsync<GameObject>(reference);

            await hadle.Task;

            _handleMap[reference] = hadle;

            return hadle.Result.GetComponent<T>();
        }

        public void Release(string reference)
        {
            AsyncOperationHandle handle = _handleMap[reference];

            Addressables.Release(handle);

            _handleMap.Remove(reference);
        }

        public void Dispose()
        {
            var references = _handleMap.Keys.ToList();

            var count = references.Count;

            for(int i = 0; i < count; i++)
            {
                string reference = references[i];

                Release(reference);
            }

            _handleMap.Clear();
        }
    }
}
