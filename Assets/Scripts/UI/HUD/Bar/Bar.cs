using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    private const float Percantages = 100f;

    [Header("Indicator")]
    [SerializeField] private Indicator _target;

    protected float CurrentValue => _target.CurrentValue;
    protected float MaxValue => _target.MaxValue;
    protected float TargetValue => CurrentValue / MaxValue;
    protected float PercentageValue => TargetValue * Percantages;

    private void Start()
    {
        HandleView();
    }

    private void OnEnable()
    {
        _target.Increased += HandleView;
        _target.Decreased += HandleView;
    }

    private void OnDisable()
    {
        _target.Increased -= HandleView;
        _target.Decreased -= HandleView;
    }

    protected abstract void HandleView();
}