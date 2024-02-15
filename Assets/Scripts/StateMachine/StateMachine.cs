using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RamiresTechGames
{
    public abstract class StateMachine
    {
        protected IState currentState;
        protected IState previousState;

        public void ChangeState(IState newState)
        {
            currentState?.Exit();

            previousState = currentState;
            currentState = newState;

            currentState?.Enter();
        }

        public void RevertToPreviousState()
        {
            ChangeState(previousState);
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void FixedUpdate()
        {
            currentState?.FixedUpdate();
        }

        public void HandleInput()
        {
            currentState?.HandleInput();
        }

        public void OnAnimationEnterEvent()
        {
            currentState?.OnAnimationEnterEvent();
        }

        public void OnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }

        public void OnAnimationTransitionEvent()
        {
            currentState?.OnAnimationTransitionEvent();
        }
    }
}
