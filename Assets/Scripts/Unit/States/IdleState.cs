using UnityEngine;

public class IdleState : IUnitState
{
    private readonly Unit _unit;

    public IdleState(Unit unit)
    {
        _unit = unit;
    }

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}