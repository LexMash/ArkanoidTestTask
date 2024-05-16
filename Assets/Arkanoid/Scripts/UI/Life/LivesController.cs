using Arkanoid.Ball;
using System;

namespace Arkanoid.UI.LifeBar
{
    public class LivesController : IDisposable, ILivesNotifier
    {
        public event Action NoMoreLives;
        public event Action LifeLost;
        public event Action LifeAdd;

        private const int MAX_LIVES = 4;
        private const int DEFAULT_LIVES = 2; //это значение конечно должно прийти из вне, но пока так

        private readonly IBallDestroyNotifier _ballDestroyNotificator;

        private int _counter;

        public LivesController(IBallDestroyNotifier ballDestroyNotificator)
        {
            _ballDestroyNotificator = ballDestroyNotificator;

            _ballDestroyNotificator.BallDestroed += OnBallDestroed;
        }

        public void SetInitState()
        {
            _counter = DEFAULT_LIVES;

            for(int i = 0; i < _counter; i++)
            {
                LifeAdd?.Invoke();
            }
        }

        public void AddLife()
        {
            if(_counter + 1 < MAX_LIVES)
            {
                _counter++;

                LifeAdd?.Invoke();
            }         
        }

        public void Dispose()
        {
            _ballDestroyNotificator.BallDestroed -= OnBallDestroed;
        }

        private void OnBallDestroed()
        {
            _counter--;

            LifeLost?.Invoke();

            if (_counter == 0)
            {
                NoMoreLives?.Invoke();
            }
        }
    }
}
