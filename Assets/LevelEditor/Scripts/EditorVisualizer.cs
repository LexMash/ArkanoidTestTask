using Arkanoid.Bricks;
using Arkanoid.Levels;
using Arkanoid.PowerUPs;
using LevelEditor.Data;
using LevelEditor.Factories;
using LevelEditor.View;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class EditorVisualizer : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private EditorBrickData _brickData;
        [SerializeField] private EditorModData _modData;
        [SerializeField] private BrickEditorView _brickPrefab;
        [SerializeField] private PowerUpEditorView _modPrefab;

        private EditorBrickFactory _brickFactory;
        private EditorModFactory _modFactory;

        private GameObject _currentObject;
        private Dictionary<Vector2, List<GameObject>> _objectMap = new();

        private void Awake()
        {
            _brickFactory = new EditorBrickFactory(_brickData, _brickPrefab);
            _modFactory = new EditorModFactory(_modData, _modPrefab);
        }

        public void Setup(LevelData data)
        {
            _objectMap.Clear();

            for (int i = 0; i < data.BricksData.Length; i++)
            {
                BrickDTO brickData = data.BricksData[i];

                var position = new Vector2(brickData.XPosition, brickData.YPosition);

                var brick = _brickFactory.Create(brickData.Type);
                brick.transform.position = position;

                var mod = _modFactory.Create(brickData.FxType);
                mod.transform.position = position;

                _objectMap[position] = new List<GameObject>()
                {
                    brick.gameObject, mod.gameObject
                };
            }
        }

        public void SetBrick(BrickType type)
        {
            Destroy(_currentObject);

            _currentObject = _brickFactory.Create(type).gameObject;
        }

        public void SetMod(ModType type)
        {
            Destroy(_currentObject);

            _currentObject = _modFactory.Create(type).gameObject;
        }

        public void PlaceObject(Vector2 position)
        {
            if (!CanPlace(position))
                return;

            if (_objectMap.ContainsKey(position) == false)
            {
                _objectMap[position] = new List<GameObject>();
            }

            _currentObject.transform.position = position;

            _objectMap[position].Add(_currentObject);

            ResetCurrentObject();
        }

        private bool CanPlace(Vector2 position) => _objectMap.ContainsKey(position) == false || _objectMap[position].Count < 2;

        public void RemoveObject(Vector2 position)
        {
            if (_objectMap.ContainsKey(position))
            {
                var objects = _objectMap[position];

                foreach (var obj in objects)
                {
                    Destroy(obj);
                }

                objects.Clear();
            }
        }

        public void ResetCurrentObject()
        {
            _currentObject = null;
        }

        public void DestroyAll()
        {
            foreach(var kvp in _objectMap)
            {
                foreach (var obj in kvp.Value)
                {
                    Destroy(obj);
                }

                kvp.Value.Clear();
            }

            _objectMap.Clear();
        }

        private void Update()
        {
            if (_currentObject == null)
                return;

            _currentObject.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);

        }
    }
}
