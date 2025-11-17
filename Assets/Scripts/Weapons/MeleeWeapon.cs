using System;

public class MeleeWeapon : Weapon
{
    private readonly MeleeWeaponConfiguration _configuration;

    public MeleeWeapon(MeleeWeaponConfiguration configuration, ITimerService<IWeapon> timerService) : base(configuration, timerService)
    {
        _configuration = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
    }

    public int MaxTargets => _configuration.MaxTargets;

    public override void Reload()
    {
        throw new NotImplementedException();
    }
}