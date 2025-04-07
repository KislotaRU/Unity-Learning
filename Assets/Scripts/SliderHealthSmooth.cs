using System.Collections;
using UnityEngine;

public class SliderHealthSmooth : SliderHealth
{
    [Header("Smooth")]
    [SerializeField] private float _speedTranslatedSmooth;

    private IEnumerator TranslatingValueWithSmooth()
    {
        float elapsedTime = 0f;
        float availableTime = 1f;

        float startValue = _slider.value;
        float endValue = TargetValue;

        while (elapsedTime < availableTime)
        {
            elapsedTime += _speedTranslatedSmooth * Time.deltaTime;
            _slider.value = Mathf.Lerp(startValue, endValue, elapsedTime);

            yield return null;
        }
    }

    protected override void HandleTranslating()
    {
        StartCoroutine(TranslatingValueWithSmooth());
    }
}