using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private Weapon _weapon;

    public void Attack()
    {
        _weapon?.Attack();
    }

    public void Reload()
    {
        _weapon?.Reload();
    }

    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
    }
}