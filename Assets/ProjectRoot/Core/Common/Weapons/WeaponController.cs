using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons = new();

    private Weapon _weapon;
    private int _index;

    private void Awake()
    {
        if (_weapons.Count == 0)
            throw new InvalidOperationException();

        _weapon = _weapons[_index];
    }

    public void Attack()
    {
        _weapon.Attack();
    }

    public void Reload()
    {
        _weapon.Reload();
    }

    public void Switch()
    {
        _index = (_index + 1) % _weapons.Count;
        _weapon = _weapons[_index];
    }
}