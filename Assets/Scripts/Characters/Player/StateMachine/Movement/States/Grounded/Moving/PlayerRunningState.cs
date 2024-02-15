using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerRunningState : PlayerMovingState
    {
        private float startTime;

        private PlayerRunData runData;

        public PlayerRunningState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            runData = groundedData.runData;
        }

        #region IState

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = groundedData.runData.speedModifier;

            startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (!stateMachine.playerReusableData.shouldWalk)
            {
                return;
            }

            if (Time.time < startTime + runData.runToWalkTime)
            {
                return;
            }

            StopRunning();
        }

        #endregion

        #region Main Methods

        private void StopRunning()
        {
            if (stateMachine.playerReusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.idlingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.playerWalkingState);
        }

        #endregion

        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.playerMediumStoppingState);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.playerWalkingState);
        }

        #endregion

    }
}
