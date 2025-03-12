using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private void Start()
    {
        SwitchState(new PLayerTestState(this));
    }
    void start()
    {

    }
}
