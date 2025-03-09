using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

    private void Awake()
    {
        if (Animator == null)
        {
            Animator = GetComponent<Animator>();
        }

        if (Controller == null)
        {
            Controller = GetComponent<CharacterController>();
        }
    }

    private void Start()
    {
        if (InputReader == null || Animator == null)
        {
            return;
        }

        SwitchState(new PlayerTestState(this));
    }
}
