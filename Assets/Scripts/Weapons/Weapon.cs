using System;

public abstract class Weapon : IWeapon
{
    private const int SecondCount = 60;

    protected readonly ITimerService<IWeapon> _timerService;

    private readonly WeaponConfiguration _configuration;

    public Weapon(WeaponConfiguration configuration, ITimerService<IWeapon> timerService)
    {
        _configuration = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
        _timerService = timerService ?? throw new ArgumentNullException(nameof(timerService));
    }

    public float Damage => _configuration.Damage;
    public float AttackRate => _configuration.AttackRate;
    public float Range => _configuration.Range;

    public float TimeBetweenAttacks => SecondCount / AttackRate;
    public virtual bool CanAttack => true;

    public virtual void Attack()
    {
        if (CanAttack == false)
            return;
    }

    public abstract void Reload();
}