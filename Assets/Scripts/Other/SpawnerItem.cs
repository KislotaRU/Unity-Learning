using UnityEngine;

public class SpawnerItem : Spawner<Item>
{
    [Header("Parameters SpawnZone")]
    [SerializeField] protected SpawnZoneStorage _spawnZoneStorage;

    protected override void Start()
    {
        for (int i = 0; i < _capacity; i++)
            Spawn();

        base.Start();
    }

    public override void Spawn()
    {
        if (_spawnZoneStorage.IsFreeZone)
            base.Spawn();
    }

    protected override Item Create()
    {
        Item item = base.Create();

        item.transform.parent = _container;
        item.Collected += HandleReleasePosition;
        item.Destroyed += HandleRelease;

        return item;
    }

    protected override void Get(Item item)
    {
        Vector3 position = _spawnZoneStorage.GetRandomPosition();

        item.Initialize(_spawnZoneStorage.CurrentSpawnZone, position); 

        base.Get(item);
    }

    protected override void Release(Item item)
    {
        item.transform.SetParent(_container);

        _spawnZoneStorage.ReleasePosition(item.SpawnZone, item.SpawnPosition);

        base.Release(item);
    }

    protected override void Destroy(Item item)
    {
        item.Collected -= HandleReleasePosition;
        item.Destroyed -= HandleRelease;

        Destroy(item.gameObject);
    }

    private void HandleReleasePosition(Item item)
    {
        _spawnZoneStorage.ReleasePosition(item.SpawnZone, item.SpawnPosition);
    }
}