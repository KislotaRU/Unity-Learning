using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public event Action Dead;
    public event Action TakedDamage;

    public float CurrentValue { get; private set; }

    private void Awake()
    {
        CurrentValue = _maxValue;
    }

    public void TakeHealth(float health)
    {
        CurrentValue += health;

        if (CurrentValue >= _maxValue)
            CurrentValue = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        CurrentValue -= damage;
        TakedDamage?.Invoke();

        if (CurrentValue <= 0)
            Dead?.Invoke();
    }
}