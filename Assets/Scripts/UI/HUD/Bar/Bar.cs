using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    private const float Percantages = 100f;

    [Header("Target")]
    [SerializeField] private Health _target;

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
        _target.AcceptedHealth += HandleView;
        _target.AcceptedDamage += HandleView;
    }

    private void OnDisable()
    {
        _target.AcceptedHealth -= HandleView;
        _target.AcceptedDamage -= HandleView;
    }

    protected abstract void HandleView();
}