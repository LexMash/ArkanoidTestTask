using Arkanoid.Bricks;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelEditor.UI
{
    public class BrickButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;

        public event Action<BrickType> BrickTypeClicked;

        private BrickType _type;

        public void Construct(BrickType type, Sprite sprite)
        {
            _type = type;
            _image.sprite = sprite;
            _image.preserveAspect = true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            BrickTypeClicked?.Invoke(_type);
        }
    }
}
