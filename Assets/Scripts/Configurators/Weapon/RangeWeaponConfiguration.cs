using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeaponConfiguration", menuName = "Configurators/Weapon/RangeWeaponConfiguration")]
public class RangeWeaponConfiguration : WeaponConfiguration
{
    [field: Header("Ranged Specific")]
    [field: SerializeField, Min(0f)] public float ReloadTime { get; private set; }
    [field: SerializeField, Min(0f)] public float ProjectileVelocity { get; private set; }
    [field: SerializeField, Min(0f)] public float ProjectileLifeTime { get; private set; }
    [field: SerializeField, Range(1, 200)] public int MaxAmmo { get; private set; }
    [field: SerializeField, Range(1, 100)] public int ProjectilesPerShot { get; private set; }
}