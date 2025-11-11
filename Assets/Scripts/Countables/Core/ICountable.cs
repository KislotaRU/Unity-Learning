using System;

public interface ICountable
{
    event Action ChangedValue;

    event Action Increased;
    event Action Decreased;

    event Action Filled;
    event Action Devastated;

    float CurrentValue { get; }
    float MaxValue { get; }
}