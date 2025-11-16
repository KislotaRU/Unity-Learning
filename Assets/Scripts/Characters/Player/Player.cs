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

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        _movementInputHandler.UpdateInput();
    }

    private void OnDestroy()
    {
        _movementInputHandler.Dispose();
        _combatInputHandler.Dispose();
    }
}