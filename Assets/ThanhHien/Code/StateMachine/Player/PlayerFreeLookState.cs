using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");

    private const float AnimatorDampTime = 0.1f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget; 
        stateMachine.Animator.Play(FreeLookBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        
        if (movement.sqrMagnitude > 0.01f) 
        {
            FaceMovementDirection(movement, deltaTime);
        }


        stateMachine.Controller.Move(movement * stateMachine.FreeLookMovementSpeed * deltaTime);

        float speed = movement.magnitude;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, speed > 0.1f ? 1f : 0f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        if (stateMachine.Targeter.GetComponent<Targeter>().SelectTarget()) return;
        stateMachine.SwitchState(new PlayerTargetState(stateMachine));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        if (movement.sqrMagnitude < Mathf.Epsilon) return; 

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            targetRotation,
            deltaTime * stateMachine.RotationDamping);
    }
}
