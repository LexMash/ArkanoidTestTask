using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Bricks
{
    [CreateAssetMenu(fileName = "BricksConfig", menuName = "Arkanoid/Bricks/BricksConfig")]
    public class BrickConfig : ScriptableObject
    {
        [SerializeField] private List<BrickMetaData> _datas;

        public IReadOnlyList<BrickMetaData> Datas => _datas;
    }
}