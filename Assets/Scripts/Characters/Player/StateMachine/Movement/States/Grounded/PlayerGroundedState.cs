using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerGroundedState : PlayerMovementState
    {
        private SlopeData slopeData;

        public PlayerGroundedState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
            slopeData = stateMachine.player.colliderUtility.slopeData;
        }

        #region IState

        public override void FixedUpdate() {
            base.FixedUpdate();

            FloatCapsuleCollider();
        }

        #endregion

        #region Main Methods

        private void FloatCapsuleCollider()
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.player.colliderUtility.colliderData.collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.floatRayDistance, stateMachine.player.layerData.groundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                float slopeSpeedModifier = SetSlopeSpeedModifierAngle(groundAngle);

                if (slopeSpeedModifier == 0)
                {
                    return;
                }

                float distanceToFloatingPoint = stateMachine.player.colliderUtility.colliderData.colliderCenterInLocalSpace.y * stateMachine.player.transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0)
                {
                    return;
                }

                float amountToLift = distanceToFloatingPoint * slopeData.stepReachForce - GetPlayerVerticalVelocity().y;

                Vector3 liftForce = Vector3.up * amountToLift;

                stateMachine.player.body.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        private float SetSlopeSpeedModifierAngle(float angle)
        {
            float slopeSpeedModifier = groundedData.slopeSpeedAngles.Evaluate(angle);

            stateMachine.playerReusableData.movementOnSlopesSpeedModifier = slopeSpeedModifier;

            return slopeSpeedModifier;
        }

        #endregion

        #region Reusable Methods

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Movement.canceled += OnMovementCanceled;

            stateMachine.player.playerInput.playerActions.Dash.started += OnDashStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.player.playerInput.playerActions.Movement.canceled -= OnMovementCanceled;

            stateMachine.player.playerInput.playerActions.Dash.started -= OnDashStarted;
        }

        protected virtual void OnMove()
        {
            if (stateMachine.playerReusableData.shouldWalk)
            {
                stateMachine.ChangeState(stateMachine.playerWalkingState);
                return;
            }

            stateMachine.ChangeState(stateMachine.playerRunningState);

        }

        #endregion

        #region Input Methods

        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.idlingState);
        }

        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.dashingState);
        }

        #endregion
    }
}
