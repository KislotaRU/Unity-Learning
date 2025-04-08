using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(Damager))]
public class ButtonAttack : MonoBehaviour
{
    [SerializeField] private Health _target;

    private Damager _damager;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _damager = GetComponent<Damager>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleAttack);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleAttack);
    }

    private void HandleAttack()
    {
        _damager.Attack(_target);
    }
}