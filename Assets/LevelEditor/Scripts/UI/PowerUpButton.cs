using Arkanoid.PowerUPs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelEditor.UI
{
    public class PowerUpButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;

        public event Action<ModType> ModTypeClicked;

        private ModType _type;

        public void Construct(ModType type, Sprite sprite)
        {
            _type = type;
            _image.sprite = sprite;
            _image.preserveAspect = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ModTypeClicked?.Invoke(_type);
        }
    }
}
