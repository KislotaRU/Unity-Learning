using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private List<Unit> _units;
    [SerializeField] private List<Unit> _freeUnits;
    [SerializeField] private Queue<Item> _targets;

    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _basket;

    private void Awake()
    {
        _targets = new Queue<Item>();
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
        {
            RegisterUnit(unit);

            if (unit.IsFree)
                _freeUnits.Add(unit);
        }
    }

    private void HandleScanner()
    {
        if (_targets.Count != 0)
            return;

        _targets = _scanner.GetTargets();
    }

    private void HandleCollect()
    {
        Item target;

        if (_targets.Count == 0)
            return;

        if (TryGetFreeUnit(out Unit unit))
        {
            _freeUnits.Remove(unit);

            target = _targets.Dequeue();

            unit.ResetCommands();
            unit.AddCommand(new MoveCommand(unit, target.transform.position));
            unit.AddCommand(new CollectCommand(unit, target));
            unit.AddCommand(new MoveCommand(unit, _basket.transform.position));
            unit.AddCommand(new GiveCommand(unit, target));
        }
    }

    private void HandleUnit(Unit unit)
    {
        if (unit.IsFree)
            _freeUnits.Add(unit);
    }

    private void RegisterUnit(Unit unit)
    {
        unit.CompletedCommands += HandleUnit;
    }

    private void UnregisterUnit(Unit unit)
    {
        unit.CompletedCommands -= HandleUnit;
    }

    private bool TryGetFreeUnit(out Unit unit) =>
        unit = _freeUnits.Count > 0 ? _freeUnits[0] : null;
}