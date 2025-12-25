using System;

public interface ICounter
{
    const float MinValue = 0f;

    event Action ChangedValue;

    event Action Increased;
    event Action Decreased;

    event Action Filled;
    event Action Devastated;

    float CurrentValue { get; }
    float MaxValue { get; }
    float Ratio => MaxValue > MinValue ? CurrentValue / MaxValue : 0;
    bool IsFull => CurrentValue >= MaxValue;
    bool IsEmpty => CurrentValue <= MinValue;

    void Increase(float value);

    void Decrease(float value);

    void SetMaxValue(float newMaxValue);

    void SetCurrentValue(float newCurrentValue);

    void Fill();

    void Clear();
}