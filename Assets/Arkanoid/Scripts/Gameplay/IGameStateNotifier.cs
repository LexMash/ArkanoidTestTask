using System;

namespace Arkanoid
{
    public interface IGameStateNotifier
    {
        event Action LevelCompleted;
        event Action GameOver;
    }
}
