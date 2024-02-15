using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerStoppingState : PlayerGroundedState
    {

        public PlayerStoppingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = 0;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!IsMovingHorizontally())
            {
                return;
            }

            DecelerateHorizontally();
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            stateMachine.ChangeState(stateMachine.idlingState);
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Movement.started -= OnMovementStarted;
        }

        #endregion

        #region Inputs Methods

        protected virtual void OnMovementStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {

        }

        #endregion

    }
}
