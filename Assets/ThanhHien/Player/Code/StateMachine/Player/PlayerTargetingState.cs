using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private Vector2 dodgeDirectionInput;
    private float remainingDodgeTime;
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.DogdeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;

        if (stateMachine.Targeter.SelectTarget())
        {
            stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
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
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockState(stateMachine));
            return;
        }
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement(deltaTime);
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        float speed = movement.magnitude;
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, speed > 0.1f ? 1f : 0f, AnimatorDampTime, deltaTime);

        UpdateAnimator(deltaTime);
        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.DogdeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnTarget()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        remainingDodgeTime = stateMachine.DodgetDuration;
    }

    private void OnDodge()
    {
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgetCooldown)
        {
            return;
        }
        stateMachine.SetDodgeTime(Time.time);

        dodgeDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgetDuration;
    }
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
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

    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();
        if (remainingDodgeTime > 0f)
        {
            movement += stateMachine.transform.right * dodgeDirectionInput.x * stateMachine.DodgetLength / stateMachine.DodgetDuration;
            movement += stateMachine.transform.forward * dodgeDirectionInput.y * stateMachine.DodgetLength / stateMachine.DodgetDuration;

            remainingDodgeTime = Mathf.Max(remainingDodgeTime - deltaTime, 0f);
        }
        else
        {
            movement += stateMachine.transform.right * dodgeDirectionInput.x * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * dodgeDirectionInput.y * stateMachine.InputReader.MovementValue.y;
        }

        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        movement += forward * stateMachine.InputReader.MovementValue.y;
        movement += right * stateMachine.InputReader.MovementValue.x;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.Animator.HasParameter(TargetingForwardHash))
        {
            float forwardValue = stateMachine.InputReader.MovementValue.y == 0 ? 0 : Mathf.Sign(stateMachine.InputReader.MovementValue.y);
            stateMachine.Animator.SetFloat(TargetingForwardHash, forwardValue, AnimatorDampTime, deltaTime);
        }

        if (stateMachine.Animator.HasParameter(TargetingRightHash))
        {
            float rightValue = stateMachine.InputReader.MovementValue.x == 0 ? 0 : Mathf.Sign(stateMachine.InputReader.MovementValue.x);
            stateMachine.Animator.SetFloat(TargetingRightHash, rightValue, AnimatorDampTime, deltaTime);
        }
    }
}

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