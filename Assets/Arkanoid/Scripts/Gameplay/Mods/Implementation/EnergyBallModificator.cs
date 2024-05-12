using Arkanoid.Gameplay.Modificators;
using Arkanoid.Levels;
using Arkanoid.Paddle.FX.Configs;
using Arkanoid.PowerUPs;
using System;
using System.Linq;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class EnergyBallModificator : IModificator
    {
        public ModType Type => ModType.EnergyBall;

        public event Action<IModificator> Expired;

        private readonly ILevelController _levelController;
        private readonly Timer _timer;
        private readonly ModificatorData _data;        

        public EnergyBallModificator(Timer timer, ModsConfig config, ILevelController levelController)
        {
            _timer = timer;

            _data = config.ModDatas.First(data => data.Type == Type);

            _levelController = levelController;
        }

        public void Apply()
        {
            _timer.Completed += OnCompleted;

            _timer.Start(_data.Time);

            _levelController.CollisionEnable(false);
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

            _levelController.CollisionEnable(true);
        }
    }
}
