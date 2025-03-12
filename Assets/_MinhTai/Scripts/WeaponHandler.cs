using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{

    [SerializeField] private List<GameObject> weaponColliders; // Danh sách vùng va chạm của vũ khí

    public void EnableWeapon()
    {
        foreach (GameObject collider in weaponColliders)
        {
            collider.SetActive(true);
        }
    }

    public void DisableWeapon()
    {
        foreach (GameObject collider in weaponColliders)
        {
            collider.SetActive(false);
        }
    }
}
