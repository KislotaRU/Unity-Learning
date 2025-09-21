using UnityEngine;

public class GiveCommand : Command
{
    private readonly Unit _unit;
    private readonly Item _item;

    private GivingState _givingState;

    public GiveCommand(Unit unit, Item item)
    {
        _unit = unit;
        _item = item;
    }

    public override void Execute()
    {
        _unit.StateMachine.SetState<GivingState>(UnitStateType.Giving, givingState =>
        {
            _givingState = givingState;
            givingState.Given += HandleCommandCompleted;
            givingState.SetTarget(_item);
        });
    }

    protected override void HandleCommandCompleted()
    {
        _givingState.Given -= HandleCommandCompleted;

        base.HandleCommandCompleted();
    }
}