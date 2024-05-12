using Arkanoid.Gameplay.Modificators;
using Arkanoid.Infrastracture.Pool;
using Arkanoid.Paddle;
using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class ExpandModificator : IModificator
    {      
        public event Action<IModificator> Expired;
        public event Action<IReusable> Released;
        public ModType Type => ModType.Expand;

        private readonly IPaddleSizeController _sizeController;

        public ExpandModificator(IPaddleSizeController sizeController)
        {
            _sizeController = sizeController;
        }

        public void Apply()
        {
            _sizeController.Increase();

            Expired?.Invoke(this);
        }

        public void Rollback()
        {
        }
    }
}
