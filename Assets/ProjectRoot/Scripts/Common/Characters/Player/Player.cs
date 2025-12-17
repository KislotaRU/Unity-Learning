using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private EntityAnimator _entityAnimator;
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
        if (_entityAnimator == null)
            throw new ArgumentNullException(nameof(_entityAnimator));

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
        _entityAnimator.SetParametrs(_movementInputHandler.Mover.CurrentSpeed);
    }
}