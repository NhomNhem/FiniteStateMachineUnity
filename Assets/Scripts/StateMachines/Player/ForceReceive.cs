using UnityEngine;

public class ForceReceive : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private float verticalVelocity;

    public Vector3 Movement => Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}