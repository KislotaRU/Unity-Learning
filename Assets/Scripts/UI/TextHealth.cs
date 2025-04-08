using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealth : TextBar
{
    protected override void HandleView()
    {
        _textMeshPro.text = $"{CurrentValue}/{MaxValue}";
    }
}