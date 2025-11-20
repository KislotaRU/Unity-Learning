using System;

public class MeleeWeapon : Weapon
{
    private readonly MeleeWeaponConfiguration _configuration;

    public MeleeWeapon(MeleeWeaponConfiguration configuration, ITimerService<IWeapon> timerService) : base(configuration, timerService)
    {
        _configuration = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
    }

    public int MaxTargets => _configuration.MaxTargets;

    public override bool CanAttack => CanHit;
    private bool CanHit { get; set; }

    public override void Attack()
    {
        base.Attack();

        CanHit = false;

        Reload();
    }

    public override void Reload()
    {
        if (CanHit)
            return;

        if (_timerService.GetAccumulatedTime(this) > 0f)
            return;

        _timerService.CreateTimer(this, TimeBetweenAttacks, () =>
        {
            CanHit = true;
        });
    }
}