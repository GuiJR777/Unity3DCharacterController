using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerMovementState : IState
    {

        protected PlayerMovementStateMachine stateMachine;

        protected PlayerGroundedData groundedData;

        public PlayerMovementState(PlayerMovementStateMachine stateMachine){
            this.stateMachine = stateMachine;

            groundedData = stateMachine.player.playerData.playerGroundedData;

            InitializeData();
        }

        private void InitializeData()
        {
            stateMachine.playerReusableData.timeToReachTargetRotation = groundedData.baseRotationData.targetRotationReachTime;
        }

        #region IState

        public virtual void Enter(){
            Debug.Log($"Entering {this.GetType().Name} State");

            AddInputActionsCallbacks();
        }

        public virtual void Exit(){
            Debug.Log($"Exiting {this.GetType().Name} State");

            RemoveInputActionsCallbacks();
        }


        public virtual void HandleInput(){
            ReadMovementInput();
        }

        public virtual void Update(){
        }

        public virtual void FixedUpdate(){
            Move();
        }

        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        {

        }

        public virtual void OnAnimationTransitionEvent()
        {

        }

        #endregion

        #region Main Methods

        private void ReadMovementInput(){
            stateMachine.playerReusableData.movementInput = stateMachine.player.playerInput.playerActions.Movement.ReadValue<Vector2>();
        }

        private void Move(){
            if (stateMachine.playerReusableData.movementInput == Vector2.zero || stateMachine.playerReusableData.movementSpeedModifier == 0f)
            {
                return;
            }

            Vector3 movementDirection = GetMovementDirection();

            float targetRotationYAngle = Rotate(movementDirection);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            Vector3 desiredForce = targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity;

            stateMachine.player.body.AddForce(desiredForce, ForceMode.VelocityChange);
        }

        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        private float AddCameraRotationToAngle(float angle)
        {
            angle += stateMachine.player.cameraTransform.eulerAngles.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        private void UpdateTargetRotationData(float targetAngle)
            {
                stateMachine.playerReusableData.currentTargetRotation.y = targetAngle;

                stateMachine.playerReusableData.dampedTargetRotationPassedTime.y = 0f;
            }

        private static float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        #endregion

        #region Reusable Methods

        protected Vector3 GetMovementDirection(){
            return new Vector3(stateMachine.playerReusableData.movementInput.x, 0f, stateMachine.playerReusableData.movementInput.y);
        }

        protected float GetMovementSpeed(){
            return groundedData.baseSpeed * stateMachine.playerReusableData.movementSpeedModifier * stateMachine.playerReusableData.movementOnSlopesSpeedModifier;
        }

        protected Vector3 GetPlayerHorizontalVelocity(){
            Vector3 horizontalVelocity = stateMachine.player.body.velocity;

            horizontalVelocity.y = 0f;

            return horizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelocity(){
            return new Vector3(0f, stateMachine.player.body.velocity.y, 0f);
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.player.body.rotation.eulerAngles.y;

            if (currentYAngle == stateMachine.playerReusableData.currentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.playerReusableData.currentTargetRotation.y, ref stateMachine.playerReusableData.dampedTargetRotationCurrentVelocity.y, stateMachine.playerReusableData.timeToReachTargetRotation.y - stateMachine.playerReusableData.dampedTargetRotationPassedTime.y);

            stateMachine.playerReusableData.dampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            stateMachine.player.body.MoveRotation(targetRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }


            if (directionAngle != stateMachine.playerReusableData.currentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            stateMachine.player.body.velocity = Vector3.zero;
        }

        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.player.playerInput.playerActions.WalkToggle.started += OnWalkToggleStarted;
        }


        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.player.playerInput.playerActions.WalkToggle.started -= OnWalkToggleStarted;
        }

        #endregion

        #region Input Methods

        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            stateMachine.playerReusableData.shouldWalk = !stateMachine.playerReusableData.shouldWalk;
        }

        #endregion
    }
}
