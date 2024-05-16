using Arkanoid.Ball;
using System;

namespace Arkanoid
{
    public interface IGameStateNotifier
    {
        event Action Win;
        event Action GameOver;
    }
}
