using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [SerializeField] public InputReader InputReader {  get; private set; } 

    private void Start()
    {
        SwitchState(new PlayerTestState(this));
    }

  
}
