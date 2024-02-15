using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RamiresTechGames
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void HandleInput();
        public void Update();
        public void FixedUpdate();
        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        public void OnAnimationTransitionEvent();
    }
}
