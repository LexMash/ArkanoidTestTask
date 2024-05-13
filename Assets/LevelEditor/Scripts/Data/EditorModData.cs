using Arkanoid.PowerUPs;
using System;
using UnityEngine;

namespace LevelEditor.Data
{
    [CreateAssetMenu(fileName = "EditorModData", menuName = "LevelEditor/Data/EditorModData")]
    public class EditorModData : ScriptableObject
    {
        public ModViewData[] Data;
    }

    [Serializable]
    public class ModViewData
    {
        public ModType Type;
        public Sprite Visual;
    }
}