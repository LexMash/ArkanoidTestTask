using Arkanoid.Gameplay.Data;
using System;

namespace Arkanoid.UI
{
    public interface IScoreController
    {
        event Action<NewScoreData> ScoreChanged;

        void BindData(IReadOnlyGameData gameData);
        void ResetScore();
    }
}