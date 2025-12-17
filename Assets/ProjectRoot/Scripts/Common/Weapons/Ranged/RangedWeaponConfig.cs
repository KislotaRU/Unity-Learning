using UnityEngine;

[CreateAssetMenu(fileName = "RangedWeaponConfig", menuName = "Configs/Weapon/RangedWeaponConfig")]
public class RangedWeaponConfig : WeaponConfig
{
    [field: Header("Ranged Specific")]
    [field: SerializeField, Min(0f)] public float ReloadTime { get; private set; }
    [field: SerializeField, Min(0f)] public float ProjectileVelocity { get; private set; }
    [field: SerializeField, Min(0f)] public float ProjectileLifeTime { get; private set; }
    [field: SerializeField, Range(1, 200)] public int ProjectileMaxCount { get; private set; }
    [field: SerializeField, Range(1, 100)] public int ProjectilesPerShot { get; private set; }
}