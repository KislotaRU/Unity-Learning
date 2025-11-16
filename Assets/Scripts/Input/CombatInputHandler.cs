using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatInputHandler : MonoBehaviour
{
    private readonly InputActions _inputActions;

    public CombatInputHandler(InputActions inputActions, IAttacker attacker, IHealer healer)
    {
        _inputActions = inputActions ?? throw new ArgumentNullException(nameof(inputActions));

        Attacker = attacker ?? throw new ArgumentNullException(nameof(attacker));
        Healer = healer ?? throw new ArgumentNullException(nameof(healer));

        Subscribe();
    }

    public IAttacker Attacker { get; }
    public IHealer Healer { get; }

    public void Dispose() =>
        Unsubscribe();

    private void Subscribe()
    {
        _inputActions.Player.Enable();

        _inputActions.Player.Shoot.performed += OnShoot;
        _inputActions.Player.Reload.performed += OnReload;

        _inputActions.Player.Heal.performed += OnHeal;
    }

    private void Unsubscribe()
    {
        _inputActions.Player.Shoot.performed -= OnShoot;
        _inputActions.Player.Reload.performed -= OnReload;

        _inputActions.Player.Heal.performed -= OnHeal;

        _inputActions.Player.Disable();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Attacker.Attack();
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        Attacker.Reload();
    }

    private void OnHeal(InputAction.CallbackContext context)
    {
        Healer.Heal();
    }
}