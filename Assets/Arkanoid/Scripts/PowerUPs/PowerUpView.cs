using Arkanoid.Infrastracture.Pool;
using System;
using UnityEngine;

namespace Arkanoid.PowerUPs
{
    /// <summary>
    /// Капсулы с бонусами и модификаторами
    /// </summary>
    public class PowerUpView : MonoBehaviour, IReusable, IDestroyable
    {
        [field: SerializeField] public ModType ModType { get; private set; }
        [SerializeField] private float _fallSpeed = 3f;

        public event Action<IReusable> Released;
        public event Action<IDestroyable> Destroyed;

        private void Update()
        {
            transform.position += _fallSpeed * Time.deltaTime * Vector3.down;
        }

        public void Destroy()
        {
            Released?.Invoke(this);
            Destroyed?.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}
