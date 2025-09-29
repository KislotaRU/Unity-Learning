using UnityEngine;

public class SpawnerBot : Spawner<Bot>
{
    public override Bot Spawn()
    {
        Bot bot = null;

        if (_spawnZoneStorage.IsFreeZone)
            bot = base.Spawn();

        return bot;
    }

    protected override Bot Create()
    {
        Bot bot = base.Create();

        bot.transform.parent = _container;
        bot.Destroyed += HandleRelease;

        return bot;
    }

    protected override void Get(Bot bot)
    {
        Vector3 position = _spawnZoneStorage.GetRandomPosition();

        bot.Initialize(_spawnZoneStorage.CurrentSpawnZone, position);

        HandleReleasePosition(bot);

        base.Get(bot);
    }

    protected override void Release(Bot bot)
    {
        bot.transform.SetParent(_container);

        base.Release(bot);
    }

    protected override void Destroy(Bot bot)
    {
        bot.Destroyed -= HandleRelease;

        Destroy(bot.gameObject);
    }

    private void HandleReleasePosition(Bot bot)
    {
        _spawnZoneStorage.ReleasePosition(bot.SpawnZone, bot.SpawnPosition);
    }
}