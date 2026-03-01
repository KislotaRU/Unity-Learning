using System;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private PlayerInput _input;

    public event Action<Player> Died;

    private void Awake()
    {
        if (_input == null)
            throw new ArgumentNullException(nameof(_input));
    }

    private void Update()
    {
        HandleAnimator();
        HandleInput();
    }

    private void HandleInput()
    {
        _input.UpdateInput();
    }

    protected override void HandleAnimator()
    {
        _animator.SetParametrs(_input.Mover.CurrentSpeed);
    }

    protected override void OnDied()
    {
        Died?.Invoke(this);
    }
}