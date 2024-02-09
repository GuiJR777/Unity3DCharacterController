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

        public PlayerWalkingState playerWalkingState { get; }

        public PlayerRunningState playerRunningState { get; }

        public PlayerSprintingState playerSprintingState { get; }


    public PlayerMovementStateMachine(Player player)
        {
            this.player = player;
            playerReusableData = new PlayerReusableData();

            idlingState = new PlayerIdlingState(this);
            playerWalkingState = new PlayerWalkingState(this);
            playerRunningState = new PlayerRunningState(this);
            playerSprintingState = new PlayerSprintingState(this);
        }
    }
}
