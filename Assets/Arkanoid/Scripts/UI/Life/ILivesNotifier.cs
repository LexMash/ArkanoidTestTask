using System;

namespace Arkanoid.UI.LifeBar
{
    public interface ILivesNotifier
    {
        event Action LifeAdd;
        event Action LifeLost;
        event Action NoMoreLives;
    }
}