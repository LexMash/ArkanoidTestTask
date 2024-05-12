using Arkanoid.Bricks;
using Arkanoid.Infrastracture.Pool;
using System;
using UnityEngine;

namespace Arkanoid.Paddle.FX.Laser
{
    public class Projectile : MonoBehaviour, IReusable
    {
        [SerializeField] private DestroyFxBase _destroyFx;
        [SerializeField] private GameObject _visual;
        [SerializeField] private float _speed = 15f;
       
        public event Action<IReusable> Released;

        private bool _notCollisioned;

        private void OnEnable()
        {
            _visual.SetActive(true);
            _notCollisioned = true;
        }

        private void Update()
        {
            if(_notCollisioned)
                transform.position += Vector3.up * _speed * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _notCollisioned = false;

            _visual.SetActive(false);
            gameObject.SetActive(false);
            Released?.Invoke(this);//test

            _destroyFx.Played += OnPlayed;
            _destroyFx.Play();
        }

        private void OnPlayed()
        {
            _destroyFx.Played -= OnPlayed;
            Released?.Invoke(this);
        }

        private void Reset()
        {
            _destroyFx = GetComponent<DestroyFxBase>();
        }
    }
}
