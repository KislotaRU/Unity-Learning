using System;

public abstract class Countable : ICountable
{
    private const float MinValue = 0f;

    public event Action ChangedValue;
    public event Action Increased;
    public event Action Decreased;

    public event Action Filled;
    public event Action Devastated;

    public Countable(float maxValue)
    {
        MaxValue = maxValue >= MinValue ? maxValue : throw new ArgumentOutOfRangeException(nameof(maxValue));

        CurrentValue = MaxValue;
    }

    public float MaxValue { get; private set; }
    public float CurrentValue { get; private set; }

    private bool IsFull => CurrentValue == MaxValue;
    private bool IsEmpty => CurrentValue == MinValue;

    public void Increase(float value)
    {
        if (value < MinValue)
            throw new ArgumentOutOfRangeException(nameof(value));

        float temporaryValue = CurrentValue + value;

        CurrentValue = Math.Min(temporaryValue, MaxValue);

        ChangedValue?.Invoke();
        Increased?.Invoke();

        if (IsFull)
            Filled?.Invoke();
    }

    public void Decrease(float value)
    {
        if (value < MinValue)
            throw new ArgumentOutOfRangeException(nameof(value));

        float temporaryValue = CurrentValue - value;

        CurrentValue = Math.Max(temporaryValue, MinValue);

        ChangedValue?.Invoke();
        Decreased?.Invoke();

        if (IsEmpty)
            Devastated?.Invoke();
    }
}