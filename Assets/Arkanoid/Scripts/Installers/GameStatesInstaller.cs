using Zenject;
using StateMachine;

namespace Arkanoid.Installers
{
    public class GameStatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<StateChangeProvider>().AsSingle().NonLazy();
            Container.Bind<InitializeState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<RestartLevelState>().AsSingle().NonLazy();
            Container.Bind<GamePlayState>().AsSingle().NonLazy();
            Container.Bind<EndGameState>().AsSingle().NonLazy(); 
            
            Container.BindInterfacesAndSelfTo<GameStateNotifier>().AsSingle();         
        }
    }
}
