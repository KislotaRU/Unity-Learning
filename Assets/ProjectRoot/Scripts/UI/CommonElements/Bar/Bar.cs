using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    private Counter _counter;

    protected float CurrentValue => _counter.CurrentValue;
    protected float MaxValue => _counter.MaxValue;
    protected float PercentageValue => CurrentValue / MaxValue;

    private void Awake()
    {
        _counter = new Counter(100f);
        _counter.SetCurrentValue(0f);
    }

    private void OnEnable()
    {
        _counter.ChangedValue += HandleView;
    }

    private void OnDisable()
    {
        _counter.ChangedValue -= HandleView;
    }

    public void SetValue(float value)
    {
        _counter.SetCurrentValue(value);
    }

    protected abstract void HandleView();
}