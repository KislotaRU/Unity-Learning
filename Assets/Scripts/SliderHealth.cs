using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealth : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Health _targetHealth;

    protected Slider _slider;

    protected float TargetValue => Mathf.Clamp(_targetHealth.CurrentValue / _targetHealth.MaxValue, _slider.minValue, _slider.maxValue);

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = TargetValue;
    }

    private void OnEnable()
    {
        _targetHealth.AcceptedHealth += HandleTranslating;
        _targetHealth.AcceptedDamage += HandleTranslating;
    }

    private void OnDisable()
    {
        _targetHealth.AcceptedHealth -= HandleTranslating;
        _targetHealth.AcceptedDamage -= HandleTranslating;
    }

    protected virtual void HandleTranslating()
    {
        _slider.value = TargetValue;
    }
}