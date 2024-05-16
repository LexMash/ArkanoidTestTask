using Arkanoid.Levels;
using Zenject;

namespace Arkanoid.Installers
{
    public class DataServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<JsonSaveLoadService>().AsSingle();
            Container.BindInterfacesAndSelfTo<NewtonJsonSerializator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameDataProvider>().AsSingle();
        }
    }
}
