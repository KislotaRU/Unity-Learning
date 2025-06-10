using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;
    [SerializeField] private CollisionHandler _collisionHandler;

    private IReceiverScore _shooter;

    public event Action<Bullet> Destroyed;

    private void Update()
    {
        HandleMove();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += HandleCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= HandleCollision;
    }

    public void Initialize(Vector2 position, Quaternion rotation, IReceiverScore shooter)
    {
        transform.position = position;
        transform.rotation = rotation;

        _shooter = shooter;
    }

    private void HandleCollision(IInteractable interactable)
    {
        if (interactable is Block)
        {
            HandleDestroy();
        }
        else if (interactable is Enemy)
        {
            _shooter?.AddScore();

            HandleDestroy();
        }
    }

    private void HandleMove()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime, Space.World);
    }

    private void HandleDestroy()
    {
        Destroyed?.Invoke(this);
    }
}