using UnityEngine;

namespace Arkanoid.GameField
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        public void SetBackground(Sprite sprite) => _spriteRenderer.sprite = sprite;
    }
}
