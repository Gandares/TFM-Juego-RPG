using UnityEngine;
using ScriptableObjectArchitecture;

[CreateAssetMenu(fileName = "newAbility", menuName = "Scriptable Objects/Weapons/ability")]
public class AbilitySO : ScriptableObject
{
    public string name;
    public float damage;
    public float costeMana;
    public string MagicType;
    public Sprite Image;
}
