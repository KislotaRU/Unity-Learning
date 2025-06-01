using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private BirdMover _birdMover;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BirdCollisionHandler _birdCollisionHandler;

    public Action GameOver;

    private void OnEnable()
    {
        _birdCollisionHandler.CollisionDetected += HandelCollision;
    }

    private void OnDisable()
    {
        _birdCollisionHandler.CollisionDetected -= HandelCollision;
    }

    private void HandelCollision(IInteractable interactable)
    {
        if (interactable is Bullet)
        {
            GameOver?.Invoke();
        }
        else if (interactable is ScoreZone)
        {
            _scoreCounter.Add();
        }
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _birdMover.Reset();
    }
}