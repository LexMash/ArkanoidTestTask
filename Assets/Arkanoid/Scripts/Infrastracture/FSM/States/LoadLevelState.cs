using Arkanoid.Levels;
using Arkanoid;
using Infrastructure;
using StateMachine;

public sealed class LoadLevelState : GameStateBase
{
    private readonly IGameDataProvider _gameDataProvider;
    private readonly ILevelController _levelController;   

    public LoadLevelState(
        StateChangeProvider stateChangeProvider,
        IGameDataProvider gameDataProvider,
        ILevelController levelController   
        ) : base(stateChangeProvider)
    {
        _gameDataProvider = gameDataProvider;
        _levelController = levelController;
    }

    public override void Enter()
    {
        base.Enter();

        int index = _gameDataProvider.Data.CurrentLevelIndex;

        _levelController.Load(index);

        _stateChangeProvider.ChangeState(GameStateType.GamePlay);
    }
}
