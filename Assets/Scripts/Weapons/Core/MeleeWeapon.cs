using System;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public MeleeWeapon(MeleeWeaponConfiguration configuration) : base(configuration)
    {
        MaxTargets = configuration.MaxTargets;
    }

    public int MaxTargets { get; }

    public override void Attack(IHealth health)
    {
        if (CanAttack() == false)
            return;

        if (health == null)
            throw new ArgumentNullException(nameof(health));
    }

    public override bool CanAttack()
    {
        return true;
    }

    public override void Reload()
    {
        Debug.Log("Reloaded");
    }
}