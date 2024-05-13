using Arkanoid.Bricks;
using System;
using UnityEngine;

namespace LevelEditor.Data
{
    [CreateAssetMenu(fileName = "EditorBrickData", menuName = "LevelEditor/Data/EditorBrickData")]
    public class EditorBrickData : ScriptableObject
    {
        public BrickViewData[] Data;
    }

    [Serializable]
    public class BrickViewData
    {
        public BrickType Type;
        public Sprite Visual;
    }
}
