using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue {  get; private set; }
    public event Action JumpEvent;
    public event Action DogdeEvent;
    private Controls controls;
    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) {return; }
        JumpEvent?.Invoke();
    }

    public void OnDogde(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        DogdeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }
}
