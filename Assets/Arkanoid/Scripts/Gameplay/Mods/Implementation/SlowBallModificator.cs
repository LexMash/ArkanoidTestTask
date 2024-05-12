using Arkanoid.Ball;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle.FX.Configs;
using Arkanoid.PowerUPs;
using System;
using System.Linq;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class SlowBallModificator : IModificator
    {
        public ModType Type => ModType.SlowBall;

        public event Action<IModificator> Expired;

        private readonly IBallController _ballController;
        private readonly Timer _timer;
        private readonly ModificatorData _data;

        public SlowBallModificator(Timer timer, ModsConfig config, IBallController ballController)
        {
            _timer = timer;

            _data = config.ModDatas.First(data => data.Type == Type);

            _ballController = ballController;
        }

        public void Apply()
        {
            _timer.Completed += OnCompleted;

            _timer.Start(_data.Time);

            _ballController.SpeedDown();
        }

        private void OnCompleted()
        {
            Expired?.Invoke(this);

            Rollback();
        }

        public void Rollback()
        {
            _timer.Stop();

            _timer.Completed -= OnCompleted;

            _ballController.ResetSpeed();
        }
    }
}
