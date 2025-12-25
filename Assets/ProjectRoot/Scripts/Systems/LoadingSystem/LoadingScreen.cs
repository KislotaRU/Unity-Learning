using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _message;

    [Header("Settings")]
    [SerializeField, Range(0f, 3f)] private float _durationAppearance;
    [SerializeField, Range(0f, 3f)] private float _durationFade;
    [Space]
    [SerializeField] private Counter _counter;
    [Space]
    [SerializeField] private CanvasGroup _canvasGroup;

    public ICounter Counter => _counter;

    private void Awake()
    {
        if (_name == null)
            throw new ArgumentNullException(nameof(_name));

        if (_message == null)
            throw new ArgumentNullException(nameof(_message));

        if (_counter == null)
            throw new ArgumentNullException(nameof(_counter));

        if (_canvasGroup == null)
            throw new ArgumentNullException(nameof(_canvasGroup));
    }

    private void Start()
    {
        _canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void SetName(string name)
    {
        if (_canvasGroup.alpha > 0f)
            throw new InvalidOperationException();

        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException();

        _name.text = name;
    }

    public void SetMessage(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new InvalidOperationException();

        _message.text = text;
    }

    public void SetProgress(float value)
    {
        if (value < 0f)
            throw new ArgumentOutOfRangeException(nameof(value));

        _counter.SetCurrentValue(value);
    }

    public async UniTask Show()
    {
        gameObject.SetActive(true);

        await AppearAsync();
    }

    public async UniTask Hide()
    {
        await FadeOutAsync();

        _counter.SetCurrentValue(0f);
        _message.text = string.Empty;
        _name.text = string.Empty;

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