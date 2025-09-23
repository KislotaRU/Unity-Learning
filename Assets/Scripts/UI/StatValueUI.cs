using TMPro;
using UnityEngine;

public class StatValueUI : MonoBehaviour
{
    [SerializeField] private StatValue _statValue;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _statValue.Changed += UpdateUI;
    }

    private void OnDisable()
    {
        _statValue.Changed -= UpdateUI;
    }

    public void SetStat(StatValue statValue)
    {
        _statValue = statValue;
        _statValue.Changed += UpdateUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_text != null)
            UpdateText();
    }

    private void UpdateText()
    {
        _text.text = $"{_statValue.Current}";
    }
}