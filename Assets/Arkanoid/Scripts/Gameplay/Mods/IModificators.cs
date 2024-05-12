using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;

namespace Arkanoid.Gameplay
{
    public interface IModificators
    {      
        IModificator Get(ModType type);
    }
}
