using Arkanoid.PowerUPs;
using Arkanoid.Infrastracture.Pool;
using System;

namespace Arkanoid.Gameplay.Modificators
{
    public interface IModificator : IReusable, IDisposable
    {
        event Action<IModificator> Expired;

        ModType Type { get; }

        void Apply();
        void Reapply();
        void Rollback();
    }
}
