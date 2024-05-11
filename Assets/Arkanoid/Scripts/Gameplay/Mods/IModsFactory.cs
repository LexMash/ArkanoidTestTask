using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;

namespace Arkanoid.Gameplay
{
    public interface IModsFactory
    {      
        IModificator Create(ModType type);
    }
}
