using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputReader _inputReader;

    [Header("Animation")]
    [SerializeField] private PlayerAnimator _playerAnimator;

    [Header("Movements")]
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private Flipper _flipper;

    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _flipper = GetComponent<Flipper>();

        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleAnimation();
        HandleMovement();
        HandleJump();
        HandleFlip();
    }

    private void HandleAnimation()
    {

    }

    private void HandleMovement()
    {
        _mover.Move(_rigidbody2D, _inputReader.MoveDirection);
    }

    private void HandleJump()
    {
        _jumper.Jump(_collider2D, _inputReader.IsJumping);
    }

    private void HandleFlip()
    {
        _flipper.Flip(transform, _inputReader.MoveDirection);
    }
}