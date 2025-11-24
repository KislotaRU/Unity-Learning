using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    protected ITimerService<IWeapon> _timerService;

    protected WeaponConfiguration _baseConfiguration;

    public float Damage => _baseConfiguration.Damage;
    public float AttackRate => _baseConfiguration.AttackRate;
    public float Range => _baseConfiguration.Range;

    public float TimeBetweenAttacks => _baseConfiguration.TimeBetweenAttacks;

    public abstract void Attack();

    public abstract void Reload();
}