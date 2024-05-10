using System;

namespace Arkanoid.Infrastracture.Pool
{
    public interface IReusable
    {
        event Action<IReusable> Released;
    }
}
