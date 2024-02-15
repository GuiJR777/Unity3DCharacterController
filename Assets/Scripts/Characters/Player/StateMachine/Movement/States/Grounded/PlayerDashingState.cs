using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData dashingData;

        private float startTime;

        private int consecutiveDashesUsed;

        private bool shouldKeepRotating;

        public PlayerDashingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            dashingData = stateMachine.player.playerData.playerGroundedData.dashData;
        }

        #region ISate

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = dashingData.speedModifier;

            stateMachine.playerReusableData.rotationData = dashingData.rotationData;

            AddForceOnTransitionFromStationaryState();

            shouldKeepRotating = stateMachine.playerReusableData.movementInput != Vector2.zero;

            UpdateConsecutiveDashes();

            startTime = Time.time;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (!shouldKeepRotating)
            {
                return;
            }

            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if (stateMachine.playerReusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.playerHardStoppingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.playerSprintingState);
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();
        }

        #endregion

        #region Main Methods

        public void AddForceOnTransitionFromStationaryState()
        {
            if (stateMachine.playerReusableData.movementInput != Vector2.zero)
            {
                return;
            }

            Vector3 characterFaceDirection = stateMachine.player.transform.forward;

            characterFaceDirection.y = 0f;

            UpdateTargetRotation(characterFaceDirection, false);

            stateMachine.player.body.velocity = characterFaceDirection * GetMovementSpeed();
        }

        public void UpdateConsecutiveDashes()
        {
            if (!IsConsecutiveDash())
            {
                consecutiveDashesUsed = 0;
            }

            ++consecutiveDashesUsed;

            if (consecutiveDashesUsed == dashingData.limitConsecutiveDashes)
            {
                consecutiveDashesUsed = 0;

                stateMachine.player.playerInput.DisableActionForTime(stateMachine.player.playerInput.playerActions.Dash, dashingData.dashLimitReachedCooldown);
            }
        }

        private bool IsConsecutiveDash()
        {
            return Time.time < startTime + dashingData.timeToBeConsideredConsecutive;
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Movement.performed += OnMovementPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Movement.performed -= OnMovementPerformed;
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {

        }

        protected void OnMovementPerformed(InputAction.CallbackContext context)
        {
            shouldKeepRotating = true;
        }

        #endregion
    }
}
