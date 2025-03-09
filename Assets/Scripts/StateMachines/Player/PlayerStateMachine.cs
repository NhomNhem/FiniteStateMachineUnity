using System.Threading;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
     [field:SerializeField]   public InputReader inputReader {  get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SwitchState(new PlayerTestState(this));
    }
   
   
}
