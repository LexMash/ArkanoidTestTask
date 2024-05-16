using System;
using System.Collections.Generic;
using Zenject;

namespace Infrastructure
{
    public sealed class GameStateMachine : IGameStateMachine, IInitializable, IDisposable
    {
        private readonly GameStateFactory _factory;
        private readonly Dictionary<GameStateType, IGameState> _stateMap = new();
        private IGameState _currentState;

        public GameStateMachine(GameStateFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _stateMap[GameStateType.Init] = _factory.CreateState<InitializeState>();
            _stateMap[GameStateType.LevelLoad] = _factory.CreateState<LoadLevelState>();
            _stateMap[GameStateType.GamePlay] = _factory.CreateState<GamePlayState>();
            _stateMap[GameStateType.LevelRestart] = _factory.CreateState<RestartLevelState>();
            _stateMap[GameStateType.EndGame] = _factory.CreateState<EndGameState>();

            SetState(GameStateType.Init);
        }

        public void SetState(GameStateType type)
        {
            if(_stateMap.TryGetValue(type, out IGameState state))
            {
                _currentState?.Exit();

                _currentState = state;

                _currentState.Enter();
            }
            else
            {
                throw new StateMachineException($"StateMachine doesn't have this {type} of state");
            }
        }

        public void Dispose()
        {
            _currentState = null;

            _stateMap.Clear();
        }
    }
}
