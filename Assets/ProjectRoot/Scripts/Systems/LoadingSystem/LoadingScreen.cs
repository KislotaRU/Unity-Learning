using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [Header("Name")]
    [SerializeField] private TextMeshProUGUI _name;
    // Добавить изменение фона/картинки.

    [Header("Bar")]
    [SerializeField] private SliderBar _sliderBar;
    [SerializeField] private TextBar _textBar;
    [Space]
    [SerializeField] private TextMeshProUGUI _message;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float _durationAppearance;
    [SerializeField, Range(0f, 3f)] private float _durationFade;

    private void Awake()
    {
        if (_canvasGroup == null)
            throw new ArgumentNullException(nameof(_canvasGroup));

        if (_name == null)
            throw new ArgumentNullException(nameof(_name));

        if (_sliderBar == null)
            throw new ArgumentNullException(nameof(_sliderBar));

        if (_textBar == null)
            throw new ArgumentNullException(nameof(_textBar));

        if (_message == null)
            throw new ArgumentNullException(nameof(_message));

        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void SetName(string name)
    {
        if (_canvasGroup.alpha > 0f)
            throw new InvalidOperationException();

        if (String.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException();

        _name.text = name;
    }

    public void UpdateMessage(string text)
    {
        if (String.IsNullOrWhiteSpace(text))
            throw new InvalidOperationException();

        _message.text = text;
    }

    public void UpdateProgress(float progress)
    {
        if (progress < 0f)
            throw new ArgumentOutOfRangeException(nameof(progress));

        _textBar.SetValue(progress);
        _sliderBar.SetValue(progress);
    }

    public async UniTask Show()
    {
        gameObject.SetActive(true);

        await AppearAsync();
    }

    public async UniTask Hide()
    {
        await FadeOutAsync();

        gameObject.SetActive(false);
    }

    private async UniTask FadeOutAsync()
    {
        float alpha = _canvasGroup.alpha;
        float duration = _durationFade;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = alpha - (elapsed / duration);

            await UniTask.Yield();
        }

        _canvasGroup.alpha = 0f;
    }

    private async UniTask AppearAsync()
    {
        float alpha = _canvasGroup.alpha;
        float duration = _durationAppearance;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _canvasGroup.alpha = alpha + (elapsed / duration);

            await UniTask.Yield();
        }

        _canvasGroup.alpha = 1f;
    }
}