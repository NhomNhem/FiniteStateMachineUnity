using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");

    private const float CrossFadeDuration = 0.1f;
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
       stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }

}
