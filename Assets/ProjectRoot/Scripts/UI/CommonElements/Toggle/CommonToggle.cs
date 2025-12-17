using UnityEngine;
using UnityEngine.UI;

public abstract class CommonToggle : MonoBehaviour
{
    [SerializeField] private Toggle _toggle;

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(HandleValueChanged);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(HandleValueChanged);
    }

    protected abstract void HandleValueChanged(bool value);
}