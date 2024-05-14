using Arkanoid.Bricks;
using Arkanoid.Gameplay.Data;
using System;
using System.Collections.Generic;

namespace Arkanoid.UI
{
    public class ScoreController : IDisposable, IScoreNotifier
    {
        public event Action<NewScoreData> ScoreChanged;

        private readonly IBrickEventNotifier _notifier;
        private readonly Dictionary<BrickType, int> _scoreMap = new();
        private readonly ScoreData _scoreData;

        public ScoreController(IBrickEventNotifier notifier, BrickConfig config, ScoreData scoreData)
        {
            _notifier = notifier;
            _scoreData = scoreData;

            FillDictionary(config);

            _notifier.OnBrickDestroyed += OnDestroyBrick;
        }

        public void ResetScore()
        {
            _scoreData.CurrentScore = 0;
            Notify();
        }

        public void Dispose()
        {
            _notifier.OnBrickDestroyed -= OnDestroyBrick;
            _scoreMap.Clear();
        }

        private void OnDestroyBrick(HitBrickData data)
        {
            UpdateScore(data);
        }

        private void UpdateScore(HitBrickData data)
        {
            if (_scoreMap.TryGetValue(data.BrickType, out int value))
            {
                _scoreData.CurrentScore += value;

                if (_scoreData.CurrentScore > _scoreData.HighScore)
                {
                    _scoreData.HighScore = _scoreData.CurrentScore;
                }

                Notify();
                return;
            }

            throw new NullReferenceException($"ScoreMap doesnt contain this type of brick {data.BrickType}");
        }

        private void FillDictionary(BrickConfig config)
        {
            var count = config.Datas.Count;

            for (int i = 0; i < count; i++)
            {
                BrickMetaData data = config.Datas[i];
                _scoreMap[data.Type] = data.Score;
            }
        }

        private void Notify() => ScoreChanged?.Invoke(new NewScoreData(_scoreData.HighScore, _scoreData.CurrentScore));
    }
}
