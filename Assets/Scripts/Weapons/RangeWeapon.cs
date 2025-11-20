using System;
using UnityEngine;

public class RangeWeapon : Weapon
{
    private readonly BulletSpawner _bulletSpawner;
    private readonly RangeWeaponConfiguration _configuration;

    public RangeWeapon(RangeWeaponConfiguration configuration, ITimerService<IWeapon> timerService, BulletSpawner bulletSpawner) : base(configuration, timerService)
    {
        _configuration = configuration != null ? configuration : throw new ArgumentNullException(nameof(configuration));
        _bulletSpawner = bulletSpawner != null ? bulletSpawner : throw new ArgumentNullException(nameof(bulletSpawner));

        ProjectileCount = MaxAmmo;
    }

    public Vector2 Position { get; private set; }
    public Vector2 Direction => Vector2.right;
    public float ProjectileCount { get; private set; }
    public float ReloadTime => _configuration.ReloadTime;
    public float ProjectileVelocity => _configuration.ProjectileVelocity;
    public int MaxAmmo => _configuration.MaxAmmo;
    public int ProjectilesPerShot => _configuration.ProjectilesPerShot;

    public override bool CanAttack => IsReloaded && CanShoot;
    private bool IsReloaded => ProjectileCount >= ProjectilesPerShot;
    private bool CanShoot { get; set; }

    public override void Attack()
    {
        Bullet bullet;

        base.Attack();

        ProjectileCount -= ProjectilesPerShot;

        bullet = _bulletSpawner.Spawn();

        bullet.Destroyed += HandleProjectileDestroyed;
        bullet.Hit += HandleProjectileHit;

        bullet.Initialize(Position, Direction);
        CanShoot = false;

        _timerService.CreateTimer(this, TimeBetweenAttacks, () =>
        {
            CanShoot = true;
        });
    }

    public override void Reload()
    {
        if (ProjectileCount >= MaxAmmo)
            return;

        if (_timerService.GetAccumulatedTime(this) > 0f)
            return;

        _timerService.CreateTimer(this, ReloadTime, () =>
        {
            ProjectileCount = MaxAmmo;
        });
    }

    private void HandleProjectileHit(IHealth health)
    {
        if (health == null)
            throw new ArgumentNullException(nameof(health));

        health.TakeDamage(Damage);
    }

    private void HandleProjectileDestroyed(Bullet bullet)
    {
        bullet.Destroyed -= HandleProjectileDestroyed;
        bullet.Hit -= HandleProjectileHit;
    }
}