using System;
using UnityEngine;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private MovementInputHandler _movementInputHandler;
    [SerializeField] private CombatInputHandler _combatInputHandler;

    private InputActions _inputActions;

    [Inject]
    private void Consturct(InputActions inputActions)
    {
        _inputActions = inputActions;
    }

    public IMover Mover => _movementInputHandler.Mover;

    private void Awake()
    {
        if (_inputActions == null)
            throw new ArgumentNullException(nameof(_inputActions));

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

    public void UpdateInput()
    {
        _movementInputHandler.UpdateInput();
    }
}