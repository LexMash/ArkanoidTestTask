using Arkanoid.PowerUPs;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LevelEditor.View
{
    public class PowerUpEditorView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer _renderer;

        public event Action<Vector2, ModType> PowerUpClicked;

        private ModType _type;

        public void Setup(ModType type, Sprite sprite)
        {         
            _type = type;
            _renderer.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PowerUpClicked?.Invoke(transform.position, _type);
        }
    }
}
