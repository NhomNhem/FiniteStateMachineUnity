using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }
    protected void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) { return; }

        Vector3 LookFos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        LookFos.y = 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(LookFos);
    }
}
