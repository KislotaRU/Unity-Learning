using System.Collections.Generic;
using UnityEngine;

public class EnemyTrapBlock : MonoBehaviour
{
    [Header("Movements")]
    [SerializeField] private Mover _mover;

    [Header("Attack")]
    [SerializeField] private Damager _damager;

    [SerializeField] private float _forwardSpeed = 5f;
    [SerializeField] private float _backSpeed = 1f;

    [Space]
    [SerializeField] private List<DirectionToMovement2D> _directions;

    private readonly float _offsetRaycast = 0.5f;

    private Vector2 _defaultPosition;
    private Vector2 _targetPosition;

    private bool _isForwardMoving = false;
    private bool _isBackMoving = false;

    private void Awake()
    {
        _defaultPosition = transform.position;

        if (_directions.Count == 0)
            enabled = false;
    }
    
    private void FixedUpdate()
    {
        if (_isForwardMoving == false && _isBackMoving == false)
            if (CanMove() == false)
                return;

        if (_isForwardMoving)
            MoveForward();
        else if (_isBackMoving)
            MoveBack();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isForwardMoving == false)
            return;

        if (collision.collider.TryGetComponent(out Health targetHealth))
            _damager.Attack(targetHealth);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 directionRay;

        foreach (var direction in _directions)
        {
            directionRay = direction.Direction * (direction.Range + _offsetRaycast);
            Gizmos.DrawRay(transform.position, directionRay);
        }
    }

    private bool CanMove()
    {
        foreach (var direction in _directions)
        {
            RaycastHit2D[] raycastHits2D = Physics2D.RaycastAll(transform.position, direction.Direction, direction.Range + _offsetRaycast);

            foreach (var raycastHit2D in raycastHits2D)
            {
                if (raycastHit2D.collider == null)
                    continue;

                if (raycastHit2D.collider.TryGetComponent(out Health _))
                {
                    _targetPosition = (Vector2)transform.position + direction.Direction * direction.Range;
                    _isForwardMoving = true;

                    return true;
                }
            }
        }

        return false;
    }

    private void MoveForward()
    {
        _mover.SetMoveSpeed(_forwardSpeed);
        _mover.MoveToPosition(_targetPosition);

        if (TryFinishMove(_targetPosition))
        {
            _isForwardMoving = false;
            _isBackMoving = true;
        }
    }

    private void MoveBack()
    {
        _mover.SetMoveSpeed(_backSpeed);
        _mover.MoveToPosition(_defaultPosition);

        if (TryFinishMove(_defaultPosition))
        {
            _isBackMoving = false;
        }
    }
    
    private bool TryFinishMove(Vector2 targetPosition)
    {
        if (Vector2.Distance(transform.position, targetPosition) == 0)
            return true;

        return false;
    }
}