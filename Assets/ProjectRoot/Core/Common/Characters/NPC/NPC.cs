using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private EntityAnimator _entityAnimator;
    [SerializeField] private Follower _follower;
    [SerializeField] private Mover _mover;
    [SerializeField] private Rotator _rotator;

    private void Awake()
    {
        if (_entityAnimator == null)
            throw new ArgumentNullException(nameof(_entityAnimator));

        if (_follower == null)
            throw new ArgumentNullException(nameof(_follower));

        if (_mover == null)
            throw new ArgumentNullException(nameof(_mover));
    }

    private void Update()
    {
        HandleAnimator();
        HandleMove();
        HandleRotator();
    }

    private void HandleRotator()
    {
        _rotator.HandleLook(_follower.Direction);
    }

    private void HandleMove()
    {
        //transform.position += (_mover.Speed * Time.deltaTime * _follower.Direction);
        _mover.HandleMove(_follower.Direction);
    }

    private void HandleAnimator()
    {
        _entityAnimator.SetParametrs(_mover.CurrentSpeed);
    }
}