using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LoconmotionHash = Animator.StringToHash("loconmotion");

    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LoconmotionHash, CrossFadeDuration);
    }
    public override void Tick(float detlatime)
    {
        Move(detlatime);
        if(IsInChaseRange())
        {
            Debug.Log("In Range");
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampTime, detlatime);
    }
    public override void Exit()
    {
        
    }

}
