﻿using System;

namespace Arkanoid.Ball
{
    public interface IBallController
    {
        event Action BallDestroed;

        int BallsCount { get; }

        public void FirstLaunch();
        void AddBall();
        void SetInitialState();
        void ResetSpeed();
        void SpeedDown();
        void MagnetModeEnable(bool enable);
        void ReleaseMagnet();
    }
}