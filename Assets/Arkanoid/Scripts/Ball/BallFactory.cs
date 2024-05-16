using Arkanoid.Infrastracture;

namespace Arkanoid.Ball
{
    public class BallFactory : SimpleFactory<BallView>
    {
        private const string REFERENCE = "Ball";

        public BallFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        public async override void Init()
        {
            _objPrefab = await _assetProvider.LoadPrefab<BallView>(REFERENCE);
        }
    }
}
