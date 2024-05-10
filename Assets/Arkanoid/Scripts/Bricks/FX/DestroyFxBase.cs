using System;
using UnityEngine;

namespace Arkanoid.Bricks
{
    public abstract class DestroyFxBase : MonoBehaviour
    {
        public event Action Played;

        public virtual void Play() 
            => Played?.Invoke();
    }
}