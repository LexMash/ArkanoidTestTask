using Arkanoid.Bricks;
using Arkanoid.PowerUPs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelEditor.Editor
{
    public class EditorLevelData
    {
        private readonly BrickConfig _config;

        private readonly Dictionary<Vector2, BrickDTO> _brickMap = new();

        public EditorLevelData(BrickConfig config)
        {
            _config = config;
        }

        public void AddBrick(Vector2 position, BrickType type)
        {
            if (_brickMap.TryGetValue(position, out BrickDTO dto)) 
            {
                dto.Type = type;
            }

            _brickMap[position] = GetDTO(type, position);
        }

        public void RemoveBrick(Vector2 position)
        {
            if (_brickMap.ContainsKey(position))
            {
                _brickMap.Remove(position);
            }
        }

        public bool TryAddPowerUp(Vector2 position, ModType type)
        {
            if (_brickMap.TryGetValue(position, out BrickDTO dto))
            {
                dto.FxType = type;

                return true;
            }

            return false;
        }

        public BrickDTO[] GetData() => _brickMap.Values.ToArray();

        public void ResetData() => _brickMap.Clear();

        private BrickDTO GetDTO(BrickType type, Vector2 position)
        {
            BrickMetaData data = _config.Datas.First(d => d.Type == type);

            return new BrickDTO(type, ModType.None, position, data.IsDestroyable);
        }
    }
}
