using UnityEngine;

public class ForceReceive : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private Vector3 dampingVelocity;
    private float verticalVelocity;
    [SerializeField] private float drag = 0.3f;
    private Vector3 impact;
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
       
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity,drag);
       
    }
    public void AddForce(Vector3 force)
    {
        impact += force;
        //if(agent != null)
        //{
        //    agent.enabled = false;
        //}
    }
    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }
}
