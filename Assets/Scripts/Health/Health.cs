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
        if (health < 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + health, 0, MaxValue);
        AcceptedHealth?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue - damage, 0, MaxValue);
        AcceptedDamage?.Invoke();

        if (CurrentValue == 0)
            Died?.Invoke();
    }
}