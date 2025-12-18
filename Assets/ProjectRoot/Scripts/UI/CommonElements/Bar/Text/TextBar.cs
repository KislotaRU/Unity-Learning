using System;
using TMPro;
using UnityEngine;

public class TextBar : Bar
{
    [Header("Setting TextBar")]
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TypeDisplay _typeDisplay;
    [Space]
    [SerializeField] private bool _isPercentageValue;

    protected override void Display()
    {
        switch (_typeDisplay)
        {
            case TypeDisplay.Default:
                _textMeshPro.text = $"{DisplayedValue: #0.}";
                return;

            case TypeDisplay.Percent:
                _textMeshPro.text = $"{DisplayedValue: #0.} %";
                return;

            case TypeDisplay.MaxValue:
                _textMeshPro.text = $"{DisplayedValue: #0.}/{MaxValue}";
                return;
        }

        throw new ArgumentOutOfRangeException(nameof(_typeDisplay));
    }

    protected override float GetOperatingValue()
    {
        return _isPercentageValue ? PercentageValue : CurrentValue;
    }
}