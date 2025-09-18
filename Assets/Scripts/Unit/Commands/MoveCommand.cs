using System;
using UnityEngine;

public class MoveCommand : Command
{
    private Unit _unit;
    private Vector3 _targetPosition;

    public override event Action Completed;

    public MoveCommand(Unit unit, Vector3 targetPosition)
    {
        _unit = unit;
        _targetPosition = targetPosition;
    }

    public override void Execute()
    {
        _unit.StateMachine.HandleState<MovingState>(UnitStateType.Moving, movingState =>
        {
            movingState.SetTargetPosition(_targetPosition);
            movingState.ReachedTarget += HandleCommandCompleted;
        });
    }

    public override void Undo()
    {
        HandleCommandCompleted();
    }

    private void HandleCommandCompleted()
    {
        IsCompleted = true;

        _unit.StateMachine.HandleState<IdleState>(UnitStateType.Idle, null);

        Completed?.Invoke();
    }
}