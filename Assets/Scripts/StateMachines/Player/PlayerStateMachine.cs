using System.Threading;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
   
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field:SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    
    [field: SerializeField] public float RotationDamping { get; private set; }
    public Transform MaincameraTransform { get; private set; }
    [field: SerializeField] public ForceReceive ForceReceiver { get; private set; }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        MaincameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
   
   
}
