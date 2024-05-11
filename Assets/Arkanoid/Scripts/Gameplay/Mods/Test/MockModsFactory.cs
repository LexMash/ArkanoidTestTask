using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle.FX.Configs;
using System.Collections.Generic;
using System.Linq;

namespace Arkanoid.Gameplay.Mods
{
    public class MockModsFactory : IModsFactory
    {
        public List<Timer> timers = new List<Timer>();

        private readonly ModsConfig _config;

        public MockModsFactory(ModsConfig config)
        {
            _config = config;
        }

        public IModificator Create(ModType type)
        {
            var data = _config.ModDatas.FirstOrDefault(data => data.Type == type);

            var timer = new Timer();
            timers.Add(timer);

            return new MockModificator(data, timer);
        }
    }
}
