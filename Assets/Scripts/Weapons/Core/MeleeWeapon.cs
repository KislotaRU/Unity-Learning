using System;

public class MeleeWeapon : Weapon
{
    public MeleeWeapon(float damage, float attackRate, float range, ITimerService<IWeapon> timerService,
        int maxTarget)
        : base(damage, attackRate, range, timerService)
    {
        MaxTargets = maxTarget > 0 ? maxTarget : throw new ArgumentOutOfRangeException(nameof(maxTarget));
    }

    public int MaxTargets { get; }

    public override void Reload()
    {
        throw new NotImplementedException();
    }
}