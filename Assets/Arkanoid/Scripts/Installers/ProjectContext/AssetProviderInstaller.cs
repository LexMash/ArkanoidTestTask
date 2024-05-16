using Arkanoid.Infrastracture.AssetService;
using Zenject;

namespace Arkanoid.Installers
{
    public class AssetProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();
        }
    }
}
