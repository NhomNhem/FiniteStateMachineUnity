using System;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previosFrameTime;

    private bool alreadyAppliedForce;

    private Attack attack;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);

        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        if (normalizedTime >= previosFrameTime && normalizedTime < 1f)
        {

            if (normalizedTime >= attack.ForceTime)
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
            if (stateMachine.Targeter.CurrentTarget != null)
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
   
}
