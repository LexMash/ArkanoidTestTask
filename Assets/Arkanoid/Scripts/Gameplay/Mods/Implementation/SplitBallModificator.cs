using Arkanoid.Ball;
using Arkanoid.Ball.Data;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Infrastracture.Pool;
using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class SplitBallModificator : IModificator
    {
        public ModType Type => ModType.SplitBall;

        public event Action<IModificator> Expired;
        public event Action<IReusable> Released;

        private readonly IBallController _ballController;
        private readonly BallConfig _config;

        public SplitBallModificator(IBallController ballController, BallConfig config)
        {
            _ballController = ballController;
            _config = config;
        }

        public void Apply()
        {
            int amount = _config.SplitAmount;

            for (int i = 0; i < amount; i++)
            {
                _ballController.AddBall();
            }
            
            Expired?.Invoke(this);
        }

        public void Rollback()
        {
        }
    }
}
