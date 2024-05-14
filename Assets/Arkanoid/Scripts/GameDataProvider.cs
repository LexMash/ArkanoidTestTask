using Arkanoid.Levels;

namespace Arkanoid
{
    public class GameDataProvider
    {
        private readonly ILevelDataService _levelDataService;

        private readonly ILevelsEventNotifier _notifier;
    }
}
