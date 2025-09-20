using System;
using UnityEngine;

public class GivingState : State
{
    private readonly Unit _unit;

    private Item _item;

    public event Action Given;

    public GivingState(Unit unit)
    {
        _unit = unit;
    }

    public void SetTarget(Item item) =>
        _item = item;

    public override void Enter()
    {
        if (_unit.Hand.transform.childCount > 0)
            if (_unit.Hand.GetChild(0).TryGetComponent(out Item item))
                if (_item.GetType() == item.GetType())
                    item.HandleDestroy();

        _unit.StateMachine.SetState<IdleState>(UnitStateType.Idle, null);

        Given?.Invoke();
    }
}