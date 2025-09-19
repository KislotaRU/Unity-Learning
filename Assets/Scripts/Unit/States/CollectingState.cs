using System;
using UnityEngine;

public class CollectingState : State
{
    private readonly Unit _unit;

    public event Action Collected;
    
    public CollectingState(Unit unit)
    {
        _unit = unit;
    }

    public override void Enter()
    {
        //Physics.OverlapBox();
        Collected?.Invoke();
    }
}