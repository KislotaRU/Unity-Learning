using System.Collections;
using UnityEngine;

public class SliderBarHealthSmooth : SliderBarHealth
{
    [Header("Parameters Smooth")]
    [SerializeField] private float _speedTranslated;

    private Coroutine _currentCoroutine;

    protected override void HandleView()
    {
        if (_currentCoroutine == null)
            _currentCoroutine = StartCoroutine(TranslatingValueWithSmooth());
    }

    private IEnumerator TranslatingValueWithSmooth()
    {
        float elapsedTime = 0f;
        float availableTime = 1f;

        float startValue = _slider.value;
        float endValue = TargetValue;

        while (elapsedTime < availableTime)
        {
            elapsedTime += _speedTranslated * Time.deltaTime;
            _slider.value = Mathf.Lerp(startValue, endValue, elapsedTime);

            yield return null;
        }

        _currentCoroutine = null;

        if (_slider.value != TargetValue)
            HandleView();
    }
}