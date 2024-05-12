using Arkanoid.Infrastracture;
using Arkanoid.Paddle.FX.Laser;

namespace Arkanoid.Ball
{
    public class BallFactory : SimpleFactory<BallView>
    {
        public BallFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        protected override void LoadPrefab()
        {
            //_objPrefab = _assetProvider.Load
        }
    }
}
