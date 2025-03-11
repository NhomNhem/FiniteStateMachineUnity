using Unity.VisualScripting;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private float vertialVelocity;
    public Vector3 Movement => Vector3.up * vertialVelocity;
    private void Update()
    {
        if( vertialVelocity < 0 || controller.isGrounded)
        {
            vertialVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            vertialVelocity += Physics.gravity.y * Time.deltaTime;
        }
    }
}
