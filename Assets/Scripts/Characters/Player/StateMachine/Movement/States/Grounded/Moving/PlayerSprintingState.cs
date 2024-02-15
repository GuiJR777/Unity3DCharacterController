using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintingData sprintingData;

        private bool isSprinting;

        private float startTime;

        public PlayerSprintingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            sprintingData = groundedData.sprintingData;
        }


        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = sprintingData.speedModifier;

            startTime = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (isSprinting)
            {
                return;
            }

            if (Time.time < startTime + sprintingData.sprintingToRunTime)
            {
                return;
            }

            StopSprinting();
        }

        public override void Exit()
        {
            base.Exit();

            isSprinting = false;
        }

        #endregion

        #region Main Methods

        private void StopSprinting()
        {
            if (stateMachine.playerReusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.idlingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.playerRunningState);
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Sprint.performed += OnSprintPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Sprint.performed -= OnSprintPerformed;
        }

        #endregion

        #region Input Methods

        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            isSprinting = true;
        }

        #endregion

    }
}
