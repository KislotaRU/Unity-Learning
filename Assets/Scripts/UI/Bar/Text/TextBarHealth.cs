using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextBarHealth : TextBar
{
    protected override void HandleView()
    {
        _textMeshPro.text = $"{CurrentValue}/{MaxValue}";
    }
}