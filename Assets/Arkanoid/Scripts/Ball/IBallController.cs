using System;

namespace Arkanoid.Ball
{
    public interface IBallController
    {
        event Action BallDestroed;

        int BallsCount { get; }

        void AddBall();
        void SetInitialState();
        void ResetSpeed();
        void SpeedDown();
        void FirstLaunch();
        void MagnetModeEnable(bool enable);
        void ReleaseMagnet();
    }
}