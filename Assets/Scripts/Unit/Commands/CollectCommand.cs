public class CollectCommand : Command
{
    private readonly Unit _unit;
    private readonly Item _item;

    private CollectingState _collectingState;

    public CollectCommand(Unit unit, Item item)
    {
        _unit = unit;
        _item = item;
    }

    public override void Execute()
    {
        //_unit.StateMachine.SetState<CollectingState>(UnitStateType.Collecting, collectingState =>
        //{
        //    _collectingState = collectingState;

        //    collectingState.Collected += HandleCommandCompleted;
        //    collectingState.SetTarget(_item);
        //});
    }

    protected override void HandleCommandCompleted()
    {
        _collectingState.Collected -= HandleCommandCompleted;

        base.HandleCommandCompleted();
    }
}