using Arkanoid.Ball;
using Arkanoid.Ball.Data;
using UnityEngine;
using Zenject;

namespace Arkanoid.Installers
{
    public class BallSystemInstaller : MonoInstaller
    {
        [SerializeField] private BallConfig _config;

        public override void InstallBindings()
        {
            Container.Bind<BallConfig>().FromInstance(_config).AsSingle();

            Container.BindInterfacesAndSelfTo<BallController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BallFactory>().AsSingle().NonLazy();
        }
    }
}
