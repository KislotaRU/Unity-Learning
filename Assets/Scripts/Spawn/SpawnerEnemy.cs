using UnityEngine;

public class SpawnerEnemy : Spawner<Enemy>
{
    [SerializeField] private SpawnZone _zone;

    protected override Enemy Create()
    {
        Enemy enemy = base.Create();

        enemy.transform.parent = Containers.EnemyContainer;

        enemy.Destroyed += HandleRelease;

        return enemy;
    }

    protected override void Get(Enemy enemy)
    {
        Vector3 position = _zone.GetRandomPosition();

        enemy.Initialize(position);

        base.Get(enemy);

        enemy.HandleShoot();
    }

    protected override void Destroy(Enemy enemy)
    {
        enemy.Destroyed -= HandleRelease;

        Destroy(enemy.gameObject);
    }
}