using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private EnemyFrogAnimator _enemyFrogAnimator;

    [Header("Movements")]
    [SerializeField] private Mover _mover;
    [SerializeField] private Flipper _flipper;

    [Header("Health")]
    [SerializeField] private Health _health;

    [Header("Attack")]
    [SerializeField] private Damager _damager;

    [Space]
    [SerializeField] private Way _way;

    private readonly float _distanceToTargetNeeded = 0.2f;

    private Vector2 _currentTarget;

    private Vector2 CurrentDirection => (_currentTarget - (Vector2)transform.position).normalized;

    private void Awake()
    {
        _enemyFrogAnimator = GetComponent<EnemyFrogAnimator>();
        _mover = GetComponent<Mover>();
        _flipper = GetComponent<Flipper>();

        _health = GetComponent<Health>();
        _damager = GetComponent<Damager>();

        if (_way == null)
            enabled = false;
        else
            _currentTarget = _way.GetNextPosition();
    }

    private void Update()
    {
        HandleAnimation();
        HandleMovement();
        HandleFlip();
    }

    private void HandleAnimation()
    {
        _enemyFrogAnimator.Setup(_mover.Speed);
    }

    private void HandleMovement()
    {
        _mover.Move(CurrentDirection);

        if (Vector2.Distance(transform.position, _currentTarget) <= _distanceToTargetNeeded)
            _currentTarget = _way.GetNextPosition();
    }

    private void HandleFlip()
    {
        _flipper.Flip(CurrentDirection);
    }
}