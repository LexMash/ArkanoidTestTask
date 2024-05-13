using LevelEditor.View;
using LevelEditor.Data;
using UnityEngine;
using System.Linq;
using Arkanoid.PowerUPs;

namespace LevelEditor.Factories
{
    public class EditorModFactory
    {
        private readonly EditorModData _data;
        private readonly PowerUpEditorView _prefab;

        public EditorModFactory(EditorModData data, PowerUpEditorView prefab)
        {
            _data = data;
            _prefab = prefab;
        }

        public PowerUpEditorView Create(ModType type)
        {
            PowerUpEditorView powerUp = Intantiate(_prefab, Vector2.zero);
            Sprite sprite = _data.Data.First(d => d.Type == type).Visual;

            powerUp.Setup(type, sprite);

            return powerUp;
        }

        private static PowerUpEditorView Intantiate(PowerUpEditorView prefab, Vector2 position)
        {
            return Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<PowerUpEditorView>();
        }
    }
}
