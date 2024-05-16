using Arkanoid.Bricks;
using Arkanoid.Infrastracture;
using Arkanoid.Infrastracture.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Levels
{
    public class BrickFactory : IDisposable
    {
        private const string REFERENCE = "bricks";

        private readonly IAssetProvider _assetProvider;
        private readonly Dictionary<BrickType, BrickView> _prefabMap = new();
        private readonly Dictionary<BrickType, List<BrickView>> _released = new();
        private readonly List<BrickView> _active = new();

        public BrickFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;

            Load();
        }

        private async void Load()
        {
            List<BrickView> brickViews = await _assetProvider.LoadPrefabs<BrickView>(REFERENCE);

            for (int i = 0; i < brickViews.Count; i++)
            {
                BrickView brickView = brickViews[i];

                _prefabMap[brickView.Type] = brickView;
            }
        }

        public BrickView Create(BrickType type, Vector2 position)
        {
            BrickView brick = null;

            if (_released.TryGetValue(type, out List<BrickView> list))
            {
                brick = list[list.Count - 1];
                list.Remove(brick);

                if (list.Count == 0)
                    _released.Remove(type);
            }
            else
            {
                BrickView prefab = _prefabMap[type];
                brick = Intantiate(prefab, position);
            }

            brick.Released += AddToReleased;

            _active.Add(brick);

            return brick;
        }

        private void AddToReleased(IReusable reusable)
        {
            var brick = reusable as BrickView;

            brick.Released -= AddToReleased;

            if (_released.TryGetValue(brick.Type, out List<BrickView> list))
            {
                list.Add(brick);
            }
            else
            {
                var newList = new List<BrickView>();

                _released[brick.Type] = newList;

                newList.Add(brick);
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

            foreach (var brick in _active)
            {
                brick.Released -= AddToReleased;

                UnityEngine.Object.Destroy(brick.gameObject);
            }

            _assetProvider.Release(REFERENCE);

            _active.Clear();
        }

        private static BrickView Intantiate(BrickView prefab, Vector2 position)
        {
            return UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
