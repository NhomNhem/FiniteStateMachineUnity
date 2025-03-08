using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timer;
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float detlatime)
    {
        timer += detlatime;
        Debug.Log(timer);
    }
    public override void Exit()
    {

        stateMachine.InputReader.JumpEvent -= OnJump;
    }
    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine));
    }


}
