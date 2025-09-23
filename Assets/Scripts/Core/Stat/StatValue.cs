using System;
using UnityEngine;

[Serializable]
public class StatValue
{
    public const float Min = 0f;

    [SerializeField] private float _max;
    [SerializeField] private float _current;

    public event Action Increased;
    public event Action Decreased;
    public event Action Changed;

    public event Action Filled;
    public event Action Empty;

    public bool IsFull => Current == Max;
    public bool IsEmpty => Current == Min;

    public float Max
    {
        get
        {
            return _max;
        }

        private set
        {
            if (value > Min)
            {
                _max = value;

                Changed?.Invoke();
            }
        }
    }

    public float Current
    {
        get
        {
            return _current;
        }

        private set
        {
            float newValue = Mathf.Clamp(value, Min, Max);

            if (_current != newValue)
            {
                _current = value;

                Changed?.Invoke();
            }
        }
    }

    public StatValue(float maxValue)
    {
        Max = maxValue;
        Current = Max;
    }

    public StatValue(float maxValue, float currentValue)
    {
        Max = maxValue;
        Current = currentValue;
    }

    public void Increase(float value)
    {
        Current += value;

        Increased?.Invoke();

        if (IsFull)
            Filled?.Invoke();
    }

    public void Decrease(float value)
    {
        Current -= value;

        Decreased?.Invoke();

        if (IsEmpty)
            Empty?.Invoke();
    }

    public void SetMax(float maxValue)
    {
        Max = maxValue;
    }
}