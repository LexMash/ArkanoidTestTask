using Arkanoid.Levels;
using Infrastructure;
using StateMachine;

public class RestartLevelState : GameStateBase
{
    private readonly ILevelController _levelController;

    public RestartLevelState(
        StateChangeProvider stateChangeProvider,
        ILevelController levelController
        ) : base(stateChangeProvider)
    {
        _levelController = levelController;
    }

    public override void Enter()
    {
        base.Enter();

        _levelController.Restart();

        _stateChangeProvider.ChangeState(GameStateType.GamePlay);
    }
}
