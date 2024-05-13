using Arkanoid.Bricks;
using LevelEditor.View;
using LevelEditor.Data;
using UnityEngine;
using System.Linq;

namespace LevelEditor.Factories
{
    public class EditorBrickFactory
    {
        private readonly EditorBrickData _brickData;
        private readonly BrickEditorView _prefab;

        public EditorBrickFactory(EditorBrickData data, BrickEditorView prefab)
        {
            _brickData = data;
            _prefab = prefab;
        }

        public BrickEditorView Create(BrickType type)
        {
            BrickEditorView brick = Intantiate(_prefab, Vector2.zero);
            Sprite sprite = _brickData.Data.First(d => d.Type == type).Visual;

            brick.Setup(type, sprite);

            return brick;
        }

        private static BrickEditorView Intantiate(BrickEditorView prefab, Vector2 position)
        {
            return Object.Instantiate(prefab, position, Quaternion.identity).GetComponent<BrickEditorView>();
        }
    }
}
