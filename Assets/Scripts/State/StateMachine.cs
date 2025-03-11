using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
    public void SwitchState (State newstate)
    {

        currentState?.Exit (); 
        currentState = newstate;
        currentState.Enter();

       
    }
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
