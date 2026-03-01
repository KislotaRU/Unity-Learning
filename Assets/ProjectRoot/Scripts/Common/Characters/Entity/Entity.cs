using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected EntityAnimator _animator;
    [SerializeField] protected Health _health;

    private void Awake()
    {
        if (_animator == null)
            throw new ArgumentNullException(nameof(_animator));
    }

    private void OnEnable()
    {
        _health.Devastated += OnDied;
    }

    private void OnDisable()
    {
        _health.Devastated -= OnDied;
    }

    protected abstract void HandleAnimator();

    protected abstract void OnDied();
}