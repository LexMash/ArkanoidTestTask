using Arkanoid.Bricks;
using System;

namespace Arkanoid.PowerUPs
{
    public class BrickDestroyHandle : IDisposable
    {
        private readonly IBrickEventNotifier _notifier;
        private readonly PowerUPFactory _factory;

        public BrickDestroyHandle(IBrickEventNotifier notifier, PowerUPFactory factory)
        {
            _notifier = notifier;
            _factory = factory;

            _notifier.OnBrickDestroyed += OnBrickDestroyed;
        }

        public void Dispose()
        {
            _notifier.OnBrickDestroyed -= OnBrickDestroyed;
        }

        private void OnBrickDestroyed(HitBrickData data)
        {
            if (data.ModType == ModType.None)
                return;

            _factory.Create(data.ModType, data.Position);
        }
    }
}
