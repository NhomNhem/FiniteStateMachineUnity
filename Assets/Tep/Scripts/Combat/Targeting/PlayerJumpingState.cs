using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private const float CrossFaceDuration = 0.1f;
    private Vector3 momentum;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        stateMachine.ForceReceive.Jump(stateMachine.JumpForce);
        momentum = stateMachine.Controller.velocity;
        momentum.y = 0f;
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFaceDuration);
        stateMachine.LedgeDectector.OnLedgeDetect += HandleLedgedetect;
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);
        if(stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }
        FaceTarget(); 

    }

    public override void Exit()
    {
        stateMachine.LedgeDectector.OnLedgeDetect -= HandleLedgedetect;
    }
    private void HandleLedgedetect(Vector3 ledgeForward)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward));
    }

}
