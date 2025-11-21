using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private MovementInputHandler _movementInputHandler;
    [SerializeField] private CombatInputHandler _combatInputHandler;

    private InputActions _inputActions;

    [Inject]
    private void Consturct(InputActions inputActions)
    {
        _inputActions = inputActions;
    }

    private void Awake()
    {
        if (_playerAnimator == null)
            throw new ArgumentNullException(nameof(_playerAnimator));

        if (_movementInputHandler == null)
            throw new ArgumentNullException(nameof(_movementInputHandler));

        if (_combatInputHandler == null)
            throw new ArgumentNullException(nameof(_combatInputHandler));
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        HandleInput();
        HandleAnimator();
    }

    private void HandleInput()
    {
        _movementInputHandler.UpdateInput();
    }

    private void HandleAnimator()
    {
        _playerAnimator.SetParametrs(_movementInputHandler.Mover.CurrentSpeed);
    }
}