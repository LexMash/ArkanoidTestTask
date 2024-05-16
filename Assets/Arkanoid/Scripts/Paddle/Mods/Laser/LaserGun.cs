using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

namespace Arkanoid.Paddle.FX.Laser
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private Transform[] _projectileOrigins;
        [SerializeField] private Transform _visualRoot;
        [SerializeField] private float _enabledPositionY = 1f;
        [SerializeField] private float _animSpeed = 0.5f;
        [SerializeField] private Ease _ease;

        public event Action Fired;

        private bool _isEnabled;
        private PaddleConfig _config;
        private ProjectileFactory _factory;

        private Vector3 _initPosition;
        
        private Tween _tweenEnable;
        private Tween _tweenDisable;

        private float _counter;
        private float _fireInterval;

        [Inject]
        public void Construct(PaddleConfig config, ProjectileFactory factory)
        {
            _config = config;
            _factory = factory;

            _initPosition = _visualRoot.position;

            _tweenEnable.SetAutoKill(false);
            _tweenDisable.SetAutoKill(false);

            _fireInterval = 60f/_config.BulletPerMinute;

            _tweenEnable = _visualRoot.DOMoveY(_enabledPositionY, _animSpeed).SetEase(_ease).OnComplete(() => _isEnabled = true);
            _tweenEnable.Pause();

            _tweenDisable = _visualRoot.DOMoveY(_initPosition.y, _animSpeed).SetEase(_ease);
            _tweenDisable.Pause();
        }

        public void Enable()
        {
            _tweenEnable.Restart();
        }

        public void Disable()
        {
            _tweenDisable.Restart();

            _isEnabled = false;
        }

        private void Update()
        {
            if (_isEnabled == false)
                return;

            _counter -= Time.deltaTime;

            if(_counter <= 0)
            {
                _counter = _fireInterval;

                Fire();
            }
        }

        private void Fire()
        {          
            for (int i = 0; i < _projectileOrigins.Length; i++)
            {
                Transform origin = _projectileOrigins[i];
                Projectile projectile = _factory.Create();
                projectile.transform.position = origin.position;
            }

            Fired?.Invoke();
        }

        private void OnDestroy()
        {
            _tweenEnable.Kill();
            _tweenDisable.Kill();
        }
    }
}
