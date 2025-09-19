using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private List<Unit> _freeUnits;
    [SerializeField] private Queue<Vector3> _targets;
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _basket;

    private void Awake()
    {
        _targets = new Queue<Vector3>();
        _freeUnits = new List<Unit>();
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

        if (TryGetFreeUnit(out Unit unit))
        {
            _freeUnits.Remove(unit);

            targetPosition = _targets.Dequeue();

            unit.ResetCommands();
            unit.AddCommand(new MoveCommand(unit, targetPosition));
            unit.AddCommand(new CollectCommand(unit));
            unit.AddCommand(new MoveCommand(unit, _basket.transform.position));
        }
    }

    public void RegisterUnit(Unit unit)
    {
        unit.CompletedCommand += HandleUnit;

        if (unit.IsFree)
            _freeUnits.Add(unit);
    }

    public void UnregisterUnit(Unit unit)
    {
        unit.CompletedCommand -= HandleUnit;
    }

    public bool TryGetFreeUnit(out Unit unit)
    {
        return unit = _freeUnits.Count > 0 ? _freeUnits[0] : null;
    }

    private void HandleUnit(Unit unit)
    {
        if (unit.IsFree)
            _freeUnits.Add(unit);
    }
}