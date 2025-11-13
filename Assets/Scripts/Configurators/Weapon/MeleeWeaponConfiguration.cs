using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeWeaponConfiguration", menuName = "Configurators/Weapon/MeleeWeaponConfiguration")]
public class MeleeWeaponConfiguration : WeaponConfiguration
{
    [Header("Melee Specific")]
    [SerializeField, Min(1)] private int _maxTargets;

    public int MaxTargets => _maxTargets;
}