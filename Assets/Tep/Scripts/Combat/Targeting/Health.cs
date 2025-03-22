using UnityEngine;
using UnityEngine.UI; // Import UI
using UnityEngine.SceneManagement; // Import ?? load l?i scene

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private GameObject gameOverUI; // Thêm UI Game Over

    private void Start()
    {
        currentHealth = maxHealth;
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false); // ?n Game Over khi b?t ??u
        }
    }

    public void DealDamage(int damage)
    {
        if (currentHealth == 0) return;

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        Debug.Log("Máu còn l?i: " + currentHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " ?ã ch?t!");
        ShowGameOver();
    }

    private void ShowGameOver()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true); // Hi?n UI Game Over
        }
        Time.timeScale = 0; // D?ng game
    }

    public void RestartGame() // Nút Restart
    {
        Time.timeScale = 1; // Ch?y l?i game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GainHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Nh?t Item! Máu hi?n t?i: " + currentHealth);
    }
}
