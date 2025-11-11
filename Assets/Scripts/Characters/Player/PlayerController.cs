using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Shooter))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _inputActions;

    private Mover _mover;
    private Rotator _rotator;
    private Shooter _shooter;

    public float SpeedMovement => _mover.SpeedMovement;

    private void Awake()
    {
        _inputActions = new PlayerInput();

        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _shooter = GetComponent<Shooter>();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();

        _inputActions.Player.Shoot.performed += OnShoot;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();

        _inputActions.Player.Shoot.performed -= OnShoot;
    }

    public void UpdateInput()
    {
        _mover.HandleMove(_inputActions.Player.Move.ReadValue<Vector2>());
        _rotator.HandleLook(_inputActions.Player.Look.ReadValue<Vector2>());
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        _shooter.Shoot();
    }
}