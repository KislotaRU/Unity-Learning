using UnityEngine;

public class CollectCommand : Command
{
    private Unit _unit;

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
}