using UnityEngine;

public abstract class WeaponConfiguration : ScriptableObject
{
    private const int SecondCount = 60;

    [field: Header("Base Weapon Settings")]
    [field: SerializeField, Min(0f)] public float Damage { get; private set; }
    [field: SerializeField, Min(0f)] public float AttackRate { get; private set; }
    [field: SerializeField, Min(0f)] public float Range { get; private set; }

    public float TimeBetweenAttacks => SecondCount / AttackRate;
}