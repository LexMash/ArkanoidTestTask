using Arkanoid.Infrastracture;
using Cysharp.Threading.Tasks;

namespace Arkanoid.Paddle.FX.Laser
{
    public class ProjectileFactory : SimpleFactory<Projectile>
    {
        private const string REFERENCE = "Projectile";

        public ProjectileFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        public async override UniTask Init()
        {
            _objPrefab = await _assetProvider.LoadPrefab<Projectile>(REFERENCE);
        }
    }
}