using Arkanoid.Infrastracture.Pool;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

namespace Arkanoid.Infrastracture
{
    public abstract class SimpleFactory<TObject> : IDisposable, IFactory<TObject> where TObject : MonoBehaviour, IReusable
    {
        protected readonly IAssetProvider _assetProvider;

        protected readonly List<TObject> _active = new();
        protected readonly List<TObject> _released = new();

        protected TObject _objPrefab;

        public SimpleFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public abstract UniTask Init();

        public TObject Create(Vector3 position)
        {
            TObject obj = default;

            if (_released.Count != 0)
            {
                obj = _released[^1];

                _released.Remove(obj);

                obj.gameObject.SetActive(true);
            }
            else
            {
                obj = Intantiate(position);
            }           

            obj.Released += AddToReleased;

            _active.Add(obj);
          
            return obj;
        }

        public TObject Create() => Create(Vector3.zero);

        public void Dispose()
        {
            foreach (TObject obj in _active)
            {
                obj.Released -= AddToReleased;
            }

            _active.Clear();
            _released.Clear();
        }

        protected TObject Intantiate(Vector3 position)
        {
            return UnityEngine.Object.Instantiate(_objPrefab, position, Quaternion.identity).GetComponent<TObject>();
        }

        private void AddToReleased(IReusable reusable)
        {
            var obj = (TObject) reusable;

            obj.Released -= AddToReleased;

            obj.gameObject.SetActive(false);

            _active.Remove(obj);
            _released.Add(obj);
        }
    }
}