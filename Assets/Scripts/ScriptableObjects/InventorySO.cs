using UnityEngine;

[CreateAssetMenu(fileName = "newInventory", menuName = "Scriptable Objects/Inventory")]
public class InventorySO : ScriptableObject
{
    [Header("Inventory Information")]
    public int gold;
    public WeaponSO firstWeapon;
    public WeaponSO secondWeapon;
}
