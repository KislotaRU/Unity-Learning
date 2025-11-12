using System;
using System.Collections;
using UnityEngine;

public class RangeWeapon : Weapon
{
    public RangeWeapon(RangeWeaponConfiguration configuration) : base(configuration)
    {
        MaxAmmo = configuration.MaxAmmo;
        ReloadTime = configuration.ReloadTime;
        ProjectileVelocity = configuration.ProjectileVelocity;

        Magazine = new Counter(MaxAmmo);
    }

    public int MaxAmmo { get; }
    public float ReloadTime { get; }
    public float ProjectileVelocity { get; }
    public ICounter Magazine { get; }

    public IEnumerator CooldownCoroutine(float cooldownInSecond)
    {
        yield return new WaitForSeconds(cooldownInSecond);
    }

    public override void Attack(IHealth health)
    {
        if (CanAttack() == false)
            return;

        if (health == null)
            throw new ArgumentNullException(nameof(health));
    }

    public override bool CanAttack()
    {
        return false;
    }

    public override void Reload()
    {
        Debug.Log("Reloaded");
    }
}