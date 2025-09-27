using System;

public class GivingState : State
{
    private readonly Unit _unit;

    public event Action Given;

    private Facility _facility;
    private Item _item;

    public GivingState(Unit unit)
    {
        _unit = unit;
    }

    public void SetTarget(Item item) =>
        _item = item;

    public void SetReceiver(Facility facility) =>
        _facility = facility;

    public override void Enter()
    {
        //for (int i = 0; i < _unit.Hand.transform.childCount; i++)
        //{
        //    if (_unit.Hand.GetChild(i).TryGetComponent(out Item item) == false)
        //        continue;

        //    if (_item.GetType() != item.GetType())
        //        continue;

        //    item.HandleDestroy();
        //    _facility.ResourcesCapacity.Increase(1f);
        //}

        //_unit.StateMachine.SetState<IdleState>(UnitStateType.Idle, null);

        //Given?.Invoke();
    }
}