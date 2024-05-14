using Arkanoid.Infrastracture;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Arkanoid.GameField
{
    public class ArenaController
    {
        private readonly Arena _arena;
        private readonly ArenaBackgroundCollection _backgrounds;
        private readonly IAssetProvider _assetProvider;

        private Sprite _currentBackground;

        public ArenaController(Arena arena, ArenaBackgroundCollection backgrounds, IAssetProvider assetProvider)
        {
            _arena = arena;
            _backgrounds = backgrounds;
            _assetProvider = assetProvider;

            ChangeBackground();
        }

        public void ChangeBackground()
        {
            var index = Random.Range(0, _backgrounds.Sprites.Count);

            Addressables.Release(_currentBackground);

            var reference = _backgrounds.Sprites[index];

            var sprite = Addressables.LoadAssetAsync<Sprite>(reference.AssetGUID).Result;

            _currentBackground = sprite;
        }
    }
}
