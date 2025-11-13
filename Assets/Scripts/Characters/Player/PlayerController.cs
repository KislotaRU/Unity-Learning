using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Attacker))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _inputActions;

    private Mover _mover;
    private Rotator _rotator;
    private Attacker _attacker;

    public float SpeedMovement => _mover.SpeedMovement;

    private void Awake()
    {
        _inputActions = new PlayerInput();

        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();

        _inputActions.Player.Shoot.performed += OnShoot;
        _inputActions.Player.Reload.performed += OnReload;
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();

        _inputActions.Player.Shoot.performed -= OnShoot;
        _inputActions.Player.Reload.performed -= OnReload;
    }

    public void UpdateInput()
    {
        _mover.HandleMove(_inputActions.Player.Move.ReadValue<Vector2>());
        _rotator.HandleLook(_inputActions.Player.Look.ReadValue<Vector2>());
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        _attacker.Attack();
    }

    private void OnReload(InputAction.CallbackContext context)
    {
        _attacker.Reload();
    }
}