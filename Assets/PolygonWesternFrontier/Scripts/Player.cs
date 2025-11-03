using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _lookSpeed;

    private PlayerInput _inputActions;

    private Vector2 _moveDirection;
    private Vector2 _lookDirection;

    private void Awake()
    {
        _inputActions = new PlayerInput();
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

    private void Update()
    {
        _moveDirection = _inputActions.Player.Move.ReadValue<Vector2>();
        _lookDirection = _inputActions.Player.Look.ReadValue<Vector2>();

        Move();
        Look();
    }

    private void Move()
    {
        if (_moveDirection.sqrMagnitude <= 0.1f)
            return;

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(_moveDirection.x, 0f, _moveDirection.y) * scaledMoveSpeed;

        transform.Translate(offset);
    }

    private void Look()
    {
        if (_lookDirection.sqrMagnitude <= 0.1f)
            return;

        float scaledLookSpeed = _lookSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(-_lookDirection.y, _lookDirection.x, 0f) * scaledLookSpeed;

        transform.Rotate(offset);
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void Shoot()
    {
        Debug.Log($"Shoot");
    }
}