using Arkanoid.Gameplay.Data;

namespace Arkanoid
{
    public interface IGameDataProvider
    {
        IReadOnlyGameData Data { get; }

        void Save();
        void Load();
    }
}