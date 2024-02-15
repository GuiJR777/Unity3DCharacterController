using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RamiresTechGames
{
    public class PlayerInput : MonoBehaviour
    {
       public PlayerInputActions playerInputActions { get; private set; }
       public PlayerInputActions.PlayerActions playerActions { get; private set; }

        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
            playerActions = playerInputActions.Player;
        }

        private void OnEnable()
        {
            playerInputActions.Enable();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();
        }

        public void DisableActionForTime(InputAction action, float time)
        {
            action.Disable();
            StartCoroutine(EnableActionAfterTime(action, time));
        }

        private IEnumerator EnableActionAfterTime(InputAction action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Enable();
        }
    }
}
