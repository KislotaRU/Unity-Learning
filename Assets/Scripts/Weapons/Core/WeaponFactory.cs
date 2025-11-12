public class WeaponFactory
{
    public Weapon CreateAxe(MeleeWeaponConfiguration configuration) =>
        new WeaponAxe(configuration);

    public Weapon CreatePickacxe(MeleeWeaponConfiguration configuration) =>
        new WeaponPickacxe(configuration);

    public Weapon CreateRevolver(RangeWeaponConfiguration configuration) =>
        new WeaponRevolver(configuration);

    public Weapon CreateRifle(RangeWeaponConfiguration configuration) =>
        new WeaponRifle(configuration);

    public Weapon CreateShotgun(RangeWeaponConfiguration configuration) =>
        new WeaponShotgun(configuration);
}