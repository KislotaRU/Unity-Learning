using System;
using UnityEngine;

public class Health : Indicator
{
    public event Action AcceptedHealth;
    public event Action AcceptedDamage;

    public event Action Died;

    private void OnEnable()
    {
        Increased += HandleIncrease;
        Decreased += HandleDecrease;

        Devastated += HandleDevastate;
    }

    private void OnDisable()
    {
        Increased -= HandleIncrease;
        Decreased -= HandleDecrease;

        Devastated -= HandleDevastate;
    }

    public void TakeHealth(float health) =>
        base.Increase(health);

    public void TakeDamage(float damage) =>
        base.Decrease(damage);

    private void HandleIncrease() =>
        AcceptedHealth?.Invoke();

    private void HandleDecrease() =>
        AcceptedDamage?.Invoke();

    private void HandleDevastate() =>
        Died?.Invoke();
}