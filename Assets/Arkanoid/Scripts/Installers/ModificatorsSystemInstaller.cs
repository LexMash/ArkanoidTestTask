using Arkanoid.Gameplay;
using Arkanoid.Gameplay.Mods.Implementation;
using Arkanoid.Paddle.FX.Configs;
using UnityEngine;
using Zenject;

namespace Arkanoid.Installers
{
    public class ModificatorsSystemInstaller : MonoInstaller
    {
        [SerializeField] private ModsConfig _config;

        public override void InstallBindings()
        {           
            Container.BindInterfacesAndSelfTo<ModsController>().AsSingle().NonLazy();            

            Container.Bind<BallKeeperModificator>().AsSingle().NonLazy();
            Container.Bind<EnergyBallModificator>().AsSingle().NonLazy();
            Container.Bind<ExpandModificator>().AsSingle().NonLazy();
            Container.Bind<MagnetModificator>().AsSingle().NonLazy();
            Container.Bind<ShrinkModificator>().AsSingle().NonLazy();
            Container.Bind<SlowBallModificator>().AsSingle().NonLazy();
            Container.Bind<SplitBallModificator>().AsSingle().NonLazy();
            Container.Bind<LaserGunModificator>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<Timer>().AsCached();
            Container.BindInterfacesAndSelfTo<ModsConfig>().FromInstance(_config).AsSingle();
        }
    }
}
