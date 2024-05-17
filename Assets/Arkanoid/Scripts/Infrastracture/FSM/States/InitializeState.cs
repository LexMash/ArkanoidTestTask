using Arkanoid;
using Arkanoid.Ball;
using Arkanoid.Input;
using Arkanoid.Levels;
using Arkanoid.Paddle.FX.Laser;
using Arkanoid.PowerUPs;
using Arkanoid.UI;
using Arkanoid.UI.LifeBar;
using Infrastructure;
using StateMachine;
using UnityEngine;

public class InitializeState : GameStateBase
{
    private readonly IGameDataProvider _gameDataProvider;
    private readonly IScoreController _scoreController;
    private readonly BallFactory _ballFactory;
    private readonly ProjectileFactory _projectileFactory;
    private readonly BrickFactory _brickFactory;
    private readonly PowerUPFactory _powerUPFactory;
    private readonly ILevelController _levelController;
    private readonly LivesController _livesController;
    private readonly IInput _input;

    public InitializeState(
        StateChangeProvider stateChangeProvider,
        IGameDataProvider gameDataProvider,
        IScoreController scoreController,
        BallFactory ballFactory,
        ProjectileFactory projectileFactory,
        BrickFactory brickFactory,
        PowerUPFactory powerUPFactory,
        ILevelController levelController,
        IInput input,
        LivesController livesController
        ) : base(stateChangeProvider)
    {
        _gameDataProvider = gameDataProvider;
        _scoreController = scoreController;
        _ballFactory = ballFactory;
        _projectileFactory = projectileFactory;
        _brickFactory = brickFactory;
        _powerUPFactory = powerUPFactory;
        _levelController = levelController;
        _input = input;
        _livesController = livesController;
    }

    public override async void Enter()
    {
        base.Enter();

        Application.targetFrameRate = 60;

        _input.Disable();

        _gameDataProvider.Load();

        _scoreController.BindData(_gameDataProvider.Data);
        _scoreController.ResetScore();

        await _levelController.Init();
        await _ballFactory.Init();
        await _projectileFactory.Init();
        await _powerUPFactory.Init();
        await _brickFactory.Init();
        
        _livesController.SetInitState();

        _stateChangeProvider.ChangeState(GameStateType.LevelLoad);
    }
}
