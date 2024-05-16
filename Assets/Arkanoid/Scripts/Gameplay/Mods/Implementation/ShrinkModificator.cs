using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle;
using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class ShrinkModificator : IModificator
    {       
        public event Action<IModificator> Expired;
        public ModType Type => ModType.Shrink;

        private readonly PaddleController _paddleController;

        public ShrinkModificator(PaddleController paddleController)
        {
            _paddleController = paddleController;
        }

        public void Apply()
        {
            _paddleController.DecreaseSize();

            Expired?.Invoke(this);
        }

        public void Rollback()
        {
        }
    }
}
