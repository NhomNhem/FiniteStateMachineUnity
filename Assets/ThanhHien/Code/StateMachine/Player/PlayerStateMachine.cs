using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    public Transform MainCameraTransform { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgetDuration { get; private set; }
    [field: SerializeField] public float DodgetLength { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    private Quaternion originalRotation;

    private void Awake()
    {
        if (Animator == null) Animator = GetComponent<Animator>();
        if (Controller == null) Controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        originalRotation = transform.rotation; 
        if (InputReader == null || Animator == null) return;

        SwitchState(new PlayerFreeLookState(this));
    }

    public void SetTargeter(Targeter targeter)
    {
        Targeter = targeter;
    }

    public void FaceTarget()
    {
        if (Targeter?.CurrentTarget != null)
        {
            Vector3 direction = (Targeter.CurrentTarget.transform.position - transform.position).normalized;
            direction.y = 0f;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    public void ResetRotation() 
    {
        transform.rotation = originalRotation;
    }
    private void OnEnable()
    {

        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }
    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }
    private void HandleTakeDamage()
    {
        SwitchState(new PlayerImpactState(this));
    }
    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }
}
