using Zenject;

namespace Infrastructure
{
    public interface IGameStateMachine
    {
        void SetState(GameStateType type);
    }
}
