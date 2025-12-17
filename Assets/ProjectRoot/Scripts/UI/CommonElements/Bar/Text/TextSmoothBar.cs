using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TextSmoothBar : TextBar
{
    [SerializeField] private float _speed;

    private float _valueOnScreen = 0f;

    //private void Show(float value)
    //{

    //}

    protected override void HandleView()
    {
        TranslatingValue(CurrentValue).Forget();
    }

    private async UniTaskVoid TranslatingValue(float value)
    {
        float duration = 1f;
        float elapsed = 0f;
        float startValue = _valueOnScreen;
        float endValue = value;

        while (elapsed < duration)
        {
            elapsed += _speed * Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, endValue, elapsed);

            newValue = (float)Math.Round(newValue, 2);

            _textMeshPro.text = $"{newValue * 100} %";
            _valueOnScreen = newValue;

            await UniTask.Yield();
        }
    }
}