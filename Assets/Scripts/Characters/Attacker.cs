using System;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private WeaponFactory _weaponFactory;
    private IWeapon _weapon;

    private void Awake()
    {
        _weaponFactory = new WeaponFactory();

        SetWeapon(_weaponFactory.CreateHands(null));
    }

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