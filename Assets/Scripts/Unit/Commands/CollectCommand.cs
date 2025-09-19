using System;

public class CollectCommand : Command
{
    private Unit _unit;

    public override event Action Completed;

    public CollectCommand(Unit unit)
    {
        _unit = unit;
    }

    public override void Execute()
    {
        _unit.StateMachine.SetState<CollectingState>(UnitStateType.Collecting, collectingState =>
        {
            collectingState.Collected += HandleCommandCompleted;
        });
    }

    public override void Undo()
    {
        HandleCommandCompleted();
    }

    private void HandleCommandCompleted()
    {
        IsCompleted = true;

        //_unit.StateMachine.SetState<IdleState>(UnitStateType.Idle, null);

        Completed?.Invoke();
    }
}