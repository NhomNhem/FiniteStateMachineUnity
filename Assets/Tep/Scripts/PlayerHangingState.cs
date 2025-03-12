using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private readonly int HangingHash = Animator.StringToHash("Hanging"); 
    private const float CrossFaceDuration = 0.1f;
    
    private Vector3 ledgeForward;
    public PlayerHangingState(PlayerStateMachine stateMachine,Vector3 ledgeForward) : base(stateMachine)
    {
        
        this.ledgeForward = ledgeForward;
    }
    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward,Vector3.up);
        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFaceDuration);
    }
public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue.y > 0f)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }
        else
        if (stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.ForceReceive.Reset();
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
          
        }
    }
    public override void Exit()
    {
        
    }

    
}
   
