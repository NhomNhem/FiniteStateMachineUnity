internal class PlayerTargetState : State
{
    private PlayerStateMachine stateMachine;

    public PlayerTargetState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        // Add logic to handle entering the target state
    }

    public override void Tick(float deltaTime)
    {
        // Add logic to handle updates in the target state
    }

    public override void Exit()
    {
        // Add logic to handle exiting the target state
    }
}