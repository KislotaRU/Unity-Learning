using UnityEngine;

public class CollectCommand : Command
{
    private Unit _unit;
    private Item _item;

    public CollectCommand(Unit unit, Item item)
    {
        _unit = unit;
        _item = item;
    }

    public override void Execute()
    {
        _unit.StateMachine.SetState<CollectingState>(UnitStateType.Collecting, collectingState =>
        {
            collectingState.SetTarget(_item);
            collectingState.Collected += HandleCommandCompleted;
        });
    }
}