using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");
    private const float CrossFadeDuration = 0.1f;

    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Blocking Started");

        stateMachine.Health.SetInvunerable(true);
        if (stateMachine.Animator.HasParameter(BlockHash))
        {
            stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
        }
        else
        {
            Debug.LogError("Animator does not have a parameter named 'Block'");
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            Debug.Log("Blocking Stopped - Switching to Targeting State");
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }
        if(stateMachine.Targeter.CurrentTarget == null )
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        Debug.Log("Exiting Block State");
        stateMachine.Health.SetInvunerable(false);
    }
}
