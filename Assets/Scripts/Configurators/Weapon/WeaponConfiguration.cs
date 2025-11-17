using UnityEngine;

public abstract class WeaponConfiguration : ScriptableObject
{
    [field: Header("Base Weapon Settings")]
    [field: SerializeField, Min(0f)] public float Damage { get; private set; }
    [field: SerializeField, Min(0f)] public float AttackRate { get; private set; }
    [field: SerializeField, Min(0f)] public float Range { get; private set; }
}