using Arkanoid.Ball;
using Arkanoid.Levels;
using Arkanoid.UI.LifeBar;
using System;

namespace Arkanoid
{
    public interface IGameStateNotifier
    {
        event Action Win;
        event Action GameOver;
    }

    public class GameStateNotifier : IGameStateNotifier, IDisposable
    {
        public event Action Win;
        public event Action GameOver;

        private readonly ILevelsEventNotifier _levelNotifier;
        private readonly ILivesNotifier _livesNotifier;

        public GameStateNotifier(ILevelsEventNotifier levelNotifier, ILivesNotifier livesNotifier)
        {
            _levelNotifier = levelNotifier;
            _levelNotifier.LevelCompleted += OnLevelCompleted;

            _livesNotifier = livesNotifier;
            _livesNotifier.NoMoreLives += OnNoMoreLives;
        }

        private void OnNoMoreLives() => GameOver?.Invoke();

        private void OnLevelCompleted() => Win?.Invoke();

        public void Dispose()
        {
            _livesNotifier.NoMoreLives -= OnNoMoreLives;
            _levelNotifier.LevelCompleted -= OnLevelCompleted;
        }
    }
}
