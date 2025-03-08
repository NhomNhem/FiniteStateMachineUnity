using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input_Action
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public event Action OnJumpEvent;
        public event Action OnDodgeEvent;
        
        private Controls _controls;

        private void Start() {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);

            _controls.Player.Enable();
        }

        private void OnDestroy() {
            _controls.Player.Disable();
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (context.performed) {
                OnJumpEvent?.Invoke();
            }
        }

        public void OnDodge(InputAction.CallbackContext context) {
            if (context.performed) {
                Debug.Log("Dodge");
            }
        }

    }
}
