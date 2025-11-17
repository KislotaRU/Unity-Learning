using System;

public abstract class Weapon : IWeapon
{
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
    public virtual bool CanAttack => true;

    public virtual void Attack()
    {
        if (CanAttack == false)
            throw new InvalidOperationException();
    }

    public abstract void Reload();
}