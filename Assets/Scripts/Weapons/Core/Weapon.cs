public abstract class Weapon : IWeapon
{
    public Weapon(WeaponConfiguration configuration)
    {
        Damage = configuration.Damage;
        AttackRate = configuration.AttackRate;
        Range = configuration.Range;
    }

    public float Damage { get; }
    public float AttackRate { get; }
    public float Range { get; }

    public abstract void Attack(IHealth health);

    public abstract bool CanAttack();

    public abstract void Reload();
}