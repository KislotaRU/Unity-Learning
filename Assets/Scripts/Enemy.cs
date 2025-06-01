using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Shooter _shooter;

    public event Action<Enemy> Destroyed;

    private Vector2 _currentTargetPosition;

    private Vector2 CurrentDirection => (_currentTargetPosition - (Vector2)transform.position).normalized;

    private void Update()
    {

    }

    private void HandleAttack()
    {

    }

    private void HandleDie()
    {
        Destroyed?.Invoke(this);
    }
}