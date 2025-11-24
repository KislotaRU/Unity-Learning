using System;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private RangeWeaponConfiguration _configuration;
    [Space]
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _transformParent;
    [SerializeField] private Transform _transformBullet;

    public float ReloadTime => _configuration.ReloadTime;
    public float ProjectileVelocity => _configuration.ProjectileVelocity;
    public float ProjectileLifeTime => _configuration.ProjectileLifeTime;
    public int MaxAmmo => _configuration.MaxAmmo;
    public int ProjectilesPerShot => _configuration.ProjectilesPerShot;

    public float CurrentAmmo { get; private set; }
    public bool IsReloading { get; private set; }
    public bool CanShoot { get; private set; }

    public bool CanAttack => CanShoot && IsReloading == false && CurrentAmmo >= ProjectilesPerShot;

    private void Awake()
    {
        if (_configuration == null)
            throw new ArgumentNullException(nameof(_configuration));

        _baseConfiguration = _configuration;

        _timerService = new TimerService<IWeapon>();

        transform.parent = _transformParent;
        CurrentAmmo = MaxAmmo;
        CanShoot = true;
    }

    private void Update()
    {
        _timerService.Tick(Time.deltaTime);
    }

    public override void Attack()
    {
        if (CanAttack == false)
            return;

        Shoot();

        CanShoot = false;

        _timerService.CreateTimer(this, TimeBetweenAttacks, () =>
        {
            CanShoot = true;
        });
    }

    public override void Reload()
    {
        if (CurrentAmmo >= MaxAmmo)
            return;

        if (IsReloading)
            return;

        IsReloading = true;

        _timerService.CreateTimer(this, ReloadTime, () =>
        {
            CurrentAmmo = MaxAmmo;

            IsReloading = false;
        });
    }

    private void Shoot()
    {
        Bullet bullet;

        CurrentAmmo -= ProjectilesPerShot;

        bullet = _bulletSpawner.Spawn();

        bullet.Destroyed += HandleProjectileDestroyed;
        bullet.Hit += HandleProjectileHit;

        bullet.Initialize(_transformBullet.position, _transformBullet.rotation, ProjectileVelocity, ProjectileLifeTime);
    }

    private void HandleProjectileHit(IHealth health)
    {
        if (health == null)
            throw new ArgumentNullException(nameof(health));

        health.TakeDamage(Damage);
    }

    private void HandleProjectileDestroyed(Bullet bullet)
    {
        if (bullet == null)
            throw new ArgumentNullException(nameof(bullet));

        bullet.Destroyed -= HandleProjectileDestroyed;
        bullet.Hit -= HandleProjectileHit;
    }
}