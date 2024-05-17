using Arkanoid.GameField;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle.FX.Configs;
using Arkanoid.PowerUPs;
using System;
using System.Linq;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class BallKeeperModificator : IModificator
    {
        public ModType Type => ModType.BallKeeper;

        public event Action<IModificator> Expired;

        private readonly BallKeeper _ballKeeper;
        private readonly ModificatorData data;
        private readonly Timer _timer;

        public BallKeeperModificator(BallKeeper ballKeeper, ModsConfig config, Timer timer)
        {
            _ballKeeper = ballKeeper;

            data = config.ModDatas.First(data => data.Type == Type);

            _timer = timer;
        }

        public void Apply()
        {
            _timer.Start(data.Time);

            _timer.Completed += OnCompleted;

            _ballKeeper.Enable();
        }

        private void OnCompleted()
        {
            Rollback();

            Expired?.Invoke(this);
        }

        public void Rollback()
        {
            _timer.Stop();

            _timer.Completed -= OnCompleted;

            _ballKeeper.Disable();
        }
    }
}
