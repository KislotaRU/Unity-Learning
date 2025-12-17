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
    public float Ratio => MaxValue > MinValue ? CurrentValue / MaxValue : 0;
    public bool IsFull => CurrentValue >= MaxValue;
    public bool IsEmpty => CurrentValue <= MinValue;

    public void Increase(float value = 1f)
    {
        if (value < MinValue)
            throw new ArgumentOutOfRangeException(nameof(value));

        float newCurrentValue = CurrentValue + value;

        SetCurrentValue(newCurrentValue);

        Increased?.Invoke();
    }

    public void Decrease(float value = 1f)
    {
        if (value < MinValue)
            throw new ArgumentOutOfRangeException(nameof(value));

        float newCurrentValue = CurrentValue - value;

        SetCurrentValue(newCurrentValue);

        Decreased?.Invoke();
    }

    public void SetMaxValue(float value)
    {
        if (value < MinValue)
            throw new ArgumentOutOfRangeException(nameof(value));

        MaxValue = value;
        CurrentValue = Math.Min(CurrentValue, MaxValue);

        ChangedValue?.Invoke();

        if (IsEmpty)
            Devastated?.Invoke();

        if (IsFull)
            Filled?.Invoke();
    }

    public void SetCurrentValue(float value)
    {
        CurrentValue = Math.Clamp(value, MinValue, MaxValue);

        ChangedValue?.Invoke();

        if (IsEmpty)
            Devastated?.Invoke();

        if (IsFull)
            Filled?.Invoke();
    }

    public void Fill()
    {
        CurrentValue = MaxValue;

        ChangedValue?.Invoke();
        Filled?.Invoke();
    }

    public void Reset()
    {
        CurrentValue = MinValue;

        ChangedValue?.Invoke();
        Devastated?.Invoke();
    }
}