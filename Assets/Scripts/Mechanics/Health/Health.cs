using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public event Action Died;
    public event Action AcceptedDamage;

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
        AcceptedDamage?.Invoke();

        if (CurrentValue <= 0)
            Died?.Invoke();
    }
}