using System;
using UnityEngine;

public class MovingState : State
{
    private const float MaxDistance = 0.05f;

    private readonly Unit _unit;
    private readonly MovingStateConfiguration _configuration;

    public event Action ReachedTarget;

    private Vector3 _targetPosition;

    private Vector3 Position
    {
        get => _unit.transform.position;
        set => _unit.transform.position = value;
    }

    public MovingState(Unit unit, MovingStateConfiguration configuration)
    {
        _unit = unit;
        _configuration = configuration;
    }

    public void SetTargetPosition(Vector3 targetPosition) =>
        _targetPosition = targetPosition;

    public override void Update()
    {
        Position = Vector3.MoveTowards(Position, _targetPosition, _configuration.MoveSpeed * Time.deltaTime);

        if ((_targetPosition - Position).sqrMagnitude <= MaxDistance)
        {
            ReachedTarget?.Invoke();
        }
    }
}