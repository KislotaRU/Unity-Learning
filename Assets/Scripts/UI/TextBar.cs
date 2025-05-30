using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextBar : Bar
{
    protected TextMeshProUGUI _textMeshPro;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    protected override void HandleView()
    {
        _textMeshPro.text = $"{CurrentValue}";
    }
}