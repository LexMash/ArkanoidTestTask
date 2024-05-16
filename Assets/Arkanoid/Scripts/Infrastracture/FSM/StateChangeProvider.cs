using Infrastructure;

namespace StateMachine
{
    public sealed class StateChangeProvider
    {
        private readonly IGameStateMachine _stateMachine;

        public StateChangeProvider(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void ChangeState(GameStateType type) 
            => _stateMachine.SetState(type);
    }
}
