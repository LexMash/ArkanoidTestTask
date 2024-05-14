using System;

namespace Arkanoid.Levels
{
    public interface ILevelsEventNotifier
    {
        event Action<string> LevelLoaded;
        event Action<string> LevelRestarted;
        event Action LevelCompleted;
    }
}