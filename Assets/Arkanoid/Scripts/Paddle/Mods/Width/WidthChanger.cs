using DG.Tweening;
using UnityEngine;

namespace Arkanoid.Paddle
{
    public class WidthChanger : MonoBehaviour, IPaddleSizeController
    {
        [SerializeField] private CapsuleCollider2D _collider;
        [SerializeField] private SpriteRenderer _visual;

        [Space]
        [Header("Animation parameters")]
        [SerializeField] private float _changeSizeSpeed = 0.3f;
        [SerializeField] private Ease _ease;

        public float Width => _collider.size.x;

        private PaddleConfig _config;
        private Tween _scaleTween;
       
        public void Construct(PaddleConfig config)
        {
            _config = config;

            _scaleTween.SetAutoKill(false);
        }

        public void SetInitialSize()
        {
            _visual.size = new Vector3(_config.InitSize, _visual.size.y);
        }

        public void Increase()
        {
            var step = _config.SizeChangeStep;

            ChangeColliderWidth(step);
            ChangeVisualWidth(step);
        }

        public void Decrease()
        {
            var step = -_config.SizeChangeStep;

            ChangeColliderWidth(step);
            ChangeVisualWidth(step);
        }

        private void ChangeVisualWidth(float value)
        {
            Vector2 size = _visual.size;

            _scaleTween = DOVirtual.Float(size.x, size.x + value, _changeSizeSpeed, SetWidth).SetEase(_ease);

            //я не одобряю использование вложенных методов, но тут уже слишком много всего
            void SetWidth(float value) => _visual.size = new Vector2(value, size.y);
        }

        private void ChangeColliderWidth(float value)
        {
            Vector2 currentColliderSize = _collider.size;

            _collider.size = new Vector2(currentColliderSize.x + value, currentColliderSize.y);
        }

        public bool CanIncrease()
            => Width + _config.SizeChangeStep <= _config.MaxSize;

        public bool CanDecrease()
            => Width - _config.SizeChangeStep >= _config.MinSize;

        private void OnDestroy()
        {
            _scaleTween.Kill();
            _config = null;
        }
    }
}
