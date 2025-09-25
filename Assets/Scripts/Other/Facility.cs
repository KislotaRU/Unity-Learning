using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _unitContainer;
    [SerializeField] private Transform _basket;
    [SerializeField] private SpawnerUnit _spawnerUnit;

    [SerializeField] private List<Unit> _freeUnits;
    [SerializeField] private StatValue _resourcesCapacity;

    private Queue<Item> _targets;

    public StatValue ResourcesCapacity => _resourcesCapacity;

    private void Awake()
    {
        _freeUnits = new List<Unit>();
        _targets = new Queue<Item>();
    }

    public void Update()
    {
        if (_targets.Count == 0)
        {
            HandleScanner();

            if (_freeUnits.Count > 0)
                HandleIdleUnit();
        }
        else
        {
            HandleCollect();
        }
    }

    private void OnEnable()
    {
        _spawnerUnit.Spawned += RegisterUnit;
    }

    private void OnDisable()
    {
        _spawnerUnit.Spawned -= RegisterUnit;
    }

    private void HandleScanner()
    {
        _targets = _scanner.GetTargets();
    }

    private void HandleCollect()
    {
        Item target;

        if (TryGetFreeUnit(out Unit unit))
        {
            _freeUnits.Remove(unit);

            target = _targets.Dequeue();

            unit.ResetCommands();
            unit.AddCommand(new MoveCommand(unit, target.transform.position));
            unit.AddCommand(new CollectCommand(unit, target));
            unit.AddCommand(new MoveCommand(unit, _basket.transform.position));
            unit.AddCommand(new GiveCommand(unit, target, this));
        }
    }

    private void HandleIdleUnit()
    {
        if (TryGetFreeUnit(out Unit unit))
        {
            _freeUnits.Remove(unit);

            unit.ResetCommands();
            unit.AddCommand(new MoveCommand(unit, unit.SpawnPosition));
        }
    }

    private void HandleCompletedCommands(Unit unit)
    {
        if (unit.IsFree)
            _freeUnits.Add(unit);
    }

    private void RegisterUnit(Unit unit)
    {
        unit.CompletedCommands += HandleCompletedCommands;
        unit.Destroyed += UnregisterUnit;

        HandleCompletedCommands(unit);
    }

    private void UnregisterUnit(Unit unit)
    {
        unit.CompletedCommands -= HandleCompletedCommands;
        unit.Destroyed -= UnregisterUnit;

        _freeUnits.Remove(unit);
    }

    private bool TryGetFreeUnit(out Unit unit) =>
        unit = _freeUnits.Count > 0 ? _freeUnits[0] : null;
}