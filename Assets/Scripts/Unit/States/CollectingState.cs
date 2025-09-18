using UnityEngine;

public class CollectingState : State
{
    private readonly Unit _unit;
    
    public CollectingState(Unit unit)
    {
        _unit = unit;
    }

    public override void Enter()
    {
        //Physics.OverlapBox();
    }
}