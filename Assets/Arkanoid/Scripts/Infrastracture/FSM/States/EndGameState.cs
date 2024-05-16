using Arkanoid.UI;
using Infrastructure;
using StateMachine;

public class EndGameState : GameStateBase
{
    private readonly RestartButton _restartButton;

    public EndGameState(
        StateChangeProvider stateChangeProvider,
        RestartButton restartButton
        ) : base(stateChangeProvider)
    {
        _restartButton = restartButton;
    }

    public override void Enter()
    {
        base.Enter();

        _restartButton.RestartPerformed += OnRestartPerformed;

        _restartButton.Show();
    }

    public override void Exit()
    {
        base.Exit();

        _restartButton.RestartPerformed -= OnRestartPerformed;

        _restartButton.Hide();
    }

    private void OnRestartPerformed() => _stateChangeProvider.ChangeState(GameStateType.LevelRestart);
}
