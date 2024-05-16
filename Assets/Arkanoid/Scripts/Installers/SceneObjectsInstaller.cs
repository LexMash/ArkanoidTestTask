using Arkanoid.GameField;
using UnityEngine;
using Zenject;

namespace Arkanoid.Installers
{
    public class SceneObjectsInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Arena _arena;
        [SerializeField] private BallKeeper _ballKeeper;
        [SerializeField] private ArenaBackgroundCollection _backgroundCollection;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Camera>().FromInstance(_camera).AsSingle();
            Container.BindInterfacesAndSelfTo<Arena>().FromInstance(_arena).AsSingle();
            Container.BindInterfacesAndSelfTo<BallKeeper>().FromInstance(_ballKeeper).AsSingle();
            Container.BindInterfacesAndSelfTo<ArenaBackgroundCollection>().FromInstance(_backgroundCollection).AsSingle();

            Container.BindInterfacesAndSelfTo<BackGroundService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ArenaController>().AsSingle();
        }
    }
}
