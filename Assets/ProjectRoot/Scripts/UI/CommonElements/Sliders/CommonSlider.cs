using UnityEngine;
using UnityEngine.UI;

public abstract class CommonSlider : MonoBehaviour
{
    [SerializeField] protected Slider _slider;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(HandleValueChanged);
    }

    protected abstract void HandleValueChanged(float value);
}