using Arkanoid.Infrastracture;
using Arkanoid.Infrastracture.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.PowerUPs
{
    public class PowerUPFactory : IDisposable
    {
        private readonly IAssetProvider _assetProvider;

        private readonly Dictionary<ModType, PowerUpView> _prefabMap = new();
        private readonly Dictionary<ModType, List<PowerUpView>> _released = new();
        private readonly List<PowerUpView> _active = new();

        public PowerUPFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        //public async void Initialize()
        //{
        //    _handle = Addressables.LoadAssetAsync<GameObject>("TouchFx");
        //    await _handle.Task;
        //    var go = _handle.Result;
        //    _fx = go.GetComponent<UIVisualFxBase>();
        //}

        public PowerUpView Create(ModType type, Vector2 position)
        {
            PowerUpView powerUp = null;

            if (_released.TryGetValue(type, out List<PowerUpView> list))
            {
                powerUp = list[list.Count - 1];
                list.Remove(powerUp);

                if (list.Count == 0)
                    _released.Remove(type);
            }
            else
            {
                PowerUpView prefab = _prefabMap[type];
                powerUp = Intantiate(prefab, position);
            }

            powerUp.Released += AddToReleased;

            _active.Add(powerUp);

            return powerUp;
        }

        private static PowerUpView Intantiate(PowerUpView prefab, Vector2 position)
        {
            return UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<PowerUpView>();
        }

        private void AddToReleased(IReusable reusable)
        {
            var powerUp = reusable as PowerUpView;

            powerUp.Released -= AddToReleased;

            if (_released.TryGetValue(powerUp.ModType, out List<PowerUpView> list))
            {
                list.Add(powerUp);
            }
            else
            {
                var newList = new List<PowerUpView>();

                _released[powerUp.ModType] = newList;
               
                newList.Add(powerUp);
            }
        }

        public void Dispose()
        {
            foreach (var kvp in _released)
            {
                kvp.Value.Clear();
            }

            _prefabMap.Clear();
            _released.Clear();

            foreach(var powerUp in _active)
            {
                powerUp.Released -= AddToReleased;

                UnityEngine.Object.Destroy(powerUp.gameObject);
            }

            _active.Clear();
        }
    }
}
