using Arkanoid.Capsules;
using System;

namespace Arkanoid.Gameplay
{
    public interface IModsController
    {
        event Action<ModType> ModAdded;
        event Action<ModType> ModRemoved;

        void ApplyFx(ModType type);
    }
}