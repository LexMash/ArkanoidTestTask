using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Gameplay.Modificators
{
    public interface IModificator
    {
        event Action<IModificator> Expired;

        ModType Type { get; }

        void Apply();
        void Rollback();
    }
}
