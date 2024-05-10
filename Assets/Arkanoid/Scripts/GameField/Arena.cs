using UnityEngine;

namespace Arkanoid.GameField
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Gate _gate;

        public void SetBackground(Sprite sprite) => _spriteRenderer.sprite = sprite;

        public void OpenGate() => _gate.Open();

        public void CloseGate() => _gate.Close();
    }
}
