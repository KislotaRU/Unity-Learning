using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [Header("Common settings")]
    [SerializeField] protected TypeProcessing _typeProcessing;
    [Space]
    [SerializeField, Range(0.1f, 5f)] protected float _speed;
    [SerializeField, Range(0.1f, 5f)] protected float _duration;
    
    private Counter _counter;

    public float DisplayedValue { get; private set; }
    public float CurrentValue => _counter.CurrentValue;
    public float MaxValue => _counter.MaxValue;
    public float PercentageValue => CurrentValue / MaxValue;

    // Убрать ручное создание счётчика
    private void Awake()
    {
        _counter = new Counter(100f);
        _counter.SetCurrentValue(0f);

        DisplayedValue = CurrentValue;
    }

    private void OnEnable()
    {
        _counter.ChangedValue += OnChangedValue;
    }

    private void OnDisable()
    {
        _counter.ChangedValue -= OnChangedValue;
    }

    // Убрать прямое воздействие на UI
    public void SetValue(float value)
    {
        _counter.SetCurrentValue(value);
    }

    protected abstract void Display();

    protected abstract float GetOperatingValue();

    private async UniTaskVoid LerpValue()
    {
        float duration = _duration;
        float elapsed = 0f;

        float startValue = DisplayedValue;
        float endValue = GetOperatingValue();
        float newValue;

        while (elapsed < duration)
        {
            elapsed += _speed * Time.deltaTime;
            newValue = Mathf.Lerp(startValue, endValue, elapsed);

            DisplayedValue = newValue;

            Display();

            await UniTask.Yield();
        }
    }

    protected void OnChangedValue()
    {
        switch (_typeProcessing)
        {
            case TypeProcessing.Instant:
                DisplayedValue = GetOperatingValue();
                Display();
                return;

            case TypeProcessing.Smooth:
                LerpValue().Forget();
                return;
        }

        throw new ArgumentOutOfRangeException(nameof(_typeProcessing));
    }
}