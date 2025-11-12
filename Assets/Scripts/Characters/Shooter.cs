using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private WeaponConfiguration _configuration;

    private IWeapon _weapon;

    private void Awake()
    {
        if (_configuration == null)
            throw new ArgumentNullException(nameof(_configuration));

        InitializeWeapon();
    }

    public void Shoot()
    {
        Debug.Log($"Shoot");
    }

    private void InitializeWeapon()
    {
        switch (_configuration)
        {
            case MeleeWeaponConfiguration configuration:
                _weapon = new MeleeWeapon(configuration);
                break;
            case RangeWeaponConfiguration configuration:
                _weapon = new RangeWeapon(configuration);
                break;
        }

        if (_weapon == null)
            throw new ArgumentNullException(nameof(_weapon));
    }
}