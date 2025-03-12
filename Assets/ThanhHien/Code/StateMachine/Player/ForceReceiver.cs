using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float drag = 0.3f;
    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Awake()
    {

        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
        }
    }

    private void Update()
    {
        if (controller == null) return; 

        if (verticalVelocity < 0 || controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void ResetForce()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }
}
