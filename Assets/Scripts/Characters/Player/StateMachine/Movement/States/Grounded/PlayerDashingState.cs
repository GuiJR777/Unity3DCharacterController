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

        public PlayerDashingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            dashingData = stateMachine.player.playerData.playerGroundedData.dashData;
        }

        #region ISate

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = dashingData.speedModifier;

            AddForceOnTransitionFromStationaryState();

            UpdateConsecutiveDashes();

            startTime = Time.time;
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


        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {

        }

        #endregion
    }
}
