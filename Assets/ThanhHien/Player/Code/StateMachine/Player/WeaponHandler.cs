using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    public void EnableWeaponLogic()
    {
        weaponLogic.SetActive(true);
    }
    public void DisableWeaponLogic()
    {
        weaponLogic.SetActive(false);
    } 
}
