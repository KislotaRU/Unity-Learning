using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [Space]
    [SerializeField] private Mover _mover;
    [SerializeField] private Shooter _shooter;
    [Space]
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;

    public Action GameOver;

    private void Update()
    {
        HandleMovement();
        HandleShoot();
    }

    private void OnEnable()
    {
        _birdCollisionHandler.CollisionDetected += HandleCollision;
    }

    private void OnDisable()
    {
        _birdCollisionHandler.CollisionDetected -= HandleCollision;
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
    }

    private void HandleCollision(IInteractable interactable)
    {
        if (interactable is Bullet)
        {
            GameOver?.Invoke();
        }
        else if (interactable is Floor)
        {
            GameOver?.Invoke();
        }
        else if (interactable is ScoreZone)
        {
            _scoreCounter.Add();
        }
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
}