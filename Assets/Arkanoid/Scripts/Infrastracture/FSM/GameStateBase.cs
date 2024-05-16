using StateMachine;
using UnityEngine;

namespace Infrastructure
{
    public abstract class GameStateBase : IGameState
    {
        protected readonly StateChangeProvider _stateChangeProvider;

        protected GameStateBase(StateChangeProvider stateChangeProvider)
        {
            _stateChangeProvider = stateChangeProvider;
        }

        public virtual void Enter() 
            => Debug.Log($"Enter State {GetType().Name}");

        public virtual void Exit() 
            => Debug.Log($"Exit State {GetType().Name}");

        public virtual void Update() { }
    }
}

