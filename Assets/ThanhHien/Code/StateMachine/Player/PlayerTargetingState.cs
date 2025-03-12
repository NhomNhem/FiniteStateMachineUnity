using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward"); // Sửa tên đúng
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight"); // Sửa tên đúng
    private const float CrossFaceDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;

        if (stateMachine.Targeter.SelectTarget())
        {
            stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFaceDuration);
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        float speed = movement.magnitude;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, speed > 0.1f ? 1f : 0f, AnimatorDampTime, deltaTime);

        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private new void FaceTarget()
    {
        if (stateMachine.Targeter.CurrentTarget == null) return;

        Vector3 targetPosition = stateMachine.Targeter.CurrentTarget.transform.position;
        Vector3 direction = (targetPosition - stateMachine.transform.position).normalized;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            stateMachine.transform.rotation = Quaternion.Lerp(
                stateMachine.transform.rotation,
                targetRotation,
                Time.deltaTime * stateMachine.RotationDamping);
        }
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

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.Animator.HasParameter(TargetingForwardHash))
        {
            float forwardValue = stateMachine.InputReader.MovementValue.y == 0 ? 0 : Mathf.Sign(stateMachine.InputReader.MovementValue.y);
            stateMachine.Animator.SetFloat(TargetingForwardHash, forwardValue, 0.1f, deltaTime);
        }

        if (stateMachine.Animator.HasParameter(TargetingRightHash))
        {
            float rightValue = stateMachine.InputReader.MovementValue.x == 0 ? 0 : Mathf.Sign(stateMachine.InputReader.MovementValue.x);
            stateMachine.Animator.SetFloat(TargetingRightHash, rightValue, 0.1f, deltaTime);
        }
    }
}

// Thêm extension để kiểm tra parameter có tồn tại trong Animator không
public static class AnimatorExtensions
{
    public static bool HasParameter(this Animator animator, int hash)
    {
        for (int i = 0; i < animator.parameterCount; i++)
        {
            if (animator.parameters[i].nameHash == hash)
                return true;
        }
        return false;
    }
}
