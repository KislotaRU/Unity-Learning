using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConfiguration1", menuName = "Configurators/Weapon/MeleeWeaponConfiguration")]
public class MeleeWeaponConfiguration : WeaponConfiguration
{
    [field: Header("Melee Specific")]
    [field: SerializeField, Min(1)] public int MaxTargets { get; private set; }
}