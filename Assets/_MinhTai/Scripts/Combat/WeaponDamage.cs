using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

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
            health.DealDamage(10);
        }
    }
}
