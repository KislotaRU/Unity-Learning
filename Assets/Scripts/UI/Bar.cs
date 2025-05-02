using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [Header("Indicator")]
    [SerializeField] private Indicator _target;

    protected float CurrentValue => _target.CurrentValue;

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