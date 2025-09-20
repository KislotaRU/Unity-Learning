using UnityEngine;

public class GiveCommand : Command
{
    private Unit _unit;
    private Item _item;

    public GiveCommand(Unit unit, Item item)
    {
        _unit = unit;
        _item = item;
    }

    public override void Execute()
    {
        _unit.StateMachine.SetState<GivingState>(UnitStateType.Giving, givingState =>
        {
            givingState.SetTarget(_item);
            givingState.Given += HandleCommandCompleted;
        });
    }
}