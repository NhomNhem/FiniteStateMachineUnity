using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private State currentState;


    // Update is called once per frame
    public void SwitchState(State newState)
    {
       
            currentState?.Exit();
        
        currentState = newState;
        currentState?.Enter();
    }
    
    private void Update()
    {
            currentState?.Tick(Time.deltaTime);
        
    }
}
