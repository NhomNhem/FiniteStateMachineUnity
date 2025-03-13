using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Máu tối đa

    private int health; // Máu hiện tại

    private bool isInvulnerable; // Vô hại

    public event Action OnTakeDamage;

    public event Action OnDie;

    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvulnerable(bool invulnerable)
    {
        this.isInvulnerable = invulnerable;
    }

    public void DealDamage(int damage)
    {
        if (health == 0) { return; }

        if (isInvulnerable) { return; }

        health = Mathf.Max(health - damage, 0);

        OnTakeDamage?.Invoke();

        if (health == 0)
        {
            OnDie?.Invoke();
        }

        Debug.Log(health);
    }
}
