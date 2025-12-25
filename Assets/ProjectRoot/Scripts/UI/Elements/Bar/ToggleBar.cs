using UnityEngine;
using UnityEngine.UI;

public class ToggleBar : Bar
{
    [Header("ToggleBar")]
    [SerializeField] private Toggle _toggle;

    protected override void Display()
    {
        _toggle.isOn = DisplayedValue == 1f;
    }

    protected override float GetOperatingValue()
    {
        return PercentageValueFloat;
    }
}