using System;
using System.Collections.Generic;

namespace Arkanoid.Bricks
{
    public class BrickService : IDisposable, IBrickService, IBrickEventNotifier
    {
        public event Action AllBricksRemoved;
        public event Action<HitBrickData> OnHitBrick;
        public event Action<HitBrickData> OnDestroyBrick;       

        private Dictionary<BrickView, BrickData> _brickMap = new();

        public void Init(Dictionary<BrickView, BrickData> brickMap)
        {
            Clear();

            _brickMap = brickMap;

            foreach (KeyValuePair<BrickView, BrickData> kvp in _brickMap)
            {
                BrickView brick = kvp.Key;

                brick.Hited += OnBrickHited;
                brick.Triggered += OnBrickTriggered;
            }
        }

        public void EnableTriggerMode(bool enabled)
        {
            foreach (KeyValuePair<BrickView, BrickData> kvp in _brickMap)
            {
                kvp.Key.SetTriggerMode(enabled);
            }
        }

        public void Dispose()
        {
            Clear();
        }

        private void OnBrickHited(BrickView brick)
        {
            BrickData data = _brickMap[brick];

            ApplyHit(data);

            if (data.Health <= 0)
            {
                DestroyBrick(brick, data);
                return;
            }

            OnHitBrick?.Invoke(new HitBrickData(brick.Type, brick.transform.position, true));
        }

        private void DestroyBrick(BrickView brick, BrickData data)
        {
            brick.Destroy();

            brick.Hited -= OnBrickHited;
            brick.Triggered -= OnBrickTriggered;

            OnDestroyBrick?.Invoke(new HitBrickData(brick.Type, brick.transform.position, true, data.ModType));

            _brickMap.Remove(brick);

            if (AreAllBricksDestroed())
                AllBricksRemoved?.Invoke();
        }

        private bool AreAllBricksDestroed() => _brickMap.Count == 0;

        private void ApplyHit(BrickData data) => data.Health -= 1;

        private void OnBrickTriggered(BrickView brick)
        {
            BrickData data = _brickMap[brick];

            DestroyBrick(brick, data);
        }

        private void Clear()
        {
            foreach (KeyValuePair<BrickView, BrickData> kvp in _brickMap)
            {
                BrickView brick = kvp.Key;

                brick.Hited -= OnBrickHited;
                brick.Triggered -= OnBrickTriggered;
            }

            _brickMap.Clear();
        }
    }
}
