using Arkanoid.PowerUPs;
using Arkanoid.Paddle.FX.Configs;
using System;

namespace Arkanoid.Gameplay
{
    public interface IModificatorNotifier
    {
        event Action<ModificatorData> ModAdded;
        event Action<ModificatorData> ModRemoved;
    }
}