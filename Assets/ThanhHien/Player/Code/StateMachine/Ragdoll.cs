using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;
    private Collider[] allColliders;
    private Rigidbody[] allRigidbodies;

    private void Start()
    {
        allColliders = GetComponentsInChildren<Collider>(true);
        allRigidbodies = GetComponentsInChildren<Rigidbody>(true);
        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider col in allColliders)
        {
            if (col != null && col.gameObject.CompareTag("Ragdoll"))
            {
                col.enabled = isRagdoll;
            }
        }

        foreach (Rigidbody rb in allRigidbodies)
        {
            if (rb != null && rb.gameObject.CompareTag("Ragdoll"))
            {
                rb.isKinematic = !isRagdoll;
                rb.useGravity = isRagdoll;
            }
        }

        if (controller != null)
        {
            controller.enabled = !isRagdoll;
        }

        if (animator != null)
        {
            animator.enabled = !isRagdoll;
        }
    }
}
