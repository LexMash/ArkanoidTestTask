using UnityEngine;

namespace Arkanoid.GameField
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundRenderer;
        public void SetBackground(Sprite sprite) => _backgroundRenderer.sprite = sprite;
    }
}
