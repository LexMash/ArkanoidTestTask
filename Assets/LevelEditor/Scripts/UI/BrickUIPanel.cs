using Arkanoid.Bricks;
using LevelEditor.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.UI
{
    public class BrickUIPanel : MonoBehaviour
    {
        [SerializeField] private BrickButton _prefab;
        [SerializeField] private EditorBrickData _data;

        public event Action<BrickType> BrickTypeChoused;

        private List<BrickButton> _buttons;

        private void Awake()
        {
            _buttons = new();

            for (int i = 0; i < _data.Data.Length; i++)
            {
                BrickViewData data = _data.Data[i];

                BrickButton button = Instantiate(_prefab, transform);
                button.Construct(data.Type, data.Visual);
                _buttons.Add(button);
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                var bttn = _buttons[i];

                bttn.BrickTypeClicked += OnBrickTypeClicked;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                var bttn = _buttons[i];

                bttn.BrickTypeClicked -= OnBrickTypeClicked;
            }
        }

        private void OnBrickTypeClicked(BrickType type)
        {
            BrickTypeChoused?.Invoke(type);
        }
    }
}
