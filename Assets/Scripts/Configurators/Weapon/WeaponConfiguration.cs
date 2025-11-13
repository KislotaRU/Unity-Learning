using System;
using UnityEngine;

public abstract class WeaponConfiguration : ScriptableObject
{
    [Header("Base Weapon Settings")]
    [SerializeField, Min(0f)] private float _damage;
    [SerializeField, Min(0f)] private float _attackRate;
    [SerializeField, Min(0f)] private float _range;

    public float Damage => _damage;
    public float AttackRate => _attackRate;
    public float Range => _range;
}