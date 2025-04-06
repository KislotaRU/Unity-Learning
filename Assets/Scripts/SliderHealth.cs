using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealth : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Health _targetHealth;

    [Header("Smooth")]
    [SerializeField] private bool _isTranslatedSmooth;
    [SerializeField] private float _speedTranslatedSmooth;

    private Slider _slider;

    private float TargetValue => _targetHealth.CurrentValue;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.maxValue = _targetHealth.MaxValue;
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

    private IEnumerator TranslatingValueWithSmooth()
    {
        while (_slider.value != TargetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, TargetValue, _speedTranslatedSmooth * Time.deltaTime);

            yield return null;
        }
    }

    private void HandleTranslating()
    {
        if (_isTranslatedSmooth)
            StartCoroutine(TranslatingValueWithSmooth());
        else
            _slider.value = TargetValue;
    }
} 