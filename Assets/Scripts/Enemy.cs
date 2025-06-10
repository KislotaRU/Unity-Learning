using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private Shooter _shooter;
    [SerializeField] private CollisionHandler _collisionHandler;

    public event Action<Enemy> Destroyed;

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += HandleCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= HandleCollision;
    }

    public void Initialize(Vector2 position)
    {
        transform.position = position;
    }

    public void HandleShoot()
    {
        _shooter.StartShooting();
    }

    private void HandleCollision(IInteractable interactable)
    {
        if (interactable is Bullet)
            HandleDie();
    }

    private void HandleDie()
    {
        Destroyed?.Invoke(this);
    }
}