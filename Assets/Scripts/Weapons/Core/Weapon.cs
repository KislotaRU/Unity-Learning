using System;

public abstract class Weapon : IWeapon
{
    protected readonly ITimerService<IWeapon> _timerService;

    public Weapon(float damage, float attackRate, float range, ITimerService<IWeapon> timerService)
    {
        Damage = damage >= 0f ? damage : throw new ArgumentOutOfRangeException(nameof(damage));
        AttackRate = attackRate >= 0f ? attackRate : throw new ArgumentOutOfRangeException(nameof(attackRate));
        Range = range >= 0f ? range : throw new ArgumentOutOfRangeException(nameof(range));
        _timerService = timerService ?? throw new ArgumentNullException(nameof(timerService));
    }

    public float Damage { get; }
    public float AttackRate { get; }
    public float Range { get; }
    public virtual bool CanAttack => true;

    public virtual void Attack()
    {
        if (CanAttack == false)
            throw new InvalidOperationException();
    }

    public abstract void Reload();
}