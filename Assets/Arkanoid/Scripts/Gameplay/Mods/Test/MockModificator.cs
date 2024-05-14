using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle.FX.Configs;
using System;
using UnityEngine;

namespace Arkanoid.Gameplay.Mods
{
    public class MockModificator : IModificator
    {
        public ModType Type => _data.Type;

        public event Action<IModificator> Expired;

        private readonly ModificatorData _data;
        private readonly Timer _timer;

        public MockModificator(ModificatorData data, Timer timer)
        {
            _data = data;
            _timer = timer;
        }

        public void Apply()
        {
            _timer.Start(_data.Time);

            Debug.Log($"Mod {Type} applyed");

            _timer.Completed += OnCompleted;
        }

        public void Reapply()
        {
            _timer.Start(_data.Time);

            Debug.Log($"Mod {Type} reapplyed");
        }

        public void Rollback()
        {
            Debug.Log($"Mod {Type} rollback");
        }

        public void Dispose()
        {
            _timer.Completed -= OnCompleted;
        }

        private void OnCompleted()
        {
            _timer.Completed -= OnCompleted;

            Rollback();

            Expired?.Invoke(this);
        }
    }
}
