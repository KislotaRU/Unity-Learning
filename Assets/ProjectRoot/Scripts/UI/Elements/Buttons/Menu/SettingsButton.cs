using System;
using UnityEngine;

public class SettingsButton : ActionButton
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private Canvas _canvas;

    private CanvasGroup _mainCanvasGroup;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        if (_mainCanvas == null)
            throw new ArgumentNullException(nameof(_mainCanvas));

        if (_canvas == null)
            throw new ArgumentNullException(nameof(_canvas));

        if (_mainCanvas.TryGetComponent(out CanvasGroup mainCanvasGroup) == false)
            throw new ArgumentNullException(nameof(_canvas));

        if (_mainCanvas.TryGetComponent(out CanvasGroup canvasGroup) == false)
            throw new ArgumentNullException(nameof(canvasGroup));

        _mainCanvasGroup = mainCanvasGroup;
        _canvasGroup = canvasGroup;
    }

    protected override void OnClick()
    {
        Debug.Log("Settings");

        _mainCanvasGroup.alpha = 0f;
        _mainCanvasGroup.blocksRaycasts = false;
        _mainCanvasGroup.interactable = false;
        //_mainCanvas.gameObject.SetActive(false);

        _canvas.gameObject.SetActive(true);
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }
}