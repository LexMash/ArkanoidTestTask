using System;

namespace Arkanoid
{
    public interface IDestroyable
    {
        public event Action<IDestroyable> Destroyed;
        void Destroy();
    }
}