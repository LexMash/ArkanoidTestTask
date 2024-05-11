using System;

namespace Arkanoid.UI
{
    public interface IScoreController
    {
        event Action<NewScoreData> ScoreChanged;

        void ResetScore();
    }
}