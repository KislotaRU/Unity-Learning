using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ActionButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Button _button;

    private void Awake()
    {
        if (_button == null)
            throw new ArgumentNullException(nameof(_button));
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    protected abstract void OnClick();
}