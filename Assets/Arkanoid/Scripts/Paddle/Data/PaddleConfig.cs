﻿using UnityEngine;

namespace Arkanoid.Paddle
{
    [CreateAssetMenu(fileName = "New PaddleConfig", menuName = "Arkanoid/Paddle/PaddleConfig")]
    public class PaddleConfig : ScriptableObject
    {
        [field: SerializeField] public float InitSize { get; private set; } = 2.0f;
        [field: SerializeField] public float MinSize { get; private set; } = 1.0f;
        [field: SerializeField] public float MaxSize { get; private set; } = 4.0f;
        [field: SerializeField] public float SizeChangeStep { get; private set; } = 1f;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 2.0f;
        [field: SerializeField] public float SmoothMoveFactor { get; private set; } = .15f;
        [field: SerializeField] public float FieldWidth { get; private set; } = 5f;
        [field: SerializeField] public float BulletPerMinute { get; private set; } = 60f;
    }
}
