using System;
using UnityEngine;
using Zenject;

public class MeleeWeapon : Weapon
{
    [SerializeField] private MeleeWeaponConfig _configuration;

    public int MaxTargets => _configuration.MaxTargets;
    public bool CanAttack => CanHit;

    public bool CanHit { get; private set; }

    [Inject]
    private void Construct(ITimerService<IWeapon> timerService)
    {
        _timerService = timerService;
    }

    private void Awake()
    {
        if (_configuration == null)
            throw new ArgumentNullException(nameof(_configuration));

        _baseConfiguration = _configuration;
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