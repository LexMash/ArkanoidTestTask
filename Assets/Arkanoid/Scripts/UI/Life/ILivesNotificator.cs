using System;

namespace Arkanoid.UI.LifeBar
{
    public interface ILivesNotificator
    {
        event Action LifeAdd;
        event Action LifeLost;
        event Action NoMoreLives;
    }
}