using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class CombatInputHandler :MonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;

    private InputActions _inputActions;

    [Inject]
    private void Consturct(InputActions inputActions)
    {
        _inputActions = inputActions;
    }

    private void Awake()
    {
        if (_inputActions == null)
            throw new ArgumentNullException(nameof(_inputActions));

        if (_weaponController == null)
            throw new ArgumentNullException(nameof(_weaponController));
    }

    private void OnEnable()
    {
        _inputActions.Player.Shoot.performed += OnShoot;
        _inputActions.Player.Reload.performed += OnReload;
        _inputActions.Player.SwitchWeapon.performed += OnSwitch;
    }

    private void OnDisable()
    {
        _inputActions.Player.Shoot.performed -= OnShoot;
        _inputActions.Player.Reload.performed -= OnReload;
        _inputActions.Player.SwitchWeapon.performed -= OnSwitch;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        _weaponController.Attack();
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        _weaponController.Reload();
    }

    private void OnSwitch(InputAction.CallbackContext context)
    {
        _weaponController.Switch();
    }
}