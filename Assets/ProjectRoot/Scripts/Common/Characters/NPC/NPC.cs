using System;
using UnityEngine;

public class NPC : Entity
{
    [SerializeField] private Follower _follower;

    [SerializeField] private float _moveSpeed;

    public event Action<NPC> Died;

    public float CurrentSpeed { get; private set; }

    private void Update()
    {
        HandleAnimator();

        HandleMove();
        HandleRotator();
    }

    public void SetTarget(Transform target)
    {
        _follower.SetTarget(target);
    }

    protected override void HandleAnimator()
    {
        _animator.SetParametrs(CurrentSpeed);
    }

    protected virtual void HandleRotator()
    {
        transform.LookAt(_follower.Target);
    }

    protected virtual void HandleMove()
    {
        CurrentSpeed = _follower.Direction.sqrMagnitude;

        if (_follower.Direction.sqrMagnitude <= 0.1f)
            return;

        transform.position += _moveSpeed * Time.deltaTime * _follower.Direction;
    }

    protected override void OnDied()
    {
        Died?.Invoke(this);
    }
}