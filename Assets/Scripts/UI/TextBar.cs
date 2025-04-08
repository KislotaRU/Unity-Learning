using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class TextBar : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Health _target;

    protected TextMeshProUGUI _textMeshPro;

    protected float CurrentValue => _target.CurrentValue;
    protected float MaxValue => _target.MaxValue;

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
        _target.AcceptedHealth += HandleView;
        _target.AcceptedDamage += HandleView;
    }

    private void OnDisable()
    {
        _target.AcceptedHealth -= HandleView;
        _target.AcceptedDamage -= HandleView;
    }

    protected abstract void HandleView();
}