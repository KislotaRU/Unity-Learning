using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Healer))]
public class ButtonHeal : MonoBehaviour
{
    [SerializeField] private Health _targetHealth;

    private Healer _healer;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _healer = GetComponent<Healer>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }

    private void HandleClick()
    {
        HandleHeal();
    }

    private void HandleHeal()
    {
        _healer.Heal(_targetHealth);
    }
}