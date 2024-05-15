using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;

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
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(reference);

            await handle.Task;

            _handleMap[reference] = handle;

            return handle.Result.GetComponent<T>();
        }

        public async UniTask<List<T>> LoadPrefabs<T>(string groupName) where T : MonoBehaviour
        {
            List<T> prefabs = new();

            AsyncOperationHandle<IList<IResourceLocation>> groupHandle = Addressables.LoadResourceLocationsAsync(groupName);

            await groupHandle.Task;          

            IList<IResourceLocation> locationsList = groupHandle.Result;

            for (int i = 0; i < locationsList.Count; i++)
            {
                IResourceLocation location = locationsList[i];

                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(location.PrimaryKey);

                await handle.Task;

                T obj = handle.Result.GetComponent<T>();

                prefabs.Add(obj);
            }

            _handleMap[groupName] = groupHandle;

            return prefabs;
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

        private async UniTask<T> GetPrefab<T>(string reference, AsyncOperationHandle<GameObject> handle) where T : MonoBehaviour
        {
            handle = Addressables.LoadAssetAsync<GameObject>(reference);

            await handle.Task;

            return handle.Result.GetComponent<T>();
        }
    }
}
