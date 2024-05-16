using Arkanoid.Gameplay.Data;
using Arkanoid.UI;
using System;

namespace Arkanoid
{
    public sealed class GameDataProvider : IDisposable, IGameDataProvider
    {
        private const string FILE_NAME = "GameData.dat";
      
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateNotifier _gameStateNotifier;
        private readonly IScoreNotifier _scoreNotifier;

        private GameData _data;

        public GameDataProvider(ISaveLoadService saveLoadService, IGameStateNotifier gameStateNotifier, IScoreNotifier scoreNotifier)
        {
            _saveLoadService = saveLoadService;
            _gameStateNotifier = gameStateNotifier;         
            _scoreNotifier = scoreNotifier;

            _gameStateNotifier.Win += OnLevelCompleted;
            _scoreNotifier.ScoreChanged += OnScoreChanged;

            _data = null;
        }

        public IReadOnlyGameData Data => _data;

        public void Save() => _saveLoadService.Save(_data, FILE_NAME);

        public void Load()
        {
            _data = _saveLoadService.Load<GameData>(FILE_NAME);

            _data ??= new GameData();
        }

        public void Dispose()
        {
            _gameStateNotifier.Win -= OnLevelCompleted;
            _scoreNotifier.ScoreChanged -= OnScoreChanged;

            _data = null;
        }

        private void OnLevelCompleted()
        {
            _data.CurrentLevelIndex++;
        }

        private void OnScoreChanged(NewScoreData newData)
        {
            if (newData.HighScore > _data.HighScore)
                _data.HighScore = newData.HighScore;
        }
    }
}
