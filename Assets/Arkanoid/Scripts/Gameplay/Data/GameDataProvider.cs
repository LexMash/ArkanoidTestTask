using Arkanoid.Gameplay.Data;
using Arkanoid.UI;
using System;

namespace Arkanoid
{
    public sealed class GameDataProvider : IDisposable, IGameDataProvider
    {
        private const string FILE_NAME = "GameData.dat";

        private readonly IGameEndNotifier _notifier;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IScoreNotifier _scoreNotifier;
        private GameData _data;

        public GameDataProvider(ISaveLoadService saveLoadService, IGameEndNotifier notifier, IScoreNotifier scoreNotifier)
        {
            _notifier = notifier;
            _saveLoadService = saveLoadService;
            _scoreNotifier = scoreNotifier;

            _notifier.LevelCompleted += OnLevelCompleted;
            _notifier.GameOver += OnGameOver;
            _scoreNotifier.ScoreChanged += OnScoreChanged;
        }

        public IReadOnlyGameData Data => _data;

        public void Dispose()
        {
            _notifier.LevelCompleted -= OnLevelCompleted;
            _notifier.GameOver -= OnGameOver;
            _scoreNotifier.ScoreChanged -= OnScoreChanged;

            _data = null;
        }

        private void OnLevelCompleted()
        {
            _data.CurrentLevelIndex++;

            Save();
        }

        private void OnGameOver()
        {
            Save();
        }

        private void OnScoreChanged(NewScoreData newData)
        {
            if (newData.HighScore > _data.HighScore)
                _data.HighScore = newData.HighScore;
        }

        private void Save() => _saveLoadService.Save(_data, FILE_NAME);

        private void Load()
        {
            _data = _saveLoadService.Load<GameData>(FILE_NAME);

            _data ??= new GameData();
        }
    }
}
