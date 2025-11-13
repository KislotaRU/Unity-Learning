using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponConfiguration", menuName = "Configurators/Weapon/RangeWeaponConfiguration")]
public class RangeWeaponConfiguration : WeaponConfiguration
{
    [Header("Ranged Specific")]
    [SerializeField, Range(1, 200)] private int _maxAmmo;
    [SerializeField, Min(0f)] private float _reloadTime;
    [SerializeField, Min(0f)] private float _projectileVelocity;
    [SerializeField, Min(1)] private int _projectilesPerShot;

    public int MaxAmmo => _maxAmmo;
    public float ReloadTime => _reloadTime;
    public float ProjectileVelocity => _projectileVelocity;
    public int ProjectilesPerShot => _projectilesPerShot;
}