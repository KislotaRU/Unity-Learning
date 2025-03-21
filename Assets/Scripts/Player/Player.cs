using UnityEngine;

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

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _flipper = GetComponent<Flipper>();
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
        _playerAnimator.Setup(_mover.Speed, _jumper.IsGrounded);
    }

    private void HandleMovement()
    {
        _mover.Move(_inputReader.MoveDirection);
    }

    private void HandleJump()
    {
        _jumper.Jump(_inputReader.IsJumping);
    }

    private void HandleFlip()
    {
        _flipper.Flip(_inputReader.MoveDirection);
    }
}