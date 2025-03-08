using UnityEngine;

namespace FiniteStateMachine.Player
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine _stateMachine;

        public PlayerBaseState(PlayerStateMachine stateMachine) {
            _stateMachine = stateMachine;
        }


    }
}
