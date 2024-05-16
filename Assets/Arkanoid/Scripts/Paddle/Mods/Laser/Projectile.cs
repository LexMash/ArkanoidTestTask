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
                transform.position += _speed * Time.deltaTime * Vector3.up;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _notCollisioned = false;

            _visual.SetActive(false);                    

            if(_destroyFx != null)
            {
                _destroyFx.Played += OnPlayed;
                _destroyFx.Play();
            }
            else
            {
                gameObject.SetActive(false);
                Released?.Invoke(this);
            }              
        }

        private void OnPlayed()
        {
            _destroyFx.Played -= OnPlayed;

            Released?.Invoke(this);
        }
    }
}
