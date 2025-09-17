using UnityEngine;

public class IdleState : BaseState
{
    private readonly Unit _unit;

    public IdleState(Unit unit)
    {
        _unit = unit;
    }
}