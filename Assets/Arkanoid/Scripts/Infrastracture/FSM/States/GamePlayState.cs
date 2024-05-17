using Arkanoid;
using Arkanoid.Ball;
using Arkanoid.Gameplay;
using Arkanoid.Input;
using Arkanoid.Levels;
using Arkanoid.PowerUPs;
using Arkanoid.UI.LifeBar;
using DG.Tweening;
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
    private readonly PowerUpSpawner _powerUpSpawner;

    public GamePlayState(
        StateChangeProvider stateChangeProvider,
        IInput input,
        IBallController ballController,
        IGameDataProvider gameDataProvider,
        ILivesNotifier livesNotificator,
        ILevelsEventNotifier levelsEventNotifier,
        ModsController modsController,
        PowerUpSpawner powerUpSpawner
        ) : base(stateChangeProvider)
    {
        _input = input;
        _ballController = ballController;
        _gameDataProvider = gameDataProvider;
        _livesNotificator = livesNotificator;
        _levelsEventNotifier = levelsEventNotifier;
        _modsController = modsController;
        _powerUpSpawner = powerUpSpawner;
    }

    public override void Enter()
    {
        base.Enter();

        _input.Enable();        

        _input.ActionPerformed += OnActionPerformed;

        _ballController.BallDestroed += OnBallDestroed;
        _livesNotificator.NoMoreLives += OnNoMoreLives;
        _levelsEventNotifier.LevelCompleted += OnLevelCompleted;
    }

    public override void Exit()
    {
        base.Exit();

        _input.Disable();

        _gameDataProvider.Save();

        _ballController.DestroyExtraBalls();

        _input.ActionPerformed -= OnActionPerformed;

        _ballController.BallDestroed -= OnBallDestroed;
        _livesNotificator.NoMoreLives -= OnNoMoreLives;
        _levelsEventNotifier.LevelCompleted -= OnLevelCompleted;
    }

    private void OnActionPerformed()
    {
        _ballController.FirstLaunch();

        _input.ActionPerformed -= OnActionPerformed;
    }

    private void OnBallDestroed()
    {
        _ballController.SetInitialState();

        RemoveAllObjectsFromScreen();

        _input.ActionPerformed += OnActionPerformed;
    }

    private void OnLevelCompleted()
    {
        RemoveAllObjectsFromScreen();

        DOVirtual.DelayedCall(2f, ()=> _stateChangeProvider.ChangeState(GameStateType.LevelLoad));
    }

    private void OnNoMoreLives()
    {
        RemoveAllObjectsFromScreen();

        _stateChangeProvider.ChangeState(GameStateType.EndGame); ;
    }

    private void RemoveAllObjectsFromScreen()
    {
        _modsController.RemoveAllApplyedMods();
        _powerUpSpawner.RemoveAllSpawned();
    }
}
