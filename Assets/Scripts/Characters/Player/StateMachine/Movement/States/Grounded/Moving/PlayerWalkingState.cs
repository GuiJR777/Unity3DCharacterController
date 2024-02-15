using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = groundedData.walkData.speedModifier;
        }

        #endregion


        #region Input Methods

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.playerLightStoppingState);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.playerRunningState);
        }

        #endregion
    }
}
