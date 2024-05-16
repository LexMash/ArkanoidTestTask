using Arkanoid.Input;
using Arkanoid.Paddle;
using Arkanoid.Paddle.FX.Laser;
using UnityEngine;
using Zenject;

namespace Arkanoid.Installers
{   
    public class PaddleSystemInstaller : MonoInstaller
    {
        [SerializeField] private PaddleView _paddle;
        [SerializeField] private PaddleMobileMoveController _moveController;
        [SerializeField] private PaddleConfig _config;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PaddleView>().FromInstance(_paddle).AsSingle();
            Container.BindInterfacesAndSelfTo<PaddleMobileMoveController>().FromInstance(_moveController).AsSingle();

            Container.BindInterfacesAndSelfTo<PaddleController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ProjectileFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<PaddleConfig>().FromInstance(_config).AsSingle();
        }
    }
}
