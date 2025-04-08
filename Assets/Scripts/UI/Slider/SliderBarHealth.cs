using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderBarHealth : SliderBar
{
    protected override void HandleView()
    {
        _slider.value = TargetValue;
    }
}