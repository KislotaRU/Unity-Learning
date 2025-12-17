using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextSmoothBar _progressText;
    [SerializeField] private SliderSmoothBar _progressBar;
    [SerializeField] private TextMeshProUGUI _message;
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        if (_progressBar == null)
            throw new ArgumentNullException(nameof(_progressBar));

        if (_progressText == null)
            throw new ArgumentNullException(nameof(_progressText));

        if (_canvasGroup == null)
            throw new ArgumentNullException(nameof(_canvasGroup));
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public async UniTask Hide()
    {
        await FadeOutAsync();

        gameObject.SetActive(false);
    }

    public void UpdateMessage(string text)
    {
        _message.text = text;
    }

    public void UpdateProgress(float progress)
    {
        _progressText.SetValue(progress);
        _progressBar.SetValue(progress);
    }

    private async UniTask FadeOutAsync()
    {
        float alpha = _canvasGroup.alpha;
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = alpha - (elapsed / duration);

            await UniTask.Yield();
        }
    }
}