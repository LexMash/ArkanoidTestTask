using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;
using System;
using System.Collections.Generic;

namespace Arkanoid.Gameplay
{
    public interface IModificators
    {      
        IModificator Get(ModType type);
    }

    public class ModificatorsHolder : IModificators, IDisposable
    {
        private readonly Dictionary<ModType, IModificator> _modificatorsMap = new();

        public ModificatorsHolder()
        {
        }

        public IModificator Get(ModType type) => _modificatorsMap[type];

        public void Dispose()
        {
            _modificatorsMap.Clear();
        }
    }
}
