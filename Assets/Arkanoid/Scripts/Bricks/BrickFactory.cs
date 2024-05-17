using Arkanoid.Bricks;
using Arkanoid.Infrastracture;
using Arkanoid.Infrastracture.Pool;
using Cysharp.Threading.Tasks;
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
        private readonly Dictionary<BrickType, Queue<BrickView>> _released = new();
        private readonly List<BrickView> _active = new();

        public BrickFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public async UniTask Init()
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

            if (_released.TryGetValue(type, out Queue<BrickView> queue))
            {
                brick = queue.Dequeue();

                brick.gameObject.transform.position = position;

                if(queue.Count == 0)
                    _released.Remove(type);
            }
            else
            {
                BrickView prefab = _prefabMap[type];

                brick = Intantiate(prefab, position);
            }

            brick.gameObject.SetActive(true);

            brick.Released += AddToReleased;

            _active.Add(brick);

            return brick;
        }

        private void AddToReleased(IReusable reusable)
        {
            var brick = reusable as BrickView;

            brick.Released -= AddToReleased;

            _active.Remove(brick);

            brick.gameObject.SetActive(false);

            if (_released.TryGetValue(brick.Type, out Queue<BrickView> queue))
            {
                queue.Enqueue(brick);
            }
            else
            {
                var newQueue = new Queue<BrickView>();

                _released[brick.Type] = newQueue;

                newQueue.Enqueue(brick);
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
            _active.Clear();
        }

        private static BrickView Intantiate(BrickView prefab, Vector2 position)
        {
            return UnityEngine.Object.Instantiate(prefab, position, Quaternion.identity);
        }
    }
}
