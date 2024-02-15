using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RamiresTechGames
{
    public class PlayerMovementStateMachine : StateMachine
    {
        public Player player { get; private set; }

        public PlayerReusableData playerReusableData { get; private set; }

        public PlayerIdlingState idlingState { get; }

        public PlayerDashingState dashingState { get; }

        public PlayerWalkingState playerWalkingState { get; }

        public PlayerRunningState playerRunningState { get; }

        public PlayerSprintingState playerSprintingState { get; }

        public PlayerLightStoppingState playerLightStoppingState { get; }

        public PlayerMediumStoppingState playerMediumStoppingState { get; }

        public PlayerHardStoppingState playerHardStoppingState { get; }


    public PlayerMovementStateMachine(Player player)
        {
            this.player = player;
            playerReusableData = new PlayerReusableData();

            idlingState = new PlayerIdlingState(this);
            dashingState = new PlayerDashingState(this);

            playerWalkingState = new PlayerWalkingState(this);
            playerRunningState = new PlayerRunningState(this);
            playerSprintingState = new PlayerSprintingState(this);

            playerLightStoppingState = new PlayerLightStoppingState(this);
            playerMediumStoppingState = new PlayerMediumStoppingState(this);
            playerHardStoppingState = new PlayerHardStoppingState(this);
        }
    }
}
