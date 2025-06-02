using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    protected override Bullet Create()
    {
        Bullet bullet = base.Create();

        //bullet.Destroyed += HandleRelease;

        //if (_bombSpawner != null)
        //    bullet.BombSpawning += _bombSpawner.SpawnInPosition;

        return bullet;
    }

    protected override void Get(Bullet bullet)
    {
        //Vector3 position = _zone.GetRandomPosition();

        //bullet.Initialize(position);

        base.Get(bullet);
    }

    protected override void Destroy(Bullet bullet)
    {
        //bullet.Destroyed -= HandleRelease;

        //if (_bombSpawner != null)
        //    bullet.BombSpawning -= _bombSpawner.SpawnInPosition;

        Destroy(bullet.gameObject);
    }
}