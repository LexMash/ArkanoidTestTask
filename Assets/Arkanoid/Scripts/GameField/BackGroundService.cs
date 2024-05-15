using Arkanoid.Infrastracture;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Arkanoid.GameField
{
    public class BackGroundService : IDisposable
    {
        private readonly ArenaBackgroundCollection _backgrounds;
        private readonly IAssetProvider _assetProvider;

        private string _currentReference;

        public BackGroundService(ArenaBackgroundCollection backgrounds, IAssetProvider assetProvider)
        {
            _backgrounds = backgrounds;
            _assetProvider = assetProvider;

            _currentReference = string.Empty;
        }

        public async UniTask<Sprite> GetBackground()
        {
            int index = UnityEngine.Random.Range(0, _backgrounds.Sprites.Count);

            if (_currentReference.Equals(string.Empty) != false)
            {
                _assetProvider.Release(_currentReference);
            }

            string reference = _backgrounds.Sprites[index].AssetGUID;

            return await _assetProvider.LoadAsset<Sprite>(reference);
        }

        public void Dispose()
        {
            _assetProvider.Release(_currentReference);

            _currentReference = null;
        }
    }
}