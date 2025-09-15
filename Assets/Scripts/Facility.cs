using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Queue<Vector3> _targets;
    [SerializeField] private Scanner _scanner;

    private readonly Dictionary<UnitStateType, List<Unit>> _unitsByState;

    private void Awake()
    {
        _targets = new Queue<Vector3>();

        Initialize();
    }

    public void Update()
    {
        HandleScanner();
        HandleCollect();
    }

    private void Initialize()
    {
        foreach (var unit in _units)
            RegisterUnit(unit);
    }

    private void HandleScanner()
    {
        if (_targets.Count != 0)
            return;

        _targets = _scanner.GetTargets();
    }

    private void HandleCollect()
    {
        if (TryGetUnitByState(UnitStateType.Idle, out Unit unit))
        {
            unit.HandleMoving(_targets.Dequeue());
        }
    }

    public void RegisterUnit(Unit unit)
    {
        unit.StateChanged += HandleUnitStateChanged;
    }

    public void UnregisterUnit(Unit unit)
    {
        unit.StateChanged -= HandleUnitStateChanged;
    }

    public bool TryGetUnitByState(UnitStateType stateType, out Unit unit)
    {
        List<Unit> units;

        unit = null;

        if (_unitsByState.ContainsKey(stateType) == false)
            return false;

        units = _unitsByState[stateType];

        if (units[0] == null)
            return false;

        unit = units[0];

        return true;
    }

    public List<Unit> GetUnitsInState(UnitStateType state)
    {
        return _unitsByState.TryGetValue(state, out var units) ? units : new List<Unit>();
    }

    public Unit GetFirstAvailableUnit(UnitStateType state)
    {
        return GetUnitsInState(state).FirstOrDefault();
    }

    private void HandleUnitStateChanged(Unit unit, UnitStateType newState)
    {
        _unitsByState[unit.PreviousStateType].Remove(unit);

        if (_unitsByState[unit.PreviousStateType].Count == 0)
            _unitsByState.Remove(unit.PreviousStateType);

        if (_unitsByState.ContainsKey(newState) == false)
            _unitsByState.Add(newState, new List<Unit>());

        _unitsByState[newState].Add(unit);
    }
}