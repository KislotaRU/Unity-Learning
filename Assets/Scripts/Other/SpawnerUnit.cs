using UnityEngine;

public class SpawnerUnit : Spawner<Unit>
{
    [Header("Parameters SpawnZone")]
    [SerializeField] protected SpawnZoneStorage _spawnZoneStorage;

    public override Unit Spawn()
    {
        Unit unit = null;

        if (_spawnZoneStorage.IsFreeZone)
            unit = base.Spawn();

        return unit;
    }

    protected override Unit Create()
    {
        Unit unit = base.Create();

        unit.transform.parent = _container;
        unit.Destroyed += HandleRelease;

        return unit;
    }

    protected override void Get(Unit unit)
    {
        Vector3 position = _spawnZoneStorage.GetRandomPosition();

        unit.Initialize(_spawnZoneStorage.CurrentSpawnZone, position);

        HandleReleasePosition(unit);

        base.Get(unit);
    }

    protected override void Release(Unit unit)
    {
        unit.transform.SetParent(_container);

        base.Release(unit);
    }

    protected override void Destroy(Unit unit)
    {
        unit.Destroyed -= HandleRelease;

        Destroy(unit.gameObject);
    }

    private void HandleReleasePosition(Unit unit)
    {
        _spawnZoneStorage.ReleasePosition(unit.SpawnZone, unit.SpawnPosition);
    }
}