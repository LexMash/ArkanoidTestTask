namespace Arkanoid.Gameplay.Data
{
    public interface IReadOnlyGameData
    {
        int CurrentLevelIndex { get; }
        int HighScore { get; }
    }
}
