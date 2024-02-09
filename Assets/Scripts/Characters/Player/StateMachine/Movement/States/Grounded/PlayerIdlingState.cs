using UnityEngine;

namespace RamiresTechGames
{
    public class PlayerIdlingState : PlayerGroundedState
    {
        public PlayerIdlingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementSpeedModifier = 0f;

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (stateMachine.playerReusableData.movementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }

        #endregion

    }
}
