using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LoconmotionHash = Animator.StringToHash("loconmotion");

    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LoconmotionHash, CrossFadeDuration);
    }
    public override void Tick(float detlatime)
    {
        
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, detlatime);
    }
    public override void Exit()
    {

    }

}