using System;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    private const int SecondCount = 60;

    [SerializeField] private MeleeWeaponConfiguration _configuration;

    private ITimerService<IWeapon> _timerService;

    public float Damage => _configuration.Damage;
    public float AttackRate => _configuration.AttackRate;
    public float Range => _configuration.Range;
    public int MaxTargets => _configuration.MaxTargets;
    public float TimeBetweenAttacks => SecondCount / AttackRate;
    public bool CanAttack => CanHit;
    private bool CanHit { get; set; }

    private void Awake()
    {
        if (_configuration == null)
            throw new ArgumentNullException(nameof(_configuration));

        _timerService = new TimerService<IWeapon>();
    }

    private void Update()
    {
        _timerService.Tick(Time.deltaTime);
    }

    public override void Attack()
    {
        if (CanAttack == false)
            return;

        CanHit = false;

        Reload();
    }

    public override void Reload()
    {
        if (CanHit)
            return;

        _timerService.CreateTimer(this, TimeBetweenAttacks, () =>
        {
            CanHit = true;
        });
    }
}