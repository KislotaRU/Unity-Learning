using System;

public abstract class RangeWeapon : Weapon
{
    public RangeWeapon(
        float damage,
        float attackRate,
        float range,
        int maxAmmo,
        float reloadTime,
        float projectileVelocity,
        int projectilesPerShot)
        : base(damage,
            attackRate,
            range)
    {
        MaxAmmo = maxAmmo > 0 ? maxAmmo : throw new ArgumentOutOfRangeException(nameof(maxAmmo));
        ReloadTime = reloadTime >= 0f ? reloadTime : throw new ArgumentOutOfRangeException(nameof(reloadTime));
        ProjectileVelocity = projectileVelocity >= 0f ? projectileVelocity : throw new ArgumentOutOfRangeException(nameof(projectileVelocity));
        ProjectilesPerShot = projectilesPerShot > 0 ? projectilesPerShot : throw new ArgumentOutOfRangeException(nameof(projectilesPerShot));
        Magazine = new Counter(MaxAmmo);
    }

    public int MaxAmmo { get; }
    public float ReloadTime { get; }
    public float ProjectileVelocity { get; }
    public int ProjectilesPerShot { get; }
    public ICounter Magazine { get; }
    public override bool CanAttack => Magazine.CurrentValue >= ProjectilesPerShot;

    public override void Reload()
    {
        throw new NotImplementedException();
    }
}