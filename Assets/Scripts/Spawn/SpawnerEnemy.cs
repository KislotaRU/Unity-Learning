using UnityEngine;

public class SpawnerEnemy : Spawner<Enemy>
{
    [SerializeField] private SpawnZone _zone;
    [Space]
    [SerializeField] private Transform _container;
    [Space]
    [SerializeField] private SpawnerBullet _spawnerBullet;

    protected override Enemy Create()
    {
        Enemy enemy = base.Create();

        enemy.transform.parent = _container;

        enemy.Destroyed += HandleRelease;

        //if (_spawnerBullet != null)
        //    enemy._spawnerBullet += _spawnerBullet.SpawnInPosition;

        return enemy;
    }

    protected override void Get(Enemy enemy)
    {
        Vector3 position = _zone.GetRandomPosition();

        //enemy.Initialize(position);

        base.Get(enemy);
    }

    protected override void Destroy(Enemy enemy)
    {
        enemy.Destroyed -= HandleRelease;

        //if (_bombSpawner != null)
        //    enemy.BombSpawning -= _bombSpawner.SpawnInPosition;

        Destroy(enemy.gameObject);
    }
}