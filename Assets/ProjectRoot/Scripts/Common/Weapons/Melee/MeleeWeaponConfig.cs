using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConfig", menuName = "Configs/Weapon/MeleeWeaponConfig", order = 51)]
public class MeleeWeaponConfig : WeaponConfig
{
    [field: Header("Melee Specific")]
    [field: SerializeField, Min(1)] public int MaxTargets { get; private set; }
}