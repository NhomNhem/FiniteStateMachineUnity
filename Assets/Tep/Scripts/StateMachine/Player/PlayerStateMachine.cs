using System.Collections;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceive ForceReceive { get; private set; }
    [field: SerializeField] public WeaponHitbox Weapon { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public LedgeDectector LedgeDectector { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public Health PlayerHealth { get; private set; } // Thêm health vào PlayerStateMachine

    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;
    public Transform MainCameraTransform { get; private set; }
    private bool isSpeedBoosted = false;
    private bool isInvisible = false;

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // N?u va ch?m v?i quái v?t
        {
            PlayerHealth.DealDamage(10); // G?i hàm tr? máu
            Debug.Log("B? quái v?t t?n công! Tr? 10 máu.");
        }
    }
    private IEnumerator SpeedBoost()
    {
        if (isSpeedBoosted) yield break;
        isSpeedBoosted = true;

        FreeLookMovementSpeed *= 2; // T?ng t?c ?? ch?y
        Debug.Log("T?ng t?c ?? ch?y!");

        yield return new WaitForSeconds(2);

        FreeLookMovementSpeed /= 2; // Tr? t?c ?? v? bình th??ng
        isSpeedBoosted = false;
        Debug.Log("T?c ?? ch?y tr? l?i bình th??ng.");
    }

    private IEnumerator Invisibility()
    {
        if (isInvisible) yield break;
        isInvisible = true;

        // ?n nhân v?t b?ng cách t?t MeshRenderer ho?c ??i màu
        Renderer renderer = GetComponent<Renderer>();
        if (renderer) renderer.enabled = false;

        Debug.Log("Nhân v?t tàng hình!");

        yield return new WaitForSeconds(2);

        if (renderer) renderer.enabled = true; // Hi?n l?i nhân v?t
        isInvisible = false;
        Debug.Log("Nhân v?t hi?n tr? l?i.");
    }

    private void Teleport()
    {
        Vector3 teleportDirection = transform.forward * 5f; // D?ch chuy?n 5 ??n v? v? phía tr??c
        Controller.enabled = false;
        transform.position += teleportDirection;
        Controller.enabled = true;

        Debug.Log("D?ch chuy?n!");
    }
}
