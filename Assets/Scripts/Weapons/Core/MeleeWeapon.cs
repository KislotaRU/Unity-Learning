using System;

public abstract class MeleeWeapon : Weapon
{
    public MeleeWeapon(
        float damage,
        float attackRate,
        float range,
        int maxTarget)
        : base(damage,
            attackRate,
            range)
    {
        MaxTargets = maxTarget > 0 ? maxTarget : throw new ArgumentOutOfRangeException(nameof(maxTarget));
    }

    public int MaxTargets { get; }

    public override void Reload()
    {
        throw new NotImplementedException();
    }
}