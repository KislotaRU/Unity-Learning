using System;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private MeleeWeaponConfiguration _configuration;

    public int MaxTargets => _configuration.MaxTargets;
    public bool CanAttack => CanHit;

    public bool CanHit { get; private set; }

    private void Awake()
    {
        if (_configuration == null)
            throw new ArgumentNullException(nameof(_configuration));

        _baseConfiguration = _configuration;

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