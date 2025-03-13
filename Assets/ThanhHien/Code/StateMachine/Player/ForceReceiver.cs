using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float drag = 0.3f;
    [SerializeField] private NavMeshAgent agent;
    private Vector3 dampingVelocity;
    private Vector3 impact;
    private float verticalVelocity;

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogWarning("CharacterController is missing!", this);
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

        if (agent != null)
        {
            if(impact.sqrMagnitude < 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;

        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    public void ResetForce()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }
}
