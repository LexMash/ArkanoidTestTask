using Arkanoid.PowerUPs;
using LevelEditor.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.UI
{
    public class PowerUpUIPanel : MonoBehaviour
    {
        [SerializeField] private PowerUpButton _prefab;
        [SerializeField] private EditorModData _data;

        public event Action<ModType> ModTypeChoused;

        private List<PowerUpButton> _buttons;

        private void Awake()
        {
            _buttons = new();

            for (int i = 0; i < _data.Data.Length; i++)
            {
                ModViewData data = _data.Data[i];

                PowerUpButton button = Instantiate(_prefab, transform);
                button.Construct(data.Type, data.Visual);
                _buttons.Add(button);
            }
        }

        private void OnEnable()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                var bttn = _buttons[i];

                bttn.ModTypeClicked += OnModTypeClicked;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                var bttn = _buttons[i];

                bttn.ModTypeClicked -= OnModTypeClicked;
            }
        }

        private void OnModTypeClicked(ModType type)
        {
            ModTypeChoused?.Invoke(type);
        }
    }
}
