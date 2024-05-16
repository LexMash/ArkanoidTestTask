using Arkanoid.Gameplay.Modificators;
using Arkanoid.Gameplay.Mods.Implementation;
using Arkanoid.PowerUPs;
using System.Collections.Generic;
using Zenject;

namespace Arkanoid.Gameplay.Mods
{
    public class Modificators : IModificators
    {
        private readonly DiContainer _container;

        private readonly Dictionary<ModType, IModificator> _modMap = new();

        public Modificators(DiContainer container)
        {
            _container = container;          
        }

        public void Init()
        {
            _modMap[ModType.Expand] = _container.Resolve<ExpandModificator>();
            _modMap[ModType.SplitBall] = _container.Resolve<SplitBallModificator>();
            _modMap[ModType.LaserGun] = _container.Resolve<LaserGunModificator>();
            _modMap[ModType.SlowBall] = _container.Resolve<SlowBallModificator>();
            _modMap[ModType.BallKeeper] = _container.Resolve<BallKeeperModificator>();
            _modMap[ModType.Magnet] = _container.Resolve<MagnetModificator>();
            _modMap[ModType.Shrink] = _container.Resolve<ShrinkModificator>();
            _modMap[ModType.EnergyBall] = _container.Resolve<EnergyBallModificator>();
        }

        public IModificator Get(ModType type)
        {
            return _modMap[type];
        }
    }
}
