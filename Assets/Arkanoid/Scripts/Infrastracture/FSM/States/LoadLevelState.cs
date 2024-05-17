using Arkanoid.Levels;
using Arkanoid;
using Infrastructure;
using StateMachine;
using Arkanoid.UI;
using Arkanoid.Paddle;
using Arkanoid.Ball;
using Arkanoid.GameField;

public sealed class LoadLevelState : GameStateBase
{
    private readonly IGameDataProvider _gameDataProvider;
    private readonly ILevelController _levelController;
    private readonly IScoreController _scoreController;
    private readonly IBallController _ballController;
    private readonly PaddleController _paddleController;
    private readonly IArenaController _arenaController;

    public LoadLevelState(
        StateChangeProvider stateChangeProvider,
        IGameDataProvider gameDataProvider,
        ILevelController levelController,
        IScoreController scoreController,
        IBallController ballController,
        PaddleController paddleController,
        IArenaController arenaController
        ) : base(stateChangeProvider)
    {
        _gameDataProvider = gameDataProvider;
        _levelController = levelController;
        _scoreController = scoreController;
        _ballController = ballController;
        _paddleController = paddleController;
        _arenaController = arenaController;
    }

    public override void Enter()
    {
        base.Enter();
     
        _arenaController.ChangeBackground();

        _scoreController.ResetScore();
        _ballController.SetInitialState();
        _paddleController.SetInitialState();

        int index = _gameDataProvider.Data.CurrentLevelIndex;

        _levelController.Load(index);      

        _stateChangeProvider.ChangeState(GameStateType.GamePlay);
    }
}
