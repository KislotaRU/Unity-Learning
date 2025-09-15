using UnityEngine;

public class MovingState : IUnitState, IParametrizedState
{
    private readonly Unit _unit;

    private Vector3 _targetPosition;

    public MovingState(Unit unit)
    {
        _unit = unit;
    }

    public void SetParameters(params object[] args)
    {
        if (args.Length > 0 && args[0] is Vector3 target)
            _targetPosition = target;
    }

    public void Enter() { }

    public void Update()
    {
        _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, _targetPosition, _unit.MoveSpeed * Time.deltaTime);

        if (Vector3.Distance(_unit.transform.position, _targetPosition) < 0.1f)
        {
            _unit.SetState(UnitStateType.Idle);
        }
    }

    public void Exit() { }
}