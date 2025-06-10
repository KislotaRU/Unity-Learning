using System;
using UnityEngine;

public class Bird : MonoBehaviour, IReceiverScore
{
    [SerializeField] private InputReader _inputReader;
    [Space]
    [SerializeField] private Mover _mover;
    [SerializeField] private Shooter _shooter;
    [Space]
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private CollisionHandler _collisionHandler;

    public event Action GameOver;

    private void Update()
    {
        HandleMovement();
        HandleShoot();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += HandleCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= HandleCollision;
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
    }

    public void AddScore()
    {
        _scoreCounter.Add();
    }

    private void HandleCollision(IInteractable interactable)
    {
        if (interactable is Bullet)
            HandleDie();
        else if (interactable is Block)
            HandleDie();
    }

    private void HandleMovement()
    {
        _mover.Move(_inputReader.IsFlying);
    }

    private void HandleShoot()
    {
        if (_inputReader.IsShooting)
            _shooter.Shoot();
    }

    private void HandleDie()
    {
        GameOver?.Invoke();
    }
}