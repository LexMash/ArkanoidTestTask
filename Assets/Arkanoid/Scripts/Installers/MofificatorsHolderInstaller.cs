using Arkanoid.Gameplay;
using Arkanoid.Gameplay.Mods;
using Zenject;

namespace Arkanoid.Installers
{
    public class MofificatorsHolderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            IModificators modificators = new Modificators(Container);

            Container.BindInterfacesAndSelfTo<Modificators>().FromInstance(modificators).AsSingle();
        }      
    }
}
