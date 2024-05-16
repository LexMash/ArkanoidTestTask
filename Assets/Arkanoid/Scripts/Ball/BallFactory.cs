using Arkanoid.Infrastracture;
using Cysharp.Threading.Tasks;

namespace Arkanoid.Ball
{
    public class BallFactory : SimpleFactory<BallView>
    {
        private const string REFERENCE = "Ball";

        public BallFactory(IAssetProvider assetProvider) : base(assetProvider)
        {
        }

        public async override UniTask Init()
        {
            _objPrefab = await _assetProvider.LoadPrefab<BallView>(REFERENCE);
        }
    }
}
