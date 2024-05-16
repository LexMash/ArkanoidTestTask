using Arkanoid.Infrastracture;

namespace Arkanoid.Paddle.FX.Laser
{
    public class ProjectileFactory : SimpleFactory<Projectile>
    {
        private const string REFERENCE = "Projectile";

        public ProjectileFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
            LoadPrefab();
        }

        protected async override void LoadPrefab()
        {
            _objPrefab = await _assetProvider.LoadPrefab<Projectile>(REFERENCE);
        }
    }
}