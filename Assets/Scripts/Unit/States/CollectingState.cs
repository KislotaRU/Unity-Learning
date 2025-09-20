using System;
using UnityEngine;

public class CollectingState : State
{
    private readonly Unit _unit;
    private readonly Item _item;

    public event Action Collected;
    
    public CollectingState(Unit unit)
    {
        _unit = unit;
    }

    public override void Enter()
    {
        //Physics.OverlapBox();

        _unit.StateMachine.SetState<IdleState>(UnitStateType.Idle, null);

        Collected?.Invoke();
    }
}