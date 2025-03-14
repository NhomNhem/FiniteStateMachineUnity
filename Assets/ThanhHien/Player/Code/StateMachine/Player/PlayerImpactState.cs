using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int impactHash = Animator.StringToHash("Impact");
    private const float CrossFadeDuration = 0.1f;
    private float duration = 1f;
    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime("Impact", CrossFadeDuration);
    } 
    public override void Tick(float detlatime)
    {
        Move(detlatime);
        duration -= detlatime;
        if (duration <= 0)
        {
            ReturnToLoconmotion();
        }
    }
    public override void Exit()
    {
 
    }
}
