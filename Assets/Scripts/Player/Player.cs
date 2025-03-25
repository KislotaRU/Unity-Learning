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

    [Header("Health")]
    [SerializeField] private Health _health;
    [SerializeField] private Repulsiver _repulsiver;

    [Header("Attack")]
    [SerializeField] private Damager _damager;
    [SerializeField] private Weapon _weapon;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();

        _playerAnimator = GetComponent<PlayerAnimator>();

        _mover = GetComponent<Mover>();
        _jumper = GetComponent<Jumper>();
        _flipper = GetComponent<Flipper>();

        _health = GetComponent<Health>();
        _repulsiver = GetComponent<Repulsiver>();

        _damager = GetComponent<Damager>();
        _weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        HandleAnimation();

        HandleMovement();
        HandleJump();
        HandleFlip();

        HandleAttack();
    }

    private void OnEnable()
    {
        _health.TakedDamage += HandleRepulsion;
    }

    private void OnDisable()
    {
        _health.TakedDamage -= HandleRepulsion;
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

    private void HandleAttack()
    {
        if (_inputReader.IsAttacking == false)
            return;

        if (_weapon.TryAttack(out Health targetHealth))
            _damager.Attack(targetHealth);
    }

    private void HandleRepulsion()
    {
        _repulsiver.Push(_flipper.FaceDirection);
    }
}