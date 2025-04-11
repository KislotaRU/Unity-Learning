using System;
using UnityEngine;

public abstract class Indicator : MonoBehaviour
{
    private const float MinValue = 0f;

    [SerializeField] protected float _maxValue;

    public event Action Increased;
    public event Action Decreased;

    public event Action Filled;
    public event Action Devastated;

    public float MaxValue => _maxValue;
    public float CurrentValue { get; private set; }
    public bool IsFull => CurrentValue == MaxValue;
    public bool IsEmpty => CurrentValue == MinValue;

    private void Awake()
    {
        CurrentValue = MaxValue;
    }

    protected virtual void Increase(float value)
    {
        if (value < 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue + value, MinValue, MaxValue);

        Increased?.Invoke();

        if (IsFull)
            Filled?.Invoke();
    }

    protected virtual void Decrease(float value)
    {
        if (value < 0)
            return;

        CurrentValue = Mathf.Clamp(CurrentValue - value, MinValue, MaxValue);

        Decreased?.Invoke();

        if (IsEmpty)
            Devastated?.Invoke();
    }
}