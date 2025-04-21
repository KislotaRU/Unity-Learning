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

    [Header("Weapon")]
    [SerializeField] private Katana _katana;
    [SerializeField] private Vampirism _vampirism;

    [Header("Collector")]
    [SerializeField] private Collector _collector;

    private void Update()
    {
        HandleAnimation();

        HandleMovement();
        HandleJump();
        HandleFlip();

        HandleAttack();
        HandleAbility();
    }

    private void OnEnable()
    {
        _health.AcceptedDamage += HandleRepulsion;
        _health.Died += HandleDie;
    }

    private void OnDisable()
    {
        _health.AcceptedDamage -= HandleRepulsion;
        _health.Died -= HandleDie;
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
        if (_jumper.IsGrounded)
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

        _katana.Attack();
    }

    private void HandleAbility()
    {
        if (_inputReader.IsVampiring == false)
            return;

        _vampirism.Attack();
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