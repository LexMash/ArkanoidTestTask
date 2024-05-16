using Arkanoid;
using Arkanoid.Ball;
using Arkanoid.GameField;
using Arkanoid.Gameplay;
using Arkanoid.Input;
using Arkanoid.Levels;
using Arkanoid.Paddle;
using Arkanoid.UI;
using Arkanoid.UI.LifeBar;
using Infrastructure;
using StateMachine;

public class GamePlayState : GameStateBase
{
    private readonly IInput _input;
    private readonly IBallController _ballController;
    private readonly IGameDataProvider _gameDataProvider;
    private readonly ILivesNotifier _livesNotificator;
    private readonly ILevelsEventNotifier _levelsEventNotifier;
    private readonly ModsController _modsController;
    private readonly PaddleController _paddleController;
    private readonly IScoreController _scoreController;
    private readonly ArenaController _arenaController;

    public GamePlayState(
        StateChangeProvider stateChangeProvider,
        IInput input,
        IBallController ballController,
        IGameDataProvider gameDataProvider,
        ILivesNotifier livesNotificator,
        ILevelsEventNotifier levelsEventNotifier,
        ModsController modsController,
        PaddleController paddleController,
        IScoreController scoreController,
        ArenaController arenaController
        ) : base(stateChangeProvider)
    {
        _input = input;
        _ballController = ballController;
        _gameDataProvider = gameDataProvider;
        _livesNotificator = livesNotificator;
        _levelsEventNotifier = levelsEventNotifier;
        _modsController = modsController;
        _paddleController = paddleController;
        _scoreController = scoreController;
        _arenaController = arenaController;
    }

    public override void Enter()
    {
        base.Enter();

        _input.Enable();

        _arenaController.ChangeBackground();

        _ballController.SetInitialState();
        _paddleController.SetInitialState();
        _scoreController.ResetScore();

        _input.ActionPerformed += OnActionPerformed;
        _livesNotificator.NoMoreLives += OnNoMoreLives;
        _levelsEventNotifier.LevelCompleted += OnLevelCompleted;
    }

    public override void Exit()
    {
        base.Exit();

        _input.Disable();

        _gameDataProvider.Save();

        _modsController.RemoveAllApplyedMods();

        _livesNotificator.NoMoreLives -= OnNoMoreLives;
        _levelsEventNotifier.LevelCompleted -= OnLevelCompleted;
    }

    private void OnActionPerformed()
    {
        _ballController.FirstLaunch();

        _input.ActionPerformed -= OnActionPerformed;
    }

    private void OnLevelCompleted()
    {
        _stateChangeProvider.ChangeState(GameStateType.LevelLoad);
    }

    private void OnNoMoreLives()
    {        
        _stateChangeProvider.ChangeState(GameStateType.EndGame);
    }
}
