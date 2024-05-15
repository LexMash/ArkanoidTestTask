using System;

namespace Arkanoid
{
    public interface IGameEndNotifier
    {
        event Action LevelCompleted;
        event Action GameOver;
    }
}
