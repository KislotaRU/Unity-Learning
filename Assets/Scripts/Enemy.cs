using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Shooter _shooter;

    public event Action<Enemy> Destroyed;

    private void Update()
    {
        HandleShoot();
    }

    public void Initialize(Vector2 position)
    {
        transform.position = position;
    }

    private void HandleShoot()
    {
        _shooter.Shoot();
    }

    private void HandleDie()
    {
        Destroyed?.Invoke(this);
    }
}