using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    private const int PercentageMultiplier = 100;

    [Header("Settings")]
    [SerializeField] private Counter _counter;
    [Space]
    [SerializeField] protected TypeProcessing _typeProcessing;
    [Space]
    [SerializeField, Range(0.1f, 5f)] protected float _speed;
    [SerializeField, Range(0.1f, 5f)] protected float _duration;

    private CancellationTokenSource _cancellationTokenSource;

    public ICounter Counter => _counter;
    public float DisplayedValue { get; private set; }

    public float CurrentValue => _counter.CurrentValue;
    public float MaxValue => _counter.MaxValue;

    public float PercentageValueFloat => CurrentValue / MaxValue;
    public float PercentageValueInt => PercentageValueFloat * PercentageMultiplier;

    private void Awake()
    {
        if (_counter == null)
            throw new ArgumentNullException(nameof(_counter));

        DisplayedValue = CurrentValue;
    }

    private void OnEnable()
    {
        Counter.ChangedValue += OnChangedValue;
    }

    private void OnDisable()
    {
        Counter.ChangedValue -= OnChangedValue;
    }

    protected abstract void Display();

    protected abstract float GetOperatingValue();

    private async UniTaskVoid LerpValueAsync(CancellationToken token)
    {
        float duration = _duration;
        float elapsed = 0f;

        float startValue = DisplayedValue;
        float endValue = GetOperatingValue();
        float newValue;

        while (elapsed < duration && token.IsCancellationRequested == false)
        {
            elapsed += _speed * Time.deltaTime;
            newValue = Mathf.Lerp(startValue, endValue, elapsed);

            if (token.IsCancellationRequested == false)
            {
                DisplayedValue = newValue;

                Display();
            }

            await UniTask.Yield(token);
        }
    }

    private void HandleProcessingInstant()
    {
        DisplayedValue = GetOperatingValue();

        Display();
    }

    private void HandleProcessingSmooth()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        LerpValueAsync(_cancellationTokenSource.Token).Forget();
    }

    private void OnChangedValue()
    {
        switch (_typeProcessing)
        {
            case TypeProcessing.Instant:
                HandleProcessingInstant();
                return;

            case TypeProcessing.Smooth:
                HandleProcessingSmooth();
                return;
        }

        throw new ArgumentOutOfRangeException(nameof(_typeProcessing));
    }
}