using System;

public class RangeWeapon : Weapon
{
    private readonly RangeWeaponConfiguration _configuration;

    public RangeWeapon(RangeWeaponConfiguration configuration, ITimerService<IWeapon> timerService) : base(configuration, timerService)
    {
        _configuration = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));

        Magazine = new Counter(MaxAmmo);
    }

    public ICounter Magazine { get; }
    public float ReloadTime => _configuration.ReloadTime;
    public float ProjectileVelocity => _configuration.ProjectileVelocity;
    public int MaxAmmo => _configuration.MaxAmmo;
    public int ProjectilesPerShot => _configuration.ProjectilesPerShot;
    public override bool CanAttack => Magazine.CurrentValue >= ProjectilesPerShot;

    public override void Reload()
    {
        _timerService.CreateTimer(this, ReloadTime, () =>
        {
            Magazine.Fill();
        });
    }
}