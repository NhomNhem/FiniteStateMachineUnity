using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;

    private float knockback;

    private List<Collider> alreadyColidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyColidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == myCollider) { return; }

        if (alreadyColidedWith.Contains(other)) { return; }

        alreadyColidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }

        if(other.TryGetComponent<ForceReceive>(out ForceReceive forceReceive))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;

            forceReceive.AddForce(direction * knockback);
        }
    }

    public void SetAttack(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }
}
