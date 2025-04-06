using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHealth : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Health _targetHealth;

    private TextMeshProUGUI _textMeshPro;   

    private float CurrentValue => _targetHealth.CurrentValue;
    private float MaxValue => _targetHealth.MaxValue;

    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        HandleView();
    }

    private void OnEnable()
    {
        _targetHealth.AcceptedHealth += HandleView;
        _targetHealth.AcceptedDamage += HandleView;
    }

    private void OnDisable()
    {
        _targetHealth.AcceptedHealth -= HandleView;
        _targetHealth.AcceptedDamage -= HandleView;
    }

    private void HandleView()
    {
        _textMeshPro.text = $"{CurrentValue}/{MaxValue}";
    }
}