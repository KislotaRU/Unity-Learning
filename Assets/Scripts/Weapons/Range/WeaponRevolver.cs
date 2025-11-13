public class WeaponRevolver : RangeWeapon
{
    public WeaponRevolver(
        float damage,
        float attackRate,
        float range,
        int maxAmmo,
        float reloadTime,
        float projectileVelocity,
        int projectilesPerShot)
        : base(damage,
            attackRate,
            range,
            maxAmmo,
            reloadTime,
            projectileVelocity,
            projectilesPerShot)
    {
    }
}