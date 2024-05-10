using System;

namespace Arkanoid.Gameplay
{
    public class Timer
    {
        public event Action<Timer> Completed;

        private float _time;
        private bool _isCounting;

        public void Start(float time)
        {
            _isCounting = true;
            _time = time;
        }

        public void Update(float timeDelta)
        {
            if (!_isCounting)
                return;

            _time -= timeDelta;

            if (TimeOut())
            {
                _isCounting = false;
                Completed?.Invoke(this);
            }
        }

        private bool TimeOut() => _time <= 0;
    }
}
