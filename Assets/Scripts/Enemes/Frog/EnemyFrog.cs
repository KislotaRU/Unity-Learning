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
        _repulsiver = GetComponent<Repulsiver>();

        _damager = GetComponent<Damager>();
        _weapon = GetComponent<Weapon>();

        _viewer = GetComponent<Viewer>();

        if (_way == null)
            enabled = false;
        else
            _lastTarget = _way.GetNextPosition();

        _currentTarget = _lastTarget;
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
    }

    private void OnDisable()
    {
        _health.TakedDamage -= HandleRepulsion;
    }

    private void HandleAnimation()
    {
        _enemyFrogAnimator.Setup(_mover.Speed);
    }

    private void HandleMovement()
    {
        _mover.Move(CurrentDirection);

        if (Vector2.Distance(transform.position, _currentTarget) <= _distanceToTargetNeeded)
        {
            _lastTarget = _way.GetNextPosition();
            _currentTarget = _lastTarget;
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
            _currentTarget = targetPosition;
        }
        else
        {
            _currentTarget = _lastTarget;
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
}