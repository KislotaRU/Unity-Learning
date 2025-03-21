using UnityEngine;

public class EnemyFrog : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private EnemyFrogAnimator _enemyFrogAnimator;

    [Header("Movements")]
    [SerializeField] private Mover _mover;
    [SerializeField] private Flipper _flipper;

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