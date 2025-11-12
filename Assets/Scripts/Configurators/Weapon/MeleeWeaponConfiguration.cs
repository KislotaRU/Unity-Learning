using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConfiguration", menuName = "Configurators/Weapon/MeleeWeaponConfiguration")]
public class MeleeWeaponConfiguration : WeaponConfiguration
{
    [Header("Melee Specific")]
    [SerializeField, Min(1)] private int _maxTargets = 1;

    public int MaxTargets => _maxTargets;
}