using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private Queue<Vector3> _targets;
    [SerializeField] private Scanner _scanner;

    private Dictionary<UnitStateType, List<Unit>> _unitsByState;

    private void Awake()
    {
        _targets = new Queue<Vector3>();
        _unitsByState = new Dictionary<UnitStateType, List<Unit>>();
    }

    private void Start()
    {
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
        Vector3 targetPosition;

        if (_targets.Count == 0)
            return;

        if (TryGetUnitByState(UnitStateType.Idle, out Unit unit))
        {
            targetPosition = _targets.Dequeue();

            unit.HandleMoving(targetPosition);
        }
    }

    public void RegisterUnit(Unit unit)
    {
        unit.ChangedState += HandleUnitStateChanged;

        if (_unitsByState.ContainsKey(unit.CurrentStateType) == false)
            _unitsByState.Add(unit.CurrentStateType, new List<Unit>());

        _unitsByState[unit.CurrentStateType].Add(unit);
    }

    public void UnregisterUnit(Unit unit)
    {
        unit.ChangedState -= HandleUnitStateChanged;
    }

    public bool TryGetUnitByState(UnitStateType stateType, out Unit unit)
    {
        unit = null;

        if (_unitsByState.ContainsKey(stateType) == false)
            return false;

        if (_unitsByState[stateType].Count == 0)
            return false;

        unit = _unitsByState[stateType][0];

        return unit != null;
    }

    private void HandleUnitStateChanged(Unit unit, UnitStateType newStateType)
    {
        _unitsByState[unit.PreviousStateType].Remove(unit);

        if (_unitsByState[unit.PreviousStateType].Count == 0)
            _unitsByState.Remove(unit.PreviousStateType);

        if (_unitsByState.ContainsKey(newStateType) == false)
            _unitsByState.Add(newStateType, new List<Unit>());

        _unitsByState[newStateType].Add(unit);
    }
}