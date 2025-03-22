using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private int healAmount = 10; // L??ng máu h?i
    [SerializeField] private float lifeTime = 10f; // Th?i gian t?n t?i

    private void Start()
    {
        Destroy(gameObject, lifeTime); // T? h?y sau 10 giây
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // N?u Player ch?m vào
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.GainHealth(healAmount);
                Destroy(gameObject); // Xóa Item sau khi nh?t
            }
        }
    }
}
