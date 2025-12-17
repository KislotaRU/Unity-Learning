using System;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    private const int MaxAmount = 90;

    private Dictionary<AmmoType, int> _ammoInventory;

    private void Awake()
    {
        _ammoInventory = new Dictionary<AmmoType, int>();

        foreach (AmmoType type in Enum.GetValues(typeof(AmmoType)))
            _ammoInventory.Add(type, MaxAmount);
    }

    public void AddAmmo(AmmoType type, int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (_ammoInventory.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        int currenAmount = _ammoInventory[type] += amount;

        _ammoInventory[type] = Math.Min(currenAmount, MaxAmount);
    }

    public int RequestAmmo(AmmoType type, int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (_ammoInventory.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        int amountResult = Mathf.Min(amount, _ammoInventory[type]);

        _ammoInventory[type] -= amountResult;

        return amountResult;
    }

    public int GetAmmoAmount(AmmoType type)
    {
        if (_ammoInventory.ContainsKey(type) == false)
            throw new ArgumentOutOfRangeException(nameof(type));

        return _ammoInventory[type];
    }
}