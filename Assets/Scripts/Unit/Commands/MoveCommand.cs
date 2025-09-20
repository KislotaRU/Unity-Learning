using UnityEngine;

public class MoveCommand : Command
{
    private Unit _unit;
    private Vector3 _targetPosition;

    public MoveCommand(Unit unit, Vector3 targetPosition)
    {
        _unit = unit;
        _targetPosition = targetPosition;
    }

    public override void Execute()
    {
        _unit.StateMachine.SetState<MovingState>(UnitStateType.Moving, movingState =>
        {
            movingState.SetTargetPosition(_targetPosition);
            movingState.ReachedTarget += HandleCommandCompleted;
        });
    }
}