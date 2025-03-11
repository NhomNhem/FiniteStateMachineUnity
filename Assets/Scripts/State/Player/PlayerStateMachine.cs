using UnityEngine;

public abstract class PlayerStateMachine : StateMachine
{
    protected PlayerStateMachine stateMachine;

    public PlayerStateMachine(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
