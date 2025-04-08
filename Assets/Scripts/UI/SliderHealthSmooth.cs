using System.Collections;
using UnityEngine;

public class SliderHealthSmooth : SliderHealth
{
    [Header("Parameters Smooth")]
    [SerializeField] private float _speedTranslated;

    protected override void HandleView()
    {
        StartCoroutine(TranslatingValueWithSmooth());
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
    }
}