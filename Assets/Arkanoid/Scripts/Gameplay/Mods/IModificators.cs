using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;

namespace Arkanoid.Gameplay
{
    public interface IModificators
    {
        void Init();
        IModificator Get(ModType type);
    }
}
