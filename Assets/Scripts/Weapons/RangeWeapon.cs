using System;
using UnityEngine;

public class RangeWeapon : Weapon
{
    private const int SecondCount = 60;

    [SerializeField] private RangeWeaponConfiguration _configuration;
    [SerializeField] private BulletSpawner _bulletSpawner;
    [SerializeField] private Transform _transformParent;
    [SerializeField] private Transform _transformBullet;

    private ITimerService<Weapon> _timerService;

    public float Damage => _configuration.Damage;
    public float AttackRate => _configuration.AttackRate;
    public float Range => _configuration.Range;
    public float ProjectileCount { get; private set; }
    public float ReloadTime => _configuration.ReloadTime;
    public float ProjectileVelocity => _configuration.ProjectileVelocity;
    public float ProjectileLifeTime => _configuration.ProjectileLifeTime;
    public int MaxAmmo => _configuration.MaxAmmo;
    public int ProjectilesPerShot => _configuration.ProjectilesPerShot;
    public float TimeBetweenAttacks => SecondCount / AttackRate;
    public bool CanAttack => IsReloaded && CanShoot;
    private bool IsReloaded => ProjectileCount >= ProjectilesPerShot;
    private bool CanShoot { get; set; }
    private bool CanReload { get; set; }

    private void Awake()
    {
        if (_configuration == null)
            throw new ArgumentNullException(nameof(_configuration));

        _timerService = new TimerService<Weapon>();

        CanShoot = true;
        CanReload = true;

        transform.parent = _transformParent;
    }

    private void Start()
    {
        ProjectileCount = MaxAmmo;
    }

    private void Update()
    {
        _timerService.Tick(Time.deltaTime);
    }

    public override void Attack()
    {
        if (CanAttack == false)
            return;

        Bullet bullet;

        ProjectileCount -= ProjectilesPerShot;

        bullet = _bulletSpawner.Spawn();

        bullet.Destroyed += HandleProjectileDestroyed;
        bullet.Hit += HandleProjectileHit;

        bullet.Initialize(_transformBullet.position, _transformBullet.rotation, ProjectileVelocity, ProjectileLifeTime);

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

        if (CanReload == false)
            return;

        CanReload = false;

        _timerService.CreateTimer(this, ReloadTime, () =>
        {
            CanReload = true;
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