using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private IWeapon _weapon;

    public void Attack()
    {
        _weapon?.Attack();
    }

    public void Reload()
    {
        _weapon?.Reload();
    }

    public void SetWeapon(IWeapon weapon)
    {
        _weapon = weapon ?? throw new ArgumentNullException(nameof(weapon));
    }
}