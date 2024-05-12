using Arkanoid.Gameplay.Modificators;
using Arkanoid.Infrastracture.Pool;
using Arkanoid.Paddle;
using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class ShrinkModificator : IModificator
    {       
        public event Action<IModificator> Expired;
        public event Action<IReusable> Released;

        public ModType Type => ModType.Shrink;

        private readonly IPaddleSizeController _sizeController;

        public ShrinkModificator(IPaddleSizeController sizeController)
        {
            _sizeController = sizeController;
        }

        public void Apply()
        {
            _sizeController.Decrease();

            Expired?.Invoke(this);
        }

        public void Rollback()
        {
        }
    }
}
