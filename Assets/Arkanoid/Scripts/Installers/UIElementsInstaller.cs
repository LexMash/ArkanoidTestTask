using Arkanoid.Input;
using Arkanoid.UI;
using Arkanoid.UI.LifeBar;
using UnityEngine;
using Zenject;

namespace Arkanoid.Installers
{
    public class UIElementsInstaller : MonoInstaller
    {
        [SerializeField] private ScorePanel _scorePanel;
        [SerializeField] private RestartButton _restartBttn;
        [SerializeField] private LifeBarPanel _lifeBarPanel;
        [SerializeField] private TouchZone _touchZone;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScorePanel>().FromInstance(_scorePanel).AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle().NonLazy();

            Container.Bind<RestartButton>().FromInstance(_restartBttn).AsSingle();

            Container.Bind<LifeBarPanel>().FromInstance(_lifeBarPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<LivesController>().AsSingle().NonLazy();

            Container.Bind<IInput>().To<TouchZone>().FromInstance(_touchZone).AsSingle();
        }
    }
}
