using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponConfiguration", menuName = "Configurators/Weapon/RangeWeaponConfiguration")]
public class RangeWeaponConfiguration : WeaponConfiguration
{
    [Header("Ranged Specific")]
    [SerializeField, Range(1, 200)] private int _maxAmmo = 1;
    [SerializeField, Min(0f)] private float _reloadTime;
    [SerializeField, Min(1f)] private float _projectileVelocity = 1f;

    public int MaxAmmo => _maxAmmo;
    public float ReloadTime => _reloadTime;
    public float ProjectileVelocity => _projectileVelocity;
}