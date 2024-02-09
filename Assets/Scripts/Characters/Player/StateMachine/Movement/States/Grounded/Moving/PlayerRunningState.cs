using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerRunningState : PlayerMovingState
    {
        public PlayerRunningState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = groundedData.runData.speedModifier;
        }

        #endregion

        #region Input Methods

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.playerWalkingState);
        }

        #endregion

    }
}
