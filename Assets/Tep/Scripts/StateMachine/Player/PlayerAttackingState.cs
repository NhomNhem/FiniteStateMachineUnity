using System;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previosFrameTime;

    private Attack attack;
    private bool alreadyAppliedForce;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack((int)attack.Damage);
        stateMachine.Animator.CrossFadeInFixedTime (attack.AnimationName, attack.TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= previosFrameTime && normalizedTime < 1f)
        {
            if(normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
           if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        previosFrameTime = normalizedTime;
    }

    public override void Exit()
    {
       
    }

    private void TryComboAttack(float normalizedTime)
    {
        if(attack.ComeboStateIndex == -1) { return; }

        if(normalizedTime < attack.ComeboAttackTime) { return; }

        stateMachine.SwitchState
        (
           new PlayerAttackingState
           (
              stateMachine,
              attack.ComeboStateIndex
           )
        );
    }
    private void TryApplyForce()
    {
        if(alreadyAppliedForce) { return; }
        stateMachine.ForceReceive.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }
    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    } 
}
