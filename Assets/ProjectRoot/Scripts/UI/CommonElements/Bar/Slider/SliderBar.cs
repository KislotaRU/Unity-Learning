using UnityEngine;
using UnityEngine.UI;

public class SliderBar : Bar
{
    [SerializeField] protected Slider _slider;

    protected override void HandleView()
    {
        _slider.value = CurrentValue;
    }
}