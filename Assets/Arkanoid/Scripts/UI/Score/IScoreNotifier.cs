using Arkanoid.Gameplay.Data;
using System;

namespace Arkanoid.UI
{
    public interface IScoreNotifier
    {
        event Action<NewScoreData> ScoreChanged;
    }
}