using UnityEngine;

namespace Arkanoid.Infrastracture
{
    public interface IFactory<T> where T : class
    {
        T Create();
        T Create(Vector3 position);
    }
}