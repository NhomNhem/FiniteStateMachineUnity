using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHandler : MonoBehaviour
{

    [SerializeField] private GameObject weaponLogic;

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    }
    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }
}