using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAWeapon : MonoBehaviour
{
    public WeaponSO weapon;
    public InventorySO inventory;
    private bool equiped = false;
    public void OnTakeWeapon()
    {
        if (inventory.firstWeapon == null && !equiped)
        {
            inventory.firstWeapon = weapon;
            equiped = true;
        }
        
        if(inventory.secondWeapon == null && !equiped)
        {
            inventory.secondWeapon = weapon;
            equiped = true;
        }
    }
}
