using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;
    [SerializeField] private CollisionHandler _collisionHandler;

    public event Action<Bullet> Destroyed;

    private IShooter _shooter;

    private void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += HandleCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= HandleCollision;
    }

    public void Initialize(Vector2 position, Quaternion rotation, IShooter shooter)
    {
        transform.position = position;
        transform.rotation = rotation;

        _shooter = shooter;
    }

    private void HandleCollision(IInteractable interactable)
    {
        if (interactable is Floor)
        {
            HandleDestroy();
        }
        else if (interactable is Enemy)
        {
            _shooter?.AddScore();

            HandleDestroy();
        }
    }

    private void Move()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime, Space.World);
    }

    private void HandleDestroy()
    {
        Destroyed?.Invoke(this);
    }
}