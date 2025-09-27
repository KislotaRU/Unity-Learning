using System;
using UnityEngine;

public class MovingState : State
{
    private readonly Unit _unit;
    private readonly MovingConfiguration _configuration;

    public event Action ReachedTarget;

    private Vector3 _targetPosition;

    private Vector3 Position
    {
        get => _unit.transform.position;
        set => _unit.transform.position = value;
    }

    public MovingState(Unit unit, MovingConfiguration configuration)
    {
        _unit = unit;
        _configuration = configuration;
    }

    public void SetTargetPosition(Vector3 targetPosition) =>
        _targetPosition = targetPosition;

    public override void Update()
    {
        //Vector3 direction;
        //Quaternion targetRotation;

        //Position = Vector3.MoveTowards(Position, _targetPosition, _configuration.MoveSpeed * Time.deltaTime);
        //direction = _targetPosition - Position;

        //if (direction != Vector3.zero)
        //{
        //    targetRotation = Quaternion.LookRotation(direction);
        //    _unit.transform.rotation = Quaternion.Slerp(_unit.transform.rotation, targetRotation, _configuration.RotateSpeed * Time.deltaTime);
        //}

        //if (direction.sqrMagnitude <= 0)
        //{
        //    _unit.StateMachine.SetState<IdleState>(UnitStateType.Idle, null);

        //    ReachedTarget?.Invoke();
        //}
    }
}