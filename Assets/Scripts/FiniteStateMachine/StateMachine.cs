using System;
using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        private State _currentState;

        public void Update() {
            _currentState.Tick(Time.deltaTime);
        }

        public void SwitchState(State newState) {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}