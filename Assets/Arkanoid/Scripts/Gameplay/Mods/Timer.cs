using System;

namespace Arkanoid.Gameplay
{
    public class Timer
    {
        public event Action Completed;

        private float _time;
        private bool _isCounting;

        public void Start(float time)
        {
            _isCounting = true;
            _time = time;
        }

        public void Stop() => _isCounting = false;

        public void Update(float timeDelta)
        {
            if (!_isCounting)
                return;

            _time -= timeDelta;

            if (TimeOut())
            {
                _isCounting = false;
                Completed?.Invoke();
            }
        }

        private bool TimeOut() => _time <= 0;
    }
}
