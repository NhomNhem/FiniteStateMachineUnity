using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetinglendTreeHash = Animator.StringToHash("TargetinglendTreeHash");
    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.TargetEvent += OnTarget;

        stateMachine.Animator.Play(TargetinglendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Targeter != null)
        {
            Debug.Log(stateMachine.Targeter.name);
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
    }

    private void OnTarget()
    {
        stateMachine.SetTargeter(null);
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }
}
