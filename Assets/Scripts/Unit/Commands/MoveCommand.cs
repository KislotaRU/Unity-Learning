using UnityEngine;

public class MoveCommand : Command
{
    private readonly Unit _unit;
    private readonly Vector3 _targetPosition;

    private MovingState _movingState;

    public MoveCommand(Unit unit, Vector3 targetPosition)
    {
        _unit = unit;
        _targetPosition = targetPosition;
    }

    public override void Execute()
    {
        //_unit.StateMachine.SetState<MovingState>(UnitStateType.Moving, movingState =>
        //{
        //    _movingState = movingState;
        //    movingState.ReachedTarget += HandleCommandCompleted;
        //    movingState.SetTargetPosition(_targetPosition);
        //});
    }

    protected override void HandleCommandCompleted()
    {
        _movingState.ReachedTarget -= HandleCommandCompleted;

        base.HandleCommandCompleted();
    }
}