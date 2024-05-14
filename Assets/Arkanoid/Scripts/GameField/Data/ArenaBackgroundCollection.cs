using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Arkanoid.GameField
{
    [CreateAssetMenu(fileName = "ArenaBackgroundCollection", menuName = "Arkanoid/Game/ArenaBackgroundCollection")]
    public class ArenaBackgroundCollection : ScriptableObject
    {
        [SerializeField] private List<AssetReferenceSprite> _sprites;

        public IReadOnlyList<AssetReferenceSprite> Sprites => _sprites;
    }
}
