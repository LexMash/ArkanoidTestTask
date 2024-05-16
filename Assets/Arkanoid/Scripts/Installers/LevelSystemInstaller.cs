using Arkanoid.Bricks;
using Arkanoid.Levels;
using Arkanoid.PowerUPs;
using UnityEngine;
using Zenject;

namespace Arkanoid.Installers
{
    public class LevelSystemInstaller : MonoInstaller
    {
        [SerializeField] private BrickConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<BrickConfig>().FromInstance(_config).AsSingle();

            Container.BindInterfacesAndSelfTo<BrickService>().AsSingle();           
            Container.BindInterfacesAndSelfTo<BrickFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PowerUPFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BrickDestroyHandle>().AsSingle();

            Container.BindInterfacesAndSelfTo<LevelDataService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelController>().AsSingle().NonLazy();
        }
    }
}
