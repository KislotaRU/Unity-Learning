using System;
using UnityEngine;
using Zenject;

public class RangedWeapon : Weapon
{
    [SerializeField] private RangedWeaponConfig _configuration;
    [Space]
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _transformParent;
    [SerializeField] private Transform _transformBullet;
    [Space]
    [SerializeField] private AmmoType _ammoType;

    private AmmoController _ammoController;

    public float ReloadTime => _configuration.ReloadTime;
    public float ProjectileVelocity => _configuration.ProjectileVelocity;
    public float ProjectileLifeTime => _configuration.ProjectileLifeTime;
    public int ProjectileMaxCount => _configuration.ProjectileMaxCount;
    public int ProjectilesPerShot => _configuration.ProjectilesPerShot;

    public int ProjectileCount { get; private set; }
    public bool IsReloading { get; private set; }
    public bool CanShoot { get; private set; }

    public bool CanAttack => CanShoot && IsReloading == false && ProjectileCount >= ProjectilesPerShot;

    [Inject]
    private void Construct(ITimerService<IWeapon> timerService, AmmoController ammoController)
    {
        _timerService = timerService;
        _ammoController = ammoController;
    }

    private void Awake()
    {
        if (_configuration == null)
            throw new ArgumentNullException(nameof(_configuration));

        if (_ammoController == null)
            throw new ArgumentNullException(nameof(_ammoController));

        _baseConfiguration = _configuration;

        transform.parent = _transformParent;
        ProjectileCount = ProjectileMaxCount;
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
        if (ProjectileCount >= ProjectileMaxCount)
            return;

        if (IsReloading)
            return;

        if (_ammoController.GetAmmoAmount(_ammoType) <= 0)
            return;

        IsReloading = true;

        int amount = ProjectileMaxCount - ProjectileCount;

        _timerService.CreateTimer(this, ReloadTime, () =>
        {
            ProjectileCount += _ammoController.RequestAmmo(_ammoType, amount);

            IsReloading = false;
        });
    }

    private void Shoot()
    {
        Projectile bullet;

        ProjectileCount -= ProjectilesPerShot;

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

    private void HandleProjectileDestroyed(Projectile bullet)
    {
        if (bullet == null)
            throw new ArgumentNullException(nameof(bullet));

        bullet.Destroyed -= HandleProjectileDestroyed;
        bullet.Hit -= HandleProjectileHit;
    }
}