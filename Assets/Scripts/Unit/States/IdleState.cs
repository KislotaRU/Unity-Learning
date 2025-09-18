using UnityEngine;

public class IdleState : State
{
    private readonly Unit _unit;

    public IdleState(Unit unit)
    {
        _unit = unit;
    }
}