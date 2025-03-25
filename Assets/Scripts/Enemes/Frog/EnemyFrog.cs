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
    [SerializeField] private Repulsiver _repulsiver;

    [Header("Attack")]
    [SerializeField] private Damager _damager;
    [SerializeField] private Weapon _weapon;

    [Header("View")]
    [SerializeField] private Viewer _viewer;

    [Space]
    [SerializeField] private Way _way;

    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _followingSpeed;

    private readonly float _requiredDistance = 0.2f;

    private Vector2 _currentTargetPosition;
    private Vector2 _patrolTargetPosition;

    private Vector2 CurrentDirection => (_currentTargetPosition - (Vector2)transform.position).normalized;
    private float CurrentDistanceToTargetPosition => (_currentTargetPosition - (Vector2)transform.position).sqrMagnitude;

    private void Awake()
    {
        if (_way == null)
        {
            enabled = false;
            return;
        }

        _patrolTargetPosition = _way.GetNextPosition();
        _currentTargetPosition = _patrolTargetPosition;
    }

    private void Update()
    {
        HandleAnimation();
        HandleMovement();
        HandleFlip();
        HandleView();
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
        _enemyFrogAnimator.Setup(_mover.Speed);
    }

    private void HandleMovement()
    {
        _mover.Move(CurrentDirection);

        if (CurrentDistanceToTargetPosition <= _requiredDistance)
        {
            _patrolTargetPosition = _way.GetNextPosition();
            _currentTargetPosition = _patrolTargetPosition;
        }
    }

    private void HandleFlip()
    {
        _flipper.Flip(CurrentDirection);
    }

    private void HandleView()
    {
        if (_viewer.IsTracking == false)
            return;

        if (_viewer.TryGetTarget(out Vector2 targetPosition))
        {
            _currentTargetPosition = targetPosition;
        }
        else
        {
            _currentTargetPosition = _patrolTargetPosition;
            _mover.SetMoveSpeed(_defaultSpeed);
        }
    }

    private void HandleAttack()
    {
        if (_viewer.IsExistsTargetPosition == false)
            return;

        if (_weapon.TryAttack(out Health targetHealth))
            _damager.Attack(targetHealth);

        if (_weapon.IsPunchAvailable)
            _mover.SetMoveSpeed(0f);
        else
            _mover.SetMoveSpeed(_followingSpeed);
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