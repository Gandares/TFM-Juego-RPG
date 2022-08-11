using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "newWeapon", menuName = "Scriptable Objects/Weapons/Weapon")]
public class WeaponSO : ScriptableObject
{
    public string name;
    public float damage;
    public string magicType;
    public string rareza;
    public Sprite weaponImage;
    public AbilitySO[] abilities;
}
