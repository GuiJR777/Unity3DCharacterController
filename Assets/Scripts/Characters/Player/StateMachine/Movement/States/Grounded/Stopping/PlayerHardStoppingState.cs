using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RamiresTechGames
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementDecelerationForce = groundedData.stoppingData.hardDecelerateForce;
        }

        #endregion

        #region Reusable Methods

        protected override void OnMove()
        {
            if (stateMachine.playerReusableData.shouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.playerRunningState);
        }

        #endregion
    }
}
