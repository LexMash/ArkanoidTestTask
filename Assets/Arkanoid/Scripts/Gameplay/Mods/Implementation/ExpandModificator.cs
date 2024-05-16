using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle;
using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class ExpandModificator : IModificator
    {      
        public event Action<IModificator> Expired;
        public ModType Type => ModType.Expand;

        private readonly PaddleController _paddleController;

        public ExpandModificator(PaddleController paddleController)
        {
            _paddleController = paddleController;
        }

        public void Apply()
        {
            _paddleController.IncreaseSize();

            Expired?.Invoke(this);
        }

        public void Rollback()
        {
        }
    }
}
