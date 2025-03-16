using System.Collections.Generic;
using UnityEngine;

public class TrapBlock : MonoBehaviour
{
    [SerializeField] private float _attackSpeed = 5f;
    [SerializeField] private float _retreatSpeed = 1f;

    [Space]
    [SerializeField] private List<AttackDirection> _directions;

    private Vector2 _defaultPosition;
    private Vector2 _targetPosition;

    private bool _isAttacking = false;
    private bool _isRetreating = false;
    private bool _isInPlace = false;

    private RaycastHit2D[] _raycastHits2D;

    private void Awake()
    {
        _defaultPosition = transform.position;

        if (_directions.Count == 0)
            enabled = false;
    }
    
    private void FixedUpdate()
    {
        if (_isAttacking == false && _isRetreating == false)
            if (CanAttack() == false)
                return;

        if (_isAttacking)
            Attack();
        else if (_isRetreating)
            Retreat();
    }

    private bool CanAttack()
    {
        foreach (var direction in _directions)
        {
            _raycastHits2D = Physics2D.RaycastAll(transform.position, direction.Direction, direction.Range);
            Debug.DrawRay(transform.position, direction.Direction * direction.Range, Color.red);

            foreach (var raycastHit2D in _raycastHits2D)
            {
                if (raycastHit2D.collider == null)
                    continue;

                if (raycastHit2D.collider.TryGetComponent(out Player _))
                {
                    _targetPosition = (Vector2)transform.position + direction.Direction * direction.Range;
                    _isAttacking = true;

                    return true;
                }
            }
        }

        return false;
    }

    private void Attack()
    {
        MoveToPosition(_targetPosition, _attackSpeed, _isAttacking);

        if (_isInPlace)
        {
            _isAttacking = false;
            _isRetreating = true;
        }
    }

    private void Retreat()
    {
        MoveToPosition(_defaultPosition, _retreatSpeed, _isAttacking);

        if (_isInPlace)
            _isRetreating = false;
    }

    private void MoveToPosition(Vector2 targetPosition, float speed, bool isAttacking = false)
    {
        _isInPlace = false;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) == 0)
            _isInPlace = true;
    }
}