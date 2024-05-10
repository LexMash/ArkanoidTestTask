using System;

namespace Arkanoid.Paddle
{
    public interface IWidthChangeNotifier
    {
        event Action WidthDecreased;
        event Action WidthIncreased;
    }
}