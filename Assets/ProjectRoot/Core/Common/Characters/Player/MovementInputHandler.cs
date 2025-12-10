using System;
using UnityEngine;
using Zenject;

public class MovementInputHandler : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;

    private InputActions _inputActions;

    [Inject]
    private void Consturct(InputActions inputActions)
    {
        _inputActions = inputActions;
    }

    public IMover Mover { get; private set; }
    public IRotator Rotator { get; private set; }

    private void Awake()
    {
        if (_inputActions == null)
            throw new ArgumentNullException(nameof(_inputActions));

        if (_mover == null)
            throw new ArgumentNullException(nameof(_mover));

        if (_rotator == null)
            throw new ArgumentNullException(nameof(_rotator));

        Mover = _mover;
        Rotator = _rotator;
    }

    public void UpdateInput()
    {
        Mover.HandleMove(_inputActions.Player.Move.ReadValue<Vector2>());
        Rotator.HandleLook(_inputActions.Player.Look.ReadValue<Vector2>());
    }
}  