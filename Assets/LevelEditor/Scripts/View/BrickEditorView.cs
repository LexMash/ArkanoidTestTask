using System;
using UnityEngine.EventSystems;
using UnityEngine;
using Arkanoid.Bricks;

namespace LevelEditor.View
{
    public class BrickEditorView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer _renderer;

        public event Action<Vector2, BrickType> BrickClicked;

        private BrickType _type;

        public void Setup(BrickType type, Sprite sprite)
        {
            _type = type;
            _renderer.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            BrickClicked?.Invoke(transform.position, _type);
        }
    }
}
