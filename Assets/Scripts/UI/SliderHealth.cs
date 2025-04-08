using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealth : SliderBar
{
    protected override void HandleView()
    {
        _slider.value = TargetValue;
    }
}