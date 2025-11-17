using System;
using UnityEngine;

public class MovementInputHandler : IDisposable
{
    private readonly InputActions _inputActions;

    public MovementInputHandler(InputActions inputActions, IMover mover, IRotator rotator)
    {
        _inputActions = inputActions ?? throw new ArgumentNullException(nameof(inputActions));

        Mover = mover ?? throw new ArgumentNullException(nameof(mover));
        Rotator = rotator ?? throw new ArgumentNullException(nameof(rotator));

        Subscribe();
    }

    public IMover Mover { get; }
    public IRotator Rotator { get; }

    public void Dispose() =>
        Unsubscribe();

    public void UpdateInput()
    {
        Mover.HandleMove(_inputActions.Player.Move.ReadValue<Vector2>());
        Rotator.HandleLook(_inputActions.Player.Look.ReadValue<Vector2>());
    }

    public void Subscribe()
    {
        _inputActions.Player.Enable();
    }

    private void Unsubscribe()
    {
        _inputActions.Player.Disable();
    }
}