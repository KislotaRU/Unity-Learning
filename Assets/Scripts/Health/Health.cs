using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public event Action Died;
    public event Action AcceptedDamage;
    public event Action AcceptedHealth;

    public float MaxValue { get; private set; }
    public float CurrentValue { get; private set; }

    private void Awake()
    {
        CurrentValue = _maxValue;
        MaxValue = _maxValue;
    }

    public void TakeHealth(float health)
    {
        float temporaryCurrentValue = CurrentValue + health;

        CurrentValue = temporaryCurrentValue > MaxValue ? MaxValue : temporaryCurrentValue;
        AcceptedHealth?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        float temporaryCurrentValue = CurrentValue - damage;

        CurrentValue = temporaryCurrentValue <= 0 ? 0 : temporaryCurrentValue;
        AcceptedDamage?.Invoke();

        if (CurrentValue == 0)
            Died?.Invoke();
    }
}