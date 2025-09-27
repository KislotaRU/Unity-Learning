using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facility : MonoBehaviour
{
    [SerializeField] private Scanner _scanner;
    [SerializeField] private Transform _resourceStorage;
    [SerializeField] private SpawnerUnit _spawnerUnit;

    [SerializeField] private List<Unit> _freeUnits;
    [SerializeField] private StatValue _resourcesCapacity;

    private Queue<Item> _targets;

    public StatValue ResourcesCapacity => _resourcesCapacity;
    public Vector3 StoragePosition => _resourceStorage.position;

    private void Awake()
    {
        _freeUnits = new List<Unit>();
        _targets = new Queue<Item>();
    }

    private void Update()
    {
        if (_targets.Count == 0)
        {
            Scan();

            if (_freeUnits.Count > 0)
                HandleWait();
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

    private void Scan()
    {
        _targets = _scanner.GetTargets();
    }

    private void HandleCollect()
    {
        IEnumerator sequence;
        Item item;

        if (TryGetFreeUnit(out Unit unit))
        {
            _freeUnits.Remove(unit);
            item = _targets.Dequeue();

            sequence = Collecting(unit, item, _resourceStorage.position);

            unit.ExecuteSequence(sequence);
        }
    }

    private void HandleWait()
    {
        IEnumerator sequence;

        if (TryGetFreeUnit(out Unit unit))
        {
            _freeUnits.Remove(unit);

            sequence = Waiting(unit);

            unit.ExecuteSequence(sequence);
        }
    }

    private IEnumerator Collecting(Unit unit, Item item, Vector3 storagePosition)
    {
        yield return unit.RotateTo(item.transform.position);
        yield return unit.MoveTo(item.transform.position);
        yield return unit.Collect(item);
        yield return unit.RotateTo(storagePosition);
        yield return unit.MoveTo(storagePosition);
        yield return unit.GiveItem(item);

        _resourcesCapacity.Increase();
        _freeUnits.Add(unit);
    }

    private IEnumerator Waiting(Unit unit)
    {
        if (unit.transform.position != unit.SpawnPosition)
        {
            yield return unit.RotateTo(unit.SpawnPosition);
            yield return unit.MoveTo(unit.SpawnPosition);
        }

        _freeUnits.Add(unit);
    }

    private void RegisterUnit(Unit unit)
    {
        if (_freeUnits.Remove(unit))
            UnregisterUnit(unit);

        unit.Destroyed += UnregisterUnit;

        _freeUnits.Add(unit);
    }

    private void UnregisterUnit(Unit unit)
    {
        unit.Destroyed -= UnregisterUnit;

        _freeUnits.Remove(unit);
    }

    private bool TryGetFreeUnit(out Unit unit) =>
        unit = _freeUnits.Count > 0 ? _freeUnits[0] : null;
}