using TMPro;
using UnityEngine;

public class TextBar : Bar
{
    [SerializeField] protected TextMeshProUGUI _textMeshPro;
    [SerializeField] private bool _isPercentage;

    protected override void HandleView()
    {
        if (_isPercentage)
            _textMeshPro.text = $"{CurrentValue * 100}%";
        else
            _textMeshPro.text = $"{CurrentValue}/{MaxValue}";
    }
}