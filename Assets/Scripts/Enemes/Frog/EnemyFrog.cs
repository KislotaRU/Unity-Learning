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
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _pointAttack;

    [Header("View")]
    [SerializeField] private Viewer _viewer;

    [Space]
    [SerializeField] private Way _way;

    private readonly float _distanceToTargetNeeded = 0.2f;

    private Vector2 _currentTarget;
    private Vector2 _lastTarget;

    private Vector2 CurrentDirection => (_currentTarget - (Vector2)transform.position).normalized;

    private void Awake()
    {
        _enemyFrogAnimator = GetComponent<EnemyFrogAnimator>();
        _mover = GetComponent<Mover>();
        _flipper = GetComponent<Flipper>();

        _health = GetComponent<Health>();
        _damager = GetComponent<Damager>();
        _weapon = GetComponent<Weapon>();

        _viewer = GetComponent<Viewer>();

        if (_way == null)
            enabled = false;
        else
            _currentTarget = _way.GetNextPosition();

        _lastTarget = _currentTarget;
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
        if (_viewer.TrySeeTarget(transform.position, transform.right))
        {
            _lastTarget = _currentTarget;
            _currentTarget = _viewer.TargetPosition;
        }
        else
        {
            _currentTarget = _lastTarget;
        }

        _mover.Move(CurrentDirection);

        if (Vector2.Distance(transform.position, _currentTarget) <= _distanceToTargetNeeded)
        {
            _currentTarget = _way.GetNextPosition();

            if (_viewer.IsDetected == false)
                _lastTarget = _currentTarget;
        }
    }

    private void HandleFlip()
    {
        _flipper.Flip(CurrentDirection);
    }
}