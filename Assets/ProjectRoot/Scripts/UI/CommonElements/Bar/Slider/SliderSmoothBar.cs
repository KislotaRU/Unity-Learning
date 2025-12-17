using Cysharp.Threading.Tasks;
using UnityEngine;

public class SliderSmoothBar : SliderBar
{
    [SerializeField] private float _speed;

    protected override void HandleView()
    {
        TranslatingValue(CurrentValue).Forget();
    }

    private async UniTaskVoid TranslatingValue(float value)
    {
        float duration = 1f;
        float elapsed = 0f;
        float startValue = _slider.value;
        float endValue = value;

        while (elapsed < duration)
        {
            elapsed += _speed * Time.deltaTime;
            _slider.value = Mathf.Lerp(startValue, endValue, elapsed);

            await UniTask.Yield();
        }
    }
}