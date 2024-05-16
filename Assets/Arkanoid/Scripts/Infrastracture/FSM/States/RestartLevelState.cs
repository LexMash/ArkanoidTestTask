using Arkanoid.Levels;
using Arkanoid.UI;
using Arkanoid.UI.LifeBar;
using Infrastructure;
using StateMachine;

public class RestartLevelState : GameStateBase
{
    private readonly ILevelController _levelController;
    private readonly IScoreController _scoreController;
    private readonly LivesController _livesController;

    public RestartLevelState(
        StateChangeProvider stateChangeProvider,
        ILevelController levelController,
        IScoreController scoreController,
        LivesController livesController
        ) : base(stateChangeProvider)
    {
        _levelController = levelController;
        _scoreController = scoreController;
        _livesController = livesController;
    }

    public override void Enter()
    {
        base.Enter();

        _levelController.Restart();
        _scoreController.ResetScore();
        _livesController.SetInitState();

        _stateChangeProvider.ChangeState(GameStateType.GamePlay);
    }
}
