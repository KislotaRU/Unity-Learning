using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    private MovementInputHandler _movementInputHandler;
    private CombatInputHandler _combatInputHandler;

    [Inject]
    private void Construct(MovementInputHandler movementInputHandler, CombatInputHandler combatInputHandler)
    {
        _movementInputHandler = movementInputHandler;
        _combatInputHandler = combatInputHandler;
    }

    private void Awake()
    {
        if (_movementInputHandler == null)
            throw new ArgumentNullException(nameof(_movementInputHandler));

        if (_combatInputHandler == null)
            throw new ArgumentNullException(nameof(_combatInputHandler));
    }

    private void OnEnable()
    {
        _movementInputHandler.Subscribe();
        _combatInputHandler.Subscribe();
    }

    private void OnDisable()
    {
        _movementInputHandler.Dispose();
        _combatInputHandler.Dispose();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _movementInputHandler.UpdateInput();
    }
}