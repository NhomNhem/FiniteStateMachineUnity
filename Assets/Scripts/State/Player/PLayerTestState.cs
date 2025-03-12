using UnityEngine;

public class PLayerTestState : PlayerBaseState
{
    private float timer = 5f;

    public PLayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        Debug.Log("Enter");
    }


    public override void Tick(float deltaTime)
    {
        timer -= deltaTime;
        Debug.Log("Tick");

        if (timer <= 0f)
        {
            stateMachine.SwitchState(new PLayerTestState(stateMachine));
        }

    }
    public override void Exit()
    {
        Debug.Log("Exit");

    }

}
