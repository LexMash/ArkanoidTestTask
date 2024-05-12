using Arkanoid.Infrastracture;

namespace Arkanoid.Paddle.FX.Laser
{
    public class ProjectileFactory : SimpleFactory<Projectile>
    {
        public ProjectileFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        protected override void LoadPrefab()
        {
            //TODO
        }

        public void Set(Projectile projectile)
        {
            _objPrefab = projectile;
        }
    }
}