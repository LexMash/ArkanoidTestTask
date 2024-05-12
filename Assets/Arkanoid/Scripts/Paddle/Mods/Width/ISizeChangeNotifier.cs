using System;

namespace Arkanoid.Paddle
{
    public interface ISizeChangeNotifier
    {
        event Action Decreased;
        event Action Increased;
    }
}