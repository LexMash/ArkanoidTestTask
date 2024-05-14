using System;
using System.Collections.Generic;
using System.Linq;

namespace Arkanoid.Bricks
{
    public class BrickService : IDisposable
    {
        public event Action AllBricksRemoved;
        public event Action<HitBrickData> BrickHitted;
        public event Action<HitBrickData> BrickDestroyed;

        private readonly List<BrickMetaData> _metaData;
        private Dictionary<BrickView, BrickData> _brickMap = new();

        public void Init(BrickView[] bricks, BrickDTO[] brickDTOs)
        {
            ClearExistingData();

            for(int i = 0; i < bricks.Length; i++)
            {
                BrickDTO dto = brickDTOs[i];

                if (dto.IsDestroyable == false)
                    continue;

                BrickMetaData meta = GetMeta(dto.Type);

                BrickData brickData = new(dto.FxType, meta.Health);

                BrickView brick = bricks[i];

                _brickMap[brick] = brickData;

                Subscribe(brick);
            }
        }

        public void Init(Dictionary<BrickView, BrickData> brickMap)
        {
            ClearExistingData();

            _brickMap = brickMap;

            foreach (KeyValuePair<BrickView, BrickData> kvp in _brickMap)
            {
                BrickView brick = kvp.Key;

                Subscribe(brick);
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
            ClearExistingData();
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

            BrickHitted?.Invoke(new HitBrickData(brick.Type, brick.transform.position, true));
        }

        private void DestroyBrick(BrickView brick, BrickData data)
        {
            brick.Destroy();

            Unsubscribe(brick);

            BrickDestroyed?.Invoke(new HitBrickData(brick.Type, brick.transform.position, true, data.ModType));

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

        private void ClearExistingData()
        {
            foreach (KeyValuePair<BrickView, BrickData> kvp in _brickMap)
            {
                BrickView brick = kvp.Key;
                Unsubscribe(brick);
            }

            _brickMap.Clear();
        }

        private void Subscribe(BrickView brick)
        {
            brick.Hited += OnBrickHited;
            brick.Triggered += OnBrickTriggered;
        }

        private void Unsubscribe(BrickView brick)
        {
            brick.Hited -= OnBrickHited;
            brick.Triggered -= OnBrickTriggered;
        }

        private BrickMetaData GetMeta(BrickType type) => _metaData.First(m => m.Type == type);
    }
}
