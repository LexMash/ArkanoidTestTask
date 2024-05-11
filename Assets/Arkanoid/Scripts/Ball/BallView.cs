using Arkanoid.Infrastracture.Pool;
using System;
using UnityEngine;

namespace Arkanoid.Ball
{
    /// <summary>
    /// View шара
    /// </summary>
    public class BallView : MonoBehaviour, IReusable, IDestroyable
    {
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public CollisionDetector CollisionDetector { get; private set; }

        public event Action<IReusable> Released;
        public event Action<IDestroyable> Destroyed;

        public void Destroy()
        {
            Released?.Invoke(this);
            Destroyed?.Invoke(this);
            
            gameObject.SetActive(false);
        }
    }
}
