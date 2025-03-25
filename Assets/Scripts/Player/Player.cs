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
    [SerializeField] private Walker _walker;

    [Header("Health")]
    [SerializeField] private Health _health;
    [SerializeField] private Repulsiver _repulsiver;

    [Header("Attack")]
    [SerializeField] private Damager _damager;
    [SerializeField] private Weapon _weapon;

    [Header("Collector")]
    [SerializeField] private Collector _collector;

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
        _health.Dead += HandleDie;
    }

    private void OnDisable()
    {
        _health.TakedDamage -= HandleRepulsion;
        _health.Dead -= HandleDie;
    }

    private void HandleAnimation()
    {
        _playerAnimator.Setup(_mover.Speed, _walker.IsGrounded);
    }

    private void HandleMovement()
    {
        _mover.Move(_inputReader.MoveDirection);
    }

    private void HandleJump()
    {
        if (_walker.CanWalk())
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

    private void HandleDie()
    {
        Destroy(gameObject);
    }
}