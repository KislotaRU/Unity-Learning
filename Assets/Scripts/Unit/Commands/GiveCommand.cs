public class GiveCommand : Command
{
    private readonly Unit _unit;
    private readonly Item _item;
    private readonly Facility _facility;

    private GivingState _givingState;

    public GiveCommand(Unit unit, Item item, Facility facility)
    {
        _unit = unit;
        _item = item;
        _facility = facility;
    }

    public override void Execute()
    {
        //_unit.StateMachine.SetState<GivingState>(UnitStateType.Giving, givingState =>
        //{
        //    _givingState = givingState;

        //    givingState.Given += HandleCommandCompleted;
        //    givingState.SetTarget(_item);
        //    givingState.SetReceiver(_facility);
        //});
    }

    protected override void HandleCommandCompleted()
    {
        _givingState.Given -= HandleCommandCompleted;

        base.HandleCommandCompleted();
    }
}