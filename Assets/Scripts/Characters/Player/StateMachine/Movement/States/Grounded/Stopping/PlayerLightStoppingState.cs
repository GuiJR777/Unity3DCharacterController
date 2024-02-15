using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RamiresTechGames
{
    public class PlayerLightStoppingState : PlayerStoppingState
    {
        public PlayerLightStoppingState(PlayerMovementStateMachine stateMachine) : base(stateMachine)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            stateMachine.playerReusableData.movementDecelerationForce = groundedData.stoppingData.lightDecelerateForce;
        }

        #endregion
    }
}
