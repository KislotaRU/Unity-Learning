using UnityEngine;
using UnityEngine.UI;

public class SliderBar : Bar
{
    [Header("Setting SliderBar")]
    [SerializeField] private Slider _slider;

    protected override void Display()
    {
        _slider.value = DisplayedValue;
    }

    protected override float GetOperatingValue()
    {
        return PercentageValue;
    }
}