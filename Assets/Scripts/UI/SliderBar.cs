using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderBar : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Health _target;

    protected Slider _slider;

    protected float TargetValue => _target.CurrentValue / _target.MaxValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

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

    protected virtual void HandleView() { }
}