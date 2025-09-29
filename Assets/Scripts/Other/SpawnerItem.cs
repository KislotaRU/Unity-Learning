using UnityEngine;

public class SpawnerItem : Spawner<Item>
{
    public override Item Spawn()
    {
        Item item = null;

        if (_spawnZoneStorage.IsFreeZone)
            item = base.Spawn();

        return item;
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