using DG.Tweening;
using System;
using UnityEngine;

namespace Arkanoid.Paddle
{
    public class WidthChanger : MonoBehaviour, IWidthChanger, IWidthChangeNotifier
    {
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private GameObject _visualRoot;

        [Space]
        [Header("Animation parameters")]
        [SerializeField] private float _changeSpeed = 0.3f;
        [SerializeField] private Ease _ease;

        public event Action WidthDecreased;
        public event Action WidthIncreased;

        public float Width => _collider.size.x;
        private PaddleConfig _config;
        private Tweener _tween;

        public void Construct(PaddleConfig config)
        {
            _config = config;

            Vector3 currentPaddleScale = _visualRoot.transform.localScale;
            _visualRoot.transform.localScale = new Vector3(_config.InitSize, currentPaddleScale.y, currentPaddleScale.z);

            _tween.SetAutoKill(false);
        }

        public void Increase()
        {
            if (CanIncrease())
            {
                var step = _config.SizeChangeStep;

                ChangeColliderWidth(step);
                ChangeVisualWidth(step);

                WidthIncreased?.Invoke();
            }         
        }

        public void Decrease()
        {
            if (CanDecrease())
            {
                var step = -_config.SizeChangeStep;

                ChangeColliderWidth(step);
                ChangeVisualWidth(step);

                WidthDecreased?.Invoke();
            }        
        }

        private void ChangeVisualWidth(float value)
        {
            _tween = _visualRoot.transform.DOScaleX(_visualRoot.transform.localScale.x + value, _changeSpeed).SetEase(_ease);
        }

        private void ChangeColliderWidth(float value)
        {
            Vector2 currentColliderSize = _collider.size;
            _collider.size = new Vector2(currentColliderSize.x + value, currentColliderSize.y);
        }

        private bool CanIncrease()
            => Width + _config.SizeChangeStep <= _config.MaxSize;

        private bool CanDecrease()
            => Width - _config.SizeChangeStep >= _config.MinSize;

        private void OnDestroy()
        {
            _tween.Kill();
            _config = null;
        }
    }
}
