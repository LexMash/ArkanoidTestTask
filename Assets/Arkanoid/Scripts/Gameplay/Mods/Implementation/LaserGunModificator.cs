using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle;
using Arkanoid.Paddle.FX.Configs;
using Arkanoid.PowerUPs;
using System;
using System.Linq;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class LaserGunModificator : IModificator
    {
        public ModType Type => ModType.LaserGun;

        public event Action<IModificator> Expired;

        private readonly PaddleController _paddleController;
        private readonly Timer _timer;
        private readonly ModificatorData _data;

        public LaserGunModificator(Timer timer, ModsConfig config, PaddleController paddleController)
        {
            _timer = timer;

            _data = config.ModDatas.First(data => data.Type == Type);

            _paddleController = paddleController;
        }

        public void Apply()
        {
            _timer.Completed += OnCompleted;

            _timer.Start(_data.Time);

            _paddleController.GunEnable();
        }

        private void OnCompleted()
        {
            Expired?.Invoke(this);

            Rollback();           
        }

        public void Rollback()
        {
            _timer.Completed -= OnCompleted;

            _timer.Stop();

            _paddleController.GunDisable();
        }
    }
}
