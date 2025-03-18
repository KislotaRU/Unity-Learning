using UnityEngine;

public class MoverToWaypoints : MonoBehaviour
{
    [SerializeField] private Way _way;
    [SerializeField] private float _moveSpeed = 1f;

    private readonly float _distanceToTargetNeeded = 0.2f;

    private Vector3 _currentTarget;

    private void Awake()
    {
        if (_way == null)
            enabled = false;
        else
            _currentTarget = _way.GetNextPosition();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, _currentTarget, _moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _currentTarget) > _distanceToTargetNeeded)
            return;
        
        _currentTarget = _way.GetNextPosition();
    }
}