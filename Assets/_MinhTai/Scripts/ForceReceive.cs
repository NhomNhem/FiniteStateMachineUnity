using UnityEngine;

public class ForceReceive : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float drap = 0.3f;

    private Vector3 dampingVelocity;

    private Vector3 impact;

    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if(verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drap);
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
    }
}
